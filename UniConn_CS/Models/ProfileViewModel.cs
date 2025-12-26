using System.Collections.Generic;

namespace UniConn_CS.Models
{
    public class ProfileViewModel
    {
        public STUDENTS Student { get; set; }
        public int PostCount { get; set; }
        public int CommunityCount { get; set; }

        // Topluluk Listesi
        public List<CommunityRoleInfo> MyCommunities { get; set; }

        // Kullanıcının Postları
        public List<POST> MyPosts { get; set; }
    }

    public class CommunityRoleInfo
    {
        public string CommunityName { get; set; }
        public string RoleTitle { get; set; }
    }
}