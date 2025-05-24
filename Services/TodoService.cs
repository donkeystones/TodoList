using Persistence;
using Persistence.Models;
using Services.DTO;
using Services.Mappers;

namespace Services;

public class TodoService(ITodoRepository repository)
{
    public TodoDTO AddTodo(string title)
    {
        var todo = new TodoItem
        {
            Id = Guid.NewGuid(),
            Title = title,
            Date = DateOnly.FromDateTime(DateTime.Now),
            Completed = false
        };

        repository.AddTodo(todo);

        TodoDTO dto = TodoMapper.ToDto(todo);
        
        return dto;
    }


    public List<TodoDTO> GetTodos()
    {
        List<TodoDTO> mappedTodo = new List<TodoDTO>();
        var todos = repository.GetTodos();
        foreach (TodoItem item in todos)
        {
            mappedTodo.Add(TodoMapper.ToDto(item));
        }

        return mappedTodo;
    }
}