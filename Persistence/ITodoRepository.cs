using Persistence.Models;

namespace Persistence;

public interface ITodoRepository
{
    TodoItem AddTodo(TodoItem todo);
    TodoItem EditTodo(Guid id, string editedTitle);
    bool DeleteTodo(Guid id);
    TodoItem CompleteTodo(Guid id);
    List<TodoItem> GetTodos();
    
}