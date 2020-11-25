using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WcfService
{
    [DataContract]
    public class User
    {
        int? uId;
        string uLogin = string.Empty;
        string uPassword = string.Empty;
        int? uRoleId;
        int? uDepartamentId;
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public KeyValuePair<string, string> LoginPass;
        [DataMember]
        public int RoleId { get; set; }
        [DataMember]
        public int DepartamentId { get; set; }
    }
}
