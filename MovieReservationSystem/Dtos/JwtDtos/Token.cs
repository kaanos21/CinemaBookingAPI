namespace MovieReservationSystem.Dtos.JwtDtos
{
    public class Token
    {
        public string AccesToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
