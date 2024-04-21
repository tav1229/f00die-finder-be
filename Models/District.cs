using f00die_finder_be.Common;

namespace f00die_finder_be.Models
{
    public class District : BaseEntity
    {
        public short Code { get; set; }

        public string Name { get; set; }
        
        public ProvinceOrCity ProvinceOrCity { get; set; }

        public Guid ProvinceOrCityId { get; set; }
        public List<WardOrCommune>? WardOrCommunes { get; set; }

    }
}