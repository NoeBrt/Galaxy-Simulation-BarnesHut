using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Simulation
{
    public class BodiesInitializerFactory
    {
        public static BodiesInitializer Create(SimulationParameter simulationParameter)
        {
            switch (simulationParameter.simulationType)
            {
                case SimulationType.Universe:
                    return new UniverseInitializer(simulationParameter);
                case SimulationType.Galaxy:
                    return new GalaxyInitializer(simulationParameter);
                case SimulationType.Collision:
                    return new CollisionInitializer(simulationParameter);
                case SimulationType.Photo:
                    return new PhotoInitializer(simulationParameter);
                default:
                    throw new Exception("Initializer type not found");
            }

        }
    }

}