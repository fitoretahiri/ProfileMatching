namespace ProfileMatching.Users.Controllers
{
    public class CustomUnauthorizedException : Exception
    {
        public CustomUnauthorizedException(string message) : base(message)
        {
        }
    }
}
