using f00die_finder_be.Common;

namespace f00die_finder_be.Dtos.Restaurant
{
    public class ImageDto
    {
        public Guid Id { get; set; }

        public string URL { get; set; }
        
        public ImageType ImageType { get; set; }
    }
}
