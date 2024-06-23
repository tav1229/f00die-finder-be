using f00die_finder_be.Data.Entities;
using f00die_finder_be.Dtos.Location;

namespace f00die_finder_be.Dtos.Restaurant
{
    public class UserSavedRestaurantDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public LocationDto Location { get; set; }

        public WardOrCommuneDto WardOrCommune { get; set; }

        public PriceRangePerPersonDto PriceRangePerPerson { get; set; }

        public string Image { get; set; }
        public short Rating { get; set; }
    }
}
