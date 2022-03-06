using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace WeSharper.Models
{
    public partial class ApplicationUser : IdentityUser<string>
    {
        public ApplicationUser()
        {
            FriendAcceptedUsers = new HashSet<Friend>();
            FriendRequestedUsers = new HashSet<Friend>();
            GroupPostCommentReacts = new HashSet<GroupPostCommentReact>();
            GroupPostComments = new HashSet<GroupPostComment>();
            GroupPostReacts = new HashSet<GroupPostReact>();
            GroupPosts = new HashSet<GroupPost>();
            Groups = new HashSet<Group>();
            MessageRecipientUsers = new HashSet<Message>();
            MessageSenderUsers = new HashSet<Message>();
            PostCommentReacts = new HashSet<PostCommentReact>();
            PostComments = new HashSet<PostComment>();
            PostReacts = new HashSet<PostReact>();
            Posts = new HashSet<Post>();
            Profiles = new HashSet<Profile>();

        }

        public virtual ICollection<Friend> FriendAcceptedUsers { get; set; }
        public virtual ICollection<Friend> FriendRequestedUsers { get; set; }
        public virtual ICollection<GroupPostCommentReact> GroupPostCommentReacts { get; set; }
        public virtual ICollection<GroupPostComment> GroupPostComments { get; set; }
        public virtual ICollection<GroupPostReact> GroupPostReacts { get; set; }
        public virtual ICollection<GroupPost> GroupPosts { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Message> MessageRecipientUsers { get; set; }
        public virtual ICollection<Message> MessageSenderUsers { get; set; }
        public virtual ICollection<PostCommentReact> PostCommentReacts { get; set; }
        public virtual ICollection<PostComment> PostComments { get; set; }
        public virtual ICollection<PostReact> PostReacts { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Profile> Profiles { get; set; }
    }
}