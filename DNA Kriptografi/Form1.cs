using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DNA_Kriptografi
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        public void Sezar()
        {
            textBox2.Clear();
            char[] liste = { 'a','b','c','ç', 'd','e','f', 'g', 'ğ', 'h', 'ı', 'i', 'j', 'k', 'l', 'm', 'n',
                'o', 'ö', 'p', 'r', 's', 'ş', 't', 'u', 'ü', 'v', 'y', 'z'};

            string metin = textBox1.Text.Trim();
            int kaydirma = Convert.ToInt16(textBox3.Text);
            char temp;


            foreach (char c in metin.ToLower())
            {

                temp = c;
                for (int i = 0; i < 29; i++)
                {
                    if (c == liste[i])
                    {
                        temp = liste[(i + kaydirma) % 29];
                    }
                }
                textBox2.Text = textBox2.Text + (Convert.ToString(temp));

            }
        }

        public void Ascii()
        {

            int i;
            string metin = textBox2.Text;
            textBox2.Clear();


            for (i = 0; i < metin.Length; i++)
            {
                textBox2.Text += Convert.ToInt32(metin[i])+ " ";
            }
        }

        public void BinaryValues()
        {
            string converted = string.Empty;
            string metinAscii = textBox2.Text;

            byte[] byteArray = Encoding.ASCII.GetBytes(metinAscii);
            textBox2.Clear();
            for (int i = 0; i < byteArray.Length; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    converted += (byteArray[i] & 0x80) > 0 ? "1" : "0";
                    byteArray[i] <<= 1;
                    textBox2.Text = converted;
                }
            }

        }

        public void DNA()
        {
            string binary = textBox2.Text;

            var codes = new Dictionary<string, string> {
             {"00", "A"},
             {"01", "G"},
             {"10", "C"},
             {"11", "T"}
            };

            StringBuilder builder = new StringBuilder();
            textBox2.Clear();
            for (int i = 0; i + 1 < binary.Length; i = i + 2)
            {
                var localCode = string.Format("{0}{1}", binary[i], binary[i + 1]);
                string buffer;
                var output = codes.TryGetValue(localCode, out buffer) ? buffer : string.Empty;
                builder.Append(output);
            }

            textBox2.Text = builder.ToString();
        }

        public void DnaToBinary()
        {
            string dna = textBox4.Text.Trim();

            for (int i = 0; i + 1 < dna.Length; i++)
            {
                if (dna.Contains("A"))
                {
                    dna = dna.Replace("A", "00");
                }

                if (dna.Contains("G"))
                {
                    dna = dna.Replace("G", "01");
                }

                if (dna.Contains("C"))
                {
                    dna = dna.Replace("C", "10");
                }

                else
                {
                    dna = dna.Replace("T", "11");
                }
            }
            textBox5.Text = dna;
        }

        public Byte[] GetBytesFromBinaryString(String binary)
        {
            var list = new List<Byte>();

            for (int i = 0; i < binary.Length; i += 8)
            {
                String t = binary.Substring(i, 8);

                list.Add(Convert.ToByte(t, 2));
            }

            return list.ToArray();
        }

        public void BinaryToAscii()
        {
            string binary = textBox5.Text;
            textBox5.Clear();
            var data = GetBytesFromBinaryString(binary);
            var text = Encoding.ASCII.GetString(data);
            textBox5.Text = text;
        }

        public void AsciiToText()
        {
            string metin = (textBox5.Text).Trim();
            string[] sayilar = metin.Split(' ');
            textBox5.Clear();
            foreach (string sayi in sayilar)
            {
                int s1 = Convert.ToInt32(sayi);
                textBox5.Text += ((char)s1).ToString();              
            }            

        }

        public void TextToPlainText()
        {
            
            char[] liste1 = { 'a','b','c','ç', 'd','e','f', 'g', 'ğ', 'h', 'ı', 'i', 'j', 'k', 'l', 'm', 'n',
                'o', 'ö', 'p', 'r', 's', 'ş', 't', 'u', 'ü', 'v', 'y', 'z'};

            string metin1 = textBox5.Text.Trim();
            int kaydirma1 = Convert.ToInt16(textBox6.Text);
            char temp1;

            textBox5.Clear();
            foreach (char c in metin1.ToLower())
            {

                temp1 = c;
                for (int i = 0; i < 29; i++)
                {
                    if (c == liste1[i])
                    {
                        temp1 = liste1[((i - kaydirma1)+29) % 29];
                    }
                }
                textBox5.Text = textBox5.Text + (Convert.ToString(temp1));

            }

        }

        private void sifrele_Click(object sender, EventArgs e)
        {
            Sezar();
            Ascii();
            BinaryValues();
            DNA();
        }

        private void sifreCoz_Click(object sender, EventArgs e)
        {
            DnaToBinary();
            BinaryToAscii();
            AsciiToText();
            TextToPlainText();
        }
    }
}
