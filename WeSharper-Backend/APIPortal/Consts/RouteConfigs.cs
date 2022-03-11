namespace WeSharper.APIPortal.Consts
{
    public static class RouteConfigs
    {
        //AUTHENTICATION
        public const string Register = "Register";
        public const string Login = "Login";

        //USER
        public const string UpdateProfile = "Profile";
        public const string GetProfile = "Profile";

        //HOBBY
        public const string Hobby = "Hobby";

        //USERPOST
        public const string Feeds = "Feeds";
        public const string UserPosts = "UserPosts";
        public const string UserPost = "UserPosts/{p_postID}";
        public const string UserPostComments = "UserPosts/{p_postID}/Comments";
        public const string UserPostComment = "UserPosts/{p_postID}/Comments/{p_postCommentID}";
        public const string UserPostReactions = "UserPosts/{p_postID}/Reactions";
        public const string UserPostCommentReactions = "UserPosts/{p_postID}/Comments/{p_postCommentID}/Reactions";


        //Friend
        public const string Friends = "Friend";
        public const string Friend = "Friend/{f_userID}";
        public const string UnconfirmedSentFriends = "Friend/UnconirmedSent/{f_userID}";
        public const string UserPendingFriendRequests = "Friend/PendingFriendRequests/{f_userID}";

        //Group
        public const string ApprovedGroupUser = "Group/Approved/{groupId}";
        public const string BanGroupUser = "Group/Ban/{groupId}/{userId}";
        public const string Groups = "Group";
        public const string Group = "Group/{g_userId}";

        //GROUPPOST
        public const string GroupPosts = "{p_groupID}/Posts";
        public const string GroupPost = "{p_groupID}/Posts/{p_groupPostID}";
        public const string GroupPostComments = "{p_groupID}/Posts/{p_groupPostID}/Comments";
        public const string GroupPostComment = "{p_groupID}/Posts/{p_groupPostID}/Comments/{p_groupPostCommentID}";
        public const string GroupPostReactions = "{p_groupID}/Posts/{p_groupPostID}/Reactions";
        public const string GroupPostCommentReactions = "{p_groupID}/Posts/{p_groupPostID}/Comments/{p_groupPostCommentID}/Reactions";

        //MESSAGE
        public const string CreateMessage = "";
    }
}