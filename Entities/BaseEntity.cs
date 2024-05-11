using System.ComponentModel.DataAnnotations;

namespace f00die_finder_be.Entities
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastUpdatedDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}