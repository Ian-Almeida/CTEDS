using Aula4_API.Models;

namespace Aula4_API.Repositories
{
    internal class CarRepository: BaseRepository<Car>
    {

        public CarRepository() : base("cars", "IdCar")
        {
        }
    }
}
