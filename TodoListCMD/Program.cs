// See https://aka.ms/new-console-template for more information

//Storage Configuration


//Running program

using TodoListCMD.Menu;

bool running = true;
while (running)
{
    string option = MainMenu.DisplayMenu();
    if (option == "Exit") running = false;
    else MainMenu.RunSelection(option);
}

Console.WriteLine("Exiting program!");