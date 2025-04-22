namespace ExpenseTrackerApp;



public class DataManager {

    FileSaver fileSaverExpense;
    FileSaver fileSaverCategories;
    FileSaver fileSaverNotification_Bills;
    FileSaver fileSaverNotification_Budgets;

    public List<Expense> Expenses { get; } // Change back to Expense type?
    
    public List<Category> Categories { get; }

    public List<Notification_Bill> Notification_Bills { get; }

    public List<Notification_Budget> Notification_Budgets { get; }


        
    public DataManager() {

        fileSaverExpense = new FileSaver("expenses.txt");
        fileSaverCategories = new FileSaver("categories.txt");
        fileSaverNotification_Bills = new FileSaver("notifications_bill.txt");
        fileSaverNotification_Budgets = new FileSaver("notifications_budget.txt");



        // Read in existing Expenses to List
        // This will be read in ConsoleUI
        Expenses = new List<Expense>();    // Change back to Expense type?
        

        if( File.Exists("expenses.txt") ) {
            var expensesFileContent = File.ReadAllLines("expenses.txt");
        
            foreach( var expEntry in expensesFileContent) {
                //Console.WriteLine(expEntry);
                var splitted = expEntry.Split(", ",
                    StringSplitOptions.RemoveEmptyEntries);

                var expenseID = int.Parse(splitted[0]);
                var expenseDescr = splitted[1];
                var expenseDate = DateTime.Parse(splitted[2]);
                var expenseAmount = float.Parse(splitted[3]);
                var expenseCategoryID = int.Parse(splitted[4]);

                Expenses.Add(new Expense(
                    expenseID, expenseDescr, expenseDate, expenseAmount,
                    expenseCategoryID)
                );

            }

        }




        // Read in existing Categories to List
        Categories = new List<Category>();

        if( File.Exists("categories.txt") ) {

            var categoriesFileContent = File.ReadAllLines("categories.txt");

            foreach( var catEntry in categoriesFileContent) {
                // split, assign to vars, .Add to list
                var splitted = catEntry.Split(", ", 
                    StringSplitOptions.RemoveEmptyEntries);

                var categoryID = int.Parse(splitted[0]);
                var categoryName = splitted[1];
                var categoryEnabled = bool.Parse(splitted[2]);
                var categoryBudgetAmount = float.Parse(splitted[3]);

                Categories.Add( new Category(
                    categoryID, categoryName, categoryEnabled,
                    categoryBudgetAmount)
                );

            }
        }





        // Read in existing Notification_Bills to List
        Notification_Bills = new List<Notification_Bill>();

        if( File.Exists("notifications_bill.txt") ) {
            
            var nBillsContent = File.ReadAllLines("notifications_bill.txt");

            foreach( var nBillEntry in nBillsContent) {

                var splitted = nBillEntry.Split(", ",
                    StringSplitOptions.RemoveEmptyEntries);

                var nBillID = int.Parse(splitted[0]);
                var nBillDescription = splitted[1];
                var nBillDue_Day = int.Parse(splitted[2]);
                var nBillAmount = float.Parse(splitted[3]);
                var nBillEnabled = bool.Parse(splitted[4]);

                Notification_Bills.Add( new Notification_Bill(
                    nBillID, nBillDescription, nBillDue_Day,
                    nBillAmount, nBillEnabled)
                );
            }

        }





        // Read in existing Notification_Budgets to List
        Notification_Budgets = new List<Notification_Budget>();

        if( File.Exists("notifications_budget.txt") ) {

            var nBudgetsContent = File.ReadAllLines(
                "notifications_budget.txt"
            );

            foreach( var nBudgetEntry in nBudgetsContent ) {

                var splitted = nBudgetEntry.Split(", ",
                    StringSplitOptions.RemoveEmptyEntries);

                var nBudgetID = int.Parse(splitted[0]);
                var nBudgetThresholdDay = int.Parse(splitted[1]);
                var nBudgetTolerancePercent = float.Parse(splitted[2]);
                var nBudgetExpenseCategoryID = int.Parse(splitted[3]);
                var nBudgetEnabled = bool.Parse(splitted[4]);

                Notification_Budgets.Add( new Notification_Budget(
                    nBudgetID, nBudgetThresholdDay, nBudgetTolerancePercent,
                    nBudgetExpenseCategoryID, nBudgetEnabled)
                );

            }

        }





    }





    /// Expense Helper Functions
    /// Adds new item to list AND calls method to save to local file
    public void AddNewExpenseData(Expense data) {
        this.Expenses.Add(data);
        this.fileSaverExpense.AppendExpenseData(data);

    }


    /// Keeps expenses.txt file in sync with current state of Expenses list
    public void SynchronizeExpenses() {
        File.Delete("expenses.txt");

        foreach( var expense in Expenses) {
            this.fileSaverExpense.AppendExpenseData(expense);
        }
    }


    /// For deleting expenses from Expenses list.
    /// Runs the synchronizor method to update related file as well.
    public void RemoveExpenseData(Expense data) {
        Expenses.Remove(data);

        SynchronizeExpenses();
    }



    public void EditExpenseData(
        List<Expense> expensesList,
        Expense existingData, string editParam,
        string newDescr = "", DateTime newDateTime = default(DateTime),
        float newAmount = (float)0.0, 
        int newCategoryID = 1) {

            // Get index location of item being modified
            int index = expensesList.FindIndex(x => x == existingData);

            // Update based on user-selected parameter
            if( editParam == "Description" ) {
                expensesList[index].Description = newDescr;
            } 
            else if( editParam == "Date" ) {
                expensesList[index].Date = newDateTime;
            } 
            else if( editParam == "Amount" ) {
                expensesList[index].Amount = newAmount;
            }
            else if( editParam == "Expense Category" ) {
                expensesList[index].ExpenseCategoryID = newCategoryID;
            }

            Console.WriteLine(expensesList[index] + Environment.NewLine);


            // Update expenses.txt file
            SynchronizeExpenses();
        }





    /// Category Helper Functions
    /// Adds new item to list AND calls method to save to local file
    public void AddNewCategoryData(Category data) {
        this.Categories.Add(data);
        this.fileSaverCategories.AppendCategoryData(data);
    }



    /// Keeps categories.txt file in sync with current state of Categories list
    public void SynchronizeCategories() {
        File.Delete("categories.txt");

        foreach( var category in Categories) {
            this.fileSaverCategories.AppendCategoryData(category);
        }
    }



    /// For deleting categories from Categories list.
    /// Runs the synchronizor method to update related file as well.
    public void RemoveCategoryData(Category data) {
        Categories.Remove(data);

        SynchronizeCategories();
    }



    public void EditCategoryData(
        List<Category> categoriesList,  
        Category existingData, string editParam,  
        string newName = "", float newBudgetAmount = (float)0.0, 
        bool newEnabled = true) {
        
        // Get index location of item being modified
        int index = categoriesList.FindIndex(x => x == existingData);


        // Update based on user-selected parameter
        if( editParam == "Name" ) {
            categoriesList[index].Name = newName;
        }
        else if( editParam == "Budget Amount" ) {
            categoriesList[index].Budget_Amount = newBudgetAmount;
        }
        else if( editParam == "Enabled" ) {
            categoriesList[index].Enabled = newEnabled;
        }
        

        Console.WriteLine(categoriesList[index] + Environment.NewLine);

        // Update categories.txt file        
        SynchronizeCategories();
        }





    /// Notification_Bill Helper Functions
    // Adds new item to list AND calls method to save to local file
    public void AddNewNBillData(Notification_Bill data) {
        this.Notification_Bills.Add(data);
        this.fileSaverNotification_Bills.AppendNotificationBillData(data);

    }


    // Keeps <file>.txt in sync with current state of NBills list
    public void SynchronizeNotificationBills() {
        File.Delete("notifications_bill.txt");

        foreach( var nBill in Notification_Bills) {
            this.fileSaverNotification_Bills.AppendNotificationBillData(nBill);
        }
    }


    // For deleting NBills from Notification_Bills list.
    // Runs the synchronizor method to update related file as well
    public void RemoveNotificationBillsData(Notification_Bill data) {
        Notification_Bills.Remove(data);

        SynchronizeNotificationBills();

    }



    public void EditNotificationBillData(
        List<Notification_Bill> nBillsList,
        Notification_Bill existingData,
        string editParam,
        string newDescr = "",
        int newDueDay = 1,
        float newAmount = (float)0.0,
        bool newEnabled = true) {

            // Get index location of item being modified
            int index = nBillsList.FindIndex(x => x == existingData);

            // Update based on user-selected parameter
            if( editParam == "Description") {
                nBillsList[index].Description = newDescr;
            }
            else if( editParam == "Due Day") {
                nBillsList[index].Due_Day = newDueDay;
            }
            else if( editParam == "Amount") {
                nBillsList[index].Amount = newAmount;
            }
            else if( editParam == "Enabled") {
                nBillsList[index].Enabled = newEnabled;
            }

            Console.WriteLine(nBillsList[index] + Environment.NewLine);


            // Update corresponding <file>.txt
            SynchronizeNotificationBills();

        }

    



    /// Notification_Budget Helper Functions
    // Adds new item to list AND calls method to save to local file
    public void AddNewNBudgetData(Notification_Budget data) {
        this.Notification_Budgets.Add(data);
        this.fileSaverNotification_Budgets.AppendNotificationBudgetData(data);

    }



    // Keeps <file>.txt in sync with current state of NBudgets list
    public void SynchronizeNotificationBudgets() {
        File.Delete("notifications_budget.txt");

        foreach( var nBudget in Notification_Budgets) {
            this.fileSaverNotification_Budgets.AppendNotificationBudgetData(nBudget);
        }

    }



    // For deleting NBudgets from Notification_Budgets list
    // Runs the synchronizor method to update related file as well
    public void RemoveNotificationBudgetsData(Notification_Budget data) {
        Notification_Budgets.Remove(data);

        SynchronizeNotificationBudgets();

    }



    // 
    public void EditNotificationBudgetsData(
        List<Notification_Budget> nBudgetsList,
        Notification_Budget existingData,
        string editParam,
        int newThresholdDay = 1,
        float newTolerancePercent = (float)0.0,
        int newExpenseCategoryId = 1,
        bool newEnabled = true) {

            // Get index location of item being modified
            int index = nBudgetsList.FindIndex(x => x == existingData);

            // Update based on user-selected parameter
            if( editParam == "Threshold Day" ) {
                nBudgetsList[index].Threshold_Day = newThresholdDay;
            }
            else if( editParam == "Tolerance Percent" ) {
                nBudgetsList[index].Tolerance_Percent = newTolerancePercent;
            }
            else if( editParam == "Expense Category" ) {
                nBudgetsList[index].ExpenseCategoryID = newExpenseCategoryId;
            }
            else if( editParam == "Enabled" ) {
                nBudgetsList[index].Enabled = newEnabled;
            }


            Console.WriteLine(nBudgetsList[index] + Environment.NewLine);


            // Update corresponding <file>.txt
            SynchronizeNotificationBudgets();

        }



}


