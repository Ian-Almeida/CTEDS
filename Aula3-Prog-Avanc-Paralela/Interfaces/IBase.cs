using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula3_Prog_Avanc_Paralela.Interfaces
{
    internal interface IBaseRepository<T>
    {
        List<T> ReadAll();

        T FindById(string IdPayload);

        void Create(T payload);

        void Update(T payload);

        void Delete(string id);
    }
}
