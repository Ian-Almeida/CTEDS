using Aula4_API.Interfaces;
using Aula4_API.Models;
using System.Text;

namespace Aula4_API.Repositories
{
    internal class LogRepository : BaseRepository<Log>, ILog
    {
        private const string logPath = "database/log.txt";
        private readonly FileStream logStream;
        public LogRepository(FileStream logStream) : base("logs", "IdLog")
        {
            this.logStream = logStream;
            CreateFolderAndFile(logPath);
        }

        private static string PrepareLine(User user)
        {
            return $"\nO usuário: {user.Name} ({user.JobTitle}) está acessando dados do banco. {DateTime.Now}\n";
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
                File.Create(logPath);
            }
        }

        public void RegisterAccess(User user)
        {
            string line = PrepareLine(user);

            byte[] info = new UTF8Encoding(true).GetBytes(line);
            logStream.Write(info);
        }
    }
}
