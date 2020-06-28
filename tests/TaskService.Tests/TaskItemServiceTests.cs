using FluentAssertions;
using System;
using TaskService.Services;
using Xunit;

namespace TaskService.Tests
{
    public class TaskItemServiceTests
    {

        [Fact]
        public void DefaultDataTest()
        {
            var sut = new TaskItemService();

            sut.GetAll()
                .Should()
                .HaveCount(5);
        }

        [Theory]
        [InlineData(0, 5)]
        [InlineData(-1, 5)]
        [InlineData(66, 5)]
        [InlineData(2, 2)]
        public void TaskItemService_can_add_task(int id, int expected)
        {
            var sut = new TaskItemService();

            sut.AddOrUpdate(new TaskItem { Id = id, Title = "This is a test" })
                .Id
                .Should()
                .Be(expected);
        }

        [Fact]
        public void TaskItemService_new_task_have_new_state()
        {
            var sut = new TaskItemService();

            sut.AddOrUpdate(new TaskItem { Title = "This is a test" })
                .State
                .Should()
                .Be(TaskState.New);
        }

        [Fact]
        public void TaskItemService_can_update_item()
        {
            var sut = new TaskItemService();

            var item2 = sut.GetById(2);

            item2.Title = "Title is changed by test";

            sut.AddOrUpdate(item2);

            sut.GetById(2).Title.Should().Be("Title is changed by test");
        }

        [Fact]
        public void TaskItemService_can_delet_item()
        {
            var sut = new TaskItemService();

            var newItem = sut.AddOrUpdate(new TaskItem { Title = "This is a test" });

            sut.Delete(newItem.Id);

            sut.GetAll()
                .Should()
                .HaveCount(5);
        }

        [Fact]
        public void TaskItemService_delet_throws_exception()
        {
            var sut = new TaskItemService();

            Action act = () => {
                sut.Delete(99);
            };

            act.Should().Throw<IndexOutOfRangeException>();
        }
    }
}
