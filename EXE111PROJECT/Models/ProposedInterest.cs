using System;
using System.Collections.Generic;

namespace EXE111PROJECT.Models
{
    public partial class ProposedInterest
    {
        public int Id { get; set; }
        public string InterestName { get; set; } = null!;
        public int UserId { get; set; }
        public DateTime ProposedAt { get; set; }
        public string Status { get; set; } = null!;
        public DateTime? ReviewedAt { get; set; }
        public int? AdminId { get; set; }

        public virtual User? Admin { get; set; }
        public virtual User User { get; set; } = null!;
    }
}
