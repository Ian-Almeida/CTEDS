using Aula1_Prog_Avanc.Models;

namespace Aula1_Prog_Avanc
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Product product = new Product();
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
                        if (product.ReadAll().Count() == 0)
                        {
                            Console.WriteLine("\nNão há nenhum produto cadastrado!\n");
                        }
                        else
                        {
                            var productList = product.ReadAll();
                            Console.WriteLine("\nLista de produtos:");

                            foreach (var item in productList)
                            {
                                Console.WriteLine($"{item.IdProduct} - {item.Name} - {item.Description} - R${item.Price}");
                            }
                        }
                        break;
                    case "2":
                        Console.WriteLine("\nDigite o id do produto:\n");
                        var id = Console.ReadLine();
                        Console.WriteLine("\nDigite o nome do produto:\n");
                        var name = Console.ReadLine();
                        Console.WriteLine("\nDigite a descrição do produto:\n");
                        var description = Console.ReadLine();
                        Console.WriteLine("\nDigite o preço do produto:\n");
                        var price = Console.ReadLine();

                        Product newProduct = new()
                        {
                            IdProduct = id,
                            Name = name,
                            Description = description,
                            Price = Convert.ToDecimal(price),
                        };

                        product.Create(newProduct);
                        break;
                    case "3":
                        Console.WriteLine("\nDigite o id do produto que será editado:\n");
                        id = Console.ReadLine();
                        var productsList = product.ReadAll();

                        var readProduct = product.FindById(productsList, id);

                        Console.WriteLine($"\nDeseja alterar o nome? ({readProduct.Name}):\n");
                        name = Console.ReadLine();
                        if (name != "") readProduct.Name = name;

                        Console.WriteLine($"\nDeseja alterar o a descrição? ({readProduct.Description}):\n");
                        description = Console.ReadLine();
                        if (description != "") readProduct.Description = description;

                        Console.WriteLine($"\nDeseja alterar o preço? R$({readProduct.Price}):\n");
                        price = Console.ReadLine();
                        if (price != "") readProduct.Price = Convert.ToDecimal(price);

                        product.Update(readProduct);
                        Console.WriteLine($"\nAlterações feitas com sucesso\n");
                        break;
                    case "4":
                        Console.WriteLine("\nDigite o id do produto que será editado:\n");
                        id = Console.ReadLine();
                        product.Delete(id);
                        break;
                    default:
                        break;
                }

            } while (option != "0");
        }
    }
}