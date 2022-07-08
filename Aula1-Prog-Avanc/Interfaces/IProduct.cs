using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aula1_Prog_Avanc.Models;

namespace Aula1_Prog_Avanc.Interfaces
{
    internal interface IProduct
    {
        List<Product> ReadAll();

        void Create(Product product);

        void Update(Product product);

        void Delete(string idProduct);
    }
}
