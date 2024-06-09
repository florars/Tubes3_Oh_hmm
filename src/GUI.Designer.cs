// GUI.Designer.cs merupakan frontend yang akan ditampilkan
// sebagai UI.

namespace src
{
    // Program (constructor) dijalankan melalui backend.
    partial class GUI
    {
        private System.ComponentModel.IContainer components = null;
        private Button uploadButton;
        private PictureBox pictureBox;
        private PictureBox pictureRes;
        private Button conversionButton;
        private Button KMPButton;
        private Button BMButton;
        private Button SQLConvert;
        private TextBox txtBinary;
        private TextBox txtAscii;
        private TextBox result;
        private int plusX;
        private int plusY;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // Instansiasi
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int plusX = (screenWidth - 800) / 2;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            int plusY = (screenHeight - 560) / 2;

            this.uploadButton = new Button();
            this.pictureBox = new PictureBox();
            this.pictureRes = new PictureBox();
            this.conversionButton = new Button();
            this.KMPButton = new Button();
            this.BMButton = new Button();
            this.SQLConvert = new Button();
            this.txtBinary = new TextBox();
            this.txtAscii = new TextBox();
            this.result = new TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            
            // Add backgprund
            string bg = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "bg.jpg");
            this.BackgroundImage = Image.FromFile(bg);
            this.BackgroundImageLayout = ImageLayout.Stretch;

            // Tombol untuk upload gambar, menjalankan fungsi uploadButtonClick.
            this.uploadButton.Location = new System.Drawing.Point(plusX + 10, plusY + 10);
            this.uploadButton.Name = "uploadButton";
            this.uploadButton.Size = new System.Drawing.Size(150, 40);
            this.uploadButton.TabIndex = 0;
            this.uploadButton.Text = "Upload Image";
            this.uploadButton.UseVisualStyleBackColor = false;
            this.uploadButton.BackColor = System.Drawing.ColorTranslator.FromHtml("#0A1414");
            this.uploadButton.ForeColor = System.Drawing.ColorTranslator.FromHtml("#20E5F6");
            this.uploadButton.Font = new System.Drawing.Font("Lucida Console", 10);
            this.uploadButton.MouseEnter += new System.EventHandler(this.buttonMouseEnter);
            this.uploadButton.MouseLeave += new System.EventHandler(this.buttonMouseLeave);
            this.uploadButton.Click += new System.EventHandler(this.uploadButtonClick);

            // Area untuk menampilkan gambar yang diupload.
            this.pictureBox.Location = new System.Drawing.Point(plusX + 10, plusY + 60);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(280, 280);
            this.pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 1;
            this.pictureBox.TabStop = false;
            this.pictureBox.BackColor = System.Drawing.ColorTranslator.FromHtml("#20E5F6");

            // Tombol untuk mengubah gambar ke binary dan Ascii, menjalankan fungsi convertImage.
            this.conversionButton.Location = new System.Drawing.Point(plusX + 530, plusY + 10);
            this.conversionButton.Name = "conversionButton";
            this.conversionButton.Size = new System.Drawing.Size(150, 40);
            this.conversionButton.TabIndex = 0;
            this.conversionButton.Text = "Convert Image";
            this.conversionButton.UseVisualStyleBackColor = false;
            this.conversionButton.BackColor = System.Drawing.ColorTranslator.FromHtml("#0A1414");
            this.conversionButton.ForeColor = System.Drawing.ColorTranslator.FromHtml("#20E5F6");
            this.conversionButton.Font = new System.Drawing.Font("Lucida Console", 10);
            this.conversionButton.MouseEnter += new System.EventHandler(this.buttonMouseEnter);
            this.conversionButton.MouseLeave += new System.EventHandler(this.buttonMouseLeave);
            this.conversionButton.Click += new System.EventHandler(this.convertImage);
            this.conversionButton.Enabled = false;

            // Area untuk menampilkan hasil konversi gambar ke binary.
            this.txtBinary.Location = new System.Drawing.Point(plusX + 300, plusY + 60);
            this.txtBinary.Multiline = true;
            this.txtBinary.Name = "txtBinary";
            this.txtBinary.ScrollBars = ScrollBars.Vertical;
            this.txtBinary.Size = new System.Drawing.Size(280, 280);
            this.txtBinary.TabIndex = 2;
            this.txtBinary.BackColor = System.Drawing.ColorTranslator.FromHtml("#0A1414");
            this.txtBinary.ForeColor = System.Drawing.ColorTranslator.FromHtml("#20E5F6");
            this.txtBinary.Font = new System.Drawing.Font("Lucida Console", 14);
            this.txtBinary.ReadOnly = true;

            // Area untuk menampilkan hasil konversi binary ke Ascii.
            this.txtAscii.Location = new System.Drawing.Point(plusX + 590, plusY + 60);
            this.txtAscii.Multiline = true;
            this.txtAscii.Name = "txtAscii";
            this.txtAscii.ScrollBars = ScrollBars.Vertical;
            this.txtAscii.Size = new System.Drawing.Size(280, 280);
            this.txtAscii.TabIndex = 2;
            this.txtAscii.BackColor = System.Drawing.ColorTranslator.FromHtml("#0A1414");
            this.txtAscii.ForeColor = System.Drawing.ColorTranslator.FromHtml("#20E5F6");
            this.txtAscii.Font = new System.Drawing.Font("Lucida Console", 14);
            this.txtAscii.ReadOnly = true;

            // Tombol untuk memilih algoritma KMP, menjalankan fungsi matcherKMP.
            this.KMPButton.Location = new System.Drawing.Point(plusX + 900, plusY + 60);
            this.KMPButton.Name = "KMPButton";
            this.KMPButton.Size = new System.Drawing.Size(150, 40);
            this.KMPButton.TabIndex = 0;
            this.KMPButton.Text = "Use KMP";
            this.KMPButton.UseVisualStyleBackColor = false;
            this.KMPButton.BackColor = System.Drawing.ColorTranslator.FromHtml("#0A1414");
            this.KMPButton.ForeColor = System.Drawing.ColorTranslator.FromHtml("#20E5F6");
            this.KMPButton.Font = new System.Drawing.Font("Lucida Console", 10);
            this.KMPButton.MouseEnter += new System.EventHandler(this.buttonMouseEnter);
            this.KMPButton.MouseLeave += new System.EventHandler(this.buttonMouseLeave);
            this.KMPButton.Click += new System.EventHandler(this.matcherKMP);
            this.KMPButton.Enabled = false;
            // this.KMPButton.EnabledChanged += new System.EventHandler(this.buttonEnabledChanged);

            // Tombol untuk memilih algoritma BM, menjalankan fungsi matcherBM.
            this.BMButton.Location = new System.Drawing.Point(plusX + 900, plusY + 120);
            this.BMButton.Name = "BMButton";
            this.BMButton.Size = new System.Drawing.Size(150, 40);
            this.BMButton.TabIndex = 0;
            this.BMButton.Text = "Use Boyer Moore";
            this.BMButton.UseVisualStyleBackColor = false;
            this.BMButton.BackColor = System.Drawing.ColorTranslator.FromHtml("#0A1414");
            this.BMButton.ForeColor = System.Drawing.ColorTranslator.FromHtml("#20E5F6");
            this.BMButton.Font = new System.Drawing.Font("Lucida Console", 8);
            this.BMButton.MouseEnter += new System.EventHandler(this.buttonMouseEnter);
            this.BMButton.MouseLeave += new System.EventHandler(this.buttonMouseLeave);
            this.BMButton.Click += new System.EventHandler(this.matcherBM);
            this.BMButton.Enabled = false;

            // Tombol untuk convert ke sqlite
            this.SQLConvert.Location = new System.Drawing.Point(plusX + 900, plusY + 300);
            this.SQLConvert.Name = "SQLConvert";
            this.SQLConvert.Size = new System.Drawing.Size(150, 40);
            this.SQLConvert.TabIndex = 0;
            this.SQLConvert.Text = "Convert to Sqlite";
            this.SQLConvert.UseVisualStyleBackColor = false;
            this.SQLConvert.BackColor = System.Drawing.ColorTranslator.FromHtml("#0A1414");
            this.SQLConvert.ForeColor = System.Drawing.ColorTranslator.FromHtml("#20E5F6");
            this.SQLConvert.Font = new System.Drawing.Font("Lucida Console", 8);
            this.SQLConvert.MouseEnter += new System.EventHandler(this.buttonMouseEnter);
            this.SQLConvert.MouseLeave += new System.EventHandler(this.buttonMouseLeave);
            this.SQLConvert.Click += new System.EventHandler(this.convertSqlite);

            // Area untuk menampilkan result
            this.result.Location = new System.Drawing.Point(plusX + 300, plusY + 350);
            this.result.Multiline = true;
            this.result.Name = "result";
            this.result.ScrollBars = ScrollBars.Vertical;
            this.result.Size = new System.Drawing.Size(570, 200);
            this.result.TabIndex = 2;
            this.result.BackColor = System.Drawing.ColorTranslator.FromHtml("#0A1414");
            this.result.ForeColor = System.Drawing.ColorTranslator.FromHtml("#20E5F6");
            this.result.Font = new System.Drawing.Font("Lucida Console", 14);
            this.result.ReadOnly = true;

            // Area untuk menampilkan gambar hasil.
            this.pictureRes.Location = new System.Drawing.Point(plusX + 10, plusY + 350);
            this.pictureRes.Name = "pictureRes";
            this.pictureRes.Size = new System.Drawing.Size(280, 200);
            this.pictureRes.SizeMode = PictureBoxSizeMode.Zoom;
            this.pictureRes.TabIndex = 1;
            this.pictureRes.TabStop = false;
            this.pictureRes.BackColor = System.Drawing.ColorTranslator.FromHtml("#20E5F6");

            // Menyusun GUI secara keseluruhan.
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Controls.Add(this.uploadButton);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.pictureRes);
            this.Controls.Add(this.conversionButton);
            this.Controls.Add(this.KMPButton);
            this.Controls.Add(this.BMButton);
            this.Controls.Add(this.txtBinary);
            this.Controls.Add(this.txtAscii);
            this.Controls.Add(this.SQLConvert);
            this.Controls.Add(this.result);
            this.Name = "GUI";
            this.Text = "Image Uploader";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // Kasi hover
        private void buttonMouseEnter(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                button.BackColor = System.Drawing.ColorTranslator.FromHtml("#20E5F6");
                button.ForeColor = System.Drawing.ColorTranslator.FromHtml("#0A1414");
            }
        }

        // Balikin warna button 
        private void buttonMouseLeave(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                button.BackColor = System.Drawing.ColorTranslator.FromHtml("#0A1414");
                button.ForeColor = System.Drawing.ColorTranslator.FromHtml("#20E5F6");
            }
        }

        private void buttonEnabledChanged(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                if (button.Enabled)
                {
                    button.BackColor = System.Drawing.ColorTranslator.FromHtml("#b4b4b4");
                    button.ForeColor = System.Drawing.ColorTranslator.FromHtml("#20E5F6");
                }
                else
                {
                    button.BackColor = System.Drawing.Color.Gray;
                    button.ForeColor = System.Drawing.Color.DarkGray;
                }
            }
        }
    }
}