using System;

namespace TODO.DBSchemas
{
    public class User
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public DateTime CreationDateTime { get; set; }

        public int ListId { get; set; }

        public TodoList List { get; set; }
    }
}
