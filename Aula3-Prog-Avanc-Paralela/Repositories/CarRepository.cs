using Aula3_Prog_Avanc_Paralela.Models;

namespace Aula3_Prog_Avanc_Paralela.Repositories
{
    internal class CarRepository: BaseRepository<Car>
    {

        public CarRepository() : base("cars", "IdCar")
        {
        }
    }
}
