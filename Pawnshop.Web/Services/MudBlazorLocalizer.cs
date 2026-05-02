using Microsoft.Extensions.Localization;
using MudBlazor;
using Pawnshop.Web.Services;

namespace Pawnshop.Web.Services
{
    public class MudBlazorLocalizer : MudLocalizer
    {
        private readonly LanguageService _lang;

        public MudBlazorLocalizer(LanguageService lang)
        {
            _lang = lang;
        }

        public override LocalizedString this[string resName]
        {
            get
            {
                var value = resName switch
                {
                    "MudDataGrid.RowsPerPage" => _lang.T("Wierszy na stronę:", "Rows per page:"),
                    "MudDataGrid.InfoFormat" => _lang.T("{first_item}-{last_item} z {all_items}", "{first_item}-{last_item} of {all_items}"),
                    "MudDataGrid.All" => _lang.T("Wszystkie", "All"),
                    "MudDataGrid.ItemsPerPage" => _lang.T("Elementów na stronę:", "Items per page:"),
                    "MudDataGrid.Filter" => _lang.T("Filtruj", "Filter"),
                    "MudDataGrid.Search" => _lang.T("Szukaj", "Search"),
                    "MudDataGrid.Clear" => _lang.T("Wyczyść", "Clear"),
                    "MudDataGrid.ClearFilter" => _lang.T("Wyczyść filtr", "Clear filter"),
                    "MudDataGrid.Apply" => _lang.T("Zastosuj", "Apply"),
                    "MudDataGrid.Cancel" => _lang.T("Anuluj", "Cancel"),
                    "MudDataGrid.Save" => _lang.T("Zapisz", "Save"),
                    "MudDataGrid.Operator" => _lang.T("Operator", "Operator"),
                    "MudDataGrid.Value" => _lang.T("Wartość", "Value"),
                    "MudDataGrid.Sort" => _lang.T("Sortuj", "Sort"),
                    "MudDataGrid.Unsort" => _lang.T("Usuń sortowanie", "Unsort"),
                    "MudDataGrid.AddFilter" => _lang.T("Dodaj filtr", "Add filter"),
                    "MudDataGrid.Columns" => _lang.T("Kolumny", "Columns"),
                    "MudDataGrid.Hide" => _lang.T("Ukryj", "Hide"),
                    "MudDataGrid.ShowAll" => _lang.T("Pokaż wszystko", "Show all"),
                    "MudDataGrid.Group" => _lang.T("Grupuj", "Group"),
                    "MudDataGrid.Ungroup" => _lang.T("Rozgrupuj", "Ungroup"),
                    "MudDataGrid.FilterValue" => _lang.T("Wartość", "Value"),
                    "MudDataGrid.Is" => _lang.T("Jest", "Is"),
                    "MudDataGrid.IsNot" => _lang.T("Nie jest", "Is not"),
                    "MudDataGrid.Contains" => _lang.T("Zawiera", "Contains"),
                    "MudDataGrid.NotContains" => _lang.T("Nie zawiera", "Not contains"),
                    "MudDataGrid.StartsWith" => _lang.T("Zaczyna się od", "Starts with"),
                    "MudDataGrid.EndsWith" => _lang.T("Kończy się na", "Ends with"),
                    "MudDataGrid.IsEmpty" => _lang.T("Jest pusty", "Is empty"),
                    "MudDataGrid.IsNotEmpty" => _lang.T("Nie jest pusty", "Is not empty"),
                    "MudDataGrid.True" => _lang.T("Tak", "True"),
                    "MudDataGrid.False" => _lang.T("Nie", "False"),
                    _ => resName
                };
                return new LocalizedString(resName, value);
            }
        }
    }
}
