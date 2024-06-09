// Karena ada kemungkinan tidak menemukan exact match, dibuat algoritma untuk 
// menghitung persentase kemiripan.
class TingkatKemiripan {
    // Fungsi Levenshtein Distance
    // Fungsi Levenshtein Distance adalah fungsi yang mengukur kemiripan antara dua string, 
    // dengan memperhitungkan jumlah operasi penyisipan, penghapusan, dan substitusi yang 
    // diperlukan untuk mengubah satu string ke string lainnya. 
    public static double LevenshteinDistance(string s, string t) {
        // Membuat matrix untuk menyimpan perbedaan karakter
        int[,] matrix = new int[s.Length + 1, t.Length + 1];

        // Inisialisasi matrix
        // Untuk kolom
        for (int i = 0; i <= s.Length; i++) {
            matrix[i, 0] = i; 
        }
        // Untuk baris
        for (int j = 0; j <= t.Length; j++) {
            matrix[0, j] = j; 
        }

        // Menghitung Levenshtein matrix
        for (int j = 1; j <= t.Length; j++) {
            for (int i = 1; i <= s.Length; i++) {
                // Jika karakter sama, tidak perlu ada perubahan
                if (s[i - 1] == t[j - 1])
                    matrix[i, j] = matrix[i - 1, j - 1]; 
                else
                    // Jika tidak, cari minimum antara deletion, insertion, atau substitution
                    matrix[i, j] = Math.Min(Math.Min(
                        matrix[i - 1, j] + 1, // Deletion
                        matrix[i, j - 1] + 1), // Insertion
                        matrix[i - 1, j - 1] + 1); // Substitution
            }
        }

        // Menghitung persentase nilai terakhir matrix yang merupakan Levenshtein Distance antar kedua string
        // Persentase = (1 - (Levenshtein Distance / Max Length)) * 100%
        double distance = (1 - ((double)matrix[s.Length, t.Length] / (double)Math.Max(s.Length, t.Length))) * 100;
        return distance;
    }
}