using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Test_Taste_Console_Application.Domain.DataTransferObjects;

namespace Test_Taste_Console_Application.Domain.Objects
{
    public class Planet
    {
        public string Id { get; set; }
        public float SemiMajorAxis { get; set; }
        public ICollection<Moon> Moons { get; set; }
        //public float AverageMoonGravity
        //{
        //    get
        //    {
        //        if (Moons == null || Moons.Count == 0)
        //        {
        //            return 0.0f;
        //        }

        //        float totalMoonGravity = 0.0f;
        //        foreach (var moon in Moons)
        //        {
        //            totalMoonGravity += Convert.ToSingle(moon.Gravity);
        //        }

        //        return totalMoonGravity / Moons.Count;
        //    }
           
        //}
        public float AverageMoonGravity => Convert.ToSingle(Moons.Any() ? Moons.Average(m => m.Gravity) : 0);

        public Planet(PlanetDto planetDto)
        {
            Id = planetDto.Id;
            SemiMajorAxis = planetDto.SemiMajorAxis;
            Moons = new Collection<Moon>();
            if(planetDto.Moons != null)
            {
                foreach (MoonDto moonDto in planetDto.Moons)
                {
                    Moons.Add(new Moon(moonDto));
                }
            }
        }

        public Boolean HasMoons()
        {
            return (Moons != null && Moons.Count > 0);
        }
    }
}
