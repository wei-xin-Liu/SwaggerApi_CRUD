namespace Member.Models.Auth
{
    public class AuthenticateLoginReq
    {
        public string? id { get; set; } // 登入帳號
        public string? password { get; set; } // 登入密碼
    }
}
