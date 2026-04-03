using MudBlazor;

namespace Pawnshop.Web.Themes
{
    public class PawnshopTheme : MudTheme
    {
        public PawnshopTheme()
        {
            PaletteDark = new PaletteDark()
            {
                Primary = "#C5A059",
                PrimaryDarken = "#A6864A",
                PrimaryLighten = "#D4B982",
                Secondary = "#4E5D6C",
                Background = "#0F1114",
                Surface = "#181C21",
                AppbarBackground = "#1E2329",
                AppbarText = "#E0E0E0",
                Success = "#00C853",
                Error = "#FF5252",
                Warning = "#FFAB00",
                Info = "#2196F3",
                TextPrimary = "#F5F5F5",
                TextSecondary = "#B0B0B0",
                LinesDefault = "rgba(197, 160, 89, 0.15)",
                TableLines = "rgba(255, 255, 255, 0.05)",
                Divider = "rgba(197, 160, 89, 0.2)"
            };

            PaletteLight = new PaletteLight()
            {
                Primary = "#C5A059",
                Secondary = "#4E5D6C",
                AppbarBackground = "#C5A059",
                AppbarText = "#FFFFFF",
                Background = "#F5F5F5",
                Surface = "#FFFFFF",
                TextPrimary = "#1A1A1A",
                TextSecondary = "#424242",
            };

            LayoutProperties = new LayoutProperties()
            {
                DefaultBorderRadius = "12px",
                DrawerWidthLeft = "280px",
                AppbarHeight = "72px"
            };

            ConfigureTypography();

            Shadows = new Shadow()
            {
                Elevation = new string[26]
            };

            Shadows.Elevation[0] = "none";
            Shadows.Elevation[1] = "0px 2px 4px rgba(0,0,0,0.4), 0px 0px 2px rgba(197, 160, 89, 0.1)";
            Shadows.Elevation[4] = "0px 4px 12px rgba(0,0,0,0.5), 0px 0px 8px rgba(197, 160, 89, 0.05)";
            Shadows.Elevation[8] = "0px 8px 24px rgba(0,0,0,0.6), 0px 0px 12px rgba(197, 160, 89, 0.1)";
            Shadows.Elevation[25] = "0px 12px 48px rgba(0,0,0,0.7), 0px 0px 20px rgba(197, 160, 89, 0.15)";
        }

        private void ConfigureTypography()
        {
            var outfit = new[] { "Outfit", "sans-serif" };

            Typography.Default.FontFamily = outfit;
            Typography.Default.FontSize = ".875rem";
            Typography.Default.LineHeight = "1.6";

            Typography.H1.FontFamily = outfit;
            Typography.H1.FontSize = "3rem";
            Typography.H1.FontWeight = "700";
            Typography.H1.LetterSpacing = "-0.01em";

            Typography.H4.FontFamily = outfit;
            Typography.H4.FontSize = "2rem";
            Typography.H4.FontWeight = "700";
            Typography.H4.LetterSpacing = "-0.01em";

            Typography.H6.FontFamily = outfit;
            Typography.H6.FontSize = "1.25rem";
            Typography.H6.FontWeight = "600";
            Typography.H6.LetterSpacing = "0.01em";

            Typography.Button.FontFamily = outfit;
            Typography.Button.FontSize = ".875rem";
            Typography.Button.FontWeight = "600";
            Typography.Button.LetterSpacing = "0.02em";
            Typography.Button.TextTransform = "none";
        }
    }
}
