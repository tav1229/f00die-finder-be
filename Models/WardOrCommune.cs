using f00die_finder_be.Common;

namespace f00die_finder_be.Models
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