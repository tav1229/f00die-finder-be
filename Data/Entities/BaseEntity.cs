using System.ComponentModel.DataAnnotations;

namespace f00die_finder_be.Data.Entities
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset LastUpdatedDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}