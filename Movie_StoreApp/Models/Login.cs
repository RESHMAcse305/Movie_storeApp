namespace Movie_StoreApp.Models
{
    public class Login
    {
        internal object userId;

        public int Id { get; set; }
        public string? Username {set;get;}
        public string? Password {set;get;}
        public string?UserType {set;get;}
     
    }
}
