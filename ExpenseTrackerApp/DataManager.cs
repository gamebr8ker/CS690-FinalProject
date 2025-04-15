namespace ExpenseTrackerApp;



public class DataManager {

    FileSaver fileSaverExpense;
    FileSaver fileSaverCategories;
    FileSaver fileSaverNotifications_Bill;
    FileSaver fileSaverNotitifcations_Budget;

    public List<Expense> Expenses { get; } // Change back to Expense type?
    
    public List<Category> Categories { get; }


        
    public DataManager() {

        fileSaverExpense = new FileSaver("expenses.txt");
        fileSaverCategories = new FileSaver("categories.txt");
        fileSaverNotifications_Bill = new FileSaver("notifications_bill.txt");
        fileSaverNotitifcations_Budget = new FileSaver("notifications_budget.txt");



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

}