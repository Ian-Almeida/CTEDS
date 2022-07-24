using Aula3_Prog_Avanc_Paralela.Models;
using Bogus;

namespace Aula3_Prog_Avanc_Paralela.Repositories { 

    internal class ProductRepository : BaseRepository<Product>
    {
        public ProductRepository() : base("products", "IdProduct")
        {
        }

        public void PopulateDatabase()
        {
            Faker<Product> ProductFaker() => new Faker<Product>()
                .RuleFor(d => d.IdProduct, f => f.Random.Guid().ToString())
                .RuleFor(d => d.Name, f => f.Commerce.ProductName())
                .RuleFor(d => d.Description, f => f.Commerce.ProductDescription())
                .RuleFor(d => d.Price, f => Convert.ToDecimal(f.Commerce.Price()));

            List<Product> products = ProductFaker().Generate(1000);

            foreach (var product in products)
            {
                this.Create(product);
            }
        }

    }
}
