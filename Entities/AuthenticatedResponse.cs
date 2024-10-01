namespace project_service.Entities
{
    public class AuthenticatedResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
