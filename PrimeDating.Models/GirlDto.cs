using System;
using System.Collections.Generic;

namespace PrimeDating.Models
{
    public class GirlDto
    {
        public string GirlId { get; set; }

        public string Operator { get; set; }

        public bool CanRecieveRealGifts { get; set; }

        public string Name { get; set; }

        public string Passport { get; set; }

        public List<string> PassportScans { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Height { get; set; }

        public string Weight { get; set; }

        public string BodyType { get; set; }

        public string MartialStatus { get; set; }

        public string Education { get; set; }

        public string Religion { get; set; }

        public string Smoking { get; set; }

        public string Drinking { get; set; }

        public string Occupation { get; set; }

        public DateTime DateBirth { get; set; }

        public string CountChildren { get; set; }

        public string ColorEye { get; set; }

        public string ColorHair { get; set; }

        public string LookingFor { get; set; }

        public string Description { get; set; }

        public string EngLevel { get; set; }

        public string OtherLangs { get; set; }

        public string Avatar { get; set; }

        public List<string> Images { get; set; }


    }
}
