namespace ExpenseTrackerApp;

using Spectre.Console;



public class ConsoleUI {

    DataManager dataManager;



    public ConsoleUI() {

        Console.WriteLine(
            Environment.NewLine +
            "Welcome to the Expense Tracker App" +
            Environment.NewLine);
        dataManager = new DataManager();
    }



    public void Show() {

        /// Main App Mode
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
                // List latest 10 expense entries
                int expensesEndInd = dataManager.Expenses.Count();
                int expensesStartInd;

                if( expensesEndInd <= 10 ) {
                    expensesStartInd = 0;
                } else {
                    expensesStartInd = expensesEndInd - 10;
                }
                
                Console.WriteLine(
                    Environment.NewLine +
                    "Last 10 Expense entries: " +
                    Environment.NewLine
                );



                // Print the Expenses
                for( 
                    int index = expensesStartInd; 
                    index < expensesEndInd;
                    index++
                ) {
                    Console.WriteLine(dataManager.Expenses[index]);
                }


                // Print a break before Prompt
                Console.WriteLine(
                    "--------------------" + 
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

                    
                    // Print confirmation message
                    Console.WriteLine(
                        Environment.NewLine + 
                        "New Expense (" + 
                        newExpense.ID 
                        + ") added." +
                        Environment.NewLine
                    );
                    


                    dataManager.AddNewExpenseData(newExpense);

                }





                else if( expenseMode == "Edit") {

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

                        Console.WriteLine(
                            "Selected: " +
                            selectedExpense + 
                            Environment.NewLine +
                            "---------------"
                        );


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
                        
                            // Print confirmation message
                            Console.WriteLine(
                                "Expense (" + 
                                selectedExpense.ID 
                                + ") deleted." +
                                Environment.NewLine
                            );
                        
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



                            // Print confirmation message
                            Console.WriteLine(
                                "Expense (" + 
                                selectedExpense.ID 
                                + ") modified." +
                                Environment.NewLine
                            );



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
        }    // End Expenses Sub-Menu






        


        /// Categories Submenu
        else if ( mode == "Create / Edit Categories" ) {
            Console.WriteLine(
                Environment.NewLine + 
                "Categories List: " +
                Environment.NewLine
            );

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


                /// Print Categories, via Spectre Console Table
                var categoryTable = new Table();

                categoryTable.AddColumn("ID");
                categoryTable.AddColumn("Category Name");
                categoryTable.AddColumn("Budget Amount");
                categoryTable.AddColumn("Enabled");

                foreach( var lineitem in dataManager.Categories) {
                    categoryTable.AddRow(
                        lineitem.ID.ToString(),
                        lineitem.Name,
                        lineitem.Budget_Amount.ToString(),
                        lineitem.Enabled.ToString()
                    );
                }

                AnsiConsole.Write(categoryTable);


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


                    // Print confirmation message
                    Console.WriteLine(
                        Environment.NewLine + 
                        "New Category (" + 
                        newCategory.ID 
                        + ") added." +
                        Environment.NewLine
                    );



                    dataManager.AddNewCategoryData(newCategory);


                }





                else if( categoryMode == "Edit") {

                    Category selectedCategory = AnsiConsole.Prompt(
                        new SelectionPrompt<Category>()
                            .Title("Select a Category to modify: ")
                            .AddChoices(dataManager.Categories)
                    );



                    

                    string selectedModification;

                    do {
                    Console.WriteLine(
                        "Selected: " +
                        selectedCategory + 
                        Environment.NewLine +
                        "---------------"
                    );


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

                        // Print confirmation message
                        Console.WriteLine(
                            "Category (" + 
                            selectedCategory.ID 
                            + ") deleted." +
                            Environment.NewLine
                        );
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


                    // Print confirmation message
                    Console.WriteLine(
                        "Category (" + 
                        selectedCategory.ID +
                        ") modified." +
                        Environment.NewLine
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
        }    // End Categories Sub-Menu










        /// Notifications Sub-Menu
        else if ( mode == "Create / Edit Notifications" ) {
            Console.WriteLine(
                "Selected Notifications" + 
                "This feature is still under development and is currently slated for v2.0.0" );

        }










        /// Performance Sub-Menu
        else if ( mode == "Performance") {

            /*
            // This is just a test to see how one might sort a list of objects
            var unsorted = dataManager.Expenses;

            var sorted = unsorted.OrderBy(x => x.Date).ToList();

            foreach( var lineitem in sorted ) {
                Console.WriteLine(lineitem);
            }
            */

            // Create the underlying data structure for Spectre Table
            SortedDictionary<String, List<float>> performanceDict = new SortedDictionary<String, List<float>>();


            var sortedExpenses = dataManager.Expenses.OrderBy( x => x.Date).ToList();

            foreach( Expense lineitem in sortedExpenses ) {

                // Collect lineitem values
                int dictYear = lineitem.Date.Year;
                int dictMonth = lineitem.Date.Month;
                int dictDay = 1;
                DateTime dictDateTime = new DateTime(dictYear, dictMonth, dictDay);


                int expenseCategoryID = lineitem.ExpenseCategoryID;
                string relatedCategoryName = "";
                float budgetAmount = (float)0;
                float expenseAmount = lineitem.Amount;



                // Look up the associated Category Name, Budget for the expense
                foreach(Category categoryItem in dataManager.Categories) {
                    if(categoryItem.ID == expenseCategoryID) {
                        relatedCategoryName = categoryItem.Name;
                        budgetAmount = categoryItem.Budget_Amount;
                    }
                }



                // Create dictionary key: String
                string dictKeyName = 
                    dictDateTime.ToString("MM-yyyy") + "_" + relatedCategoryName;



                // Add new key to Dict Keys if not present
                if( !performanceDict.ContainsKey(dictKeyName) ) {
                    performanceDict[dictKeyName] = new List<float>();
                    performanceDict[dictKeyName].Add( (float)budgetAmount );  // Budget
                    performanceDict[dictKeyName].Add( (float)0 );    // Expenses
                }



                // Add Expense lineitem Amount to existing dict entry
                performanceDict[dictKeyName][1] += expenseAmount;


            }





            // Generate Spectre Console Table
            var performanceTable = new Table();


            // Add columns to Spectre Table
            //performanceTable.AddColumn("Date_Category");
            performanceTable.AddColumn("Date");
            performanceTable.AddColumn("Category");
            performanceTable.AddColumn("Budget Amount");
            performanceTable.AddColumn("Expense Sum");


            // Add Rows to Spectre Table
            foreach( KeyValuePair<String, List<float>> keypair in performanceDict) {

                string[] subs = keypair.Key.Split("_");

                // row values appear to need to be Strings
                performanceTable.AddRow(
                    //keypair.Key,
                    subs[0],
                    subs[1],
                    keypair.Value[0].ToString(),
                    keypair.Value[1].ToString()
                );
            }


            // Render the Spectre Table to Console
            Console.WriteLine(
                Environment.NewLine +
                "Your current monthly summary:" +
                Environment.NewLine
            );
            AnsiConsole.Write(performanceTable);


        }    // End Performance Sub-Menu





        else {
            Environment.Exit(0);
        }
    
    
    } while (mode != "End");    // Topmost-Do While
    
    
    
    }   // End Show() Method





    /// Get the max ID value from an input list
    /// This accounts properly for deletions from Expenses File / Index.
    public static int FindMaxID_Expense(List<Expense> someList) {

        int maxID;

        List<int> IDList = new List<int>();


        if( someList.Count > 0) {

            // Create list of Expense IDs
            foreach( Expense lineitem in someList ) {
                IDList.Add(lineitem.ID);
            }
            
            // Get Last value (greatest ID)
            IDList.Sort();
            maxID = IDList.Last();

        }
        else {
            maxID = 0;
        }


        return maxID;

        } 



    /// Gotta love confusing type-enforcement rules on lists...
    public static int FindMaxID_Category(List<Category> someList) {
        
        int maxID;

        List<int> IDList = new List<int>();


        if( someList.Count > 0) {

            // Create list of Category IDs
            foreach( Category lineitem in someList ) {
                IDList.Add(lineitem.ID);
            }
            
            // Get Last value (greatest ID)
            IDList.Sort();
            maxID = IDList.Last();

        }
        else {
            maxID = 0;
        }


        return maxID;
    }




}

