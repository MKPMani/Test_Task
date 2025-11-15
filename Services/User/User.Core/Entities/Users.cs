using MongoDB.Bson.Serialization.Attributes;


namespace User.Core.Entities
{
    public class Users : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
