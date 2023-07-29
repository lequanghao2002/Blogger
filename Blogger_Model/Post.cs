using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogger_Model
{
    [Table("Posts")]
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required, StringLength(256)]
        public string Title { get; set; }

        [Required]
        public string BriefContent { get; set; }

        [Required]
        public string Content { get; set; }


        [Required, StringLength(256)]
        public string Image { get; set; }
        public bool Published { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime UpdateDate { get; set; }
        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }
        public IEnumerable<Post_Category> Post_Categories { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}
