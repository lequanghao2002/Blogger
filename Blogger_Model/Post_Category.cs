using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogger_Model
{
    [Table("Post_Categories")]
    public class Post_Category
    {
        public int PostID { get; set; }
        [ForeignKey("PostID")]
        public Post Post { get; set; }

        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public Category Category { get; set; }

    }
}

// Add-Migration AddDbInitial -StartupProject Blogger_Web
