using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Interfaces
{
    public interface IMakeModelRepository
    {
        List<CarMake> GetMakes();
        List<CarModel> GetModels();
        List<CarModel> GetModelByMake(int carMakeId);
        void AddMake(CarMake carMake);
        void AddModel(CarModel carModel);
    }
}
