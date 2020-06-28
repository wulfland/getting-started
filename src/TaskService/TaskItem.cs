using System;

namespace TaskService
{
    public enum TaskState
    {
        New,
        Doing,
        Done
    }

    public class TaskItem
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
        
        public TaskState State { get; set; }
    }
}
