using DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapModels.ViewModels
{
    public class PostCategoryVM
    {
        public long PostId { get; set; }
        public Post Post { get; set; }
        public long CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
