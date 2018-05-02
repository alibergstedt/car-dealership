using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Tables
{
    public class CarMake
    {
        [Required(ErrorMessage = "Please enter car make name")]
        public int CarMakeId { get; set; }

        [Required(ErrorMessage = "Please enter car make name")]
        public string CarMakeName { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
