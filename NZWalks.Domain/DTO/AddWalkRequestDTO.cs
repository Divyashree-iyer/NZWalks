namespace NZWalks.Domain.DTO
{
    public class AddWalkRequestDTO
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public double LengthInKm { get; set; }

        public string? WalkImageUrl { get; set; }

        public DifficultyDTO Difficulty { get; set; }

        public RegionDTO Region { get; set; }
    }
}
