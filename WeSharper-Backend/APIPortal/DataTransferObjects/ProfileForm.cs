namespace WeSharper.APIPortal.DataTransferObjects
{
    public class ProfileForm
    {
        public string? ProfileId { get; set; } // in the future, will take this out
        public string? UserId { get; set; } // in the future, will be replaced by the token
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePictureUrl { get; set; } = "https://i.kym-cdn.com/entries/icons/facebook/000/027/475/Screen_Shot_2018-10-25_at_11.02.15_AM.jpg"! ;
        public string Bio { get; set; } = "Please add a bio";

    }
}