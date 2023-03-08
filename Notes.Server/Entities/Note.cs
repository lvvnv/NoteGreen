using System;

namespace Notes.Server.Entities
{

    public class Note
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        
        public string Content { get; set; }

        private Note()
        {
        }

        public Note(Guid id, string name, string content)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id), "Id is invalid");
            }

            Id = id;
            Name = name;
            Content = content;
        }
    }
}