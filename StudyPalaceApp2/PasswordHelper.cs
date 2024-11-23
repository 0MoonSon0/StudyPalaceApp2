using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StudyPalaceApp2
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    internal class PasswordHelper
    {
        // Метод для хеширования пароля с солью
        public static (string hash, string salt) HashPassword(string password)
        {
            // Генерация случайной соли
            var saltBytes = new byte[32];  // Обычно 16-32 байта соли достаточно
            using (var rng = RandomNumberGenerator.Create())  // Используем RandomNumberGenerator для криптографической стойкости
            {
                rng.GetBytes(saltBytes);
            }

            // Преобразуем соль в строку Base64
            string salt = Convert.ToBase64String(saltBytes);

            // Хешируем пароль с добавлением соли
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = salt + password;  // Можно использовать соль до пароля или после, это зависит от реализации
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                string hash = Convert.ToBase64String(hashBytes);
                return (hash, salt);
            }
        }

        // Метод для проверки пароля
        public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            using (var sha256 = SHA256.Create())
            {
                // Объединяем соль и введённый пароль
                var saltedPassword = storedSalt + enteredPassword;

                // Хешируем объединённую строку (соль + пароль)
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));

                // Преобразуем результат в строку Base64
                string hash = Convert.ToBase64String(hashBytes);

                // Сравниваем полученный хеш с сохранённым хешем
                return hash == storedHash;
            }
        }
    }
}