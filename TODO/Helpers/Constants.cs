namespace TODO.Helpers
{
    public static class Constants
    {
        public static string UserId = "UserId";
        public static string UserType = "UserType";
        public static string[] UserNames = { "Scorsese", "Tarantino", "Spielberg", "Hitchcock", "Kubrick", "Nolan" };
        public static string[] ListNames = { "12 Angry Men", "Forrest Gump", "Inception", "The Matrix", "Terminator", "Avengers: Infinity War", "Joker" };
        public static string[] TaskNames = { "Action", "Comedy", "Drama", "Fantasy", "Horror", "Mystery", "Romance", "Thriller", "Western" };
        public static string JwtKey = "Jwt:Key";
        public static string JwtIssuer = "Jwt:Issuer";
    }
    public enum UserTypes {
        User,
        Admin
    }
}