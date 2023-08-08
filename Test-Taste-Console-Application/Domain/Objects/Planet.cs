using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Test_Taste_Console_Application.Domain.DataTransferObjects;

namespace Test_Taste_Console_Application.Domain.Objects
{
    public class Planet
    {
        public string Id { get; set; }
        public float SemiMajorAxis { get; set; }
        public ICollection<Moon> Moons { get; set; }
        public float AverageMoonGravity
        {
            get; set;
        }

        //Here all planet data and collection of moons will be received indata transfer objects
        public Planet(PlanetDto planetDto)
        {
            Id = planetDto.Id;
            SemiMajorAxis = planetDto.SemiMajorAxis;
            AverageMoonGravity = planetDto.Gravity;
            Moons = new Collection<Moon>();
            if(planetDto.Moons != null)
            {
                foreach (MoonDto moonDto in planetDto.Moons)
                {
                    Moons.Add(new Moon(moonDto));
                }
            }
        }

        //Here planet has moons or not will be checked
        public Boolean HasMoons()
        {
            return (Moons != null && Moons.Count > 0);
        }
    }
}
