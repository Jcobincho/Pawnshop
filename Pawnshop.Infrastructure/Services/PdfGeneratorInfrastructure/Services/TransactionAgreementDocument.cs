using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Pawnshop.Domain.Enums;

namespace Pawnshop.Infrastructure.Services.PdfGeneratorInfrastructure.Services;

public class TransactionAgreementDocument : IDocument
{
    private readonly dynamic _data;

    private static readonly string ColorText = "#1A1A1A";
    private static readonly string ColorSecondary = "#4E5D6C";
    private static readonly string ColorGold = "#C5A059";
    private static readonly string ColorDivider = "#E0E0E0";

    public TransactionAgreementDocument(dynamic data)
    {
        _data = data;
    }

    public void Compose(IDocumentContainer container)
    {
        container
            .Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(20, Unit.Millimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(9).FontFamily("Helvetica").FontColor(ColorText));

                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);
                page.Footer().Element(ComposeFooter);
            });
    }

    private void ComposeHeader(IContainer container)
    {
        container.PaddingBottom(15).Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                var isPurchase = _data.TypeOfTransaction == TypeOfTransactionEnum.Purchase;
                var typePl = isPurchase ? "KONSUMENCKA POŻYCZYKA LOMBARDOWA" : "UMOWA SPRZEDAŻY";
                var typeEn = isPurchase ? "CONSUMER PAWN LOAN AGREEMENT" : "SALES AGREEMENT";

                column.Item().Text($"{typePl}").FontSize(16).ExtraBold().FontColor(ColorGold);
                column.Item().PaddingBottom(5).Text($"{typeEn}").FontSize(10).SemiBold().FontColor(ColorSecondary);

                column.Item().PaddingTop(10).Row(r =>
                {
                    r.AutoItem().Text("Nr Umowy / Agreement No.: ").SemiBold();
                    r.RelativeItem().Text($"{_data.Symbol}").Bold();
                });

                column.Item().Row(r =>
                {
                    r.AutoItem().Text("Data zawarcia / Date of agreement: ").SemiBold();
                    var date = (DateTime)_data.TransactionDate;
                    TimeZoneInfo polishTimeZone;
                    try { polishTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time"); }
                    catch { polishTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Warsaw"); }

                    var localDate = TimeZoneInfo.ConvertTimeFromUtc(date.Kind == DateTimeKind.Utc ? date : DateTime.SpecifyKind(date, DateTimeKind.Utc), polishTimeZone);
                    r.RelativeItem().Text($"{localDate:dd.MM.yyyy HH:mm}");
                });
            });
        });
    }

    private void ComposeContent(IContainer container)
    {
        container.Column(column =>
        {
            // --- DECORATIVE LINE ---
            column.Item().PaddingBottom(15).Height(2).Background(ColorGold);

            // --- DANE STRON ---
            column.Item().Row(row =>
            {
                // Lombard
                row.RelativeItem().Column(col =>
                {
                    col.Item().PaddingBottom(3).Text("LOMBARD / PAWNSHOP").FontSize(8).SemiBold().FontColor(ColorGold);
                    col.Item().BorderTop(1).BorderColor(ColorGold).PaddingTop(5).Text($"{_data.Workplace.City}").Bold();
                    col.Item().Text($"{_data.Workplace.StreetAndBuildingNumber}");
                    col.Item().Text($"{_data.Workplace.ZipCode} {_data.Workplace.City}");
                    col.Item().Text($"{_data.Workplace.Country}");
                });

                row.ConstantItem(30);

                // Klient
                row.RelativeItem().Column(col =>
                {
                    col.Item().PaddingBottom(3).Text("KONTRAHENT / CONTRACTOR").FontSize(8).SemiBold().FontColor(ColorGold);
                    if (_data.Client != null)
                    {
                        col.Item().BorderTop(1).BorderColor(ColorGold).PaddingTop(5).Text($"{_data.Client.Name} {_data.Client.Surname}").Bold();
                        col.Item().Text($"PESEL: {_data.Client.Pesel}");
                        col.Item().Text($"Nr dowodu / ID: {_data.Client.IdCardNumber}");
                        if (!string.IsNullOrEmpty(_data.Client.Email)) col.Item().Text($"Email: {_data.Client.Email}");
                    }
                    else
                    {
                        col.Item().BorderTop(1).BorderColor(ColorGold).PaddingTop(5).Text("Klient Anonimowy / Anonymous Client").Italic();
                    }
                });
            });

            column.Item().PaddingTop(20);

            column.Item().PaddingBottom(5).Text("SPECYFIKACJA PRZEDMIOTÓW / ITEM SPECIFICATION").FontSize(8).SemiBold().FontColor(ColorSecondary);
            column.Item().Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(25);  // Lp
                    columns.RelativeColumn(4);   // Nazwa
                    columns.RelativeColumn(3);   // Marka/Model
                    columns.RelativeColumn(3);   // Nr Seryjny
                    columns.ConstantColumn(100); // Cena (100pt is safe within 20mm margin)
                });

                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("Lp.");
                    header.Cell().Element(CellStyle).Text("Przedmiot / Item");
                    header.Cell().Element(CellStyle).Text("Marka / Model");
                    header.Cell().Element(CellStyle).Text("Nr Seryjny / S/N");
                    header.Cell().Element(CellStyle).AlignRight().Text("Wartość / Value");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.BorderBottom(1).BorderColor(ColorGold)
                                        .DefaultTextStyle(x => x.SemiBold().FontSize(8).FontColor(ColorGold))
                                        .PaddingVertical(5).PaddingHorizontal(5);
                    }
                });

                int index = 1;
                foreach (var item in _data.Items)
                {
                    table.Cell().Element(RowStyle).Text(index.ToString());
                    table.Cell().Element(RowStyle).Text((string)item.Name).Bold();
                    table.Cell().Element(RowStyle).Text($"{item.Brand} {item.Model}");

                    string sn = string.IsNullOrEmpty((string)item.SerialNumber) ? "---" : (string)item.SerialNumber;
                    table.Cell().Element(RowStyle).Text(sn).FontSize(8);

                    table.Cell().Element(RowStyle).AlignRight().Text($"{item.Price:N2} PLN").Bold();
                    index++;

                    static IContainer RowStyle(IContainer container)
                    {
                        return container.BorderBottom(1).BorderColor(ColorDivider).PaddingVertical(4).PaddingHorizontal(5).DefaultTextStyle(x => x.FontSize(8));
                    }
                }
            });

            column.Item().PaddingTop(15).Row(row =>
            {
                row.RelativeItem();
                row.ConstantItem(200).Border(1).BorderColor(ColorGold).Padding(8).Row(summary =>
                {
                    summary.RelativeItem().Column(col =>
                    {
                        col.Item().Text("SUMA DO WYPŁATY:").FontSize(8).Bold();
                        col.Item().Text("TOTAL TO PAY:").FontSize(7).FontColor(ColorSecondary);
                    });
                    summary.RelativeItem().AlignRight().AlignMiddle().Text($"{_data.TotalPrice:N2} PLN").FontSize(12).ExtraBold().FontColor(ColorGold);
                });
            });

            column.Item().PaddingTop(30).Column(c =>
            {
                c.Item().Text("Oświadczenia i podpisy / Declarations and signatures").FontSize(8).SemiBold().FontColor(ColorSecondary);
                c.Item().PaddingTop(5).Text("Kontrahent oświadcza, że opisane powyżej przedmioty stanowią jego własność i nie pochodzą z przestępstwa oraz nie są obciążone prawami osób trzecich.").FontSize(7).Italic();
                c.Item().Text("The contractor declares that the items described above are their property, do not come from a crime and are not encumbered with the rights of third parties.").FontSize(6).Italic().FontColor(ColorSecondary);
            });

            column.Item().PaddingTop(50).Row(row =>
            {
                row.RelativeItem().Column(col =>
                {
                    col.Item().PaddingBottom(5).AlignCenter().Text("................................................").FontColor(ColorDivider);
                    col.Item().AlignCenter().Text("Podpis Kontrahenta").FontSize(8);
                    col.Item().AlignCenter().Text("Contractor's signature").FontSize(7).FontColor(ColorSecondary);
                });

                row.ConstantItem(50);

                row.RelativeItem().Column(col =>
                {
                    col.Item().PaddingBottom(5).AlignCenter().Text("................................................").FontColor(ColorDivider);
                    col.Item().AlignCenter().Text("Pieczęć i podpis pracownika").FontSize(8);
                    col.Item().AlignCenter().Text("Employee's stamp and signature").FontSize(7).FontColor(ColorSecondary);
                });
            });
        });
    }

    private void ComposeFooter(IContainer container)
    {
        container.PaddingTop(15).BorderTop(1).BorderColor(ColorDivider).Row(row =>
        {
            row.RelativeItem().Column(c =>
            {
                c.Item().Text(x =>
                {
                    x.Span("Dokument wygenerowany systemowo / System generated document: ").FontSize(7).FontColor(ColorSecondary);
                    x.Span($"{DateTime.Now:dd.MM.yyyy HH:mm}").FontSize(7).Bold().FontColor(ColorSecondary);
                });
                c.Item().Text($"Transaction ID: {_data.PurchaseSaleTransactionId}").FontSize(6).FontColor(Colors.Grey.Lighten1);
            });

            row.RelativeItem().AlignRight().Text(x =>
            {
                x.Span("Strona / Page ").FontSize(7);
                x.CurrentPageNumber().FontSize(7);
                x.Span(" z / of ").FontSize(7);
                x.TotalPages().FontSize(7);
            });
        });
    }
}
