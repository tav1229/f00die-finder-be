namespace f00die_finder_be.Data.Entities
{
    public class ProvinceOrCity : BaseEntity
    {
        public short Code { get; set; }

        public string Name { get; set; }

        public List<District>? Districts { get; set; }

    }
}