using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Swashbuckle.AspNetCore.Annotations;

namespace KickboxerApi.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [SwaggerSchema(ReadOnly = true)]
        public string? Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Email { get; set; }
        public string? Password { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [SwaggerSchema(ReadOnly = true)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [SwaggerSchema(ReadOnly = true)]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}