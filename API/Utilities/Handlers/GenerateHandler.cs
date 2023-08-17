using API.Contracts;
using API.Models;

namespace API.Utilities.Handlers
{
    public class GenerateHandler
    {

        public static string LastNik(string nik)
        {
            string defaultNIK = "111111";
            if (string.IsNullOrEmpty(nik)) // Menangani kasus nik yang null atau kosong
            {
                return defaultNIK;
            }

            if (!int.TryParse(nik, out int parsedNik)) // Memeriksa apakah konversi berhasil
            {
                return defaultNIK; // Jika konversi gagal, kembalikan defaultNIK
            }

            var newNIK = parsedNik + 1; // Melakukan operasi matematika pada nik yang sudah diubah ke tipe int

            return newNIK.ToString();
        }
    }

}

