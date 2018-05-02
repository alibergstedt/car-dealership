using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Interfaces
{
    public interface IPromoRepository
    {
        List<Promo> GetAll();
        void Insert(Promo promo);
        void Delete(int promoCodeId);
    }
}
