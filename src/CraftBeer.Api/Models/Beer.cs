namespace CraftBeer.Api.Models
{
    public class Beer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ingredients { get; set; }
        public string AlcoholContents { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }

        protected Beer() {}

        public Beer(int id)
        {
            Id = id;
        }
    }
}