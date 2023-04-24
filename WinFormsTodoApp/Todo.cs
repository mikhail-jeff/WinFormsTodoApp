using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsTodoApp
{
    public class Todo
    {

        //DATA MODEL
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("text")]
        public string Text { get; set; }

        [BsonElement("complete")]
        public string Complete { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }


        //CONSTRUCTOR
        public Todo(string text, string complete, DateTime createdAt)
        {
            Text = text;
            Complete = complete;
            CreatedAt = DateTime.Now;
        }
    }
}
