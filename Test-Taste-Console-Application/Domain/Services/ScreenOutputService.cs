using System;
using System.Linq;
using Test_Taste_Console_Application.Constants;
using Test_Taste_Console_Application.Domain.Objects;
using Test_Taste_Console_Application.Domain.Services.Interfaces;
using Test_Taste_Console_Application.Utilities;

namespace Test_Taste_Console_Application.Domain.Services
{
    /// <inheritdoc />
    public class ScreenOutputService : IOutputService
    {
        private readonly IPlanetService _planetService;

        private readonly IMoonService _moonService;

        //This is ScreenOutputService Constructor with PlanetService and MoonService interfaces
        public ScreenOutputService(IPlanetService planetService, IMoonService moonService)
        {
            _planetService = planetService;
            _moonService = moonService;
        }

        //Method for Output of AllPlanets And Their Moons To Display on Console
        public void OutputAllPlanetsAndTheirMoonsToConsole()
        {
            
            //The service gets all the planets from the API.
            var planets = _planetService.GetAllPlanets().ToArray();
            Logger.Instance.Info("Running Output of All Planets And Their Moons To Console...");
            //If the planets aren't found, then the function stops and tells that to the user via the console.
            if (!planets.Any())
            {
                Console.WriteLine(OutputString.NoPlanetsFound);
                return;
            }

            //The column sizes and labels for the planets are configured here. 
            var columnSizesForPlanets = new[] { 20, 20, 30, 20 };
            var columnLabelsForPlanets = new[]
            {
                OutputString.PlanetId, OutputString.PlanetNumber, OutputString.PlanetSemiMajorAxis,
                OutputString.TotalMoons
            };

            //The column sizes and labels for the moons are configured here.
            //The second moon's column needs the 2 extra '-' characters so that it's aligned with the planet's column.
            var columnSizesForMoons = new[] { 20, 70 + 2 };
            var columnLabelsForMoons = new[]
            {
                //Error solved for Total labels needed for data 
                OutputString.MoonNumber, OutputString.MoonId, OutputString.PlanetSemiMajorAxis, OutputString.TotalMoons
            };

            //The for loop creates the correct output.
            for (int i = 0, j = 1; i < planets.Length; i++, j++)
            {
                //First the line is created.
                ConsoleWriter.CreateLine(columnSizesForPlanets);

                //Under the line the header is created.
                ConsoleWriter.CreateText(columnLabelsForPlanets, columnSizesForPlanets);

                //Under the header the planet data is shown.
                ConsoleWriter.CreateText(
                    new[]
                    {
                        j.ToString(), CultureInfoUtility.TextInfo.ToTitleCase(planets[i].Id),
                        planets[i].SemiMajorAxis.ToString(),
                        planets[i].Moons.Count.ToString()
                    },
                    columnSizesForPlanets);

                //Under the planet data the header for the moons is created.
                ConsoleWriter.CreateLine(columnSizesForPlanets);
                ConsoleWriter.CreateText(columnLabelsForMoons, columnSizesForMoons);

                //The for loop creates the correct output.
                for (int k = 0, l = 1; k < planets[i].Moons.Count; k++, l++)
                {
                    ConsoleWriter.CreateText(
                        new[]
                        {
                            l.ToString(), CultureInfoUtility.TextInfo.ToTitleCase(planets[i].Moons.ElementAt(k).Id)
                        },
                        columnSizesForMoons);
                }

                //Under the data the footer is created.
                ConsoleWriter.CreateLine(columnSizesForMoons);
                ConsoleWriter.CreateEmptyLines(2);
                
                /*
                    This is an example of the output for the planet Uranus and their Moons:
                   --------------------+--------------------+------------------------------+--------------------
                    Planet's Id         |Planet's Number     |Planet's Semi-Major Axis      |Total Moons
                    1                   |Uranus              |2.8706583E+09                 |27
                    --------------------+--------------------+------------------------------+--------------------
                    1                   |Ariel
                    2                   |Umbriel
                    3                   |Titania
                    4                   |Oberon
                    5                   |Miranda
                    6                   |Cordelia
                    7                   |Ophelia
                    8                   |Bianca
                    9                   |Cressida
                    10                  |Desdemona
                    11                  |Juliet
                    12                  |Portia
                    13                  |Rosalind
                    14                  |Belinda
                    15                  |Puck
                    16                  |Caliban
                    17                  |Sycorax
                    18                  |Prospero
                    19                  |Setebos
                    20                  |Stephano
                    21                  |Trinculo
                    22                  |Francisco
                    23                  |Margaret
                    24                  |Ferdinand
                    25                  |Perdita
                    26                  |Mab
                    27                  |Cupid
                    --------------------+------------------------------------------------------------------------
                    and more........
                */
            }
            //To read output and stop closing it automatically
            Console.ReadKey();
        }

        public void OutputAllMoonsAndTheirMassToConsole()
        {
            
            //The function works the same way as the PrintAllPlanetsAndTheirMoonsToConsole function. You can find more comments there.
            var moons = _moonService.GetAllMoons().ToArray();
            Logger.Instance.Info("Running Output of All Moons And Their Mass To Console...");
            if (!moons.Any())
            {
                Console.WriteLine(OutputString.NoMoonsFound);
                return;
            }

            var columnSizesForMoons = new[] { 20, 20, 30, 20 };
            var columnLabelsForMoons = new[]
            {
                OutputString.MoonNumber, OutputString.MoonId, OutputString.MoonMassExponent, OutputString.MoonMassValue
            };

            ConsoleWriter.CreateHeader(columnLabelsForMoons, columnSizesForMoons);

            for (int i = 0, j = 1; i < moons.Length; i++, j++)
            {
                ConsoleWriter.CreateText(
                    new[]
                    {
                        j.ToString(), CultureInfoUtility.TextInfo.ToTitleCase(moons[i].Id),
                        moons[i].MassExponent.ToString(), moons[i].MassValue.ToString()
                    },
                    columnSizesForMoons);
            }

            ConsoleWriter.CreateLine(columnSizesForMoons);
            ConsoleWriter.CreateEmptyLines(2);

            /*
                This is an example of the output for the moon around the earth:
               --------------------+--------------------+------------------------------+--------------------
                Moon's Number       |Moon's Id           |Moon's Mass Exponent          |Moon's Mass Value
                --------------------+--------------------+------------------------------+--------------------
                1                   |Lune                |22                            |7.346
                2                   |Mimas               |19                            |3.79
                3                   |Encelade            |20                            |1.08
                and more........
                --------------------+--------------------+------------------------------+--------------------
             */
        }

        public void OutputAllPlanetsAndTheirAverageMoonGravityToConsole()
        {
            
            //The function works the same way as the PrintAllPlanetsAndTheirMoonsToConsole function. You can find more comments there.
            var planets = _planetService.GetAllPlanets().ToArray();
            Logger.Instance.Info("Running Output of All Planets And Their Average Moon Gravity To Console...");
            if (!planets.Any())
            {
                Console.WriteLine(OutputString.NoMoonsFound);
                return;
            }
            var columnSizes = new[] { 20, 30 };
            var columnLabels = new[]
            {
                //Not PlaneNumber but PlanetName to be displayed
                OutputString.PlanetName, OutputString.PlanetMoonAverageGravity
            };


            ConsoleWriter.CreateHeader(columnLabels, columnSizes);

            foreach(Planet planet in planets)
            {
                if(planet.HasMoons())
                {
                    //To display AverageMoonGravity in format of 0.0f
                    ConsoleWriter.CreateText(new string[] { $"{planet.Id}", $"{planet.AverageMoonGravity + "f"}" }, columnSizes);
                }
                else
                {
                    ConsoleWriter.CreateText(new string[] { $"{planet.Id}", $"-" }, columnSizes);
                }
            }

            ConsoleWriter.CreateLine(columnSizes);
            ConsoleWriter.CreateEmptyLines(2);
            /*
                --------------------+------------------------------
                Planet's Name       |The Planet's Average Moon Gravity
                --------------------+------------------------------
                uranus              |8.87f
                neptune             |11.15f
                jupiter             |24.79f
                mars                |3.71f
                mercure             |-
                saturne             |10.44f
                terre               |9.8f
                venus               |-
                --------------------+------------------------------
            */
        }
    }
}
