using System.Runtime.Serialization;

namespace WcfService
{
    [DataContract]
    public class Role
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
    }
}
