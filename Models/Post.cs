﻿using MongoDB.Bson.Serialization.Attributes;

namespace forum.Models
{
    [BsonIgnoreExtraElements]
    public class Post
    {
        [BsonElement("_id")]
        public int Id { get; set; }
        public PostInfo Info { get; set; } = new PostInfo();
        public User Poster { get; set; }                
        public bool Is_hidden {  get; set; } = false;

        public int Like_count { get; set; } = 0;
        public DateTime Last_update { get; set; } = DateTime.Now;
        public DateTime Create_date { get; set; } = DateTime.Now;
        public Post() { }
        public Post(int id, User poster)
        {
            Id = id;
            Poster = poster;
        }

    }
}
