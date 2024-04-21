namespace f00die_finder_be.Common
{
    public enum Role
    {
        Admin,
        RestaurantOwner,
        User
    }
    public enum RestaurantStatus
    {
        Active,
        Inactive
    }
    public enum ReservationStatus
    {
        Pending,
        Confirmed,
        Denied
    }
    public enum PriceRangePerPerson
    {
        LessThan200K,
        From200KTo500K,
        MoreThan500K
    }
    public enum ImageType
    {
        Menu,
        Restaurant
    }
}