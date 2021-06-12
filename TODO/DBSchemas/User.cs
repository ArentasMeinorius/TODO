using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TODO.DBSchemas
{
    public class User
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationDateTime { get; set; }
    }
}
