Specyfikacja projektu:

Opis aplikacji

RaidPlanner to aplikacja desktopowa stworzona w technologii WPF , której celem jest wspomaganie gracza w planowaniu i analizie rozgrywek typu "raid" (np. w grach takich jak Arc Raiders).

Aplikacja umożliwia użytkownikowi:

- planowanie przyszłych raidów,
- zarządzanie listą poszukiwanych przedmiotów (wishlist),
- prowadzenie notatek,
- zapisywanie historii przeprowadzonych raidów.

Dane są zapisywane lokalnie w plikach JSON, dzięki czemu użytkownik może wrócić do nich po ponownym uruchomieniu aplikacji.

Funkcjonalności aplikacji

Plans (Planowanie raidów)

- Dodawanie nowego planu raidu
- Edytowanie istniejącego planu
- Usuwanie planu
- Każdy plan zawiera:

- nazwę mapy
- cel raidu (objective)
- ekwipunek (gear)
- dodatkowe notatki
- powiązaną wishlistę
- Wyświetlanie szczegółów wybranego planu

Wishlist (Lista przedmiotów)

- Tworzenie nowych wishlist
- Usuwanie wishlist
- Dodawanie itemów do wishlisty
- Usuwanie itemów
- Oznaczanie itemów jako znalezione (checkbox)
- Dynamiczne wyświetlanie itemów dla wybranej wishlisty

Notes (Notatki)

- Dodawanie notatek
- Edytowanie notatek
- Usuwanie notatek
- Wyświetlanie treści notatki
- Zapisywanie notatek po zamknięciu aplikacji

History (Historia raidów)

- Dodawanie wpisów do historii
- Usuwanie wpisów
  
- Każdy wpis zawiera:
  - nazwę mapy
  - używaną wishlistę
  - znalezione przedmioty
  - datę wykonania raidu
  - Automatyczne przypisywanie aktualnej daty

Zapisywanie danych

- Automatyczne zapisywanie danych przy zamykaniu aplikacji
- Wczytywanie danych przy uruchomieniu
- Dane przechowywane w plikach:

   `wishlists.json`
  `plans.json`
  `history.json`
  (opcjonalnie notes)

Wygląd aplikacji

Aplikacja posiada prosty i czytelny interfejs użytkownika oparty o zakładki (TabControl):

Plans – planowanie raidów
Wishlist – lista przedmiotów
Notebook – notatki
History – historia raidów

Elementy interfejsu:

- ListBox do wyświetlania list (plans, wishlist, history)
- Przyciski do dodawania, edytowania i usuwania danych
- Pola tekstowe do wyświetlania szczegółów
- Checkboxy do oznaczania znalezionych itemów

Kolorystyka:

- jasne tło (#FFFFF9F0)
- ciemniejszy panel główny (#FF414141)
- kontrastowe przyciski

Wymagania techniczne

- Język: C#
- Framework: .NET 8.0 (Windows)
- Technologia UI: WPF
- IDE: Visual Studio
- System kontroli wersji: Git (GitHub)

Dodatkowe biblioteki:

- System.Text.Json (do zapisu danych)
- Microsoft.VisualBasic (InputBox)

Struktura danych

- Wishlist

```c#
class Wishlist
{
    string Name;
    ObservableCollection<WishlistItem> Items;
}

- WishlistItem

```c#
class WishlistItem
{
    string Name;
    bool IsFound;
}

- RaidPlan

```c#
class RaidPlan
{
    string MapName;
    string Objective;
    string Gear;
    string Notes;
    string WishlistName;
}

- HistoryEntry

```c#
class HistoryEntry
{
    string MapName;
    string WishlistName;
    string FoundItems;
    string Date;
}

- Note

```c#
class Note
{
    string Title;
    string Content;
}

- Przepływ działania aplikacji

1. Użytkownik uruchamia aplikację
2. Dane są wczytywane z plików JSON
3. Użytkownik może:

   * tworzyć plany raidów
   * zarządzać wishlistą
   * zapisywać historię
   * dodawać notatki
4. Przy zamknięciu aplikacji:

   * dane są zapisywane do plików JSON

---

- Wykorzystanie AI

Podczas tworzenia projektu wykorzystano wsparcie AI w następujących obszarach:

- projektowanie architektury aplikacji
- pomoc przy implementacji funkcji (CRUD)
- debugowanie błędów WPF
- generowanie fragmentów kodu

Samodzielnie wykonano:

- implementację UI (XAML)
- integrację funkcjonalności
- zarządzanie repozytorium Git
- testowanie działania aplikacji
- dostosowanie logiki do własnych potrzeb

- Podsumowanie

RaidPlanner to aplikacja wspierająca gracza w organizacji rozgrywki, łącząca funkcje planowania, zarządzania zasobami i analizy historii.

Projekt spełnia wszystkie wymagania:

- zarządzanie kodem (Git)
- dokumentacja
- struktura danych
- funkcjonalność aplikacji
