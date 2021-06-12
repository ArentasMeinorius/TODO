namespace TODO.DBSchemas
{
    public class TodoTask
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Completed { get; set; }

        public int ListId { get; set; }

        public TodoList List { get; set; }
    }
}
