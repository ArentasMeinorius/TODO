namespace TODO.DBSchemas
{
    public class TodoTask
    {
        public TodoTask()
        { }

        public TodoTask(string name, string description, bool completed, int listId, int userId)
        {
            Name = name;
            Description = description;
            Completed = completed;
            ListId = listId;
            UserId = userId;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Completed { get; set; }

        public int ListId { get; set; }

        public int UserId { get; set; }
    }
}
