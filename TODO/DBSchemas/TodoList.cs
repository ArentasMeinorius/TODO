using System.Collections.Generic;

namespace TODO.DBSchemas
{
    public class TodoList
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public ICollection<TodoTask> Tasks { get; set; }
    }
}
