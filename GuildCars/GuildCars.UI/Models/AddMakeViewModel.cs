using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class AddMakeViewModel
    {
        public int CarMakeId { get; set; }

        [Required(ErrorMessage = "Please enter car make name")]
        public string CarMakeName { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserId { get; set; }
        public List<CarMake> Makes { get; set; }
        public CarMake NewMake { get; set; }
    }
}