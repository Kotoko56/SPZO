using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPZO.Model
{
    public class Users
    {
        // Klucz główny
        [Key]
        public int UserID { get; set; }  // [web:64]

        // Nazwa użytkownika (unikalna w twoim kontekście)
        [Required]
        public string UserName { get; set; } = string.Empty;  // [web:64]

        // NOWE: przechowywanie hasła w postaci PBKDF2
        // Hash hasła (np. 32 bajty dla PBKDF2-HMAC-SHA256)
        [Required]
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();  // [web:47]

        // Losowa sól per użytkownik (np. 16 bajtów)
        [Required]
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();  // [web:47]

        // Liczba iteracji użyta do wyliczenia PBKDF2 (umożliwia podbijanie kosztu w czasie)
        [Required]
        public int PasswordIterations { get; set; }  // np. 200_000  // [web:94]

        // LEGACY: stare pole z hasłem w jawnym tekście — pozostawione wyłącznie
        // na czas migracji i powinno być docelowo usunięte po wdrożeniu.
        [NotMapped] // jeśli chcesz całkowicie przestać utrzymywać kolumnę w DB, usuń to pole i wykonaj migrację usuwającą kolumnę
        public string? UserPassword { get; set; }  // NIE używać w nowym kodzie  // [web:44]
    }
}