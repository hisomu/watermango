using System;

namespace PlantService.Models
{
    public class Plant
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public State State { get; set; }
        public string ImageUrl { get; set; }
        public DateTime LastUpdated { get; set; }

        public Plant Clone()
        {
            return (Plant)this.MemberwiseClone();
        }
    }
    public enum State
    {
        Dry,
        Ok,
        Watering,
        Watered
    }

}
