namespace Dogshouseservice.API.Models.Domain
{
    public class Dog
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Color { get; set; }

        public int Tail_length { get; set; }

        public int Weight { get; set; }
    }
}