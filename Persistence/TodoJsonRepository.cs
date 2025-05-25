using System.Text.Json;
using Persistence.Models;

namespace Persistence;

public class TodoJsonRepository
{
    private readonly string _filePath;

    public TodoJsonRepository(string filePath)
    {
        _filePath = filePath;
        
        if(!File.Exists(_filePath))
            File.WriteAllText(_filePath, "[]"); //Creates file and adds empty array
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
    
    private void SaveTodos(List<TodoItem> todos)
    {
        var json = JsonSerializer.Serialize(todos, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(_filePath, json);
    }
}