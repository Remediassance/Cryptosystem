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
        public static byte[] alicePublicKey;
        public RSACryptoServiceProvider RSA;
        byte[] dataToEncrypt;
        byte[] encryptedData;
        byte[] decryptedData;

        private void Form1_Load(object sender, EventArgs e)
        {
           RSA = new RSACryptoServiceProvider(); 
            //myRSA.eratosphen(ref myRSA.P, 10000000);
        }




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
                    createKeyBtn.Enabled = true;
                }
                catch (Exception ERROR)
                {
                    MessageBox.Show(ERROR.Message, "Error opening initial file");
                }
            }
            else
            {
                openTextFileBtn.Enabled = false;
                createKeyBtn.Enabled = false;
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
                    MessageBox.Show(ERROR.Message, "Error opening key file");
                }
            }
            else openKeyFileBtn.Enabled = false;
        }




        /*=====================================================================
         * Метод определения полного имени файла
         *=====================================================================
         */
        /*private void fileNameBox_TextChanged(object sender, EventArgs e)
        {
            if (fileNameBox.Text != "" && keyNameBox.Text != ""
                && fileNameBox.Text != keyNameBox.Text)
                encodeBtn.Enabled = true;
            else encodeBtn.Enabled = false;

            if (fileNameBox.Text != "")
                openTextFileBtn.Enabled = true;
            else openTextFileBtn.Enabled = false;

            if (keyNameBox.Text != "")
                openKeyFileBtn.Enabled = true;
            else openKeyFileBtn.Enabled = false;
        }*/




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
         *                     1) ШИФРОВАНИЕ КЛЮЧА C RSA
         *=====================================================================
         */
        private void createKeyBtn_Click(object sender, EventArgs e)
        {
            try
            {
                //Create a UnicodeEncoder to convert between byte array and string.
                UnicodeEncoding ByteConverter = new UnicodeEncoding();
                String data;
                openFile(keyNameBox.Text, out data);

                dataToEncrypt = ByteConverter.GetBytes(data);

                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(); 

                encryptedData = RSAEncrypt(dataToEncrypt, RSA.ExportParameters(false), false);

                saveSeanseKey(ByteConverter.GetString(encryptedData));

                    /*decryptedData = 
                        RSADecrypt(encryptedData, RSA.ExportParameters(true), false);*/

            }
            catch (ArgumentNullException)
            {
                //Catch this exception in case the encryption did
                //not succeed.
                Console.WriteLine("Encryption failed.");

            }
        }


        static public byte[] RSAEncrypt(byte[] DataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] encryptedData;
                //Create a new instance of RSACryptoServiceProvider.
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {

                    //Import the RSA Key information. This only needs
                    //toinclude the public key information.
                    RSA.ImportParameters(RSAKeyInfo);

                    //Encrypt the passed byte array and specify OAEP padding.  
                    //OAEP padding is only available on Microsoft Windows XP or
                    //later.  
                    encryptedData = RSA.Encrypt(DataToEncrypt, DoOAEPPadding);
                }
                return encryptedData;
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }

        }

        static public byte[] RSADecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] decryptedData;
                //Create a new instance of RSACryptoServiceProvider.
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    //Import the RSA Key information. This needs
                    //to include the private key information.
                    RSA.ImportParameters(RSAKeyInfo);

                    //Decrypt the passed byte array and specify OAEP padding.  
                    //OAEP padding is only available on Microsoft Windows XP or
                    //later.  
                    decryptedData = RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
                }
                return decryptedData;
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }

        }




        /*=====================================================================
         *            2) ГЕНЕРАЦИЯ И СОХРАНЕНИЕ СЕАНСОВОГО КЛЮЧА
         *=====================================================================
         */
        private void createSeanseBtn_Click(object sender, EventArgs e)
        {
            /*RijndaelManaged RM = new RijndaelManaged();
            RM.GenerateIV();
            RM.GenerateKey();
            saveSeanseKey(RM.Key.ToString());*/
            ECDiffieHellmanCng alice = new ECDiffieHellmanCng();
            alicePublicKey = alice.PublicKey.ToByteArray();
            saveSeanseKey(alicePublicKey.ToString());
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
         *                      ЗАПИСАТЬ ТЕКСТ В ФАЙЛ
         *=====================================================================
         */
        private void writeData(string path, int[] data)
        {
            string buf = "";
            FileStream fs = new FileStream(path, FileMode.Truncate);
            StreamWriter own3d = new StreamWriter(fs, Encoding.Unicode);
            for (int i = 0; i < data.Length; i++)
            {
                buf += (char)data[i];
            }
            own3d.Write(buf);
            own3d.Close();
        }


        /*=====================================================================
         *                  СОХРАНЕНИЕ КЛЮЧА В ФАЙЛ
         *=====================================================================
         */
        private void saveSeanseKey(String key)
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
                    MessageBox.Show(ERROR.Message, "FATAL ERROR");
                }
            }
        }

        /*=====================================================================
         *                    ИЗМЕНЕНИЕ РАЗМЕРОВ ФОРМЫ
         *=====================================================================
         */
        private void button1_Click(object sender, EventArgs e)
        {
            Size sSize = new Size(419, 278);
            Size bSize = new Size(419, 436);
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

        private void encodeSeanseBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
