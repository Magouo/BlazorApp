# Aplikacja Webowa Blazor - Baza Filmów

## Wstęp

Projekt został stworzony na platformie .NET (wersja 8.0) i implementuje aplikację webową w technologii Blazor Server. Aplikacja służy do zarządzania kolekcją filmów, umożliwiając użytkownikom dodawanie, przeglądanie, ocenianie i komentowanie filmów. Projekt wykorzystuje Entity Framework Core do interakcji z bazą danych oraz Microsoft Identity do obsługi uwierzytelniania i autoryzacji użytkowników. Aplikacja jest przygotowana do wdrożenia na platformie Microsoft Azure.

## Opis funkcjonalności

Aplikacja webowa oferuje następujące funkcjonalności:

*   **Uwierzytelnianie użytkowników:**
    *   Rejestracja nowych użytkowników.
    *   Logowanie i wylogowywanie.
    *   System oparty na Microsoft Identity (Individual Accounts).
    *   Dostęp do wybranych funkcji (dodawanie, edycja, usuwanie filmów, komentowanie) jest ograniczony tylko dla zalogowanych użytkowników.
*   **Zarządzanie filmami (CRUD):**
    *   **Dodawanie filmów:** Zalogowani użytkownicy mogą dodawać nowe filmy, podając tytuł, opis, datę premiery oraz opcjonalnie URL do obrazka okładki. Każdy dodany film jest powiązany z użytkownikiem, który go dodał.
    *   **Przeglądanie filmów:** Wszyscy użytkownicy mogą przeglądać listę dostępnych filmów. Lista zawiera podstawowe informacje takie jak tytuł, data premiery i średnia ocena.
    *   **Szczegóły filmu:** Możliwość wyświetlenia szczegółowych informacji o filmie, w tym pełnego opisu, obrazka okładki, średniej oceny, liczby ocen oraz sekcji komentarzy.
    *   **Edycja filmów:** Użytkownik, który dodał film, ma możliwość edycji jego danych.
    *   **Usuwanie filmów:** Użytkownik, który dodał film, ma możliwość jego usunięcia z bazy.
*   **Ocenianie i komentowanie:**
    *   Zalogowani użytkownicy mogą dodawać komentarze oraz oceny (np. w skali 1-10) do poszczególnych filmów.
    *   Średnia ocena filmu jest dynamicznie obliczana i aktualizowana na podstawie dodawanych ocen.
    *   Wyświetlana jest również całkowita liczba ocen dla danego filmu.
*   **Sortowanie danych:**
    *   Na stronie z listą filmów użytkownik ma możliwość sortowania filmów według średniej oceny lub daty premiery.
*   **Kontrola dostępu i personalizacja:**
    *   Dostęp do operacji modyfikujących dane (dodawanie, edycja, usuwanie) jest chroniony i wymaga autoryzacji.
    *   Operacje edycji i usuwania filmu są dostępne wyłącznie dla użytkownika, który pierwotnie dodał dany film.
    *   Menu nawigacyjne aplikacji dynamicznie dostosowuje wyświetlane opcje w zależności od statusu zalogowania użytkownika.
*   **Integracja z komponentami zewnętrznymi:**
    *   Na stronie głównej listy filmów osadzona jest interaktywna mapa Google (np. wskazująca na Wrocław).
*   **Wdrożenie w chmurze:**
    *   Aplikacja jest zaprojektowana z myślą o wdrożeniu na platformie Microsoft Azure, wykorzystując usługi takie jak Azure App Service dla hostingu aplikacji oraz Azure SQL Database dla bazy danych.

## Pliki programu oraz ich przeznaczenie

Kluczowe pliki i foldery w strukturze projektu:

*   **`Program.cs`**:
    *   Główny plik startowy aplikacji. Odpowiada za konfigurację usług (dependency injection), takich jak Entity Framework Core, Microsoft Identity, komponenty Blazor. Definiuje również potok przetwarzania żądań HTTP (middleware pipeline).
*   **`Data/ApplicationDbContext.cs`**:
    *   Klasa kontekstu bazy danych dziedzicząca po `IdentityDbContext<ApplicationUser>`. Definiuje `DbSet` dla modeli `Movie` oraz `MovieComment`, umożliwiając operacje CRUD na bazie danych przy użyciu Entity Framework Core.
*   **`Data/ApplicationUser.cs`**:
    *   Niestandardowa klasa użytkownika dziedzicząca po `Microsoft.AspNetCore.Identity.IdentityUser`. Umożliwia ewentualne rozszerzenie modelu użytkownika o dodatkowe właściwości.
*   **`Data/Movie.cs`**:
    *   Model encji reprezentujący film. Zawiera właściwości takie jak `Id`, `Title` (tytuł), `Description` (opis), `ReleaseDate` (data premiery), `Rate` (średnia ocen), `RateCount` (liczba ocen), `ImageUrl` (URL do obrazka) oraz `UserId` (identyfikator użytkownika, który dodał film).
*   **`Data/MovieComment.cs`**:
    *   Model encji reprezentujący komentarz i ocenę użytkownika do filmu. Zawiera `Id`, `MovieId` (klucz obcy do filmu), `UserId` (identyfikator użytkownika, który dodał komentarz), `Comment` (treść komentarza), `Rate` (ocena przypisana w komentarzu) oraz `Created` (data dodania komentarza).
*   **`Components/Pages/Movies/Index.razor`**:
    *   Komponent Blazor odpowiedzialny za wyświetlanie listy filmów. Umożliwia sortowanie, nawigację do tworzenia nowego filmu oraz do stron szczegółów, edycji i usuwania poszczególnych filmów. Wyświetla również osadzoną mapę Google.
*   **`Components/Pages/Movies/Create.razor`**:
    *   Komponent Blazor zawierający formularz do dodawania nowego filmu. Dostępny tylko dla zalogowanych użytkowników. Po pomyślnym dodaniu filmu, użytkownik jest przekierowywany na listę filmów.
*   **`Components/Pages/Movies/Details.razor`**:
    *   Komponent Blazor wyświetlający szczegółowe informacje o wybranym filmie, w tym jego obrazek. Umożliwia zalogowanym użytkownikom dodawanie komentarzy i ocen. Wyświetla listę istniejących komentarzy dla danego filmu.
*   **`Components/Pages/Movies/Edit.razor`**:
    *   Komponent Blazor z formularzem do edycji danych istniejącego filmu. Dostępny tylko dla użytkownika, który jest właścicielem danego filmu.
*   **`Components/Pages/Movies/Delete.razor`**:
    *   Komponent Blazor wyświetlający stronę potwierdzenia przed usunięciem filmu. Dostępny tylko dla właściciela filmu.
*   **`Components/Layout/NavMenu.razor`**:
    *   Komponent Blazor definiujący główne menu nawigacyjne aplikacji. Linki w menu (np. "Login", "Register", "Logout", nazwa zalogowanego użytkownika, "Filmy") są dynamicznie wyświetlane w zależności od statusu uwierzytelnienia użytkownika.
*   **`Components/Account/`**:
    *   Folder zawierający standardowe komponenty Razor dostarczone przez ASP.NET Core Identity, służące do obsługi procesów rejestracji, logowania, zarządzania kontem użytkownika itp.

## Działanie programu

1.  Użytkownik uruchamia aplikację w przeglądarce internetowej.
2.  Domyślnie może przeglądać publicznie dostępne strony, w tym listę filmów (`/movies`).
3.  Aby uzyskać dostęp do funkcji dodawania, edytowania, usuwania filmów lub dodawania ocen i komentarzy, użytkownik musi się zarejestrować (jeśli nie ma konta) i zalogować.
4.  Po zalogowaniu, użytkownik zyskuje dostęp do spersonalizowanych funkcji:
    *   Może dodać nowy film za pomocą formularza dostępnego pod `/movies/create`.
    *   Może przeglądać szczegóły dowolnego filmu (`/movies/details/{id}`), a także dodawać do niego własne komentarze i oceny. Dodanie nowej oceny powoduje przeliczenie i aktualizację średniej oceny filmu.
    *   Może edytować dane filmów, które sam wcześniej dodał, przechodząc do `/movies/edit/{id}`.
    *   Może usuwać filmy, których jest autorem, poprzez stronę `/movies/delete/{id}`.
5.  Interfejs użytkownika, w tym menu nawigacyjne oraz dostępne akcje (np. przyciski edycji/usuwania), dostosowuje się dynamicznie do uprawnień zalogowanego użytkownika.
6.  Wszystkie dane dotyczące użytkowników, filmów oraz komentarzy są przechowywane w relacyjnej bazie danych (np. Azure SQL Database po wdrożeniu, lub lokalna baza danych podczas developmentu).

## Wykorzystane technologie

*   **.NET 8.0**
*   **ASP.NET Core** (do budowy aplikacji webowej)
*   **Blazor Server** (do tworzenia interaktywnego interfejsu użytkownika po stronie serwera)
*   **Entity Framework Core** (jako ORM do interakcji z bazą danych)
*   **Microsoft Identity** (do obsługi uwierzytelniania i autoryzacji użytkowników)
*   **C#** (główny język programowania)
*   **HTML, CSS** (do struktury i stylizacji interfejsu)
*   **Azure App Service** (platforma do hostowania aplikacji w chmurze Microsoft Azure)
*   **Azure SQL Database** (usługa bazodanowa w chmurze Microsoft Azure)
