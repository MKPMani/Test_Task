using User.Core.Entities;
using MongoDB.Bson.Serialization.Attributes;

namespace User.Application.Responses
{
    public class UserCreatedResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
