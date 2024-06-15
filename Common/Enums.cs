namespace f00die_finder_be.Common
{
    public enum Role
    {
        Admin,
        RestaurantOwner,
        Customer
    }

    public enum RestaurantStatus
    {
        Active,
        Inactive,
        Pending
    }

    public enum ReservationStatus
    {
        Pending,
        Confirmed,
        Denied,
        Cancelled
    }

    public enum ImageType
    {
        Menu,
        Restaurant
    }

    public enum OTPType
    {
        ForgotPassword,
        VerifyEmail
    }

    public enum RestaurantSortType
    {
        Popular,
        PriceAscending,
        PriceDescending,
        Rating,
        CreatedDate
    }

    public enum UserStatus
    {
        Active,
        Blocked,
        Unverified
    }

    public enum SemanticSearchType
    {
        RestaurantName,
        CuisineName
    }
}