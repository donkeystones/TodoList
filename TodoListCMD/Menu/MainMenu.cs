using Persistence;
using Services;
using Services.DTO;
using Spectre.Console;

namespace TodoListCMD.Menu;

public class MainMenu
{
    private static ITodoService _service;

    public MainMenu(ITodoService service)
    {
        _service = service;
    }
    
    private static string[] menuOptions =
    [
        "Display todos",
        "Add todo",
        "Remove todo",
        "Edit todo",
        "Exit"
    ];

    private static string DisplayMenu()
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Please select an option.")
                .PageSize(10)
                .AddChoices(menuOptions)
        );
    }

    public static void Run()
    {
        bool running = true;
        while (running)
        {
            var option = DisplayMenu();
            running = option != "Exit";
            RunSelection(option);
        }
    }

    private static void RunSelection(string option)
    {
        switch (option)
        {
            case "Display todos":
                DisplayTodos();
                break;
            case "Add todo":
                AddTodo();
                break;
            case "Remove todo":
                DeleteTodo();
                break;
            case "Edit todo":
                EditTodo();
                break;
            case "Exit":
                break;
            default:
                Console.WriteLine("Please select a valid option.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
                break;
        }
    }

    private static void AddTodo()
    {
        var title = AnsiConsole.Prompt(new TextPrompt<string>("Title for your todo"));
        var todo = _service.AddTodo(title);
        DisplayTodo(todo);
    }

    private static void DisplayTodo(TodoDTO todo)
    {
        var table = TableLayout();
        
        table.AddRow(todo.Id.ToString(), todo.Title, todo.Date.ToString(), todo.Completed ? Emoji.Known.CheckMark : Emoji.Known.CrossMark);

        AnsiConsole.Write(table);
    }

    private static void DisplayTodos()
    {
        var todos = _service.GetTodos();
        var table = TableLayout();
        
        foreach (TodoDTO todo in todos)
        {
            table.AddRow(todo.Id.ToString(), todo.Title, todo.Date.ToString(), todo.Completed ? Emoji.Known.CheckMark : Emoji.Known.CrossMark);
        }
        
        AnsiConsole.Write(table);
    }

    private static void DeleteTodo()
    {
        var todos = _service.GetTodos();
        var todoToRemove = AnsiConsole.Prompt(
            new SelectionPrompt<TodoDTO>()
            .Title("Which todo do you want to remove?")
            .PageSize(10)
            .UseConverter(todo => todo.Title)
            .AddChoices(todos.ToArray()));

        var deleted = _service.DeleteTodo(todoToRemove.Id);
        Console.WriteLine(deleted ? $"\"{todoToRemove.Title}\" Removed!" : "Delete failed!");
    }

    private static void EditTodo()
    {
        var todos = _service.GetTodos();
        var todoToEdit = AnsiConsole.Prompt(
            new SelectionPrompt<TodoDTO>()
                .Title("Which todo do you want to edit?")
                .PageSize(10)
                .UseConverter(todo => todo.Title)
                .AddChoices(todos.ToArray()));

        var newTitle = AnsiConsole.Prompt(
            new TextPrompt<string>("").DefaultValue(todoToEdit.Title)
        );

        var todo = _service.EditTodo(todoToEdit.Id, newTitle);
        
        AnsiConsole.WriteLine($"Edited \"{todoToEdit.Title}\" to \"{todo.Title}\"");
    }

    private static Table TableLayout()
    {
        var table = new Table();
        table.AddColumn(new TableColumn("Id"));
        table.AddColumn(new TableColumn("Title"));
        table.AddColumn(new TableColumn("Date"));
        table.AddColumn(new TableColumn("Done"));
        return table;
    }
}