using Moq;
using Persistence;
using Persistence.Models;
using Services;
using Services.DTO;

namespace TestProject1;

public class Tests
{
    private Mock<ITodoRepository> _mockRepo;
    private TodoService _service;
    
    [SetUp]
    public void Setup()
    {
        _mockRepo = new Mock<ITodoRepository>();
        
        _service = new TodoService(_mockRepo.Object);
    }

    [TearDown] //Note to self, runs after each test.
    public void TearDown()
    {
        _mockRepo.Reset();
    }

    [Test]
    public void AddTodoItem()
    {
        _service.AddTodo("Test");
        
        _mockRepo.Verify(r => r.AddTodo(It.Is<TodoItem>(t => t.Title == "Test")), Times.Once());
        _mockRepo.Verify(r => r.AddTodo(It.Is<TodoItem>(t => !t.Completed)), Times.Once());
    }

    [Test]
    public void GetTodos()
    {
        var todoItems = new List<TodoItem>
        {
            new TodoItem { Id = Guid.NewGuid(), Title = "First", Completed = false, Date = DateOnly.FromDateTime(DateTime.Now) },
            new TodoItem { Id = Guid.NewGuid(), Title = "Second", Completed = true,  Date = DateOnly.FromDateTime(DateTime.Now) }
        };

        _mockRepo.Setup(r => r.GetTodos()).Returns(todoItems);

        var result = _service.GetTodos();

        Assert.Multiple(() =>
        {
            Assert.That(todoItems[0].Id,          Is.EqualTo(result[0].Id));
            Assert.That(todoItems[0].Title,       Is.EqualTo(result[0].Title));
            Assert.That(todoItems[0].Completed,   Is.EqualTo(result[0].Completed));

            Assert.That(todoItems[1].Id,          Is.EqualTo(result[1].Id));
            Assert.That(todoItems[1].Title,       Is.EqualTo(result[1].Title));
            Assert.That(todoItems[1].Completed,   Is.EqualTo(result[1].Completed));
        });
        
        _mockRepo.Verify(r => r.GetTodos(), Times.Once);
    }

    [Test]
    public void EditTodo()
    {
        
    }
}