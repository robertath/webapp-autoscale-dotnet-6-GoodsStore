using System.Runtime.Serialization;

namespace GoodsStore.App.Models
{
    [DataContract]
    public class BaseModel
    {
        [DataMember]
        public int Id { get; protected set; }
    }

}
