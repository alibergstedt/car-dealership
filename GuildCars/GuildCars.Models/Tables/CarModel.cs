using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Tables
{
    public class CarModel
    {
        public int CarMakeId { get; set; }

        public int CarModelId { get; set; }

        public string CarModelName { get; set; }
        public string CarMakeName { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
