namespace f00die_finder_be.Data.Entities
{
    public class WardOrCommune : BaseEntity
    {
        public int Code { get; set; }
        public string? Name { get; set; }

        public District? District { get; set; }
        public Guid DistrictId { get; set; }

        public List<Location>? Locations { get; set; }
    }
}