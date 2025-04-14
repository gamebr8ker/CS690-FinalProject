namespace ExpenseTrackerApp;

using Spectre.Console;



public class ConsoleUI {

    DataManager dataManager;



    public ConsoleUI() {

        Console.WriteLine("Here we go...");
        dataManager = new DataManager();
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

            string expenseMode;

            do {
                foreach(var lineitem in dataManager.Expenses) {
                    Console.WriteLine(lineitem);
                }

                expenseMode = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("What would you like to do?")
                .AddChoices( new[] {
                    "Create",
                    "Edit",
                    "Back"
                    }
                    )
                );


                if( expenseMode == "Create") {
                    // WIP
                    int newExpenseID  = FindMaxID(dataManager.Expenses) + 1;

                    string newExpenseDescr = AnsiConsole.Prompt(
                        new TextPrompt<string>(
                            "Enter an expense description")
                    ); 

                    int newExpense_Year = AnsiConsole.Prompt(
                        new TextPrompt<int>(
                            "Enter an expense year")
                    );

                    int newExpense_Month = AnsiConsole.Prompt(
                        new TextPrompt<int>(
                            "Enter an expense month")
                    );

                    int newExpense_Day = AnsiConsole.Prompt(
                        new TextPrompt<int>(
                            "Enter an expense day")
                    );

                    
                    //DateTime newExpenseDate = DateTime.Now; 
                    DateTime newExpenseDate = new DateTime(
                        newExpense_Year, newExpense_Month, 
                        newExpense_Day
                    );


                    float newExpenseAmount = AnsiConsole.Prompt(
                        new TextPrompt<float>(
                            "Enter an expense amount")
                    );


                    int newExpenseCatID = 1; 


                    Expense newExpense = new Expense(
                        newExpenseID, newExpenseDescr, 
                        newExpenseDate, newExpenseAmount,
                        newExpenseCatID
                    );

                    Console.WriteLine(newExpense + Environment.NewLine);

                    dataManager.AddNewExpenseData(newExpense);

                }



                else if( expenseMode == "Edit") {
                    Console.WriteLine("This feature is slated for V2.0.0");
                }

            } while (expenseMode != "Back");
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




    /// Get the max ID value from an input list
    public static int FindMaxID(List<Expense> someList) {

        /*
        // Original Idea (value-based)
        int maxID = 0;


        foreach(var lineitem in someList) {
            var splitted = lineitem.Split(
                ", ", StringSplitOptions.RemoveEmptyEntries
            );

            line_id = splitted[0];

            if( line_id > maxID) {
                maxID = line_id;
            }
        */

        int maxID = someList.Count;

        return maxID;

        } 



}

