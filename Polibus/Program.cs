using System;
using System.Linq;

namespace Polibus
{

    class Program
    {
        struct Cordinate
        {
            public bool Found { get; set; }
            public int currentI { get; set; }
            public int currentJ { get; set; }

        }
        public static string Normalize(string key)
        {
            string[] keyArray = key.ToLower()
                .Replace(" ", string.Empty)
                .Replace("j", "i")
                .ToCharArray().Distinct()
                .Select(c => c.ToString())
                .ToArray();
            return string.Join("", keyArray);
        }
        public static char[,] CreateMatrixWithKey(string key)
        {
            char[,] keyMatrix = new char[5, 5];
            char[] keyArray = key.ToCharArray();
            char[] alpha = "ABCDEFGHIKLMNOPQRSTUVWXYZ".ToLower().ToCharArray();
            int indexLeter;
            int idx = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    indexLeter = Array.IndexOf(alpha, key[idx]);
                    keyMatrix[i, j] = indexLeter > -1 ? alpha[indexLeter] : alpha.First();
                    alpha = indexLeter > -1 ? alpha.Where(c => c != alpha[indexLeter]).ToArray() : alpha.Where(c => c != alpha.First()).ToArray();
                    idx = idx < keyArray.Length - 1 ? ++idx : idx;
                }
            }
            return keyMatrix;
        }
        static Cordinate FindSymbolInMatrix(char a, char[,] keyMatrix)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (a == keyMatrix[i, j])
                    {
                        return new Cordinate()
                        {
                            Found = true,
                            currentI = i,
                            currentJ = j
                        };
                    }
                }
            }
            return new Cordinate() { Found = false };
        }
        static int[] CreateIndexArray(string text, char[,] keyMatrix)
        {
            int[] indexArray = new int[text.Length * 2];

            for (int idx = 0; idx < text.Length; idx++)
            {
                if (FindSymbolInMatrix(text[idx], keyMatrix).Found)
                {
                    indexArray[idx] = FindSymbolInMatrix(text[idx], keyMatrix).currentI;
                    indexArray[idx + text.Length] = FindSymbolInMatrix(text[idx], keyMatrix).currentJ;
                }
            }

            return indexArray;

        }
        static void printIndex(int[] indexArray)
        {
            for (int i = 0; i < indexArray.Length; i++)
            {
                Console.Write(indexArray[i] + "  ");
                if (i ==indexArray.Length/2-1)
                {
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
        }
        static string getEncrypedText(int[] indexArray, char[,] keyMatrix)
        {
            string returnString = "";
            for (int i = 0; i < indexArray.Length; i = i + 2)
            {
                returnString += keyMatrix[indexArray[i], indexArray[i + 1]].ToString();
            }
          
            return returnString;
        }
        static string getDecodeText(string encryptText,char[,] keyMatrix)
        {
            string returnString = "";
            int idx = 0;
            int[] indexArray = new int[encryptText.Length * 2];

            for (int i = 0; i < encryptText.Length; i++)
            {
                if (FindSymbolInMatrix(encryptText[i], keyMatrix).Found)
                {
                    indexArray[idx] = FindSymbolInMatrix(encryptText[i], keyMatrix).currentI;
                    indexArray[idx + 1] = FindSymbolInMatrix(encryptText[i], keyMatrix).currentJ;
                    idx +=2;
                }
            }
            for (int i = 0; i < indexArray.Length/2; i++)
            {
                returnString += keyMatrix[indexArray[i], indexArray[i + encryptText.Length]].ToString();
            }
            return returnString;
        }
        static void Main(string[] args)
        {
            string key, text;
            key = Normalize("You are right");
            text = "Get your pass".Replace(" ", string.Empty).ToLower();
            var keyArray = CreateMatrixWithKey(key);
            var indexArray = CreateIndexArray(text, keyArray);
            var encyptText = getEncrypedText(indexArray, keyArray);
            Console.WriteLine(encyptText);
            Console.WriteLine(getDecodeText(getEncrypedText(indexArray,keyArray),keyArray));
            //Console.WriteLine("Write your text");
            //text = Console.ReadLine().Replace(" ", string.Empty).ToLower();
            //Console.WriteLine("Write your key");
            //key = Normalize(Console.ReadLine());
            //var keyArray = CreateMatrixWithKey(key);
            //var indexArray = CreateIndexArray(text, keyArray); 
            //Console.WriteLine(getEncrypedText(indexArray, keyArray));
        }
    }
}
