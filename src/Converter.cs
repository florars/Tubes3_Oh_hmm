using System.Text.RegularExpressions;

class Converter
{
    public static string ConvertToSqlite(string mysql)
    {
        // Remove enum 
        mysql = Regex.Replace(mysql, @"enum\([^)]+\)\s*DEFAULT NULL", "TEXT");

        // Syntax
        mysql = Regex.Replace(mysql, @"(var)?char\(\d+\)", "TEXT");
        mysql = Regex.Replace(mysql, @"(tiny|small)?int\(\d+\)", "INTEGER");
        mysql = Regex.Replace(mysql, @"`(\w+)`", "$1"); 

        // Create Table 
        mysql = Regex.Replace(mysql, @"ENGINE=InnoDB", "");
        mysql = Regex.Replace(mysql, @"DEFAULT CHARSET=\w+", ""); 
        mysql = Regex.Replace(mysql, @"COLLATE=\w+", "");
        mysql = Regex.Replace(mysql, @"AUTO_INCREMENT", "AUTOINCREMENT");

        // Lock Unlock 
        mysql = Regex.Replace(mysql, @"LOCK TABLES biodata WRITE;", ""); 
        mysql = Regex.Replace(mysql, @"LOCK TABLES sidik_jari WRITE;", ""); 
        mysql = Regex.Replace(mysql, @"UNLOCK TABLES;", "");

        // ect 
        mysql = Regex.Replace(mysql, @"Displaying tubes3_stima24.sql.", "");
        mysql = Regex.Replace(mysql, @"tubes3_stima24.sql", "");

        return mysql;
    }
}
