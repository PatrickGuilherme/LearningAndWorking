using System;
using System.Security.Cryptography;
using System.Text;

namespace CriptografiaHash
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Cria a classe para a criptografia
            Cryptography cryptography = new Cryptography(SHA512.Create());

            //Converter string em hash 
            string resultMethod = cryptography.HashGenerate("string a ser convertida em hash");
            Console.WriteLine("[1] String convertidida: " + resultMethod);

            //Verifica se as strings passadas são iguais
            bool stringEquals = cryptography.HashVerify("string 2", "string 1");
            string msg;
            if (stringEquals) msg = "[2] São iguais"; else msg = "[2] Não são iguais";
            Console.WriteLine(msg);
        }
    }

    public class Cryptography
    {
        /// <summary>
        /// Atributo que irá receber um tipo de algoritmo de criptografia(hash) | Algoritmos: SHA512, MD5, RIPDEM160
        /// </summary>
        public HashAlgorithm Algorithm { get; set; }

        /// <summary>
        /// Construtor que recebe um algoritmo de criptografia(hash)
        /// Tipos de parametro: SHA512.Create(), MD5.Create(), RIPEMD160.Create()  
        /// </summary>
        public Cryptography(HashAlgorithm algorithm)
        {
            Algorithm = algorithm;
        }

        /// <summary>
        /// Metodo para converter uma string passada em hash
        /// </summary>
        /// <returns>string já convertida em hash</returns>
        public string HashGenerate(string stringToBeEncrypted)
        {
            //Encoding.UTF8: Obtém uma codificação para o formato UTF-8
            //GetBytes(stringToBeEncrypted): decodifica um conjunto de caracteres(passado por parametro) em um vetor de bytes
            var encodedValue = Encoding.UTF8.GetBytes(stringToBeEncrypted);

            //Calcula o valor do hash de um vetor de bytes especificada.
            var passwordEncrypted = Algorithm.ComputeHash(encodedValue);

            //StringBuilder: Modifica uma cadeia de caracteres sem criar um novo objeto, funciona como uma string mais leve em relação a looping de modificações
            StringBuilder sb = new StringBuilder();

            //Looping de concatenação de StringBuilder
            foreach (var caractere in passwordEncrypted)
            {
                //Acrescenta informações ao final do stringBuilder
                //O parametro passado é um byte convertido para string em um formato 
                //ToString("X2"): Formata a string como dois caracteres hexadecimais maiúsculos
                sb.Append(caractere.ToString("X2"));
            }

            //Retorna o hash convertendo de StringBuilder para String  
            return sb.ToString();
        }


        /// <summary>
        /// Verifica se as duas string são iguais em hash
        /// </summary>
        /// <param name="stringInput"> String que é inserida pelo usuario</param>
        /// <param name="stringstored"> String que vem de um banco de dados ou arquivo</param>
        /// <returns>True: Se forem iguas as strings | False: Se forem diferentes as string </returns>
        public bool HashVerify(string stringInput, string stringstored)
        {
            //Verifica se a string [stringstored] está vazia ou nula
            if (string.IsNullOrEmpty(stringstored))
            {
                //Exibe uma exception
                throw new NullReferenceException("A string [stringstored] esta nula ou vazia.");
            }

            //Calcula o valor do hash de um vetor de bytes especificada.
            var passwordEncrypted = Algorithm.ComputeHash(Encoding.UTF8.GetBytes(stringInput));

            //StringBuilder: Modifica uma cadeia de caracteres sem criar um novo objeto, funciona como uma string mais leve em relação a looping de modificações
            StringBuilder sb = new StringBuilder();

            //Looping de concatenação de StringBuilder
            foreach (var caractere in passwordEncrypted)
            {
                //Acrescenta informações ao final do stringBuilder
                //O parametro passado é um byte convertido para string em um formato 
                //ToString("X2"): Formata a string como dois caracteres hexadecimais maiúsculos
                sb.Append(caractere.ToString("X2"));
            }

            //Se ambas as string forem iguais retorna [sb.ToString()]
            return sb.ToString() == stringstored;
        }
    }
}
