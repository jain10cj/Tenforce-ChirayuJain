using System.Collections.Generic;
using Newtonsoft.Json;

namespace Test_Taste_Console_Application.Domain.DataTransferObjects
{
    public class PlanetDto
    {
        public string Id { get; set; }
        public float SemiMajorAxis { get; set; }

        //Added Average moon gravit property to wrapper class of Plnet to be able to assign and access values from.
        //This property is absent in the APi response for a plant object thus required here
        public float AverageMoonGravity { get; set; }
        public ICollection<MoonDto> Moons { get; set; }
    }
}