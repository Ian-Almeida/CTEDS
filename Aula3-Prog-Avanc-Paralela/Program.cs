using Aula3_Prog_Avanc_Paralela.Repositories;
using Aula3_Prog_Avanc_Paralela.Models;
namespace Aula3_Prog_Avanc_Paralela
{
    internal class Program
    {
        private const string path = "database/log.txt";
        static void Main(string[] args)
        {
            ProductRepository _product = new ProductRepository();

            _product.PopulateDatabase();

            FileStream logStream = File.OpenWrite(path);

            LogRepository _log = new(logStream);

            User user = new()
            {
                IdUser = Guid.NewGuid(),
                Name = "Joãozinho",
                JobTitle = "Desenvolvedor"
            };

            string option;

            do
            {
                Console.WriteLine("\n Escolha uma das opções abaixo:\n");
                Console.WriteLine("1 - Listar produtos");
                Console.WriteLine("2 - Cadastrar produto");
                Console.WriteLine("3 - Editar produto");
                Console.WriteLine("4 - Excluir produto");
                Console.WriteLine("0 - Sair da aplicação\n");

                option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        if (_product.ReadAll().Count() == 0)
                        {
                            Console.WriteLine("\nNão há nenhum produto cadastrado!\n");
                        }
                        else
                        {
                            //var productList = _product.ReadAll();
                            //Console.WriteLine("\nLista de produtos:");

                            //foreach (var item in productList)
                            //{
                            //    Console.WriteLine($"{item.IdProduct} - {item.Name} - {item.Description} - R${item.Price}");
                            //}

                            var productList = _product.ReadAll();

                            Parallel.ForEach(productList, x =>
                            {
                                Console.WriteLine($"\n O usuário: {user.Name} ({user.JobTitle}) está exibindo o produto: {x.Name} \nutilizando a Thread: {Thread.CurrentThread.ManagedThreadId} às {DateTime.Now}");
                                _log.RegisterAccess(user);
                            });
                        }
                        break;
                    //case "2":
                    //    Console.WriteLine("\nDigite o id do produto:\n");
                    //    var id = Console.ReadLine();
                    //    Console.WriteLine("\nDigite o nome do produto:\n");
                    //    var name = Console.ReadLine();
                    //    Console.WriteLine("\nDigite a descrição do produto:\n");
                    //    var description = Console.ReadLine();
                    //    Console.WriteLine("\nDigite o preço do produto:\n");
                    //    var price = Console.ReadLine();

                    //    Product newProduct = new()
                    //    {
                    //        IdProduct = id,
                    //        Name = name,
                    //        Description = description,
                    //        Price = Convert.ToDecimal(price),
                    //    };

                    //    _product.Create(newProduct);
                    //    break;
                    //case "3":
                    //    Console.WriteLine("\nDigite o id do produto que será editado:\n");
                    //    id = Console.ReadLine();

                    //    var readProduct = _product.FindById(id);

                    //    Console.WriteLine($"\nDeseja alterar o nome? ({readProduct.Name}):\n");
                    //    name = Console.ReadLine();
                    //    if (name != "") readProduct.Name = name;

                    //    Console.WriteLine($"\nDeseja alterar o a descrição? ({readProduct.Description}):\n");
                    //    description = Console.ReadLine();
                    //    if (description != "") readProduct.Description = description;

                    //    Console.WriteLine($"\nDeseja alterar o preço? R$({readProduct.Price}):\n");
                    //    price = Console.ReadLine();
                    //    if (price != "") readProduct.Price = Convert.ToDecimal(price);

                    //    _product.Update(readProduct);
                    //    Console.WriteLine($"\nAlterações feitas com sucesso\n");
                    //    break;
                    //case "4":
                    //    Console.WriteLine("\nDigite o id do produto que será editado:\n");
                    //    id = Console.ReadLine();
                    //    _product.Delete(id);
                    //    break;
                    case "0":
                        logStream.Close();
                        break;
                    default:
                        break;
                }

            } while (option != "0");
        }
    }
}