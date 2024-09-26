namespace Member.Models.Member
{
    public class MemberGetResp
    {
        public string? pk { get; set; } // 會員PK
        public string? id { get; set; } // 帳號
        public string? pwd { get; set; } // 密碼
        public string? name { get; set; } // 會員名稱
        public string? gender { get; set; } // 性別:M.男 F.女
        public string? birthday { get; set; } // 生日
        public string? remark { get; set; } // 備註
        public string? enable { get; set; } // 帳號啟用:O.是 X.否
    }
}
