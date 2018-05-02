using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Interfaces
{
    public interface ICarRepository
    {
        Car GetById(int carId);
        void Insert(Car car);
        void Update(Car car);
        void Delete(int carId);
        IEnumerable<CarDetailsItem> GetDetails();
        IEnumerable<CarDetailsFeaturedItem> GetFeatured();
        IEnumerable<CarDetailsItem> SearchAll(CarSearchParameters parameters);
        IEnumerable<CarDetailsItem> SearchUsedInventory(CarSearchParameters parameters);
        IEnumerable<CarDetailsItem> SearchNewInventory(CarSearchParameters parameters);
        List<Category> GetCarCategory();
        List<Car> GetBodyStyle();
        List<Car> GetTransmission();
        List<Car> GetCarColor();
        List<Car> GetInteriorColor();
    }
}
