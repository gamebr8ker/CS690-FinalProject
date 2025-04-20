namespace ExpenseTrackerApp.Tests;

using ExpenseTrackerApp;

public class DataManagerTests
{
    FileSaver fileSaverTest_Expense;

    List<Expense> testExpenses;

    string test_dm_FileName_Expense;
    Expense testExpenseEntry;
    Expense testExpenseEntry2;



    public DataManagerTests() {
        testExpenses = new List<Expense>();

        test_dm_FileName_Expense = "test_dm_FileExpense.txt";
        File.Delete(test_dm_FileName_Expense);   // Clear/Reset file if exists
        fileSaverTest_Expense = new FileSaver(test_dm_FileName_Expense);


        
        testExpenseEntry = new Expense(
            ID: 1,
            Description: "Test Expense 1",
            Date: DateTime.Now,
            Amount: (float)15.0,
            ExpenseCategoryID: 1
        );

        testExpenseEntry2 = new Expense(
            ID: 2,
            Description: "Test Expense 2",
            Date: DateTime.Now,
            Amount: (float)25.0,
            ExpenseCategoryID: 2
        );



    }

    [Fact]
    public void Test_AddNewExpenseData() {

        
        // Create a clean / consistent File to reference
        // Note: This doesn't hurt the actual / main file.
        File.Delete("expenses.txt");
        DataManager dataManager = new DataManager();



        // Get before-state
        // File
        int test_fileItemCount_before = 0;
        var test_ExpenseFileContent = File.ReadAllLines("expenses.txt");
        
        foreach( var lineitem in test_ExpenseFileContent ) {
            test_fileItemCount_before++;
        }

        // List
        int test_listItemCount_before = dataManager.Expenses.Count;



        // Call dataManager's AddNewExpenseData(data)
        dataManager.AddNewExpenseData(testExpenseEntry);



        // Get after-state
        // File
        int test_fileItemCount_after = 0;
        test_ExpenseFileContent = File.ReadAllLines("expenses.txt");

        foreach( var lineitem in test_ExpenseFileContent ) {
            test_fileItemCount_after++;
        }

        // List
        int test_listItemCount_after = dataManager.Expenses.Count;



        // Test before-state
        Assert.Equal(0, test_fileItemCount_before);
        Assert.Equal(0, test_listItemCount_before);

        // Test after-state
        Assert.Equal(1, test_fileItemCount_after);
        Assert.Equal(1, test_listItemCount_after);
    }






    [Fact]
    public void Test_RemoveExpenseData() {
        /*
            Note: These files save here:
            ...\0_CS 690_Software Engineering\W13_Project Code\
            ExpenseTracker\ExpenseTrackerApp.Tests\
            bin\Debug\net9.0\
            expenses.txt

        */

        // Create a clean / consistent File to reference
        // Note: This doesn't hurt the actual / main file.
        File.Delete("expenses.txt");
        DataManager dataManager = new DataManager();

        // Before modification
        int test_listItemCount_before = dataManager.Expenses.Count;  // 0
        var test_expenseFileContent = File.ReadAllLines("expenses.txt");

        int content_counter_before = 0;
        foreach( var lineitem in test_expenseFileContent) {
            content_counter_before++;
        }  // 0
        //File.Close("expenses.txt");



        // Call dataManager's AddNewExpenseData(data) twice
        // Removal w only 1 item causes a bug where file not recreated.
        dataManager.AddNewExpenseData(testExpenseEntry);
        dataManager.AddNewExpenseData(testExpenseEntry2);
        int test_listItemCount_Added = dataManager.Expenses.Count;  // 2



        // Call dataManager's RemoveExpenseData(data)
        dataManager.RemoveExpenseData(testExpenseEntry);
        int test_listItemCount_Removal = dataManager.Expenses.Count;
        var test_expenseFileContent2 = File.ReadAllLines("expenses.txt");

        int content_counter_after = 0;
        foreach( var lineitem in test_expenseFileContent2) {
            content_counter_after++;
        }  // 1



        // Added 2 items, removed 1. Thus the difference should be 1
        Assert.Equal(
            test_listItemCount_before + 1, 
            test_listItemCount_Removal
        );
        Assert.Equal(content_counter_before + 1, content_counter_after);

    }





    [Fact]
    public void Test_EditExpenseData() {

        // Create a clean / consistent File to reference
        // Note: This doesn't hurt the actual / main file.
        File.Delete("expenses.txt");
        DataManager dataManager = new DataManager();

        dataManager.AddNewExpenseData(testExpenseEntry);
        var test_expenseAmount_before = dataManager.Expenses[0].Amount;  // 15

        dataManager.EditExpenseData(
            expensesList: dataManager.Expenses,
            existingData: testExpenseEntry,
            editParam: "Amount",
            newAmount: (float)100
        );
        var test_expenseAmount_after = dataManager.Expenses[0].Amount;  // 100


        Assert.Equal((float)15.0, test_expenseAmount_before);
        Assert.Equal((float)100.0, test_expenseAmount_after);
    }





    [Fact]
    public void Test_AddNewCategoryData() { }

    [Fact]
    public void Test_RemoveCategoryData() { }

    [Fact]
    public void Test_EditCategoryData() { }


}