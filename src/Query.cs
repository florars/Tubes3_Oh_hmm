using System.Data.SQLite;
using System.Numerics;

class Query
{
    // Bikin ato override tubes3_stima24.db -> read new_converted_sqlite.sql
    public static void LoadSQLFile()
    {
        string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "tubes3_stima24.db");
        string connString = "Data Source=" + dbPath +";";
        string sqlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "new_converted_sqlite.sql");
        string sql = File.ReadAllText(sqlPath);

        using (var conn = new SQLiteConnection(connString))
        {
            try
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException sqlEx)
            {
                Console.WriteLine(sqlEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }


    private static List<BigInteger> ConvertToBigIntList(string data)
    {
        string[] stringArray = data.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        List<BigInteger> bigIntList = new List<BigInteger>();
        foreach (string str in stringArray)
        {
            bigIntList.Add(BigInteger.Parse(str));
        }
        return bigIntList;
    }

    private static string DecryptData(List<BigInteger> encryptedData)
    {
        DecryptFunc decryptFunc = new DecryptFunc(encryptedData);
        return decryptFunc.Decrypt();
    }


    // return list image path
    public static List<string> getAllPaths()
    {
        string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "tubes3_stima24.db");
        string connString = "Data Source=" + dbPath +";";
        List<string> paths = new List<string>();

        using (var conn = new SQLiteConnection(connString))
        {
            try
            {
                conn.Open();
                string query = "SELECT berkas_citra FROM sidik_jari;";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string berkasCitra = reader.GetString(0);
                                paths.Add(berkasCitra);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Gaada huhu");
                        }
                    }
                }
            }
            catch (SQLiteException sqlEx)
            {
                Console.WriteLine(sqlEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        return paths;
    }

    public static string getNameBasedOnPath(string berkasCitra)
    {
        string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "tubes3_stima24.db");
        string connString = "Data Source=" + dbPath +";";
        string name = null;

        using (var conn = new SQLiteConnection(connString))
        {
            try
            {
                conn.Open();
                string query = "SELECT nama FROM sidik_jari WHERE berkas_citra = @berkasCitra LIMIT 1;";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@berkasCitra", berkasCitra);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                name = reader.GetString(0);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Gaada huhu");
                        }
                    }
                }
            }
            catch (SQLiteException sqlEx)
            {
                Console.WriteLine(sqlEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        return name;
    }

    public static List<string> getIdentityBasedOnName(string name)
    {
        string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "tubes3_stima24.db");
        string connString = "Data Source=" + dbPath +";";
        List<string> identity = new List<string>();

        using (var conn = new SQLiteConnection(connString))
        {
            try
            {
                conn.Open();
                string query = "SELECT NIK, nama, tempat_lahir, tanggal_lahir, jenis_kelamin, golongan_darah, alamat, agama, status_perkawinan, pekerjaan, kewarganegaraan FROM biodata WHERE nama = @name LIMIT 1;";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", name);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                identity.Add(reader.GetString(0));
                                identity.Add(reader.GetString(1));
                                identity.Add(reader.GetString(2));
                                identity.Add(reader.GetString(3));
                                identity.Add(reader.GetString(4));
                                identity.Add(reader.GetString(5));
                                identity.Add(reader.GetString(6));
                                identity.Add(reader.GetString(7));
                                identity.Add(reader.GetString(8));
                                identity.Add(reader.GetString(9));
                                identity.Add(reader.GetString(10));
                            }
                        }
                        else
                        {
                            Console.WriteLine("Gaada huhu");
                        }
                        for (int i = 0; i < 11; i++)
                        {
                            List<BigInteger> encrypted = ConvertToBigIntList(identity[i]);
                            string decrypted = DecryptData(encrypted);
                            identity[i] = decrypted;
                        }
                    }
                }
            }
            catch (SQLiteException sqlEx)
            {
                Console.WriteLine(sqlEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        return identity;
    }

    // return list of dict, key = decrypted name, value = original name from database
    public static List<Dictionary<string, string>> GetAllNames()
    {
        string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "tubes3_stima24.db");
        string connString = "Data Source=" + dbPath +";";
        List<Dictionary<string, string>> namesList = new List<Dictionary<string, string>>();

        using (var conn = new SQLiteConnection(connString))
        {
            try
            {
                conn.Open();
                string query = "SELECT nama FROM biodata;";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string encryptedName = reader.GetString(0);
                                List<BigInteger> encryptedList = ConvertToBigIntList(encryptedName);
                                string decryptedName = DecryptData(encryptedList);

                                Dictionary<string, string> nameMap = new Dictionary<string, string>
                                    {
                                        { "decryptedName", decryptedName },
                                        { "originalName", encryptedName }
                                    };

                                namesList.Add(nameMap);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No data found");
                        }
                    }
                }
            }
            catch (SQLiteException sqlEx)
            {
                Console.WriteLine(sqlEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        return namesList;
    }

    // Encrypt tabel biodata 
    public static void EncryptSQL()
    {

        string filePath = "converted_sqlite.sql";
        string newFilePath = "new_converted_sqlite.sql";

        string sqlDump = File.ReadAllText(filePath);

        EncryptFunc encryptFunc = new EncryptFunc();
        encryptFunc.GetKey();

        string[] lines = sqlDump.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        List<string> newLines = new List<string>();

        foreach (string line in lines)
        {
            if (line.StartsWith("insert into biodata"))
            {
                string encryptedLine = EncryptValuesInLine(line, encryptFunc);
                newLines.Add(encryptedLine);
            }
            else
            {
                newLines.Add(line);
            }
        }

        File.WriteAllLines(newFilePath, newLines);
    }


    static string EncryptValuesInLine(string line, EncryptFunc encryptFunc)
    {
        int startIdx = line.IndexOf("values (") + 8;
        int endIdx = line.LastIndexOf(");");

        string valuesPart = line.Substring(startIdx, endIdx - startIdx);
        string[] values = valuesPart.Split(new[] { ", " }, StringSplitOptions.None);

        for (int i = 0; i < values.Length; i++)
        {
            string value = values[i].Trim('\'');
            List<BigInteger> encryptedValues = encryptFunc.Encrypt(value);
            values[i] = "'" + string.Join(" ", encryptedValues) + "'";
        }

        string newValuesPart = string.Join(", ", values);
        return line.Substring(0, startIdx) + newValuesPart + line.Substring(endIdx);
    }

}