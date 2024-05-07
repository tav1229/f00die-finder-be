using System.ComponentModel.DataAnnotations;

namespace f00die_finder_be.Dtos.ReviewComment
{
    public class ReviewCommentAddDto
    {
        public Guid RestaurantId { get; set; }
        public string Content { get; set; }

        [Range(1, 10)]
        public short Rating { get; set; }
    }
}
