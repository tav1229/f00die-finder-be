namespace f00die_finder_be.Entities
{
    public class RestaurantCustomerType : BaseEntity
    {
        public CustomerType CustomerType { get; set; }

        public Guid CustomerTypeId { get; set; }

        public Restaurant Restaurant { get; set; }

        public Guid RestaurantId { get; set; }
    }
}
