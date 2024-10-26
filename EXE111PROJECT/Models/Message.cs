using System;
using System.Collections.Generic;

namespace EXE111PROJECT.Models
{
    public partial class Message
    {
        public int Id { get; set; }
        public int ConversationId { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime SendAt { get; set; }
        public bool IsRead { get; set; }

        public virtual Conversation Conversation { get; set; } = null!;
        public virtual User Receiver { get; set; } = null!;
        public virtual User Sender { get; set; } = null!;
    }
}
