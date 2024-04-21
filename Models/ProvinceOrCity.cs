using f00die_finder_be.Common;

namespace f00die_finder_be.Models
{
    public class ProvinceOrCity : BaseEntity
    {
        public short Code { get; set; }

        public string Name { get; set; }
        
        public List<District>? Districts { get; set; }
        
    }
}