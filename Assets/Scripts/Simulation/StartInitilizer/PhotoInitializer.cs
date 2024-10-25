using System.Threading.Tasks;
using UnityEngine;
using Simulation.Struct;
using System.Collections.Generic;

namespace Simulation
{
    public class PhotoInitializer : BodiesInitializer
    {
        private Texture2D sourceImage;
        private float particleDensity = 1.0f; // Particles per pixel
        private Color32[] pixels;
        private int imageWidth;
        private int imageHeight;
        private float radius;
        private float thickness;

        public PhotoInitializer(SimulationParameter simulationParameter) : base(simulationParameter)
        {
            this.radius = simulationParameter.Radius;
            this.thickness = simulationParameter.Thickness;
            LoadImage("Assets/Scripts/Simulation/StartInitilizer/cat.png");
        }

        private void LoadImage(string imagePath)
        {
            // Load the PNG file
            byte[] fileData = System.IO.File.ReadAllBytes(imagePath);
            sourceImage = new Texture2D(2, 2);
            sourceImage.LoadImage(fileData);

            // Cache image data
            pixels = sourceImage.GetPixels32();
            imageWidth = sourceImage.width;
            imageHeight = sourceImage.height;
        }
  private void ShuffleParticles(List<Particule> particles)
        {
            int n = particles.Count;
            for (int i = n - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                // Swap particles
                Particule temp = particles[i];
                particles[i] = particles[j];
                particles[j] = temp;
            }
        }
        public override Particule[] InitStars()
        {
            List<Particule> particles = new List<Particule>();

            // Calculate world space dimensions
            float worldWidth = radius;
            float worldHeight = thickness;

            // Scale factors to convert from image space to world space
            float scaleX = worldWidth / imageWidth;
            float scaleY = worldHeight / imageHeight;

            for (int y = 0; y < imageHeight; y++)
            {
                for (int x = 0; x < imageWidth; x++)
                {
                    Color32 pixel = pixels[y * imageWidth + x];

                    // Skip transparent pixels
                    if (pixel.a < 10) continue;

                    // Use pixel brightness to determine particle density
                    float brightness = (pixel.r + pixel.g + pixel.b) / (3.0f * 255.0f);

                    // Skip darker pixels randomly
                    if (Random.value > brightness) continue;

                    var particle = new Particule();

                    // Convert image coordinates to world space
                    float worldX = (x - imageWidth/2) * scaleX;
                    float worldY = (y - imageHeight/2) * scaleY;

                    // Set particle position
                    particle.position = new Vector3(worldX, 0, worldY) + Vector3.zero;

                    // Set particle velocity (you can modify this based on pixel color if desired)
                    particle.velocity = DiscVelocity(base.initialVelocity, particle, Vector3.zero);

                    // Set particle color based on image pixel
                  //  particle.color = new Color(pixel.r/255f, pixel.g/255f, pixel.b/255f, pixel.a/255f);

                    particles.Add(particle);
                }
            }
            ShuffleParticles(particles);
            return particles.ToArray();
        }


                private Vector3 DiscVelocity(float starInitialVelocity, Particule star, Vector3 center)
        {
            Vector3 direction = (center - star.position); // direction from star to center
            float distance = direction.magnitude;
            Vector3 up = new Vector3(0, 1, 0);
            Vector3 velocityDirection = Vector3.Cross(up, direction.normalized); // Perpendicular direction
            Vector3 velocity = velocityDirection * starInitialVelocity;


             // Up vector
          //  Vector3 velocityDirection = Vector3.Cross(up, direction.normalized); // Perpendicular direction

            // Adjust the initial velocity based on distance to maintain stable circular orbits
           // float adjustedInitialVelocity = starInitialVelocity*10f * Mathf.Sqrt(distance/diameter);

            // Add a small radial component to the velocity to give spiral arms
            //float radialVelocityFactor = 0.01f; // Experiment with different values for this
          //  Vector3 radialComponent = direction.normalized //* adjustedInitialVelocity * radialVelocityFactor;

            //Vector3 velocity = velocityDirection * adjustedInitialVelocity + radialComponent;

            return velocity;


        }
    }

}