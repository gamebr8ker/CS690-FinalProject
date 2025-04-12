namespace ExpenseTrackerApp;



public class DataManager {

    FileSaver fileSaverExpense;
    FileSaver fileSaverCategories;
    FileSaver fileSaverNotifications_Bill;
    FileSaver fileSaverNotitifcations_Budget;

        
    public DataManager() {

        fileSaverExpense = new FileSaver("expenses.txt");
        fileSaverCategories = new FileSaver("categories.txt");
        fileSaverNotifications_Bill = new FileSaver("notifications_bill.txt");
        fileSaverNotitifcations_Budget = new FileSaver("notifications_budget.txt");

        /*
        if( File.Exists("expenses.txt") ) {
            var expenseFileContent = File.ReadAllLines("expenses.txt");
        } else {

        }
        */
    }


}