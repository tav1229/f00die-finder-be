namespace f00die_finder_be.Dtos.ReviewComment
{
    public class ReviewCommentDto
    {
        public Guid Id { get; set; }

        public string Content { get; set; }

        public short Rating { get; set; }

        public Guid UserId { get; set; }

        public string FullName { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
