namespace ExpenseTrackerApp;

using Spectre.Console;



public class ConsoleUI {

    DataManager dataManager;



    public ConsoleUI() {

        Console.WriteLine(
            Environment.NewLine +
            "Expense Tracker App" +
            Environment.NewLine);
        dataManager = new DataManager();
    }



    public void Show() {

        /// Main App Mode
        string mode;



        // Display upcoming Bill Notifications
        Console.WriteLine("Notifications");
        Console.WriteLine("Upcoming Bills:" + Environment.NewLine);
        List<Notification_Bill> disp = DisplayNotificationBills(
            dataManager.Notification_Bills);

        foreach( var entry in disp) {
            Console.WriteLine(entry);
        }

        Console.WriteLine(
            Environment.NewLine +
            "---------------");




        // Display Main Menu
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

                            selectedModification = "Back";
                        
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



                        // Print confirmation message
                        // Console.WriteLine(
                        //     "Expense (" + 
                        //     selectedExpense.ID 
                        //     + ") modified." +
                        //     Environment.NewLine
                        // );
                        




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
                    // Console.WriteLine(
                    //     Environment.NewLine + 
                    //     "New Category (" + 
                    //     newCategory.ID 
                    //     + ") added." +
                    //     Environment.NewLine
                    // );



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

                        selectedModification = "Back";
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

                    // Print confirmation message
                    Console.WriteLine(
                        "Category (" + 
                        selectedCategory.ID +
                        ") modified." +
                        Environment.NewLine
                    );


                    } while( selectedModification != "Back");


                }


            } while (categoryMode != "Back");
        }    // End Categories Sub-Menu










        /// Notifications Sub-Menu
        else if ( mode == "Create / Edit Notifications" ) {
            Console.WriteLine(
                "--------------------" + 
                Environment.NewLine 
            );


        
            string budgetMode;

            
            do {
            budgetMode =  AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("What would you like to do?")
                .AddChoices( new[] {
                    "Create / Edit Bill Notifications",
                    "Create / Edit Budget Notifications",
                    "Back"
                }
                )
            );



            if( budgetMode == "Create / Edit Bill Notifications" ) {
                Console.WriteLine("Selected: Create / Edit Bill Notifications");

                string budgetModeSub;

                do {

                // Write Enabled NBills to Screen
                Console.WriteLine("Enabled Bill Notifications:");
                foreach( var lineitem in dataManager.Notification_Bills) {
                    if( lineitem.Enabled == true ){
                        Console.WriteLine(lineitem);
                    }
                }

                budgetModeSub = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("What would you like to do?")
                    .AddChoices( new[] {
                        "Create",
                        "Edit",
                        "Back"
                    }
                    )
                );



                if( budgetModeSub == "Create") {

                    // Gather values to create new Notification_Bill entry
                    int newNBillID = FindMaxID_NBill(dataManager.Notification_Bills) + 1;

                    string newNBillDescription = AnsiConsole.Prompt(
                        new TextPrompt<string>(
                            "Enter a bill notification description: ")
                    );

                    int newNBillDue_Day = AnsiConsole.Prompt(
                        new TextPrompt<int>(
                            "Enter a due day (e.g. 1, 15, 20, 30): ")
                    );

                    float newNBillAmount = AnsiConsole.Prompt(
                        new TextPrompt<float>(
                            "Enter a bill notification amount: ")
                    );

                    bool newNBillEnabled = true;

                    Notification_Bill newNBill = new Notification_Bill(
                        newNBillID, newNBillDescription, newNBillDue_Day,
                        newNBillAmount, newNBillEnabled
                    );


                    // Print confirmation message
                    Console.WriteLine(
                        Environment.NewLine + 
                        "New Bill Notification (" + 
                        newNBill.ID 
                        + ") added." +
                        Environment.NewLine
                    );


                    dataManager.AddNewNBillData(newNBill);

                }



                else if( budgetModeSub == "Edit" ) {
                    
                    Notification_Bill selectedNBill = AnsiConsole.Prompt(
                        new SelectionPrompt<Notification_Bill>()
                        .Title("Select a Bill Notification to modify: ")
                        .AddChoices(dataManager.Notification_Bills)
                    );


                    string selectedModification;

                    do{
                    Console.WriteLine(
                        "Selected: " +
                        selectedNBill + 
                        Environment.NewLine +
                        "---------------"
                    );
                    

                    selectedModification = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .Title("Which would you like to modify?")
                        .AddChoices( new[] {
                            "Description",
                            "Due Day",
                            "Amount",
                            "Enabled",
                            "Delete",
                            "Back"
                        }
                        )
                    );



                    // Set the updated value and update the record
                    if( selectedModification == "Delete") {
                        dataManager.RemoveNotificationBillsData(selectedNBill);

                        // Print confirmation message
                        Console.WriteLine(
                            "Bill Notification (" +
                            selectedNBill.ID +
                            ") deleted." +
                            Environment.NewLine
                        );

                        selectedModification = "Back";
                    }

                    else if( selectedModification == "Description") {
                        var newNBillDescription = AnsiConsole.Prompt(
                            new TextPrompt<string>("Enter new Bill Notification description: ")
                        );

                        dataManager.EditNotificationBillData(
                            nBillsList: dataManager.Notification_Bills,
                            existingData: selectedNBill,
                            editParam: "Description",
                            newDescr: newNBillDescription
                        );
                    }

                    else if( selectedModification == "Due Day") {
                        
                        var newNBillDueDay = AnsiConsole.Prompt(
                            new TextPrompt<int>("Enter a new due day: ")
                        );

                        dataManager.EditNotificationBillData(
                            nBillsList: dataManager.Notification_Bills,
                            existingData: selectedNBill,
                            editParam: selectedModification,
                            newDueDay: newNBillDueDay
                        );
                    }

                    else if( selectedModification == "Amount") {

                        var newNBillAmount = AnsiConsole.Prompt(
                            new TextPrompt<float>("Enter a new amount: ")
                        );

                        dataManager.EditNotificationBillData(
                            nBillsList: dataManager.Notification_Bills,
                            existingData: selectedNBill,
                            editParam: selectedModification,
                            newAmount: newNBillAmount
                        );
                    }

                    else if( selectedModification == "Enabled") {

                        var newNBillEnabled = AnsiConsole.Prompt(
                            new SelectionPrompt<bool>()
                            .Title("Should this Bill Notification be Enabled (true) or Disabled (false)?")
                            .AddChoices(new[] {true, false})
                        );

                        dataManager.EditNotificationBillData(
                            nBillsList: dataManager.Notification_Bills,
                            existingData: selectedNBill,
                            editParam: selectedModification,
                            newEnabled: newNBillEnabled
                        );
                    }



                    } while(selectedModification != "Back");

                }



                } while (budgetModeSub != "Back");


            }







            else if( budgetMode == "Create / Edit Budget Notifications") {
                // Display
                Console.WriteLine("Selected: Create / Edit Budget Notifications");

                string budgetModeSub;

                do {

                // Write Enabled NBudgets to Screen
                Console.WriteLine("Enabled Budget Notifications:");
                foreach( var lineitem in dataManager.Notification_Budgets) {
                    if( lineitem.Enabled == true ) {
                        Console.WriteLine(lineitem);
                    }
                }

                budgetModeSub = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("What would you like to do?")
                    .AddChoices( new[] {
                        "Create",
                        "Edit",
                        "Back"
                    }
                    )
                );



                if( budgetModeSub == "Create") {

                    //Gather values to create new Notification_Budget entry
                    int newNBudgetID = FindMaxID_NBudget(
                        dataManager.Notification_Budgets) + 1;

                    int newNBudgetThresh_Day = AnsiConsole.Prompt(
                        new TextPrompt<int>(
                            "Enter a Threshold Day" + Environment.NewLine +
                            "(You will be notified if budget percent is reached / exceeded before this day)"
                        )
                    );


                    float newNBudgetTolerance_Pct = AnsiConsole.Prompt(
                        new TextPrompt<float>(
                            "Enter a Tolerance Percent" + Environment.NewLine +
                            "(You will be notified if this percent is reached / exceeded before the Threshold Day)"
                        )
                    );


                    Category newNBudgetCategory = AnsiConsole.Prompt(
                        new SelectionPrompt<Category>()
                        .Title("Select a Category for the Notification")
                        .AddChoices(dataManager.Categories)
                    );

                    int newNBudgetCat_ID = newNBudgetCategory.ID;


                    bool newNBudgetEnabled = true;



                    Notification_Budget newNBudget = new Notification_Budget(
                        newNBudgetID, newNBudgetThresh_Day, newNBudgetTolerance_Pct, newNBudgetCat_ID,
                        newNBudgetEnabled
                    );


                    // Print confirmation message
                    Console.WriteLine(
                        Environment.NewLine + 
                        "New Budget Notification (" + 
                        newNBudget.ID 
                        + ") added." +
                        Environment.NewLine
                    );


                    dataManager.AddNewNBudgetData(newNBudget);

            }


            else if( budgetModeSub == "Edit") {

                List<Notification_Budget_Aggregate> testList = 
                    DisplayNotificationBudgets2( 
                        dataManager.Notification_Budgets,
                        dataManager.Expenses,
                        dataManager.Categories
                    );
                
                foreach( var lineitem in testList ) {
                    Console.WriteLine(lineitem);
                }


                Notification_Budget selectedNBudget = AnsiConsole.Prompt(
                    new SelectionPrompt<Notification_Budget>()
                    .Title("Select a Budget Notification to modfiy: ")
                    .AddChoices(dataManager.Notification_Budgets)
                );


                string selectedModification;

                do{
                Console.WriteLine(
                    "Selected: " +
                    selectedNBudget + 
                    Environment.NewLine +
                    "---------------"
                );



                selectedModification = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("Which would you like to modify?")
                    .AddChoices( new[] {
                        "Threshold Day",
                        "Tolerance Percent",
                        "Expense Category",
                        "Enabled"
                    }
                    )
                );



                // Set the updated value and update the record
                if( selectedModification == "Delete") {
                    dataManager.RemoveNotificationBudgetsData(selectedNBudget);

                    // Print confirmation message
                    Console.WriteLine(
                        "Budget Notification (" +
                        selectedNBudget.ID +
                        ") deleted." +
                        Environment.NewLine
                    );

                    selectedModification = "Back";
                }


                else if( selectedModification == "Threshold Day") {
                    var newNBudgetThresh_Day = AnsiConsole.Prompt(
                        new TextPrompt<int>("Enter new Budget Notification Threshold day")
                    );

                    dataManager.EditNotificationBudgetsData(
                        nBudgetsList: dataManager.Notification_Budgets,
                        existingData: selectedNBudget,
                        editParam: "Threshold_Day",
                        newThresholdDay: newNBudgetThresh_Day
                    );
                }


                else if( selectedModification == "Tolerance Percent" ) {
                    var newNBudgetTolerance_Pct = AnsiConsole.Prompt(
                        new TextPrompt<int>("Enter a new Budget Notification Tolerance Percent")
                    );

                    dataManager.EditNotificationBudgetsData(
                        nBudgetsList: dataManager.Notification_Budgets,
                        existingData: selectedNBudget,
                        editParam: "Tolerance Percent",
                        newTolerancePercent: newNBudgetTolerance_Pct
                    );
                }


                else if( selectedModification == "Expense Category" ) {
                    var newNBudgetExpCategory = AnsiConsole.Prompt(
                        new SelectionPrompt<Category>()
                        .Title("Select a new Category for the Notification")
                        .AddChoices(dataManager.Categories)
                    );

                    var newNBudget_CategoryID = newNBudgetExpCategory.ID;

                    dataManager.EditNotificationBudgetsData(
                        nBudgetsList: dataManager.Notification_Budgets,
                        existingData: selectedNBudget,
                        editParam: "Expense Category",
                        newExpenseCategoryId: newNBudget_CategoryID
                    );
                }


                else if( selectedModification == "Enabled" ) {
                    var newNBudgetEnabled = AnsiConsole.Prompt(
                        new SelectionPrompt<bool>()
                        .Title("Should this Budget Notification be Enabled (true) or Disabled (false)?")
                        .AddChoices(new[] {true, false})
                    );

                    dataManager.EditNotificationBudgetsData(
                        nBudgetsList: dataManager.Notification_Budgets,
                        existingData: selectedNBudget,
                        editParam: "Enabled",
                        newEnabled: newNBudgetEnabled
                    );
                }
                    


                } while( selectedModification != "Back");

            }


            } while (budgetModeSub != "Back");
        }   // End "Create / Edit Budget Notifications"
        } while (budgetMode != "Back"); // End budgetMode
        } // End "Create / Edit Notifications"


            










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





    /// Gotta love confusing type-enforcement rules on lists...
    public static int FindMaxID_NBill(List<Notification_Bill> someList) {
        
        int maxID;

        List<int> IDList = new List<int>();


        if( someList.Count > 0) {

            // Create list of Category IDs
            foreach( Notification_Bill lineitem in someList ) {
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




    public static List<Notification_Bill> DisplayNotificationBills(
        List<Notification_Bill> nBillList) {

            // Create container for upcoming Bill Notifications
            List<Notification_Bill> upcomingNBills = new List<Notification_Bill>();


            // Create a value range, based on Current Day
            DateTime dtCurrent = DateTime.Now;
            int dtCurrent_Day = dtCurrent.Day;
            int rangeStart = dtCurrent_Day - 7;


            // Populate container with items that occur within day range
            foreach( var entry in nBillList ) {

                if( Enumerable.Range(rangeStart, dtCurrent_Day).Contains(
                    entry.Due_Day) ) {
                    
                    upcomingNBills.Add(entry);
                }
            }
            


            return upcomingNBills;

    }





    //FindMaxID_NBudget
    public static int FindMaxID_NBudget(List<Notification_Budget> someList) {
        
        int maxID;

        List<int> IDList = new List<int>();


        if( someList.Count > 0) {

            // Create list of Category IDs
            foreach( Notification_Budget lineitem in someList ) {
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



    /*
    public static List<Notification_Budget_Aggregate>           DisplayNotificationBudgets(
        List<Notification_Budget> nBudgetList,
        List<Expense> expensesList,
        List<Category> categoriesList) {


            // Create container for upcoming Bill Notifications
            List< Notification_Budget_Aggregate> DisplayNBudgets = new List<Notification_Budget_Aggregate>();

            Dictionary<int, float> BudgetExpenseSums = new Dictionary<int, float>();

            
            // Create each entry for upcoming Bill Notifications
            // This SHOULD keep both List and Dict limited to 1 entry per
            // CategoryID
            foreach( Expense lineitem in expensesList ) {

                if( BudgetExpenseSums.ContainsKey(lineitem.ID) ) {
                    // Update running sum in Dict
                    BudgetExpenseSums[lineitem.ID] += lineitem.Amount;

                    // Update the expense sum in list
                    int index = DisplayNBudgets.FindIndex(
                        x => x.expenseCategoryID == lineitem.ExpenseCategoryID);
                    
                    DisplayNBudgets[index].expenseAmountSum = 
                    BudgetExpenseSums[lineitem.ID];
                }

                else if( !BudgetExpenseSums.ContainsKey(lineitem.ID) ) {
                    // Here no related category data has been collected
                    // in Dict or list. Create / collect it.

                    // Create entry in Dict
                    BudgetExpenseSums[lineitem.ID] = (float)0;
                    BudgetExpenseSums[lineitem.ID] += lineitem.Amount;


                    // Create entry in List
                    // Get Expense Info
                    int expenseCategoryID = lineitem.ExpenseCategoryID;
                    string relatedCategoryName = "";
                    float budgetAmount = (float)0;
                    float expenseAmount = lineitem.Amount;
                    int threshold_day = 0;
                    float tolerance_pct = (float)0;
                    bool notificationEnabled = true;

                    // Get associated Category Name, Budget for expenses
                    foreach(Category categoryItem in categoriesList) {
                        if( categoryItem.ID == expenseCategoryID ) {
                            relatedCategoryName = categoryItem.Name;
                            budgetAmount = categoryItem.Budget_Amount;
                        }
                    }

                    // Get associated Notification_Budget Thresh_Day, Tol_Pct, Enabled
                    foreach(Notification_Budget budgetItem in nBudgetList) {
                        if( budgetItem.ID == expenseCategoryID ) {
                            threshold_day = budgetItem.Threshold_Day;
                            tolerance_pct = budgetItem.Tolerance_Percent;
                            notificationEnabled = budgetItem.Enabled;
                        }
                    }


                    //string dictKeyName = expenseCategoryID + "_" +
                        //relatedCategoryName;

                    DisplayNBudgets.Add(new Notification_Budget_Aggregate(
                        expenseCategoryID: expenseCategoryID,
                        expenseCategoryName: relatedCategoryName,
                        budgetAmount: budgetAmount,
                        expenseAmountSum: expenseAmount,
                        thresholdDay: threshold_day,
                        tolerancePercent: tolerance_pct,
                        notificationEnabled: notificationEnabled
                        )
                    );


                }

            } // End foreach



            return DisplayNBudgets;

    }
    */



/// <summary>
///  This should only return a (List) of objects for (unique) Category, 
///  Notification information where the given Category has some associated
///  budget notification
/// </summary>
/// <param name="nBudgetList"></param>
/// <param name="expensesList"></param>
/// <param name="categoriesList"></param>
/// <returns></returns>
    public static List<Notification_Budget_Aggregate> DisplayNotificationBudgets2(
            List<Notification_Budget> nBudgetList,
            List<Expense> expensesList,
            List<Category> categoriesList) {

        // Set up containing structures
        SortedDictionary<int, float> categoryExpenseSums = new SortedDictionary<int, float>();

        List<Notification_Budget_Aggregate> budgetDisplayList = new 
        List<Notification_Budget_Aggregate>();


        // Get unique CategoryID with sum of Expense Amount
        foreach( var expenseItem in expensesList ) {
            int currentCategoryID = expenseItem.ExpenseCategoryID;
            float currentExpenseAmount = expenseItem.Amount;

            if( categoryExpenseSums.ContainsKey(currentCategoryID) ) {
                categoryExpenseSums[currentCategoryID] += currentExpenseAmount;
            }
            else {
                categoryExpenseSums[currentCategoryID] = (float)0;
                categoryExpenseSums[currentCategoryID] += currentExpenseAmount;
            }
        
        }

        // Join Expense amount with other Categroy, Budget data
        foreach(KeyValuePair<int, float> aggItem in       categoryExpenseSums ) {

            if( nBudgetList.Find(x => x.ExpenseCategoryID == aggItem.Key) is null) {
                // Skip if there isn't a budget notification
                // associated with this category
            }


            else {

            // Get corresponding objects, given the Category ID
            Category categoryItem = categoriesList.Find(x => x.ID == aggItem.Key);
            
            Notification_Budget budgetItem = nBudgetList.Find(x => x.ExpenseCategoryID == aggItem.Key);

            // Build a Notification_Budget_Aggregate Item
            int expenseCategoryID = aggItem.Key;
            string expenseCategoryName = categoryItem.Name;
            float budgetAmount = categoryItem.Budget_Amount;
            float expenseAmount = aggItem.Value;
            int thresholdDay = budgetItem.Threshold_Day;
            float tolerancePercent = budgetItem.Tolerance_Percent;
            bool notificationEnabled = budgetItem.Enabled;

            budgetDisplayList.Add(
                new Notification_Budget_Aggregate(
                        expenseCategoryID: expenseCategoryID,
                        expenseCategoryName: expenseCategoryName,
                        budgetAmount: budgetAmount,
                        expenseAmountSum: expenseAmount,
                        thresholdDay: thresholdDay,
                        tolerancePercent: tolerancePercent,
                        notificationEnabled: notificationEnabled
                )
            );
            }

        }
            


        return budgetDisplayList;



    }



}


    

