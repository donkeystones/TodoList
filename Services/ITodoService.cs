using Services.DTO;

namespace Services;

public interface ITodoService
{
    TodoDTO AddTodo(string title);
    TodoDTO EditTodo(Guid id, string editedTitle);
    bool DeleteTodo(Guid id);
    List<TodoDTO> GetTodos();
}