using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Speech.Recognition;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Speech.V1;
using Grpc.Auth;
using Google.Apis.Services;
using System.Net;
using System.Media;
using System.Diagnostics;
using NAudio.Wave;
namespace stenografi
{
    public partial class Form1 : Form
    {
        private BufferedWaveProvider bwp;
        WaveIn waveIn;
        WaveOut waveOut;
        WaveFileWriter writer;
        WaveFileReader reader;
        string output = "audio.raw";

        static string YourSubscriptionKey = "YourSubscriptionKey";
        static string YourServiceRegion = "YourServiceRegion";
        String keyValue = "";
        public List<int> calculatedbinaries = new List<int>();
        int infonum = 0;
        String micState = "mute";
        public Form1()
        {
            InitializeComponent();
            waveOut = new WaveOut();
            waveIn = new WaveIn();
            waveIn.DataAvailable += new EventHandler<WaveInEventArgs>(waveIn_DataAvailable);
            waveIn.WaveFormat = new NAudio.Wave.WaveFormat(16000, 1);
            bwp = new BufferedWaveProvider(waveIn.WaveFormat);
            bwp.DiscardOnBufferOverflow = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sifreleButton.Visible = false;
            kaydetButton.Visible = false;
            cozButton.Visible = false;
            sifreTextbox.Enabled = false;
            mic.BackgroundImage = Properties.Resources.mute_microphone;
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            Bitmap bitmap = new Bitmap(pictureBox2.Image.Width, pictureBox2.Image.Height);
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
            byte[] imageBytes = stream.ToArray();
            string base64String = Convert.ToBase64String(imageBytes);
            keyValue = base64String.Substring(0, 16);
            if (NAudio.Wave.WaveIn.DeviceCount < 1)
            {
                MessageBox.Show("Etkin bir mikrofon bulamadım.", "Mikrofon Bağlı Değil", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void waveOut_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            try
            {
                waveOut.Stop();
                reader.Close();
                reader = null;
            }
            catch
            {

                //Exception..

            }
        }

        void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            bwp.AddSamples(e.Buffer, 0, e.BytesRecorded);

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            open.Filter = "Image Files( *.bmp;*.jpg;*.jpeg;*.png; )| *.bmp;*.jpg;*.jpeg;*.png;";
            if (open.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(open.FileName);
            }

        }

        private void sifreleRadio_CheckedChanged(object sender, EventArgs e)
        {
            sifreleButton.Visible = true;
            kaydetButton.Visible = true;
            cozButton.Visible = false;
            sifreTextbox.Enabled = true;
            sifreTextbox.Text = "";
        }

        private void cozRadio_CheckedChanged(object sender, EventArgs e)
        {
            sifreleButton.Visible = false;
            kaydetButton.Visible = false;
            cozButton.Visible = true;
            sifreTextbox.Enabled = false;
            sifreTextbox.Text = "";
        }
        private int[] decimaltobinary(int n)
        {
            int[] binaries = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };

            for (int i = 0; i < 8; i++)
            {

                if (n == 1)
                {
                    binaries[i] = 1;
                    break;
                }
                binaries[i] = n % 2;
                n = (int)n / 2;
            }
            return binaries;
        }
        private int binarytodecimal(int[] n)
        {
            int sum = 0;

            for (int i = 0; i < 8; i++)
            {
                sum += n[i] * (int)Math.Pow(2, i);
            }

            return sum;
        }
        private void createbits(char x)
        {
            int[] binaries = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
            int charnum = (int)x;

            binaries = decimaltobinary(charnum);

            for (int i = 7; i > -1; i--)
            {
                calculatedbinaries.Add(binaries[i]);
            }

        }
        private void sifreleButton_Click(object sender, EventArgs e)
        {
            infonum = 0;
            calculatedbinaries = new List<int>();
            Boolean finished = false;
            int finishcount = 0;


            Bitmap bmp = (Bitmap)pictureBox1.Image.Clone();

            String str = sifreTextbox.Text;
            str = str.Trim();

            str = EncryptStringToBytes_Aes(keyValue, str);


            for (int i = 0; i < str.Length; i++)
            {
                createbits(str[i]);
            }


            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {

                    Color pixel = bmp.GetPixel(x, y);
                    int r = pixel.R;
                    int g = pixel.G;
                    int b = pixel.B;
                    if (!finished)
                    {

                        int[] binaries = decimaltobinary(b);
                        binaries[0] = calculatedbinaries[infonum];

                        infonum++;
                        binaries[1] = calculatedbinaries[infonum];

                        infonum++;
                        binaries[2] = calculatedbinaries[infonum];

                        infonum++;
                        binaries[3] = calculatedbinaries[infonum];

                        infonum++;
                        b = binarytodecimal(binaries);
                        pixel = Color.FromArgb(r, g, b);
                        bmp.SetPixel(x, y, pixel);
                    }
                    else
                    {
                        if (finishcount / 8 >= 1 && finishcount / 8 < 4)
                        {
                            int[] binaries = decimaltobinary(b);
                            binaries[0] = 0;
                            finishcount++;
                            binaries[1] = 0;
                            finishcount++;
                            binaries[2] = 0;
                            finishcount++;
                            binaries[3] = 0;
                            finishcount++;
                            b = binarytodecimal(binaries);
                        }
                        else
                        {
                            int[] binaries = decimaltobinary(b);
                            binaries[0] = 1;
                            finishcount++;
                            binaries[1] = 1;
                            finishcount++;
                            binaries[2] = 1;
                            finishcount++;
                            binaries[3] = 1;
                            finishcount++;
                            b = binarytodecimal(binaries);
                        }

                        pixel = Color.FromArgb(r, g, b);
                        bmp.SetPixel(x, y, pixel);

                    }

                    if (infonum == calculatedbinaries.Count) finished = true;
                    if (finishcount == 24) break;
                }
                if (finishcount == 24) break;
            }


            pictureBox1.Image = bmp;
            MessageBox.Show("Şifrelendi");
        }

        private void cozButton_Click(object sender, EventArgs e)
        {
            infonum = 0;
            calculatedbinaries = new List<int>();
            String calculatedString = "";
            int finishedcount = 0;
            bool fcase = false;
            Bitmap bmp = (Bitmap)pictureBox1.Image.Clone();



            for (int y = 0; y < 256; y++)
            {
                for (int x = 0; x < 256; x++)
                {

                    Color pixel = bmp.GetPixel(x, y);
                    int b = pixel.B;

                    if (infonum % 8 == 0) fcase = true;


                    int[] binaries = decimaltobinary(b);
                    calculatedbinaries.Add(binaries[0]);
                    infonum++;
                    calculatedbinaries.Add(binaries[1]);
                    infonum++;
                    calculatedbinaries.Add(binaries[2]);
                    infonum++;
                    calculatedbinaries.Add(binaries[3]);
                    infonum++;



                    if (fcase && finishedcount / 8 >= 1 && finishedcount / 8 < 4)
                    {
                        if (binaries[0] == 0 && binaries[1] == 0 && binaries[2] == 0) finishedcount += 4;
                        else
                        {
                            fcase = false;
                            finishedcount = 0;
                        }
                    }
                    else if (fcase)
                    {
                        if (binaries[0] == 1 && binaries[1] == 1 && binaries[2] == 1) finishedcount += 4;
                        else
                        {
                            finishedcount = 0;
                            fcase = false;
                        }
                    }

                    if (finishedcount == 24) break;

                }
                if (finishedcount == 24) break;
            }
            int sum;
            for (int i = 0; i < infonum - 24; i += 8)
            {
                sum = 0;
                for (int j = 0; j < 8; j++)
                {
                    sum += (int)(calculatedbinaries[i + 7 - j] * Math.Pow(2, j));
                }
                calculatedString += (char)sum;
            }


            calculatedString = DecryptStringFromBytes_Aes(keyValue, calculatedString);

            sifreTextbox.Text = calculatedString;

        }

        private void kaydetButton_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = @"BMP|*.bmp" })
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image.Save(saveFileDialog.FileName);
                }
            }
        }


        public static string EncryptStringToBytes_Aes(string Key, string plainText)
        {
            byte[] keyBuffer = Encoding.UTF8.GetBytes(Key);
            byte[] iv = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (keyBuffer == null || keyBuffer.Length <= 0)
                throw new ArgumentNullException("Key");
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {

                aesAlg.Key = keyBuffer;
                aesAlg.IV = iv;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return Convert.ToBase64String(encrypted);
        }

        public static string DecryptStringFromBytes_Aes(string Key, string cipherText)
        {
            byte[] keyBuffer = Encoding.UTF8.GetBytes(Key);
            byte[] buffer = Convert.FromBase64String(cipherText);
            byte[] iv = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            // Check arguments.
            if (buffer == null || buffer.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (keyBuffer == null || keyBuffer.Length <= 0)
                throw new ArgumentNullException("Key");
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Padding = PaddingMode.None;
                aesAlg.Key = keyBuffer;
                aesAlg.IV = iv;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(buffer))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }

        private void mic_Click(object sender, EventArgs e)
        {
            if (micState == "mute")
            {
                sifreTextbox.Text = "Dinleniyor";
                mic.BackgroundImage = Properties.Resources.microphone;
                waveIn.StartRecording();
                micState = "unmute";
            }
            else
            {
                micState = "mute";
                mic.BackgroundImage = Properties.Resources.mute_microphone;
                sifreTextbox.Clear();
                sifreTextbox.Text = "Analiz ediliyor";
                backgroundWorker1.RunWorkerAsync();


                //SpeechRecognitionEngine sr = new SpeechRecognitionEngine();
                //Grammar word = new DictationGrammar();
                //sr.LoadGrammar(word);
                //try
                //{
                //    sifreTextbox.Text = "Dinleniyor";
                //    sr.SetInputToDefaultAudioDevice();
                //    RecognitionResult result = sr.Recognize();
                //    sifreTextbox.Clear();
                //    sifreTextbox.Text = result.Text;
                //}
                //catch
                //{
                //    sifreTextbox.Text = "";
                //    MessageBox.Show("Mikrofon bulunamadı");

                //}
                //finally
                //{
                //    sr.UnloadAllGrammars();

                //    mic.BackgroundImage = Properties.Resources.mute_microphone;
                //}

            }

        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files( *.bmp;*.jpg;*.jpeg;*.png; )| *.bmp;*.jpg;*.jpeg;*.png;";
            if (open.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image = new Bitmap(open.FileName);
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

                System.IO.MemoryStream stream = new System.IO.MemoryStream();
                Bitmap bitmap = new Bitmap(pictureBox2.Image.Width, pictureBox2.Image.Height);
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                byte[] imageBytes = stream.ToArray();
                string base64String = Convert.ToBase64String(imageBytes);
                keyValue = base64String.Substring(0, 16);
            }

        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;

            waveIn.StopRecording();

            if (File.Exists("audio.raw"))
                File.Delete("audio.raw");


            writer = new WaveFileWriter(output, waveIn.WaveFormat);



            byte[] buffer = new byte[bwp.BufferLength];
            int offset = 0;
            int count = bwp.BufferLength;

            var read = bwp.Read(buffer, offset, count);
            if (count > 0)
            {
                writer.Write(buffer, offset, read);
            }

            waveIn.Dispose();
            waveIn = null;
            writer.Close();
            writer = null;

            reader = new WaveFileReader("audio.raw"); // (new MemoryStream(bytes));
            waveOut.Init(reader);
            waveOut.PlaybackStopped += new EventHandler<StoppedEventArgs>(waveOut_PlaybackStopped);
            //    waveOut.Play();

            reader.Close();

            if (File.Exists("audio.raw"))
            {

                var speech = SpeechClient.Create();

                var response = speech.Recognize(new RecognitionConfig()
                {
                    Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
                    SampleRateHertz = 16000,
                    LanguageCode = "tr",

                }, RecognitionAudio.FromFile("audio.raw"));

                // AIzaSyCMVUZen9fupUmN2QOo1fIIvnCjoPPmEUY 

                sifreTextbox.Text = "";

                foreach (var result in response.Results)
                {
                    foreach (var alternative in result.Alternatives)
                    {
                        sifreTextbox.Text = sifreTextbox.Text + " " + alternative.Transcript;
                    }
                }

                if (sifreTextbox.Text.Length == 0)
                    sifreTextbox.Text = "Ses kaydı çok uzun ya da hiç ses algılanamadı.";
            }
            else
            {

                sifreTextbox.Text = "Ses Dosyası Bulunamadı";

            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            waveIn = new WaveIn();

            waveIn.DataAvailable += new EventHandler<WaveInEventArgs>(waveIn_DataAvailable);
            waveIn.WaveFormat = new NAudio.Wave.WaveFormat(16000, 1);
        }


    }
}
