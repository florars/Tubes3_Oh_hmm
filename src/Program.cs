// Program.cs adalah file main (pusat) di mana 
// program-program lain akan dieksekusi.

// Karena C#, program akan dibuat dalam pendekatan
// Object Oriented Programming.

using System;
using System.Windows.Forms;

namespace src
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Membuat objek GUI dan menjalankan objek tersebut
            Application.Run(new GUI());
        }
    }
}