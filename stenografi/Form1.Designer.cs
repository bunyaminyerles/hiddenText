
namespace stenografi
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.sifreTextbox = new System.Windows.Forms.TextBox();
            this.sifreleRadio = new System.Windows.Forms.RadioButton();
            this.cozRadio = new System.Windows.Forms.RadioButton();
            this.sifreleButton = new System.Windows.Forms.Button();
            this.cozButton = new System.Windows.Forms.Button();
            this.kaydetButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mic = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(86, 38);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(256, 256);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // sifreTextbox
            // 
            this.sifreTextbox.Location = new System.Drawing.Point(414, 38);
            this.sifreTextbox.Multiline = true;
            this.sifreTextbox.Name = "sifreTextbox";
            this.sifreTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.sifreTextbox.Size = new System.Drawing.Size(217, 98);
            this.sifreTextbox.TabIndex = 1;
            // 
            // sifreleRadio
            // 
            this.sifreleRadio.AutoSize = true;
            this.sifreleRadio.Location = new System.Drawing.Point(3, 3);
            this.sifreleRadio.Name = "sifreleRadio";
            this.sifreleRadio.Size = new System.Drawing.Size(54, 17);
            this.sifreleRadio.TabIndex = 2;
            this.sifreleRadio.TabStop = true;
            this.sifreleRadio.Text = "Şifrele";
            this.sifreleRadio.UseVisualStyleBackColor = true;
            this.sifreleRadio.CheckedChanged += new System.EventHandler(this.sifreleRadio_CheckedChanged);
            // 
            // cozRadio
            // 
            this.cozRadio.AutoSize = true;
            this.cozRadio.Location = new System.Drawing.Point(109, 3);
            this.cozRadio.Name = "cozRadio";
            this.cozRadio.Size = new System.Drawing.Size(43, 17);
            this.cozRadio.TabIndex = 3;
            this.cozRadio.TabStop = true;
            this.cozRadio.Text = "Çöz";
            this.cozRadio.UseVisualStyleBackColor = true;
            this.cozRadio.CheckedChanged += new System.EventHandler(this.cozRadio_CheckedChanged);
            // 
            // sifreleButton
            // 
            this.sifreleButton.Location = new System.Drawing.Point(459, 162);
            this.sifreleButton.Name = "sifreleButton";
            this.sifreleButton.Size = new System.Drawing.Size(75, 23);
            this.sifreleButton.TabIndex = 4;
            this.sifreleButton.Text = "Şifrele";
            this.sifreleButton.UseVisualStyleBackColor = true;
            this.sifreleButton.Click += new System.EventHandler(this.sifreleButton_Click);
            // 
            // cozButton
            // 
            this.cozButton.Location = new System.Drawing.Point(459, 162);
            this.cozButton.Name = "cozButton";
            this.cozButton.Size = new System.Drawing.Size(75, 23);
            this.cozButton.TabIndex = 5;
            this.cozButton.Text = "Çöz";
            this.cozButton.UseVisualStyleBackColor = true;
            this.cozButton.Click += new System.EventHandler(this.cozButton_Click);
            // 
            // kaydetButton
            // 
            this.kaydetButton.Location = new System.Drawing.Point(540, 162);
            this.kaydetButton.Name = "kaydetButton";
            this.kaydetButton.Size = new System.Drawing.Size(75, 23);
            this.kaydetButton.TabIndex = 6;
            this.kaydetButton.Text = "Kaydet";
            this.kaydetButton.UseVisualStyleBackColor = true;
            this.kaydetButton.Click += new System.EventHandler(this.kaydetButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(83, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(270, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Şifrelenecek resimi seçmek için resim kutusuna tıklayınız";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.sifreleRadio);
            this.panel1.Controls.Add(this.cozRadio);
            this.panel1.Location = new System.Drawing.Point(131, 300);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(162, 25);
            this.panel1.TabIndex = 13;
            // 
            // mic
            // 
            this.mic.BackgroundImage = global::stenografi.Properties.Resources.mute_microphone;
            this.mic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mic.Location = new System.Drawing.Point(637, 38);
            this.mic.Name = "mic";
            this.mic.Size = new System.Drawing.Size(61, 57);
            this.mic.TabIndex = 14;
            this.mic.UseVisualStyleBackColor = true;
            this.mic.Click += new System.EventHandler(this.mic_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(756, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(200, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Key seçmek için resim kutusuna tıklayınız";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(759, 38);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(256, 256);
            this.pictureBox2.TabIndex = 15;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1129, 416);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.mic);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.kaydetButton);
            this.Controls.Add(this.cozButton);
            this.Controls.Add(this.sifreleButton);
            this.Controls.Add(this.sifreTextbox);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox sifreTextbox;
        private System.Windows.Forms.RadioButton sifreleRadio;
        private System.Windows.Forms.RadioButton cozRadio;
        private System.Windows.Forms.Button sifreleButton;
        private System.Windows.Forms.Button cozButton;
        private System.Windows.Forms.Button kaydetButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button mic;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

