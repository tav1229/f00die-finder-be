namespace f00die_finder_be.Common.CurrentUserService
{
    public interface ICurrentUserService
    {
        public Guid UserId { get; }
        public Role Role { get; }
    }
}
