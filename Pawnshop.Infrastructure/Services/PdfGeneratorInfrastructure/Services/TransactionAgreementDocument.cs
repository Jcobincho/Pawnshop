using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Pawnshop.Infrastructure.Services.PdfGeneratorInfrastructure.Services;

public class TransactionAgreementDocument : IDocument
{
    private readonly dynamic _data;

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
                page.DefaultTextStyle(x => x.FontSize(10).FontFamily(Fonts.Arial));

                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);
                page.Footer().Element(ComposeFooter);
            });
    }

    private void ComposeHeader(IContainer container)
    {
        container.PaddingBottom(15).BorderBottom(2).BorderColor(Colors.Black).AlignCenter().Column(column =>
        {
            column.Item().Text($"UMOWA {_data.TypeOfTransaction}").FontSize(18).SemiBold();
            column.Item().Text(text =>
            {
                text.Span("Symbol: ");
                text.Span($"{_data.Symbol}").SemiBold();
                text.Span(" | Data: ");
                text.Span($"{_data.TransactionDate:dd.MM.yyyy}").SemiBold();
            });
        });
    }

    private void ComposeContent(IContainer container)
    {
        container.PaddingVertical(10).Column(column =>
        {
            // --- LOMBARD INFO ---
            column.Item().PaddingBottom(5).Text("Informacje o lombardzie").FontSize(12).SemiBold().FontColor(Colors.Blue.Darken2);
            column.Item().Background(Colors.Grey.Lighten4).Padding(10).Column(lombard =>
            {
                lombard.Item().Text($"Adres: {_data.Workplace.StreetAndBuildingNumber}, {_data.Workplace.ZipCode} {_data.Workplace.City}");
                lombard.Item().Text($"Region: {_data.Workplace.Region}, {_data.Workplace.Country}");
            });

            column.Item().PaddingTop(15);

            if (_data.Client != null)
            {
                column.Item().PaddingBottom(5).Text("Dane klienta").FontSize(12).SemiBold().FontColor(Colors.Blue.Darken2);
                column.Item().Row(row =>
                {
                    row.RelativeItem().Column(col =>
                    {
                        col.Item().Text($"Imię: {_data.Client.Name}");
                        col.Item().Text($"Nazwisko: {_data.Client.Surname}");
                        col.Item().Text($"Data ur.: {_data.Client.BirthDate:dd.MM.yyyy}");
                    });
                    row.RelativeItem().Column(col =>
                    {
                        col.Item().Text($"PESEL: {_data.Client.Pesel}");
                        col.Item().Text($"Nr dowodu: {_data.Client.IdCardNumber}");
                        if (_data.Client.TelephoneNumber != null) col.Item().Text($"Telefon: {_data.Client.TelephoneNumber}");
                    });
                });
            }

            column.Item().PaddingTop(15);

            column.Item().PaddingBottom(5).Text("Lista przedmiotów").FontSize(12).SemiBold().FontColor(Colors.Blue.Darken2);
            column.Item().Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(25); // Lp.
                    columns.RelativeColumn();   // Nazwa
                    columns.RelativeColumn();   // Marka
                    columns.RelativeColumn();   // Model
                    columns.RelativeColumn();   // SN
                    columns.ConstantColumn(60); // Cena
                });

                table.Header(header =>
                {
                    header.Cell().Background(Colors.Grey.Lighten3).Padding(4).Text("Lp.").SemiBold();
                    header.Cell().Background(Colors.Grey.Lighten3).Padding(4).Text("Nazwa").SemiBold();
                    header.Cell().Background(Colors.Grey.Lighten3).Padding(4).Text("Marka").SemiBold();
                    header.Cell().Background(Colors.Grey.Lighten3).Padding(4).Text("Model").SemiBold();
                    header.Cell().Background(Colors.Grey.Lighten3).Padding(4).Text("Nr seryjny").SemiBold();
                    header.Cell().Background(Colors.Grey.Lighten3).Padding(4).AlignRight().Text("Cena PLN").SemiBold();
                });

                int index = 1;
                foreach (var item in _data.Items)
                {
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(4).Text(index.ToString());
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(4).Text((string)item.Name);
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(4).Text((string)item.Brand);
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(4).Text((string)item.Model);
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(4).Text((string)item.SerialNumber);
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(4).AlignRight().Text($"{item.Price:0.00}");
                    index++;
                }
            });

            column.Item().PaddingTop(10).AlignRight().Text($"ŁĄCZNA KWOTA: {_data.TotalPrice:0.00} PLN").FontSize(14).SemiBold().FontColor(Colors.Green.Darken2);

            column.Item().PaddingTop(50).Row(row =>
            {
                row.RelativeItem().AlignCenter().Column(col =>
                {
                    col.Item().Width(150).BorderBottom(1).BorderColor(Colors.Black);
                    col.Item().PaddingTop(5).Text("Podpis klienta");
                });

                row.RelativeItem().AlignCenter().Column(col =>
                {
                    col.Item().Width(150).BorderBottom(1).BorderColor(Colors.Black);
                    col.Item().PaddingTop(5).Text("Podpis pracownika");
                });
            });
        });
    }

    private void ComposeFooter(IContainer container)
    {
        container.BorderTop(1).BorderColor(Colors.Grey.Lighten2).PaddingTop(5).Row(row =>
        {
            row.RelativeItem().Text($"Dokument wygenerowany: {DateTime.Now:dd.MM.yyyy HH:mm}").FontSize(8).FontColor(Colors.Grey.Medium);
            row.RelativeItem().AlignRight().Text($"ID Transakcji: {_data.PurchaseSaleTransactionId}").FontSize(8).FontColor(Colors.Grey.Medium);
        });
    }
}
