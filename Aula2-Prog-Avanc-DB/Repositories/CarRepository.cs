using Aula2_Prog_Avanc_DB.Models;

namespace Aula2_Prog_Avanc_DB.Repositories
{
    internal class CarRepository: BaseRepository<Car>
    {

        public CarRepository() : base("cars", "IdCar")
        {
        }
    }
}
