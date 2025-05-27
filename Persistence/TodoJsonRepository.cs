using System.Text.Json;
using Persistence.Models;

namespace Persistence;

public class TodoJsonRepository : ITodoRepository
{
    private readonly string _filePath;

    public TodoJsonRepository(string filePath)
    {
        _filePath = filePath;
        
        if(!File.Exists(_filePath))
            File.WriteAllText(_filePath, "[]"); //Creates file and adds empty array
    }

    public bool DeleteTodo(Guid id)
    {
        try
        {
            var todos = GetTodos();
            todos = todos.Where(todo => todo.Id != id).ToList();
            SaveTodos(todos);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public List<TodoItem> GetTodos()
    {
        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<TodoItem>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? new List<TodoItem>();
    }
    
    public TodoItem AddTodo(TodoItem item)
    {
        var todos = GetTodos();
        todos.Add(item);
        SaveTodos(todos);
        return item;
    }

    public TodoItem EditTodo(Guid id, string editedTitle)
    {
        throw new NotImplementedException();
    }

    private void SaveTodos(List<TodoItem> todos)
    {
        var json = JsonSerializer.Serialize(todos, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(_filePath, json);
    }
}