using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aula1_Prog_Avanc.Interfaces;

namespace Aula1_Prog_Avanc.Models
{
    internal class Product : Base, IProduct
    {
        public string IdProduct { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        private const string path = "database/products.csv";

        public Product()
        {
            CreateFolderAndFile(path);
        }

        public static string PrepareLine(Product product)
        {
            return $"{product.IdProduct};{product.Name};{product.Description};{product.Price}";
        }

        public List<Product> ReadAll()
        {
            List<Product> products = new List<Product>();
            string[] lines = File.ReadAllLines(path);

            foreach(var item in lines)
            {
                string[] line = item.Split(";");

                Product product = new Product()
                {
                    IdProduct = line[0],
                    Name = line[1],
                    Description = line[2],
                    Price = Convert.ToDecimal(line[3]),
                };

                products.Add(product);
            }

            return products;
        }

        public void Create(Product product)
        {
            string[] line = {PrepareLine(product)};
            File.AppendAllLines(path, line);
        }

        public void Update(Product product)
        {
            List<Product> list = ReadAll();

            var idx = list.FindIndex(x => x.IdProduct == product.IdProduct);
            list[idx] = product;

            List<string> lines = new();

            foreach(var item in list)
            {
                lines.Add(PrepareLine(item));
            }

            RewriteCSV(path, lines);
        }

        public void Delete (string idProduct)
        {
            List<string> lines = ReadAllLinesCSV(path);

            lines.RemoveAll( x => x.Split(";")[0] == idProduct);

            RewriteCSV(path, lines);
        }

        public Product FindById(List<Product> products, string idProduct)
        {
            return products.Find(x => x.IdProduct == idProduct); ;
        }

    }
}
