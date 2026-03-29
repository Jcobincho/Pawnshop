using System;

namespace Pawnshop.Web.Services
{
    public class LanguageService
    {
        public event Action OnLanguageChanged;
        public bool IsPolish { get; private set; } = true;

        public void ToggleLanguage()
        {
            IsPolish = !IsPolish;
            OnLanguageChanged?.Invoke();
        }

        public string T(string pl, string en) => IsPolish ? pl : en;

        // Globalne
        public string AppTitle => T("System Lombardowy", "Pawnshop System");
        public string Welcome => T("Witaj w systemie", "Welcome back");
        public string Logout => T("Wyloguj", "Logout");
        public string Close => T("Zamknij", "Close");
        public string Cancel => T("Anuluj", "Cancel");
        public string Save => T("Zapisz", "Save");
        public string Back => T("Cofnij", "Back");
        public string Next => T("Dalej", "Next");
        public string Preview => T("Podgląd", "Preview");
        public string Loading => T("Ładowanie...", "Loading...");

        // Menu
        public string Home => T("Strona główna", "Home");
        public string TradingManagement => T("Zarządzanie Transakcjami", "Trading Management");
        public string WorkplaceManagement => T("Zarządzanie Lombardami", "Workplace Management");
        public string ClientManagement => T("Zarządzanie Klientami", "Client Management");
        public string EmployeeManagement => T("Zarządzanie Pracownikami", "Employee Management");
        public string UserManagement => T("Zarządzanie Użytkownikami", "User Management");
        public string Categories => T("Kategorie", "Categories");
        public string Transactions => T("Transakcje", "Transactions");

        // Przyciski
        public string Details => T("Szczegóły", "Details");
        public string Add => T("Dodaj", "Add");
        public string AddCopy => T("Kopiuj", "Add copy");
        public string Update => T("Edytuj", "Update");
        public string Delete => T("Usuń", "Delete");

        // Formularze
        public string Description => T("Opis transakcji", "Transaction description");
        public string Name => T("Nazwa", "Name");
        public string Surname => T("Nazwisko", "Last name");
        public string SecondName => T("Drugie imię", "Second name");
        public string BirthDate => T("Data urodzenia", "Birth date");
        public string Pesel => T("PESEL", "PESEL");
        public string IdCardNumber => T("Nr dowodu", "ID card number");
        public string Email => T("E-mail", "Email");
        public string Phone => T("Telefon", "Phone");
        public string City => T("Miasto", "City");
        public string Street => T("Ulica i numer", "Street and number");
        public string ZipCode => T("Kod pocztowy", "Zip code");
        public string Region => T("Województwo / Region", "Region");
        public string Country => T("Kraj", "Country");
        public string Price => T("Cena", "Price");
        public string Category => T("Kategoria", "Category");
        public string Symbol => T("Symbol", "Symbol");
        public string Date => T("Data", "Date");
        public string Comments => T("Uwagi / Stan", "Comments / Condition");
        public string Workplace => T("Lombard", "Workplace");
        public string Client => T("Klient", "Client");
        public string Role => T("Rola", "Role");
        public string Username => T("Użytkownik", "Username");
        public string Password => T("Hasło", "Password");
        public string Brand => T("Marka", "Brand");
        public string Login => T("Zaloguj", "Login");

        // Komunikaty
        public string SuccessSave => T("Zapisano pomyślnie!", "Saved successfully!");
        public string ErrorGeneric => T("Wystąpił nieoczekiwany błąd.", "An unexpected error occurred.");
        public string ConfirmDelete => T("Czy na pewno chcesz to usunąć?", "Are you sure you want to delete this?");
    }
}
