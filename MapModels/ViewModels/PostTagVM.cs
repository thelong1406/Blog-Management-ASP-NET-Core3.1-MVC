using DataAccess.Domain;
using MapModels.ViewModels.PostVM;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapModels.ViewModels
{
    public class PostTagVM
    {
        public long PostId { get; set; }
        public PostDetailVM Post { get; set; }
        public long TagId { get; set; }
        public TagVM Tag { get; set; }
    }
}
