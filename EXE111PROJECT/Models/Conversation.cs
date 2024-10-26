using System;
using System.Collections.Generic;

namespace EXE111PROJECT.Models
{
    public partial class Conversation
    {
        public Conversation()
        {
            Messages = new HashSet<Message>();
        }

        public int Id { get; set; }
        public int User1Id { get; set; }
        public int User2Id { get; set; }
        public DateTime StartedAt { get; set; }

        public virtual User User1 { get; set; } = null!;
        public virtual User User2 { get; set; } = null!;
        public virtual ICollection<Message> Messages { get; set; }
    }
}
