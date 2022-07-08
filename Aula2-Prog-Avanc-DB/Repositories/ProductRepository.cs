using Aula2_Prog_Avanc_DB.Models;

namespace Aula2_Prog_Avanc_DB.Repositories { 

    internal class ProductRepository : BaseRepository<Product>
    {
        public ProductRepository() : base("products", "IdProduct")
        {
        }

    }
}
