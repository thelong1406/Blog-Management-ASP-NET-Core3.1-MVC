using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Domain
{
    public class Vote
    {
        public string UserId { get; set; }
        public long PostId { get; set; }
        public byte Rate { get; set; }
        public User User { get; set; }
        public Post Post { get; set; }
    }
}
