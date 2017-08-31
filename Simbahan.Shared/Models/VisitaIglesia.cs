using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Simbahan.Models
{
    [DataContract]
    public class VisitaIglesia
    {
        public VisitaIglesia()
        {
            Church = new Church();
            User = new User();
        }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public int SimbahanId { get; set; }

        [DataMember]
        public int StatusId { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public Church Church { get; set; }

        [DataMember]
        public User User { get; set; }

        [DataMember]
        public DateTime UpdatedAt { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }

        public string ToJson()
        {
            var ms = new MemoryStream();

            var ser = new DataContractJsonSerializer(typeof(VisitaIglesia));

            ser.WriteObject(ms, this);
            var json = ms.ToArray();

            ms.Close();

            return Encoding.UTF8.GetString(json, 0, json.Length);
        }
    }
}