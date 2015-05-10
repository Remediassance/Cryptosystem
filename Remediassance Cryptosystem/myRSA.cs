using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Remediassance_Cryptosystem
{
    class myRSA
    {

        private const int nBits = 256;
        private BigInteger[] C;
        private BigInteger[] M;
        public static BigInteger[] P = null;
        private BigInteger secKey;


        // Этап 1, генерация переменных q,n,e,eiler etc.
        public void generateVariables(out BigInteger[] vars)
        {

            //eratosphen(ref P, 10000000);

            BigInteger p;
            BigInteger q;

            BigInteger e;
            BigInteger d;

            BigInteger n;
            BigInteger eiler;

            BigInteger t = 0;
            BigInteger x = 0;
            BigInteger y = 0;

            BigInteger[] textIntArray;

            // Шаг 1. Генерация чисел p и q            
            p = generatePrime(nBits);
            do
            {
                q = generatePrime(nBits);
            } while (q.Equals(p) || q.ToString().Length != p.ToString().Length);

            // Шаг 2. Определяем криптомодуль n
            n = p*q;


            // Шаг 3. Нахождение значения функции Эйлера
            eiler = (p - 1) * (q - 1);

            // Шаг 4. Подбираем число е
            e = 5;
            do
            {
                e += 2;
                extendedEuclid(out t, out x, out y, e, eiler);
            } while (t > 1);


            //Шаг 5. Генерируем число d
            if (x > 0)
                d = BigInteger.Remainder(x, eiler);
            else
            {
                BigInteger a = BigInteger.Abs(x);
                BigInteger b = BigInteger.Abs(eiler);
                BigInteger c = (a / b) * (-1) - 1;
                d = x - eiler * c;
            }   

            //textIntArray = new BigInteger[text.Length];

            vars = new BigInteger[6];
            vars[0] = p;
            vars[1] = q;
            vars[2] = d;
            vars[3] = e;
            vars[4] = n;
            vars[5] = eiler;
        }



        /**====================================================================
         * Функция генерации большого простого числа
         * @param N - верхняя граница для генерируемого числа
         *=====================================================================
         */
        private BigInteger generatePrime(int nBits)
        {
            Random rand = new Random();
            BigInteger n;

            do
            {
                byte[] bytes = new byte[nBits / 8];
                rand.NextBytes(bytes);
                n = new BigInteger(bytes);
                n = BigInteger.Abs(n);
                n = 2 * n - 1;
            } while (!isPrime(n));
            return n;
        }



        /**====================================================================
         *  Функция проверки числа на простоту
         *  @param n - число для проверки
         *=====================================================================
         */
        private bool isPrime(BigInteger n)
        {
            Random rand = new Random();
            for (long j = 1; j < P.LongLength; j++)
            {
                if (BigInteger.Remainder(n, P[j]) == 0)
                {
                    if (n == P[j])
                        return true;
                    else
                        return false;
                }
            }
          
            if (rabinMiller(n, 100))
                return true;
            else return false;
            

        }


        /**====================================================================
         * Функция генерации массива простых чисел
         * @param P - массив, в который запишутся простые числа
         * @param n - число, до которого следует искать простые числа
         *=====================================================================
         */
        public static void eratosphen(ref BigInteger[] P, BigInteger n)
        {
            BigInteger[] A = new BigInteger[(long)n];
            BigInteger j = 2;
            BigInteger i;

            for (j = 2; j < n; j++)
                A[(long)j] = 1;

            j = 2;

            while (BigInteger.Pow(j, 2) <= n)
            {
                if (A[(long)j] == 1)
                {
                    i = (long)(BigInteger.Pow(j, 2));
                    while (i < n)
                    {
                        A[(long)i] = 0;
                        i += j;
                    }
                }
                j += 1;
            }


            int m = 0;
            for (j = 2; j < n; j++)
                if (A[(long)j] == 1)
                    m += 1;


            P = new BigInteger[m];
            m = 0;
            for (j = 0; j < n; j++)
            {
                if (A[(long)j] == 1)
                {
                    m += 1;
                    P[m - 1] = j;
                }
            }
        }



        /**====================================================================
         * Функция, выполняющая тест Рабина-Миллера
         * @param n - число для проверки
         * @param r - число раундов проверки
         *=====================================================================
         */
        private bool rabinMiller(BigInteger n, int r)
        {
            BigInteger s;
            BigInteger d;
            Random rand;
            int length;
            BigInteger x;
            BigInteger a;

            s = 0;
            d = n - 1;
            rand = new Random();

            
            while (d%2 == 0)
            {
                d /= 2;
                s++;
            }
            
 
            for (int i = 0; i < r; i++)
            {
                length = rand.Next(2, n.ToString().Length-1); //=======================
                byte[] bytes = new byte[length];
                rand.NextBytes(bytes);
                a = new BigInteger(bytes);
                a = BigInteger.Abs(a);

                x = BigInteger.ModPow(a, d, n);
                if (x == 1 || x == n - 1)
                    continue;
                for (int j = 0; j < s - 1; j++)
                {
                    x = (x*x)%n;
                    if (x == 1)
                        return false;
                    if (x == n - 1)
                        break;
                }
                if (x != n-1)
                    return false;
            }
            return true;
        }     





        /**====================================================================
         * Функция нахождения Наибольшего Общего Делителя 2 чисел
         * @param a - первое число
         * @param b -  второе число
         *=====================================================================
         */
        private BigInteger euclid(BigInteger a, BigInteger b)
        {
            while (b > 0)
            {
                BigInteger t = b;
                b = BigInteger.Remainder(a, b);
                a = t;
            }
            return a;
        }


        /**====================================================================
         * Реализация расширенного алгоритма Евклида. Получаем не только НОД 
         * пары чисел, но и коэффиценты x и y, для которых d = ax + by
         * @param nod - НОД чисел a и b
         * @param x   - коэффицент х
         * @param y   - коэффицент у
         * @param a   - Первое число для нахождения НОД
         * @param b   - второе число
         *=====================================================================
         */
        private void extendedEuclid(out BigInteger nod, out BigInteger x,
            out BigInteger y, BigInteger a, BigInteger b)
        {
            BigInteger x2;
            BigInteger y2;
            BigInteger q;
            BigInteger t;

            x = 1;
            y = 0;
            x2 = 0;
            y2 = 1;
            nod = 1;

            while (b > 0)
            {
                q = a / b;

                t = b;
                b = BigInteger.Remainder(a, b);
                a = t;
                nod = a;

                t = x2;
                x2 = x - q * x2;
                x = t;

                t = y2;
                y2 = y - q * y2;
                y = t;
            }
        }


        /**====================================================================
         * Функция, реализующая метод повторного возведения в квадрат
         * @param a - число для возведения в степень
         * @param b - порядок степени
         * @param n - делитель
         * @return  - остаток от деления
         *=====================================================================
         */
        private BigInteger modExp(BigInteger a, BigInteger b, BigInteger n)
        {
            BigInteger x;

            if (b == 0)
                return 1;

            if (BigInteger.Remainder(b, 2) == 0)
            {
                x = modExp(a, b / 2, n);
                return BigInteger.ModPow(x, 2, n);
            }

            x = modExp(a, (b - 1) / 2, n);
            x = BigInteger.ModPow(x, 2, n);

            return BigInteger.Remainder(a * x, n);
        }



        /**====================================================================
         * Функция шифровки текста
         *=====================================================================
         */
        public String encodeRSA(String textMessage, BigInteger[] vars)
        {
            BigInteger e;
            BigInteger n;
            BigInteger d;

            String text;
            String encryptedText = "";

            e = vars[3];
            n = vars[4];
            d = vars[2];

            text = textMessage;
            M = new BigInteger[text.Length];
            C = new BigInteger[text.Length];

            for (int i = 0; i < textMessage.Length; i++)
                M[i] = text[i];


            for (int i = 0; i < M.Length; i++) {
                C[i] = modExp(M[i], e, n);
                encryptedText += Convert.ToString(C[i].ToByteArray());
                encryptedText += "|";
            }
            return encryptedText;
        }



        /**====================================================================
         * Перевод символов текста в числовое представление
         * @param source - строка с текстом
         * @param array  - массив для заполнения
         *=====================================================================
         */
        private void text2Int(string source, BigInteger[] array)
        {
            for (int i = 0; i < source.Length; i++)
                array[i] = source[i];
        }



        /**====================================================================
         * Перевод чисел в их символьное представление
         * @param array  - массив с числами
         * @param source - строка для составления
         *=====================================================================
         */
        private void int2Text(BigInteger[] array, string source)
        {
            source = "";
            for (int i = 0; i < array.Length; i++)
                source += (char)array[i];
        }



        /**====================================================================
         * Функция дешифровки текста
         *=====================================================================
         */
        public String decodeRSA(String encodedText, BigInteger[] vars) 
        {
            BigInteger d;
            BigInteger p;
            BigInteger q;
            BigInteger n;

            BigInteger d1;
            BigInteger d2;

            BigInteger t;
            BigInteger x;
            BigInteger y;

            BigInteger e;

            BigInteger r;

            String text;

            BigInteger M1;
            BigInteger M2;

            d = vars[2];
            p = vars[0];
            q = vars[1];
            n = vars[4];
            e = vars[3];

            text = "";

            String[] textCollection;
            textCollection = encodedText.Split('|');

            for (int i = 0; i < textCollection.Length; i++)
                C[i] = BigInteger.Parse(textCollection[i]);

            //if (checkSign(secKey, aliceText.Text, e, n))
            //{
                d1 = BigInteger.Remainder(d, p - 1);
                d2 = BigInteger.Remainder(d, q - 1);

                for (int i = 0; i < M.Length; i++)
                {
                    M1 = modExp(C[i], d1, p);
                    M2 = modExp(C[i], d2, q);
                    extendedEuclid(out t, out x, out y, q, p);

                    if (x > 0)
                        r = BigInteger.Remainder(x, p);
                    else
                    {
                        BigInteger a = BigInteger.Abs(x);
                        BigInteger b = BigInteger.Abs(p);
                        BigInteger c = (a / b) * (-1) - 1;
                        r = x - p * c;
                    }

                    M[i] = BigInteger.Remainder((M1 - M2) * r, p) * q + M2;
            
                }

                for (int i = 0; i < M.Length; i++)
                    text += (char)M[i];
                return text;
                
            //}
        }




        /*=====================================================================
         * Функция создания цифрового ключа
         * @param text - шифруемый текст
         * @param d - часть секретного ключа
         * @param n - модуль ключа
         *=====================================================================
         */
        private BigInteger signMsg(String text, BigInteger d, BigInteger n)
        {
            BigInteger signature;

            int hash = text.GetHashCode();
            signature = BigInteger.ModPow(hash, d, n);

            return signature;
        }



        /*=====================================================================
         * Функция проверки цифрового ключа
         * @param key - цифровой ключ
         * @param d - часть секретного ключа
         * @param n - модуль ключа
         *=====================================================================
         */
        private bool checkSign(BigInteger sign, String text ,BigInteger e, BigInteger n)
        {
            int hash = text.GetHashCode();
            if (hash == BigInteger.ModPow(sign, e, n))
                return true;
            else
            {
                //MessageBox.Show("Цифровая подпись некорректна!");
                return false;
            }
        }




        /*private void Form1_Load(object sender, EventArgs e)
        {
            eratosphen(ref P, 10000000);
        }*/


    }
}
