using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace f00die_finder_be.Entities
{
    public class ReviewComment : BaseEntity
    {
        public string? Content { get; set; }

        [Range(1, 10)]
        public short Rating { get; set; }
        
        public Restaurant Restaurant { get; set; }

        public Guid RestaurantId { get; set; }

        public User User { get; set; }

        public Guid UserId { get; set; }
    }
}