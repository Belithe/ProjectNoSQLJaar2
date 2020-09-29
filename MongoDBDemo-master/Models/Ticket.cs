using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Ticket : IEntityBase
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Subject { get; set; }
        public TypeOfIncidentEnum Type { get; set; }
        public User User { get; set; }
        public PriorityOfIncidentEnum Priority { get; set; }
        public DeadlineEnum Deadline { get; set; }
        public string Description { get; set; }

        public IEnumerable<User> Users;
    }
}
