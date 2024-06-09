// GUI.cs merupakan progam backend berdasarkan frontend
// yang dibuat.

using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Net;
using System.Windows.Forms;
using System.Diagnostics;

namespace src
{
    public partial class GUI : Form
    {
        // Attribute
        private String imagePath;

        // Constructor frontend
        public GUI()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.imagePath = "";
        }

        // // Fungsi untuk mengupload gambar dan menyimpannya di lokal.
        private void uploadButtonClick(object sender, EventArgs e)
        {
            // Membuka folder untuk mencari image yang diupload.
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Menyimpannya di lokal berdasarkan path
                string sourceFilePath = openFileDialog.FileName;
                string fileName = Path.GetFileName(sourceFilePath);
                string destinationDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UploadedImages");
                string destinationFilePath = Path.Combine(destinationDirectory, fileName);

                if (!Directory.Exists(destinationDirectory))
                {
                    Directory.CreateDirectory(destinationDirectory);
                }

                File.Copy(sourceFilePath, destinationFilePath, true);

                this.imagePath = destinationFilePath;

                // Menampilkan gambar di aplikasi
                pictureBox.Image = new Bitmap(destinationFilePath);

                // izin nambahin di sini soalnya aku beberapa kali dapet error di sini @evelynnn04
                try
                {
                    Bitmap bitmap = new Bitmap(destinationFilePath);
                    pictureBox.Image = bitmap;
                }
                catch (Exception ex)
                {
                    // MessageBox.Show("Error: " + ex.Message);
                }
            }
            this.conversionButton.Enabled = true;
        }

        // Fungsi untuk mengubah gambar menjadi binary lalu mengubahnya
        // menjadi ASCII
        private void convertImage(object sender, EventArgs e)
        {
            conversionButton.Enabled = false;
            string binaryImage = convertImageToBinary(this.imagePath, 4, 8);

            txtBinary.Text = binaryImage;
            string asciiImage = convertBinaryToAscii(binaryImage);
            txtAscii.Text = "";
            foreach (char c in asciiImage)
            {
                txtAscii.Text += c;
            }
            conversionButton.Enabled = false;
            this.KMPButton.Enabled = true;
            this.BMButton.Enabled = true;
        }

        // Fungsi buat convert image jadi ascii
        // Izin copas dari "private void convertImage(object sender, EventArgs e)" hehe @evelynnn04
        private string convertImageToAscii(string path)
        {
            string binaryImage = convertImageToBinary(path, 6, 5);
            string asciiImage = convertBinaryToAscii(binaryImage);
            string res = "";
            foreach (char c in asciiImage)
            {
                res += c;
            }
            return res;
        }

        private string convertFullImageToAscii(string path)
        {
            Bitmap bitmap = new Bitmap(path);
            string binaryImage = convertImageToBinary(path, bitmap.Width, bitmap.Height);
            string asciiImage = convertBinaryToAscii(binaryImage);
            string res = "";
            foreach (char c in asciiImage)
            {
                res += c;
            }
            return res;
        }

        // Fungsi untuk mengubah gambar menjadi binary.
        private string convertImageToBinary(string imagePath, int widthPixels, int heightPixels)
        {
            // Izin nambahin try catch @evelynnn04
            StringBuilder binaryBuilder = new StringBuilder();
            try
            {
                Bitmap bitmap = new Bitmap(imagePath);
                int bitmapWidth = bitmap.Width;
                int bitmapHeight = bitmap.Height;

                // Jika 6 x 5 pixels
                int startWidth = Math.Max((bitmapWidth - widthPixels) / 2, 0);
                int startHeight = Math.Max((bitmapHeight - heightPixels) / 2, 0);
                int width = Math.Min(widthPixels, bitmapWidth);
                int height = Math.Min(heightPixels, bitmapHeight);

                // Jika 30 x 30 pixels
                // int startWidth = Math.Max((bitmapWidth - 30) / 2, 0);
                // int startHeight = Math.Max((bitmapHeight - 30) / 2, 0);
                // int width = Math.Min(30, bitmapWidth);
                // int height = Math.Min(30, bitmapHeight);

                int ctr = 0;
                int x = startWidth;
                int y = startHeight;
                while (ctr < widthPixels * heightPixels)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);
                    int grayscaleValue = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                    binaryBuilder.Append(grayscaleValue < 128 ? "1" : "0");
                    ctr++;
                    x++;
                    if (x >= bitmapWidth)
                    {
                        x = 0;
                        y++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return string.Empty;
            }
            return binaryBuilder.ToString();
        }

        // Fungsi untuk mengubah binary menjadi ascii.
        private string convertBinaryToAscii(string binaryString)
        {
            StringBuilder asciiString = new StringBuilder();
            for (int i = 0; i < binaryString.Length; i += 8)
            {
                string byteString = binaryString.Substring(i, Math.Min(8, binaryString.Length - i));
                int asciiCode = Convert.ToByte(byteString, 2);
                char asciiChar = (char)asciiCode;
                asciiString.Append(asciiChar);
            }
            return asciiString.ToString();
        }

        private void matchingHandler(string asciiString, string fullAscii, string algo)
        {
            // Time exe
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // TODO: 
            // 1. Query ke database(?)
            List<string> paths = Query.getAllPaths();

            // 2. Get all fingerprints
            List<Dictionary<string, string>> convertedPathsList = new List<Dictionary<string, string>>();

            foreach (string path in paths)
            {
                string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..");
                basePath = Path.Combine(basePath, path);
                string asciiImage = convertFullImageToAscii(basePath);

                Dictionary<string, string> pathMap = new Dictionary<string, string>
                {
                    { "originalPath", path },
                    { "convertedPath", asciiImage }
                };

                convertedPathsList.Add(pathMap);
            }

            double distance = -9999;
            string closest = null;

            // 3. Cari path yang mirip 
            foreach (var pathMap in convertedPathsList)
            {
                string originalPath = pathMap["originalPath"];
                string convertedPath = pathMap["convertedPath"];

                if (algo.Equals("KMP"))
                {
                    if (KMPAlgorithm.KMPSearch(asciiString, convertedPath))
                    {
                        closest = originalPath;
                        distance = 100;
                        break;
                    }
                    else
                    {
                        double dist = TingkatKemiripan.LevenshteinDistance(fullAscii, convertedPath);
                        if (distance < dist)
                        {
                            distance = dist;
                            closest = originalPath;
                        }
                    }
                }
                else
                {
                    if (BMAlgotithm.BMSearch(asciiString, convertedPath))
                    {
                        closest = originalPath;
                        distance = 100;
                        break;
                    }
                    else
                    {
                        double dist = TingkatKemiripan.LevenshteinDistance(fullAscii, convertedPath);
                        if (distance < dist)
                        {
                            distance = dist;
                            closest = originalPath;
                        }
                    }
                }
            }

            // Sampe sini dapet closest = path yang paling mirip 
            string oriName = Query.getNameBasedOnPath(closest); // -> nama yg bener

            // 4. query biodata table with original, alay, name.... uh, belum kebayang sebenernya kalo belum ada db. mungkin perlu hashmap nama normalized -> nama di db (alay)??
            List<Dictionary<string, string>> nameList = Query.GetAllNames();
            string nameRes = "";
            double min = -99999;

            foreach (var nameMap in nameList)
            {
                string decryptedName = nameMap["decryptedName"];
                string nameInDB = nameMap["originalName"];
                decryptedName = AlayMatcher.ToNormal(decryptedName);

                if (algo.Equals("KMP"))
                {
                    if (KMPAlgorithm.KMPSearch(decryptedName, oriName))
                    {
                        nameRes = nameInDB;
                        break;
                    }
                    else
                    {
                        double dist = TingkatKemiripan.LevenshteinDistance(oriName, decryptedName);
                        if (min < dist)
                        {
                            min = dist;
                            nameRes = nameInDB;
                        }
                    }
                }
                else
                {
                    if (BMAlgotithm.BMSearch(decryptedName, oriName))
                    {
                        nameRes = nameInDB;
                        break;
                    }
                    else
                    {
                        double dist = TingkatKemiripan.LevenshteinDistance(oriName, decryptedName);
                        if (min < dist)
                        {
                            min = dist;
                            nameRes = nameInDB;
                        }
                    }
                }
            }

            // Time exe
            stopwatch.Stop();

            List<String> identity = Query.getIdentityBasedOnName(nameRes);
            if (identity != null && identity.Count > 0 && distance > 50.0)
            {
                string message = "Sidik jari ditemukan!" + Environment.NewLine;
                message += "Tingkat kemiripan : " + distance + "%" + Environment.NewLine + Environment.NewLine;
                message += "Result:" + Environment.NewLine;
                message += "NIK : " + identity[0] + Environment.NewLine;
                message += "Nama : " + oriName + Environment.NewLine;
                message += "Tempat Lahir : " + identity[2] + Environment.NewLine;
                message += "Tanggal Lahir : " + identity[3] + Environment.NewLine;
                message += "Jenis Kelamin : " + identity[4] + Environment.NewLine;
                message += "Golongan Darah : " + identity[5] + Environment.NewLine;
                message += "Alamat : " + identity[6] + Environment.NewLine;
                message += "Agama : " + identity[7] + Environment.NewLine;
                message += "Status Perkawinan : " + identity[8] + Environment.NewLine;
                message += "Pekerjaan : " + identity[9] + Environment.NewLine;
                message += "Kewarganegaraan : " + identity[10] + Environment.NewLine + Environment.NewLine;
                message += "Time execution: " + stopwatch.ElapsedMilliseconds + " ms";

                result.Text = message;
                
                Bitmap bitmap = new Bitmap("../" + closest);
                pictureRes.Image = bitmap;
            }
            else
            {
                MessageBox.Show("No identity found!");
            }
        }

        private void matcherKMP(object sender, EventArgs e)
        {
            KMPButton.Enabled = false;

            // call MatchingHandler, show results
            Bitmap b = new Bitmap(imagePath);
            string asciiString = txtAscii.Text;
            string full = convertFullImageToAscii(imagePath);
            matchingHandler(asciiString, full, "KMP");

            KMPButton.Enabled = true;
        }

        private void matcherBM(object sender, EventArgs e)
        {
            BMButton.Enabled = false;

            // call MatchingHandler, show results
            string asciiString = txtAscii.Text;
            string full = convertFullImageToAscii(imagePath);
            matchingHandler(asciiString, full, "BM");

            BMButton.Enabled = true;
        }

        private void convertSqlite(object sender, EventArgs e)
        {
            SQLConvert.Enabled = false;

            // Convert ke sqlite syntax 
            // Simpen ke converted_sqlite.sql
            string mysqlDump = File.ReadAllText("tubes3_stima24.sql");
            string sqliteDump = Converter.ConvertToSqlite(mysqlDump);
            File.WriteAllText("converted_sqlite.sql", sqliteDump);

            // Encrypt converted_sqlite.sql
            // Simpen ke new_converted_sqlite.sql
            Query.EncryptSQL();

            // Bikin ato override db  
            Query.LoadSQLFile();

            Console.WriteLine("Sukses!");

            SQLConvert.Enabled = true;
        }
    }

}
