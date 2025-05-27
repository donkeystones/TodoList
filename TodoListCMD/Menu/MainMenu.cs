using Services;
using Services.DTO;
using Spectre.Console;

namespace TodoListCMD.Menu;

public class MainMenu
{
    private static ITodoService _service = null!;

    public MainMenu(ITodoService service)
    {
        _service = service;
    }
    
    private static readonly string[] MenuOptions =
    [
        "Display todos",
        "Add todo",
        "Remove todo",
        "Edit todo",
        "Complete todo",
        "Uncomplete todo",
        "Exit"
    ];

    private static string DisplayMenu()
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold underline blue]Please select an option:[/]")
                .PageSize(10)
                .AddChoices(MenuOptions)
        );
    }

    public static void Run()
    {
        bool running = true;
        while (running)
        {
            Console.Clear();
            var option = DisplayMenu();
            running = option != "Exit";
            RunSelection(option);
            
            AnsiConsole.WriteLine(); // Add space after action
            AnsiConsole.MarkupLine("[grey]Press any key to return to menu...[/]");
            Console.ReadKey(true);
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
            case "Complete todo":
                CompleteTodo();
                break;
            case "Uncomplete todo":
                UncompleteTodo();
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
        var table = TableLayout("Todo Details");
        table.AddRow(todo.Title, todo.Date.ToString(), todo.Completed ? Emoji.Known.CheckMark : Emoji.Known.CrossMark);

        AnsiConsole.Write(table);
    }

    private static void DisplayTodos()
    {
        var todos = _service.GetTodos();
        if (todos.Count != 0)
        {
            var table = TableLayout("All Todos");
            foreach (var todo in todos)
            {
                table.AddRow(todo.Title, todo.Date.ToString(), todo.Completed ? Emoji.Known.CheckMark : Emoji.Known.CrossMark);
            }
            AnsiConsole.Write(table);
        }
        else
        {
            AnsiConsole.MarkupLine("[italic grey]No todos registered![/]");
        }
    }

    private static void DeleteTodo()
    {
        var todos = _service.GetTodos();
        var todoToRemove = SelectTodo("remove", todos);

        var deleted = _service.DeleteTodo(todoToRemove.Id);
        AnsiConsole.MarkupLine(deleted
            ? $"[green]\"{todoToRemove.Title}\" removed successfully![/]"
            : $"[red]Delete failed![/]");
    }

    private static void EditTodo()
    {
        var todos = _service.GetTodos();
        var todoToEdit = SelectTodo("edit", todos);

        var newTitle = AnsiConsole.Prompt(
            new TextPrompt<string>("")
                .DefaultValue(todoToEdit.Title)
                .AllowEmpty()
        );

        if (newTitle != todoToEdit.Title){
            var todo = _service.EditTodo(todoToEdit.Id, newTitle);
            AnsiConsole.WriteLine($"Edited \"{todoToEdit.Title}\" to \"{todo.Title}\"");
        }
        else AnsiConsole.WriteLine("Title was not changed.");
    }

    private static TodoDTO SelectTodo(string promptTitle, List<TodoDTO> todos)
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<TodoDTO>()
                .Title($"Which todo do you want to {promptTitle}?")
                .PageSize(10)
                .UseConverter(todo => todo.Title)
                .AddChoices(todos.ToArray()));
    }
    
    private static void CompleteTodo()
    {
        var todos = _service.GetTodos();
        todos = todos.Where(todo => !todo.Completed).ToList();
        if (todos.Count == 0)
        {
            AnsiConsole.MarkupLine("[green]No todos to complete![/]");
        }else {
            var todoToComplete = SelectTodo("complete", todos);

            _service.CompleteTodo(todoToComplete.Id);
            AnsiConsole.MarkupLine($":check_mark_button: [green]{todoToComplete.Title} set as completed![/]");
            AnsiConsole.MarkupLine("[bold yellow]Nice work, keep it up![/]");
        }
    }

    private static void UncompleteTodo()
    {
        var todos = _service.GetTodos();
        todos = todos.Where(todo => todo.Completed).ToList();
        if (todos.Count == 0)
        {
            AnsiConsole.MarkupLine("[green]No todos to uncomplete![/]");
        }else {
            var todoToComplete = SelectTodo("complete", todos);

            _service.UncompleteTodo(todoToComplete.Id);
            AnsiConsole.MarkupLine($":cross_mark: [green]{todoToComplete.Title} set as uncompleted![/]");
            AnsiConsole.MarkupLine("[bold yellow]Nice work, keep it up![/]");
        }
    }
    
    private static Table TableLayout(string tableTitle)
    {
        var table = new Table();
        table.Title = new TableTitle(tableTitle);
        table.AddColumn(new TableColumn("Title"));
        table.AddColumn(new TableColumn("Date"));
        table.AddColumn(new TableColumn("Done"));
        return table;
    }
    
}