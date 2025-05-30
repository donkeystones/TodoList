using Persistence;
using Persistence.Models;
using Services.DTO;
using Services.Mappers;

namespace Services;

public class TodoService(ITodoRepository repository) : ITodoService
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

    public TodoDTO EditTodo(Guid id, string editedTitle)
    {
        return TodoMapper.ToDto(repository.EditTodo(id, editedTitle));
    }

    public bool DeleteTodo(Guid id)
    {
        return repository.DeleteTodo(id);
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

    public TodoDTO CompleteTodo(Guid id)
    {
        return TodoMapper.ToDto(repository.CompleteTodo(id));
    }

    public TodoDTO UncompleteTodo(Guid id)
    {
        return TodoMapper.ToDto(repository.UncompleteTodo(id));
    }
    
}