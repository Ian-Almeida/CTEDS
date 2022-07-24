using AvaliacaoD3.Interfaces;
using AvaliacaoD3.Models;
using System.Text;

namespace AvaliacaoD3.Repositories
{
    internal class LogRepository :  ILog
    {
        private string logPath = String.Empty;
        private readonly FileStream logStream;
        public LogRepository(FileStream logStream, string path)
        {
            this.logStream = logStream;
            logPath = path;
        }

        private static string PrepareLineLogin(User user)
        {
            return $"\n[LOGIN] O usuário: {user.Nome} ({user.Id}) efetuou o login no sistema. {DateTime.Now}\n";
        }
        private static string PrepareLineLogout(User user)
        {
            return $"\n[LOGOUT] O usuário: {user.Nome} ({user.Id}) deslogou do sistema. {DateTime.Now}\n";
        }
        private static string PrepareLineExitedSystem(User user)
        {
            return $"\n[EXITED] O usuário: {user.Nome} ({user.Id}) encerrou o sistema. {DateTime.Now}\n";
        }

        public static void CreateFolderAndFile(string logPath)
        {
            string folder = logPath.Split("/")[0];

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            if (!File.Exists(logPath))
            {
                File.Create(logPath).Close();
            }
        }

        public void RegisterLogin(User user)
        {
            string line = PrepareLineLogin(user);

            byte[] info = new UTF8Encoding(true).GetBytes(line);
            logStream.Write(info);
        }
        public void RegisterLogout(User user)
        {
            string line = PrepareLineLogout(user);

            byte[] info = new UTF8Encoding(true).GetBytes(line);
            logStream.Write(info);
        }
        public void RegisterExitedSystem(User user)
        {
            string line = PrepareLineExitedSystem(user);

            byte[] info = new UTF8Encoding(true).GetBytes(line);
            logStream.Write(info);
        }
    }
}
