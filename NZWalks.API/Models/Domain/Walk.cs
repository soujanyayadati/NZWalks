namespace NZWalks.API.Models.Domain
{
    public class Walk
    {
        //Guid is an Unique Identifier in C#
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }

        //Navigation Properties

        //These are one to one Relationship with Difficulty and Region to Walk
        public Difficulty Difficulty { get; set; }
        public Region  Region { get; set; }
    }
}
