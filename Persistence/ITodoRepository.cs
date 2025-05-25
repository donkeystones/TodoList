using Persistence.Models;

namespace Persistence;

public interface ITodoRepository
{
    TodoItem AddTodo(TodoItem todo);
    TodoItem EditTodo(Guid id, string editedTitle);
    void DeleteTodo(Guid id);
    List<TodoItem> GetTodos();
    
}