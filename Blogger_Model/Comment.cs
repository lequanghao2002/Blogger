using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogger_Model
{
    [Table("Comments")]
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public string CommentMsg { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CommentDate { get; set; }

        public int ParentID { get; set; }

        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }

        public int PostID { get; set; }
        [ForeignKey("PostID")]
        public Post Post { get; set; }
    }
}
