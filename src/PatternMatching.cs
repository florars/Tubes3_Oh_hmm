using System;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;


// Kelas yang berisi regex untuk melakukan konversi bahasa alay ke bahasa normal
// Konversi dilakukan untuk angka dan kapitalisasi, lalu akan dilakukan pencocokan konsonan menggunakan regex
// Jika pencocokan dengan regex menghasilkan lebih dari satu nama, algoritma pattern matching yang terpilih akan dipanggil
public class AlayMatcher {
    private static string CapitalizeNames(Match m) {
        string str = m.ToString();
        if (char.IsLower(str[0])) return char.ToUpper(str[0]) + str.Substring(1, str.Length - 1);
        else return str;
    }

    public static string ToNormal(string name) {
        string capitalization = @"(\w+)";
        string a = @"4";
        string b = @"(13)";
        string d = @"(17)";
        string e = @"3";
        string g = @"[69]";
        string i = @"1";
        string o = @"0";
        string r = @"(12)";
        string s = @"5";

        string normal;
        normal = Regex.Replace(name, b, "b");
        normal = Regex.Replace(normal, d, "d");
        normal = Regex.Replace(normal, r, "r");
        normal = Regex.Replace(normal, a, "a");
        normal = Regex.Replace(normal, e, "e");
        normal = Regex.Replace(normal, g, "g");
        normal = Regex.Replace(normal, i, "i");
        normal = Regex.Replace(normal, o, "o");
        normal = Regex.Replace(normal, s, "s");
        normal = Regex.Replace(normal, capitalization, new MatchEvaluator(CapitalizeNames));
        return normal;
    }
}

// Algoritma Knuth-Morris-Pratt (KMP)
// Algoritma KMP adalah metode pencarian suatu pattern dalam sebuah text
// dari kiri ke kanan namun lebih efisien daripada brute force, yaitu
// menggunakan array border (Border Function) yang membantu dalam menentukan 
// posisi pencocokan selanjutnya ketika terjadi ketidakcocokan.
public class KMPAlgorithm {
    // Fungsi untuk membuat array border
    // Array border adalah sebuah array yang untuk setiap posisi dalam pattern,
    // menyimpan prefix terpanjang yang juga merupakan suffix.
    // Disebut juga border (Longest Prefix Suffix) atau failure function.
    // Contoh: untuk pattern ['a', 'b', 'a', 'a', 'b', 'a'], bordernya
    // [0, 0, 1, 1, 2].
    private static int[] borderFunction(string pattern) {
        // Inisialisasi variable
        int m = pattern.Length - 1; 
        int[] border = new int[m];
        int length = 0; 
        int i = 1;

        // Elemen pertama border selalu 0
        border[0] = 0; 

        // Iterasi setiap elemen pada pattern, mulai elemen kedua
        while (i < m) {
            // Jika cocok, set border[i] menjadi length 
            // (panjang prefix yang cocok dengan suffix hingga char index ke-i)
            if (pattern[i] == pattern[length]) {
                length++;
                border[i] = length;
                i++;
            // Jika tidak cocok, lihat lengthnya
            } else {
                // Jika ada yang cocok sebelumnya, set length menjadi 
                // nilai border sebelumnya
                if (length != 0) {
                    length = border[length - 1];
                // Jika tidak ada yang cocok sebelumnya, set 0
                } else {
                    border[i] = 0;
                    i++;
                }
            }
        }

        // Kembalikan border
        return border;
    }

    // Algoritma KMP
    // Algoritma KMP akan memanfaatkan border array yang sudah dibuat
    // sebelumnya untuk meningkatkan efisiensi jika terjadi ketidakcocokan.
    public static bool KMPSearch(string pattern, string text) {
        // Inisialisasi variabel
        int m = pattern.Length;
        int n = text.Length;
        bool found = false;

        // Membuat border array
        int[] border = KMPAlgorithm.borderFunction(pattern);

        // Iterasi index
        int i = 0;
        int j = 0; 

        // Selama text belum habis,
        while ((i < n) && !found) { 
            // Jika karakter cocok, majukan kedua index
            if (pattern[j] == text[i]) {
                j++;
                i++;
            }
            // Jika sudah mencapai akhir pattern, pattern ditemukan dalam text, break dari loop
            if (j == m) {
                Console.WriteLine("Found pattern at index " + (i - j));
                found = true;
            // Jika tidak, cek apakah text belum habis dan apakah karakter tidak
            // cocok.
            } else if (i < n && pattern[j] != text[i]) {
                // Jika index untuk pattern j != 0, pindah index pattern sesuai dengan
                // index ke j - 1 pada border, memungkinkan kecocokan parsial
                if (j != 0) {
                    j = border[j - 1];
                // Jika j = 0, iterasi i
                } else {
                    i++;
                }
            }
        }

        if ((i == n) && !found) {
            Console.WriteLine("Pattern not found.");
            return false;
        }
        return true;
    }
}

// Algoritma Boyer Moore
// Algoritma Boyer Moore merupakan sebuah algoritma string matching sebuah
// text dari kiri ke kanan, namun pencocokan mulai dari kanan pattern hingga
// pojok kiri. Algoritma Boyer Moore memanfaatkan 2 buah teknik, yaitu 
// teknik looking-glass dan teknik character-jump yang akan dibahas di program.
// Algoritma Boyer Moore juga menggunakan Last Occurence Function yang 
// mengembalikan array berisi index terakhir karakter (ASCII) muncul pada
// pattern.
public class BMAlgotithm {
    // Fungsi untuk mengembalikan array berisi index terakhir
    // setiap karakter ASCII muncul dalam pattern.
    // Array akan sepanjang 128, masing-masing untuk setiap karakter
    // ASCII yang ada, dan jika karakter tersebut tidak muncul di pattern,
    // akan diisi dengan -1.
    public static int[] lastOccuranceFunction(string pattern) {
        // 256 karakter ASCII
        int[] last = new int[256];
        // Inisiasi dengan -1
        for (byte i = 0; i< 255; i++) {
            last[i] = -1;
        }
        // Mengisi array dengan index terakhir karakter muncul  
        for (int i = 0; i < pattern.Length; i++) {
            last[(byte)pattern[i]] = i;
        }
        return last;
    }

    // Algoritma BMSearch
    // Fungsi untuk menjalankan algoritma Boyer Moore pada sebuah text
    // dengan memanggil fungsi lastOccuranceFunction untuk mendapatkan
    // array last index untuk setiap karakter ASCII (-1 jika tidak ada)
    // pada pattern, kemudian mengaplikasikan looking-glass technique
    // dan character-jump technique.
    public static bool BMSearch(string pattern, string text) {
        // Instansiasi variabel
        int[] last = BMAlgotithm.lastOccuranceFunction(pattern);
        int m = pattern.Length;
        int n = text.Length;
        bool found = false;
        
        // Iterasi index, jalan dari kiri ke kanan text
        // Dicek dari kanan ke kiri.
        int i = m - 1;
        int j = m - 1;

        // Melakukan iterasi
        while ((i < n) && !found) {
            // Jika match
            if (pattern[j] == text[i]) {
                // Jika sudah karakter pertama
                if (j == 0) {
                    Console.WriteLine("Found pattern at index " + (i));
                    found = true;
                // Jika belum, terapkan looking-glass technique
                // Looking-glass technique: mencari P di T dengan berjalan mundur
                // dari ujung akhir
                } else {
                    i--;
                    j--;
                }
            // Jika mismatch / tidak cocok, terapkan character-jump technique
            // Character-jump technique: Jika ada mismatch, dengan T[i] = x,
            // dan P[j] != x, atasi diantara 3 kasus berikut:
            // 1. Jika P ada x di kiri j, shift P ke kanan untuk align dengan last index x
            // 2. Jika p ada x di kanan j, shift P ke kanan 1 karakter ke T[i+1]
            // 3. Jika x tidak ada di P, shif P sehingga P[0] = T[i+1]
            } else {
                int lo = last[text[i]];
                i = i + m - Math.Min(j, 1 + lo);
                j = m - 1;
            }
        }

        // Jika tidak ada yang match
        if ((i > n - 1) && !found) {
            Console.WriteLine("Pattern not found.");
            return false;
        }
        return true;
    }
}