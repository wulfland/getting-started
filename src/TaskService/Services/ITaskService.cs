using System.Collections.Generic;

namespace TaskService.Services
{
    public interface ITaskService
    {
        public IEnumerable<TaskItem> GetAll();

        public TaskItem GetById(int id);

        public TaskItem AddOrUpdate(TaskItem item);

        public void Delete(int id);
    }
}
