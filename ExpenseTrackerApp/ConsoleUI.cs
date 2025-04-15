namespace ExpenseTrackerApp;

using Spectre.Console;



public class ConsoleUI {

    DataManager dataManager;



    public ConsoleUI() {

        Console.WriteLine("Here we go...");
        dataManager = new DataManager();
    }



    public void Show() {

        string mode;

        do {

        mode = AnsiConsole.Prompt(
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


        


        /// Expenses Sub-Menu
        if( mode == "Create / Edit Expenses" ) {
            Console.WriteLine("Selected Expenses");

            string expenseMode;


            // Create current list of Enabled Categories
            // for assigning Expenses to.
            List<Category> enabledCategories = new List<Category>();
            
            foreach( var lineItem in dataManager.Categories ) {
                if (lineItem.Enabled == true ) {
                    enabledCategories.Add(lineItem);
                }

            }



            do {
                // Print the Expenses
                foreach(var lineitem in dataManager.Expenses) {
                    Console.WriteLine(lineitem);
                }

                // Print a break before Prompt
                Console.WriteLine("--------------------" + 
                    Environment.NewLine
                );


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
                    
                    // Gather values to create new Expense entry
                    int newExpenseID  = FindMaxID_Expense(dataManager.Expenses) + 1;

                    string newExpenseDescr = AnsiConsole.Prompt(
                        new TextPrompt<string>(
                            "Enter an expense description: ")
                    ); 

                    int newExpense_Year = AnsiConsole.Prompt(
                        new TextPrompt<int>(
                            "Enter an expense year: ")
                    );

                    int newExpense_Month = AnsiConsole.Prompt(
                        new TextPrompt<int>(
                            "Enter an expense month: ")
                    );

                    int newExpense_Day = AnsiConsole.Prompt(
                        new TextPrompt<int>(
                            "Enter an expense day: ")
                    );

                    
                    //DateTime newExpenseDate = DateTime.Now; 
                    DateTime newExpenseDate = new DateTime(
                        newExpense_Year, newExpense_Month, 
                        newExpense_Day
                    );


                    float newExpenseAmount = AnsiConsole.Prompt(
                        new TextPrompt<float>(
                            "Enter an expense amount: $")
                    );



                    Category newExpenseCategory = AnsiConsole.Prompt(
                        new SelectionPrompt<Category>()
                        .Title("Select a Category")
                        .AddChoices(enabledCategories)
                    );


                    int newExpenseCatID = newExpenseCategory.ID;
                    //int newExpenseCatID = 1;

                    Expense newExpense = new Expense(
                        newExpenseID, newExpenseDescr, 
                        newExpenseDate, newExpenseAmount,
                        newExpenseCatID
                    );

                    /*
                    Console.WriteLine(newExpense + Environment.NewLine);
                    */


                    dataManager.AddNewExpenseData(newExpense);

                }



                else if( expenseMode == "Edit") {
                    //Console.WriteLine("This feature is slated for V2.0.0");

                    // List existing expenses
                    foreach( var lineitem in dataManager.Expenses ) {
                        Console.WriteLine(lineitem);
                    }

                    Console.WriteLine(
                        "--------------------" + 
                        Environment.NewLine
                    );


                    // Prompt user to select a record
                    Expense selectedExpense = AnsiConsole.Prompt(
                        new SelectionPrompt<Expense>()
                        .Title("Select an Expense to modify: ")
                        .AddChoices(dataManager.Expenses)
                    );



                    string selectedModification;

                    do {
                        selectedModification = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                            .Title("Which would you like to modify?")
                            .AddChoices( new[] {
                                "Description",
                                "Date",
                                "Amount",
                                "Expense Category",
                                "Delete",
                                "Back"
                            }
                            )
                        );



                        // Set the updated value and update the record
                        if( selectedModification == "Delete" ) {
                            dataManager.RemoveExpenseData(selectedExpense);
                        
                        
                        }
                        else if( selectedModification == "Description" ) {
                            
                            var newExpenseDescr = AnsiConsole.Prompt(
                                new TextPrompt<string>("Enter new Expense Description: ")
                            );

                            dataManager.EditExpenseData(
                                expensesList: dataManager.Expenses,
                                existingData: selectedExpense,
                                editParam: selectedModification,
                                newDescr: newExpenseDescr
                            );
                            

                        }
                        else if( selectedModification == "Date" ) {

                            int newExpense_Year = AnsiConsole.Prompt(
                                new TextPrompt<int>(
                                    "Enter an expense year: ")
                            );

                            int newExpense_Month = AnsiConsole.Prompt(
                                new TextPrompt<int>(
                                    "Enter an expense month: ")
                            );

                            int newExpense_Day = AnsiConsole.Prompt(
                                new TextPrompt<int>(
                                    "Enter an expense day: ")
                            );

                            DateTime newExpenseDate = new DateTime(
                                newExpense_Year, newExpense_Month,
                                newExpense_Day
                            );

                            dataManager.EditExpenseData(
                                expensesList: dataManager.Expenses,
                                existingData: selectedExpense,
                                editParam: selectedModification,
                                newDateTime: newExpenseDate
                            );


                        }
                        else if( selectedModification == "Amount" ) {

                            float newExpenseAmount = AnsiConsole.Prompt(
                                new TextPrompt<float>(
                                    "Enter a new expense amount: $")
                            );

                            dataManager.EditExpenseData(
                                expensesList: dataManager.Expenses,
                                existingData: selectedExpense,
                                editParam: selectedModification,
                                newAmount: newExpenseAmount
                            );


                        }
                        else if( selectedModification == "Expense Category" ) {
                            
                            Category newExpenseCategory = AnsiConsole.Prompt(
                                new SelectionPrompt<Category>()
                                .Title("Select a new Category")
                                .AddChoices(enabledCategories)
                            );


                            int newExpenseCatID = newExpenseCategory.ID;


                            dataManager.EditExpenseData(
                                expensesList: dataManager.Expenses,
                                existingData: selectedExpense,
                                editParam: selectedModification,
                                newCategoryID: newExpenseCatID
                            );


                        }
                        










                    } while( selectedModification != "Back" );


                }


            } while (expenseMode != "Back");
        } 






        



        else if ( mode == "Create / Edit Categories" ) {
            Console.WriteLine("Selected Categories");

            string categoryMode;

            // Create the Default / general Category if needed.
            if( dataManager.Categories.Count == 0 ) {
                Category InitialCategory = new Category(
                        1, "Default Category", true, (float)0.00
                );

                dataManager.AddNewCategoryData(InitialCategory);
            }



            // Categories Sub-Menu
            do {

                // Print the Categories
                foreach( var lineitem in dataManager.Categories) {
                    Console.WriteLine(lineitem);
                }

                // Print a break before Prompt
                Console.WriteLine("--------------------" + 
                    Environment.NewLine
                );


                categoryMode = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("What would you like to do?")
                    .AddChoices( new[] {
                        "Create",
                        "Edit",
                        "Back"
                    }
                    )
                );



                if( categoryMode == "Create" ) {
                    
                    // Gather values to create new Category entry
                    int newCategoryID = FindMaxID_Category(dataManager.Categories) + 1;

                    string newCategoryName = AnsiConsole.Prompt(
                        new TextPrompt<string>(
                            "Enter a category name: ")
                    );
                    
                    bool newCategoryEnabled = true;

                    float newCategoryBudgetAmount = AnsiConsole.Prompt(
                        new TextPrompt<float>(
                            "Enter a budget amount for the category: $")
                    );



                    Category newCategory = new Category(
                        newCategoryID, newCategoryName,
                        newCategoryEnabled, newCategoryBudgetAmount
                    );


                    /*
                    Console.WriteLine(
                        Environment.NewLine + newCategory +
                        Environment.NewLine
                    );
                    */

                    dataManager.AddNewCategoryData(newCategory);


                }







                else if( categoryMode == "Edit") {
                    
                    /// List all available Categories
                    foreach( var lineitem in dataManager.Categories) {
                        Console.WriteLine(lineitem);
                    }

                    Console.WriteLine(
                        "--------------------" + 
                        Environment.NewLine
                    );


                    Category selectedCategory = AnsiConsole.Prompt(
                        new SelectionPrompt<Category>()
                            .Title("Select a Category to modify: ")
                            .AddChoices(dataManager.Categories)
                    );



                    string selectedModification;

                    do {
                    selectedModification = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .Title("Which would you like to modify?")
                        .AddChoices( new[] {
                            "Name",
                            "Budget Amount",
                            "Enabled",
                            "Delete",
                            "Back"
                        }
                        )
                    );


                    /// Set the updated value and update the record
                    if( selectedModification == "Delete") {
                        dataManager.RemoveCategoryData(selectedCategory);
                    } 
                    else if ( selectedModification == "Name" ) {
                        
                        var newCategoryName = AnsiConsole.Prompt(
                            new TextPrompt<string>("Enter new Category Name: ")
                        );

                        dataManager.EditCategoryData(
                            categoriesList: dataManager.Categories, 
                            existingData: selectedCategory,
                            editParam: selectedModification,
                            newName: newCategoryName
                        );

                    }
                    else if( selectedModification == "Budget Amount") {
                        
                        var newCategoryBudget = AnsiConsole.Prompt(
                            new TextPrompt<float>("Enter a new Budget Amount: ")
                        );

                        dataManager.EditCategoryData(
                            categoriesList: dataManager.Categories, 
                            existingData: selectedCategory,
                            editParam: selectedModification,
                            newBudgetAmount: newCategoryBudget
                        );
                    }
                    else if( selectedModification == "Enabled") {
                        
                        var newCategoryEnabled = AnsiConsole.Prompt(
                            new SelectionPrompt<bool>()
                            .Title("Should this Category be Enabled (true) or Disabled (false)? ")
                            .AddChoices(new[] {true, false})
                        );

                        dataManager.EditCategoryData(
                            categoriesList: dataManager.Categories, 
                            existingData: selectedCategory,
                            editParam: selectedModification,
                            newEnabled: newCategoryEnabled
                        );
                    }
                    } while( selectedModification != "Back");




                }


            } while (categoryMode != "Back");
        }










        else if ( mode == "Create / Edit Notifications" ) {
            Console.WriteLine(
                "Selected Notifications" + 
                "This feature is still under development and is currently slated for v2.0.0" );

        }










        else if ( mode == "Performance") {
            Console.WriteLine(
                "Selected Performance" + 
                "This feature is still under development and is currently slated for v2.0.0"
            );


                        /*
            // This is just a test to see how one might sort a list of objects
            var unsorted = dataManager.Expenses;

            var sorted = unsorted.OrderBy(x => x.Date).ToList();

            foreach( var lineitem in sorted ) {
                Console.WriteLine(lineitem);
            }
            */
        }





        else {
            Environment.Exit(0);
        }
    
    
    } while (mode != "End");    // Topmost-Do While
    
    
    
    } 




    /// Get the max ID value from an input list
    public static int FindMaxID_Expense(List<Expense> someList) {

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

        int maxID;

        if( someList.Count > 0 ) {
            maxID = someList.Count;
        } 
        
        else {
            maxID = 0;
        }

        return maxID;

        } 



    /// Gotta love confusing type-enforcement rules on lists...
    public static int FindMaxID_Category(List<Category> someList) {
        
        int maxID;

        if( someList.Count > 0 ) {
            maxID = someList.Count;
        } 
        
        else {
            maxID = 0;
        }

        return maxID;
    }




}

