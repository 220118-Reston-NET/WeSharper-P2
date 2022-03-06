using Microsoft.EntityFrameworkCore;
using WeSharper.DatabaseManagement.Interfaces;
using WeSharper.Models;

namespace WeSharper.DatabaseManagement.Implements
{
    public class ProfileManagementDL : IProfileManagementDL
    {
        private readonly WeSharperContext _context;

        public ProfileManagementDL(WeSharperContext context)
        {
            _context = context;
        }

        public Profile AddNewProfile(Profile p_profile)
        {
            p_profile.CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
            _context.Profiles.Add(p_profile);
            _context.SaveChanges();

            return p_profile;
        }
    }
}