namespace HotelListing.API.Core.Models.Users
{
    public class AuthResponseDO
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
