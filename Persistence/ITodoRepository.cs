using Persistence.Models;

namespace Persistence;

public interface ITodoRepository
{
    void AddTodo(TodoItem todo);
    void EditTodo(Guid id, string editedTitle);
    void DeleteTodo(Guid id);
    List<TodoItem> GetTodos();
    
}