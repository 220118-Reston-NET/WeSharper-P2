namespace WeSharper.APIPortal.Consts
{
    public static class RouteConfigs
    {
        //AUTHENTICATION
        public const string Register = "Register";
        public const string Login = "Login";

        //USER
        public const string UpdateProfile = "Profile";
        public const string GetAllProfiles = "GetAllProfiles";
        public const string GetAProfile = "GetAProfile";

        //HOBBY
        public const string Hobby = "Hobby";

        //USERPOST
        public const string UserPosts = "UserPosts";
        public const string UserPost = "UserPosts/{p_postID}";
      
        public const string UserPostComments = "UserPosts/{p_postID}/Comments";
        public const string UserPostReactions = "UserPosts/{p_postID}/Reactions";

        //Friend
        public const string Friends = "Friend";
        public const string Friend = "Friend/{f_userID}";
    }

}