using System;
using System.Collections.Generic;

namespace EXE111PROJECT.Models
{
    public partial class User
    {
        public User()
        {
            ConversationUser1s = new HashSet<Conversation>();
            ConversationUser2s = new HashSet<Conversation>();
            MessageReceivers = new HashSet<Message>();
            MessageSenders = new HashSet<Message>();
            ProposedInterestAdmins = new HashSet<ProposedInterest>();
            ProposedInterestUsers = new HashSet<ProposedInterest>();
            Interests = new HashSet<Interest>();
            Roles = new HashSet<Role>();
        }

        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? Avatar { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? LastLogin { get; set; }
        public string? Status { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Conversation> ConversationUser1s { get; set; }
        public virtual ICollection<Conversation> ConversationUser2s { get; set; }
        public virtual ICollection<Message> MessageReceivers { get; set; }
        public virtual ICollection<Message> MessageSenders { get; set; }
        public virtual ICollection<ProposedInterest> ProposedInterestAdmins { get; set; }
        public virtual ICollection<ProposedInterest> ProposedInterestUsers { get; set; }

        public virtual ICollection<Interest> Interests { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}
