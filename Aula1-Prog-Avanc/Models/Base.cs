using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula1_Prog_Avanc.Models
{
    /// <summary>
    /// Métodos comuns para manipulação de arquivos
    /// </summary>
    internal class Base
    {
        //database/arquivocsv
        public static void CreateFolderAndFile(string path)
        {
            string folder = path.Split("/")[0];
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
        }

        public List<string> ReadAllLinesCSV(string path)
        {
            List<string> lines = new List<string>();

            using (StreamReader file = new(path))
            {
                string line;
                while( (line = file.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            return lines;
        }

        public void RewriteCSV(string path, List<string> lines)
        {
            using ( StreamWriter output = new(path))
            {
                foreach (var line in lines)
                {
                    output.Write(line + "\n");
                }
            }
        }
    }
}
