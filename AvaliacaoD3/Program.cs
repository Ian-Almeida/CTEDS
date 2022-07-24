using AvaliacaoD3.Repositories;
using AvaliacaoD3.Models;
namespace AvaliacaoD3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UserRepository _user = new();
            LogRepository.CreateFolderAndFile(_user.loginLogPath);
            
            FileStream loginLogStream = File.OpenWrite(_user.loginLogPath);
            LogRepository _loginLog = new(loginLogStream, _user.loginLogPath);

            string? firstMenuOption;
            do
            {
                Console.WriteLine("1 - Acessar");
                Console.WriteLine("2 - Cadastrar usuário");
                Console.WriteLine("0 - Cancelar");
                firstMenuOption = Console.ReadLine();

                switch (firstMenuOption)
                {
                    case "1":
                        Console.Clear();

                        Console.WriteLine("Email: ");
                        string? loginEmail = Console.ReadLine();
                        Console.WriteLine("Senha: ");
                        string? loginPassword = Console.ReadLine();

                        var loggedUser = _user.Login(loginEmail, loginPassword);

                        if (loggedUser == null)
                        {
                            Console.WriteLine("Login inválido!");
                            continue;
                        }
                        else
                        {
                            _loginLog.RegisterLogin(loggedUser);
                            Console.WriteLine("Login efetuado com sucesso!");
                        }

                        Console.WriteLine("1 - Deslogar");
                        Console.WriteLine("0 - Encerrar sistema");

                        string? loggedInOptions = Console.ReadLine();

                        if (loggedInOptions == "1")
                        {
                            _loginLog.RegisterLogout(loggedUser);
                            Console.WriteLine("Usuário deslogado!");
                        }
                        else if (loggedInOptions == "0")
                        {
                            _loginLog.RegisterExitedSystem(loggedUser);
                            loginLogStream.Close();
                            Environment.Exit(0);
                            break;
                        }
                        break;
                    case "2":
                        Console.WriteLine("Nome: ");
                        string nome = Console.ReadLine();
                        Console.WriteLine("Email: ");
                        string email = Console.ReadLine();
                        Console.WriteLine("Senha: ");
                        string senha = Console.ReadLine();

                        var newUser = new User() {
                            Id = Guid.NewGuid(),
                            Nome = nome,
                            Email = email,
                            Senha  = senha
                        };
                        _user.Create(newUser);
                        Console.WriteLine("Usuário criado com sucesso");
                        break;
                    default:
                        loginLogStream.Close();
                        break;
                }
            } while (firstMenuOption != "0");
        }
    }
}