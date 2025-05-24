using Spectre.Console;

namespace TodoListCMD.Menu;

public class MainMenu
{
    private static string[] menuOptions = new string[]
    {
        "Display todos",
        "Add todo",
        "Remove todos",
        "Edit todos",
        "Exit"
    };
    
    public static string DisplayMenu()
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Please select an option.")
                .PageSize(10)
                .AddChoices(menuOptions)
        );
    }

    public static void RunSelection(string option)
    {
        switch (option)
        {
            case "Display todos":
                Console.WriteLine("Displaying todos");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
                break;
            case "Add todo":
                Console.WriteLine("Adding todos");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
                break;
            case "Remove todos":
                Console.WriteLine("Removing todos");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
                break;
            case "Edit todos":
                Console.WriteLine("Editing todos");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
                break;
            default:
                Console.WriteLine("Please select a valid option.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
                break;
        }
    }
}