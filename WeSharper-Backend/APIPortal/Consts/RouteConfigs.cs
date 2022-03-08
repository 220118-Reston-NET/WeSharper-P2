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
    }

}

// add one for hobby