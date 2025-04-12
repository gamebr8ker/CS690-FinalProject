namespace ExpenseTrackerApp;



public class DataManager {

    FileSaver fileSaverExpense;

        
    public DataManager() {

        fileSaverExpense = new FileSaver("expenses.txt");

        /*
        if( File.Exists("expenses.txt") ) {
            var expenseFileContent = File.ReadAllLines("expenses.txt");
        } else {

        }
        */
    }


}