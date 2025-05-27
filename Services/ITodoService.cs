using Services.DTO;

namespace Services;

public interface ITodoService
{
    TodoDTO AddTodo(string title);
    TodoDTO EditTodo(Guid id, string editedTitle);
    TodoDTO CompleteTodo(Guid id);
    TodoDTO UncompleteTodo(Guid id);
    bool DeleteTodo(Guid id);
    List<TodoDTO> GetTodos();
}