using System;
using System.Windows;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;

namespace ManipulacaoArquivoTxt
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //String a ser gravada no arquivo
            string password = "orfeus";
            string email = "patrick@gmail.com";
            string send = email + ";" + password;

            //Get do caminho de uma pasta C:\Users\patri
            //O SpecialFolder especifica o caminho para uma pasta
            //Consulte as opções do SpecialFolder em: https://docs.microsoft.com/pt-br/dotnet/api/system.environment.specialfolder?view=net-5.0
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            //Complemento do caminho (pasta)
            //Pode-se criar pastas ao adicionar 
            string finalPath = docPath + @"\NomePasta\NomeSubpasta";

            //Cria o arquivo se ele não existir
            StreamWriter sw;
            if (!File.Exists(finalPath))
            {
                System.IO.Directory.CreateDirectory(finalPath);
            }

            //Adicionando o nome do arquivo com extensão .txt
            finalPath = docPath + @"\NomePasta\NomeSubpasta\NomeArquivo.txt";

            //Escreve no arquivo pulando linha
            //Não grava-se o \n no arquivo
            //Using isola uma declaração que nao funcionara fora dele
            using (sw = File.AppendText(finalPath))
            {
                sw.WriteLine(send);
                sw.Close();
            }

            //Leitura dos dados
            using (StreamReader sr = File.OpenText(finalPath))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    string[] dadosDoCadastro = s.Split(';');
                    Console.WriteLine("EMAIL: " + dadosDoCadastro[0]);
                    Console.WriteLine("SENHA: " + dadosDoCadastro[1] + "\n");
                }
            }
        }
	}
}

