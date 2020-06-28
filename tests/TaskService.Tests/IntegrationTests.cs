using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TaskService.Tests
{
    public class IntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        readonly HttpClient _client;

        public IntegrationTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Theory]
        [InlineData("/tasks")]
        public async Task Can_get_all_task(string url)
        {
            var result = await _client.GetAsync(url);

            result.EnsureSuccessStatusCode();

            var body = await GetContentAS<IEnumerable<TaskItem>>(result.Content);

            body.Should().HaveCountGreaterOrEqualTo(5);
        }

        [Theory]
        [InlineData("/tasks/2", 2)]
        [InlineData("/tasks/3", 3)]
        public async Task Can_get_one_task(string url, int expected)
        {
            var result = await _client.GetAsync(url);

            result.EnsureSuccessStatusCode();

            var body = await GetContentAS<TaskItem>(result.Content);

            body.Id.Should().Be(expected);
        }

        [Fact]
        public async Task Can_add_new_item()
        {
            var newTask = new TaskItem { Title = "From Integration Test" };

            var result = await _client.PostAsync("/tasks", GetStringContent(newTask));

            result.EnsureSuccessStatusCode();

            var body = await GetContentAS<TaskItem>(result.Content);

            body.Title.Should().Be("From Integration Test");
        }

        [Fact]
        public async Task Can_update_item()
        {
            var result = await _client.GetAsync("/tasks/2");
            var existing = await GetContentAS<TaskItem>(result.Content);

            existing.Title = "Updated title";

            result = await _client.PostAsync("/tasks", GetStringContent(existing));

            result.EnsureSuccessStatusCode();

            var body = await GetContentAS<TaskItem>(result.Content);

            body.Title.Should().Be("Updated title");
        }

        private StringContent GetStringContent(object body)
        {
            return new StringContent(JsonConvert.SerializeObject(body), Encoding.Default, "application/json");
        }

        private async Task<T> GetContentAS<T>(HttpContent content)
        {
            var s = await content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(s);
        }
    }
}
