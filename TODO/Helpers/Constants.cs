namespace TODO.Helpers
{
    public static class Constants
    {
        public static string UserId = "UserId";
        public static string UserType = "UserType";
        public static string[] UserNames = { "Scorsese@gmail.com", "Tarantino@gmail.com", "Spielberg@gmail.com", "Hitchcock@gmail.com", "Kubrick@gmail.com", "Nolan@gmail.com" };
        public static string[] ListNames = { "12 Angry Men", "Forrest Gump", "Inception", "The Matrix", "Terminator", "Avengers: Infinity War", "Joker" };
        public static string[] TaskNames = { "Action", "Comedy", "Drama", "Fantasy", "Horror", "Mystery", "Romance", "Thriller", "Western" };
        public static string JwtKey = "Jwt:Key";
        public static string JwtIssuer = "Jwt:Issuer";
        public static string Email = "ResetPassWord:Email";
        public static string PassWord = "ResetPassWord:PassWord";
        public static string Subject = "About that password...";
        public static string MailBody = "Unfortunately password reset is not implemented yet.";
        public static string Smtp = "smtp.gmail.com";
        public static int SmtpPort = 587;
        public static string WrongCredentials = "Wrong credentials provided.";
        public static string WrongEmail = "Email you provided does not exist";
    }
    public enum UserTypes {
        User,
        Admin
    }
}