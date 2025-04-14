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



    /// Adds new item to list AND calls method to save to local file
    public void AddNewExpenseData(Expense data) {
        this.Expenses.Add(data);
        this.fileSaverExpense.AppendExpenseData(data);

    }



    /// Adds new item to list AND calls method to save to local file
    public void AddNewCategoryData(Category data) {
        this.Categories.Add(data);
        this.fileSaverCategories.AppendCategoryData(data);
    }


}