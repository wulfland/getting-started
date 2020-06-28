using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskService.Services
{
    public class TaskItemService : ITaskService
    {
        IDictionary<int,TaskItem> _tasks;

        public TaskItemService()
        {
            _tasks = new Dictionary<int, TaskItem>();

            for (int i = 0; i < 5; i++)
            {
                _tasks.Add(i, new TaskItem
                {
                    Id = i,
                    Title = $"Task item {i}",
                    State = TaskState.New,
                    Description = $"This is the {i} task."
                });
            }
        }

        public TaskItem AddOrUpdate(TaskItem item)
        {
            if (item.Id != 0 && _tasks.ContainsKey(item.Id))
            {
                _tasks[item.Id] = item;
            }
            else
            {
                item.Id = _tasks.Keys.Max() + 1;
                _tasks.Add(item.Id, item);
            }

            return item;
        }

        public void Delete(int id)
        {
            if (!_tasks.Remove(id))
            {
                throw new IndexOutOfRangeException();
            }
        }

        public IEnumerable<TaskItem> GetAll()
        {
            return _tasks.Values;
        }

        public TaskItem GetById(int id)
        {
            return _tasks[id];
        }
    }
}
