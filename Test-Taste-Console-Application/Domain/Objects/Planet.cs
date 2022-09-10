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
        
        // Modified accessor for Average moon gravity
        // Changed from a static set=> 0.0f which set the value to default 0 to be able to get and set from opeartions done below
        public float AverageMoonGravity
        {
            get; set;
        }

        public Planet(PlanetDto planetDto)
        {
            Id = planetDto.Id;
            SemiMajorAxis = planetDto.SemiMajorAxis;
            Moons = new Collection<Moon>();
            if(planetDto.Moons != null)
            {
                // Using LINQ Average method to calculate average gravity for a planets moons
                // planetDto object above provides a concise enumerable for all moons of said planet and can be easily used for such opearation.
                AverageMoonGravity = planetDto.Moons.Average(x => x.Gravity);

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
