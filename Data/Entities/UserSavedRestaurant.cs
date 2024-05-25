namespace f00die_finder_be.Data.Entities
{
    public class UserSavedRestaurant : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
