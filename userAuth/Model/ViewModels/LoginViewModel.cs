namespace userAuth.Model.ViewModels
{
    public class LoginViewModel
    {
        public int id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool IsApproved { get; set; }
    }
}
