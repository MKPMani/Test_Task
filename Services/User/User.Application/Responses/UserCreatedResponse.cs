using User.Core.Entities;
using MongoDB.Bson.Serialization.Attributes;

namespace User.Application.Responses
{
    public class UserCreatedResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }        
    }
}
