using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.IO;
using System.Security.Cryptography;


/**============================================================================
* Автор			: Бородулин Андрей
* Группа		: 22305
* Программа		: Гибридная криптосистема
*
* Описание		: Реализация гибридной криптосистемы 
*                 с генерацией ЦП и сеансового ключа
*==============================================================================
*/

namespace Remediassance_Cryptosystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public bool isSmall = true;
        TripleDESCryptoServiceProvider tripleDES;
        byte[] encryptedData;
        byte[] decryptedData;
        RSAParameters exportedParams;
        byte[] signedHash;



        /*=====================================================================
        *              ГЕНЕРАЦИЯ И СОХРАНЕНИЕ СЕАНСОВОГО КЛЮЧА
        *=====================================================================
        */
        private void createSeanseBtn_Click(object sender, EventArgs e)
        {

            tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.GenerateKey();
            String s = "";

            foreach (byte b in tripleDES.Key)
                s += b.ToString();

            saveText(s.Substring(0, 31));
            textBox1.Text = s.Substring(0, 31);
            encodeKeyBtn.Enabled = true;
        }



        /*=====================================================================
         *                 ШИФРОВАНИЕ СЕАНСОВОГО КЛЮЧА C RSA
         *=====================================================================
         */
        private void encodeKeyBtn_Click(object sender, EventArgs e)
        {
            try
            {
                UnicodeEncoding ByteConverter = new UnicodeEncoding();
                String data;
                byte[] dataToEncrypt;

                openFile(keyNameBox.Text, out data);
                dataToEncrypt = Encoding.Unicode.GetBytes(data);

                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    encryptedData = RSAEncrypt(dataToEncrypt, RSA.ExportParameters(false));
                    saveText(ByteConverter.GetString(encryptedData));

                    exportedParams = RSA.ExportParameters(true);

                    String mod = "";
                    foreach (byte b in exportedParams.Modulus)
                        mod += b.ToString();
                    nTextBox.Text = mod;

                    String d = "";
                    foreach (byte b in exportedParams.D)
                        d += b.ToString();
                    dTextBox.Text = d;

                    String exp = "";
                    foreach (byte b in exportedParams.Exponent)
                        exp += b.ToString();
                    eTextBox.Text = exp;

                    /*decryptedData = RSADecrypt(encryptedData, RSA.ExportParameters(true), false);*/
                }
                decodeKeyBtn.Enabled = true;
            }
            catch (ArgumentNullException ERROR)
            {
                MessageBox.Show(ERROR.Message, "Неудалось зашифровать текст.");
            }
        }



        /*=====================================================================
        *                  РАСШИФРОВАНИЕ СЕАНСОВОГО КЛЮЧА
        *======================================================================
        */
        private void decodeKeyBtn_Click(object sender, EventArgs e)
        {
            UnicodeEncoding ByteConverter = new UnicodeEncoding();
            String data;
            byte[] dataToDecrypt;

            openFile(keyNameBox.Text, out data);
            dataToDecrypt = Encoding.Unicode.GetBytes(data);
            decryptedData = rsaDecrypt(encryptedData, exportedParams);
            //decryptedData = rsaDecrypt(dataToDecrypt, exportedParams);
            saveText(ByteConverter.GetString(decryptedData));

            encodeBtn.Enabled = true;
        }




        /*=====================================================================
        *                      ШИФРОВАНИЕ ТЕКСТА
        *=====================================================================
        */
        private void encodeBtn_Click(object sender, EventArgs e)
        {
            String data;
            String encData;
            byte[] encrypted;
            openFile(fileNameBox.Text, out data);
            
            encryptTextToFile(data, fileNameBox.Text, tripleDES.Key, tripleDES.IV);
            textBox2.Text = data;
            decodeBtn.Enabled = true;

            openFile(fileNameBox.Text, out encData);
            encrypted = Encoding.Default.GetBytes(encData);
            signedHash = HashAndSign(encrypted);
        }




        /*=====================================================================
         *                          ДЕШИФРОВКА ТЕКСТА
         *=====================================================================
         */
        private void decodeBtn_Click(object sender, EventArgs e)
        {
            String encData;            
            SHA1Managed hash = new SHA1Managed();
            byte[] hashedData;
            byte[] encrypted;

            openFile(fileNameBox.Text, out encData);
            encrypted = Encoding.Default.GetBytes(encData);

            hashedData = hash.ComputeHash(encrypted);

            if (VerifyHash(hashedData, signedHash))
            {
                string data = decryptTextFromFile(fileNameBox.Text, tripleDES.Key, tripleDES.IV);
                saveText(data);
            }
            else MessageBox.Show("Invalid signature");
        }




        /*=====================================================================
         *                      ДОБАВЛЕНИЕ ПОДПИСИ
         *=====================================================================
         */
        public byte[] HashAndSign(byte[] encrypted)
        {
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                RSA.ImportParameters(exportedParams);

                SHA1Managed hash = new SHA1Managed();
                byte[] hashedData;

                hashedData = hash.ComputeHash(encrypted);
                return RSA.SignHash(hashedData, CryptoConfig.MapNameToOID("SHA1"));
            }
        }



        /*=====================================================================
         *                        ПРОВЕРКА ПОДПИСИ
         *=====================================================================
         */
        public bool VerifyHash(byte[] signedData, byte[] signature)
        {
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                SHA1Managed hash = new SHA1Managed();
                bool isVerified;
                byte[] hashedData;

                RSA.ImportParameters(exportedParams);
                isVerified = RSA.VerifyData(signedData, CryptoConfig.MapNameToOID("SHA1"), signature);
                hashedData = hash.ComputeHash(signedData);

                return RSA.VerifyHash(hashedData, CryptoConfig.MapNameToOID("SHA1"), signature);
            }
        }

        /*==============================================================================
         *===============================================================================
         * Далее идут вспомогательные методы
         *===============================================================================
         *==============================================================================
         */



        /*=====================================================================
         *                      ВЫБОР ФАЙЛА С ТЕКСТОМ
         *=====================================================================
         */
        private void chooseFileBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            if (DialogResult.OK == openDialog.ShowDialog())
            {
                try
                {
                    fileNameBox.Text = openDialog.FileName.ToString();
                    openTextFileBtn.Enabled = true;
                }
                catch (Exception ERROR)
                {
                    MessageBox.Show(ERROR.Message, "Не удалось открыть файл с текстом!");
                }
            }
            else
            {
                openTextFileBtn.Enabled = false;
            }
        }




        /*=====================================================================
         *                      ВЫБОР ФАЙЛА С КЛЮЧОМ
         *=====================================================================
         */
        private void chooseKeyBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            if (DialogResult.OK == openDialog.ShowDialog())
            {
                try
                {
                    keyNameBox.Text = openDialog.FileName.ToString();
                    openKeyFileBtn.Enabled = true;
                }
                catch (Exception ERROR)
                {
                    MessageBox.Show(ERROR.Message, "Не удалось открыть файл ключа!");
                }
            }
            else openKeyFileBtn.Enabled = false;
        }




        /*=====================================================================
         *                          ОТКРЫТИЕ ФАЙЛА КЛЮЧА
         *=====================================================================
         */
        private void openKeyBtn_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("notepad.exe", keyNameBox.Text);
        }




        /*=====================================================================
         *                       ОТКРЫТИЕ ФАЙЛА С ТЕКСТОМ
         *=====================================================================
         */
        private void openTextFileBtn_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(fileNameBox.Text);
        }





        /*=====================================================================
         *                      РЕАЛИЗАЦИЯ ШИФРОВКИ RSA
         *=====================================================================
         */
        static public byte[] RSAEncrypt(byte[] DataToEncrypt, RSAParameters RSAKeyInfo)
        {
            try
            {
                byte[] encryptedData;

                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKeyInfo);
                    encryptedData = RSA.Encrypt(DataToEncrypt, false);
                }
                return encryptedData;
            }

            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

        }



        /*=====================================================================
         *                      РЕАЛИЗАЦИЯ ДЕШИФРОВКИ RSA
         *=====================================================================
         */
        static public byte[] rsaDecrypt(byte[] dataToDecrypt, RSAParameters rsaKeyInfo)
        {
            try
            {
                byte[] decryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(rsaKeyInfo);
                    decryptedData = RSA.Decrypt(dataToDecrypt, false);
                }
                return decryptedData;
            }

            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }

        }




        /*=====================================================================
         *                          ОТКРЫТИЕ ФАЙЛА 
         *=====================================================================
         */
        private void openFile(string path, out string data)
        {
            StreamReader twitch = new StreamReader(path, Encoding.Default);
            data = "";
            string buf = "";
            while ((buf = twitch.ReadLine()) != null)
                data += buf;
            twitch.Close();
        }




        /*=====================================================================
         *                  СОХРАНЕНИЕ КЛЮЧА В ФАЙЛ
         *=====================================================================
         */
        private void saveText(String key)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            if (DialogResult.OK == saveDialog.ShowDialog())
            {
                StreamWriter own3dTV = new StreamWriter(saveDialog.FileName.ToString());
                try
                {
                    own3dTV.WriteLine(key);
                    own3dTV.Close();
                }
                catch (Exception ERROR)
                {
                    MessageBox.Show(ERROR.Message, "Критическая ошибка: ");
                }
            }
        }




        /*=====================================================================
         *                      ЗАПИСАТЬ ТЕКСТ В ФАЙЛ
         *=====================================================================
         */
        private void writeData(string path, String data)
        {
            FileStream fs = new FileStream(path, FileMode.Truncate);
            StreamWriter own3d = new StreamWriter(fs, Encoding.Unicode);
            own3d.Write(data);
            own3d.Close();
        }




        /*=====================================================================
         *                    ИЗМЕНЕНИЕ РАЗМЕРОВ ФОРМЫ
         *=====================================================================
         */
        private void button1_Click(object sender, EventArgs e)
        {
            Size sSize = new Size(419, 278);
            Size bSize = new Size(419, 456);
            if (isSmall == true)
            {
                this.Size = bSize;
                isSmall = false;
                expandBtn.Text = "↑";
            }
            else
            {
                this.Size = sSize;
                isSmall = true;
                expandBtn.Text = "↓";
            }
        }




        /*=======================================================================================
         *                     ЗАШИФРОВАТЬ ТЕКСТ И ЗАПИСАТЬ ЕГО В ФАЙЛ
         *=======================================================================================
         */
        public static void encryptTextToFile(String Data, String FileName, byte[] Key, byte[] IV)
        {
            try
            {
                FileStream fStream = File.Open(FileName, FileMode.OpenOrCreate);

                CryptoStream cStream = new CryptoStream(fStream,
                    new TripleDESCryptoServiceProvider().CreateEncryptor(Key, IV),
                    CryptoStreamMode.Write);

                StreamWriter sWriter = new StreamWriter(cStream);

                sWriter.WriteLine(Data);

                sWriter.Close();
                cStream.Close();
                fStream.Close();
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("A file access error occurred: {0}", e.Message);
            }

        }



        /*=====================================================================
         *                РАСШИФРОВКА ТЕКСТА И ЗАПИСЬ В ФАЙЛ
         *=====================================================================
         */
        public static string decryptTextFromFile(String FileName, byte[] Key, byte[] IV)
        {
            try
            {

                FileStream fStream = File.Open(FileName, FileMode.OpenOrCreate);

                CryptoStream cStream = new CryptoStream(fStream,
                    new TripleDESCryptoServiceProvider().CreateDecryptor(Key, IV),
                    CryptoStreamMode.Read);

                StreamReader sReader = new StreamReader(cStream);

                string val = sReader.ReadLine();

                sReader.Close();
                cStream.Close();
                fStream.Close();

                return val;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
                return null;
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("A file access error occurred: {0}", e.Message);
                return null;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Size sSize = new Size(419, 278);
            this.Size = sSize;
        }



    }
}