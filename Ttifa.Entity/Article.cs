using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ttifa.Entity
{
    public partial class Article
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(100)]
        public string Subtitle { get; set; }

        public string Content { get; set; }

        public DateTime UpdateTime { get; set; }

        [StringLength(50)]
        public string Author { get; set; }

        public int AuthorId { get; set; }
    }

    public class ArticleWithoutContent
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string Content { get; set; }

        public DateTime UpdateTime { get; set; }

        public string Author { get; set; }

        public int AuthorId { get; set; }
    }
}
