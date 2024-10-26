using System;
using System.Collections.Generic;

namespace EXE111PROJECT.Models
{
    public partial class Interest
    {
        public Interest()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string InterestName { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
