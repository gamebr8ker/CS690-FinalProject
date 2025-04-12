namespace ExpenseTrackerApp;

using Spectre.Console;



public class ConsoleUI {

    public ConsoleUI() {

        Console.WriteLine("Here we go...");
    }



    public void Show() {

        var mode = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Select a task")
            .AddChoices( new[] {
                "Create / Edit Expenses",
                "Create / Edit Categories",
                "Create / Edit Notifications",
                "Performance",
                "End"
                }
            )
        );


        


        
        if( mode == "Create / Edit Expenses" ) {
            Console.WriteLine("Selected Expenses");
        } 
        
        
        else if ( mode == "Create / Edit Categories" ) {
            Console.WriteLine("Selected Categories");
        }


        else if ( mode == "Create / Edit Notifications" ) {
            Console.WriteLine("Selected Notifications");
        }


        else if ( mode == "Performance") {
            Console.WriteLine("Selected Performance");
        }

        else {
            Environment.Exit(0);
        }
    }



}