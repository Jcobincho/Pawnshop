let map;
let activeStyleUrl;
const markers = new Map();

const defaultStyleUrl = "https://tiles.openfreemap.org/styles/liberty";

export async function renderWorkplacesMap(elementId, workplaces, dotNetReference, styleUrl = defaultStyleUrl) {
    const container = document.getElementById(elementId);
    if (!container) {
        return;
    }

    await waitForMapLibre();

    if (typeof maplibregl === "undefined") {
        return;
    }

    if (!map) {
        activeStyleUrl = styleUrl;
        map = new maplibregl.Map({
            container: elementId,
            style: styleUrl,
            center: [21.0122, 52.2297],
            zoom: 5.4,
            attributionControl: false
        });

        map.addControl(new maplibregl.NavigationControl({ visualizePitch: true }), "top-right");
        map.addControl(new maplibregl.AttributionControl({ compact: true }), "bottom-right");
    } else if (activeStyleUrl !== styleUrl) {
        activeStyleUrl = styleUrl;
        map.setStyle(styleUrl);
    }

    clearMarkers();
    const bounds = new maplibregl.LngLatBounds();
    let markerCount = 0;

    workplaces
        .filter(x => Number.isFinite(x.latitude) && Number.isFinite(x.longitude))
        .forEach(workplace => {
            const element = document.createElement("button");
            element.type = "button";
            element.className = "workplace-map-marker";
            element.setAttribute("aria-label", `${workplace.city}, ${workplace.streetAndBuildingNumber}`);

            const marker = new maplibregl.Marker({
                element,
                anchor: "bottom"
            })
                .setLngLat([workplace.longitude, workplace.latitude])
                .setPopup(
                    new maplibregl.Popup({
                        offset: 28,
                        closeButton: false,
                        className: "workplace-map-popup"
                    }).setHTML(buildPopup(workplace))
                )
                .addTo(map);

            element.addEventListener("click", () => {
                dotNetReference.invokeMethodAsync("SelectWorkplace", workplace.workplaceId);
            });

            markers.set(workplace.workplaceId, marker);
            bounds.extend([workplace.longitude, workplace.latitude]);
            markerCount += 1;
        });

    if (markerCount === 1) {
        map.easeTo({ center: bounds.getCenter(), zoom: 15, duration: 450 });
    } else if (markerCount > 1) {
        map.fitBounds(bounds, { padding: 56, maxZoom: 15, duration: 450 });
    } else {
        map.easeTo({ center: [21.0122, 52.2297], zoom: 5.4, duration: 450 });
    }

    setTimeout(() => map.resize(), 0);
}

export function focusWorkplace(workplaceId) {
    if (!map || !markers.has(workplaceId)) {
        return;
    }

    const marker = markers.get(workplaceId);
    marker.getPopup().addTo(map);
    map.easeTo({
        center: marker.getLngLat(),
        zoom: Math.max(map.getZoom(), 15),
        duration: 450
    });
}

export function disposeWorkplacesMap() {
    clearMarkers();

    if (map) {
        map.remove();
        map = null;
        activeStyleUrl = null;
    }
}

function clearMarkers() {
    markers.forEach(marker => marker.remove());
    markers.clear();
}

function buildPopup(workplace) {
    const title = escapeHtml(`${workplace.city}, ${workplace.streetAndBuildingNumber}`);
    const region = escapeHtml(`${workplace.zipCode} ${workplace.city}`);
    const country = escapeHtml(workplace.country);

    return `
        <div class="workplace-popup-content">
            <strong>${title}</strong>
            <span>${region}</span>
            <span>${country}</span>
        </div>`;
}

function escapeHtml(value) {
    return String(value ?? "")
        .replaceAll("&", "&amp;")
        .replaceAll("<", "&lt;")
        .replaceAll(">", "&gt;")
        .replaceAll('"', "&quot;")
        .replaceAll("'", "&#039;");
}

async function waitForMapLibre() {
    for (let i = 0; i < 40; i += 1) {
        if (typeof maplibregl !== "undefined") {
            return;
        }

        await new Promise(resolve => setTimeout(resolve, 50));
    }
}
