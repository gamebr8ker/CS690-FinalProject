namespace ExpenseTrackerApp.Tests;

using ExpenseTrackerApp;

public class FileSaverTests
{

    FileSaver fileSaver_Expense;
    FileSaver fileSaver_Category;
    
    string testFileName_Expense;
    string testFileName_Category;

    Expense testExpenseEntry;
    Category testCategoryEntry;



    /// <summary>
    ///  Constructor for FileSaverTests class
    /// </summary>
    public FileSaverTests() {
        testFileName_Expense = "test_file.txt";
        File.Delete(testFileName_Expense);   // Clear/Reset file if exists
        fileSaver_Expense = new FileSaver(testFileName_Expense);


        testFileName_Category = "test_file_category.txt";
        File.Delete(testFileName_Category);
        fileSaver_Category = new FileSaver(testFileName_Category);

        
        testExpenseEntry = new Expense(
            ID: 1,
            Description: "Test Expense 1",
            Date: DateTime.Now,
            Amount: (float)15.0,
            ExpenseCategoryID: 1
        );


        testCategoryEntry = new Category(
            ID: 5,
            Name: "Test Category 1",
            Enabled: true,
            Budget_Amount: (float)500.0
        );

    }





    [Fact]
    public void Test_FileSaver_Create()
    {
        File.Exists(testFileName_Expense);
    }





    [Fact]
    public void Test_FileSaver_AppendExpense()
    {
        // Create list to append Expense-like file content
        List<Expense> expenseList = new List<Expense>();

        // Append test Expense Entry to file
        fileSaver_Expense.AppendExpenseData(testExpenseEntry);
        
        // Read file, convert to Expense-like and add to list
        var contentFromFile = File.ReadAllLines(testFileName_Expense);
        foreach( var expEntry in contentFromFile ) {
            // Split given item by comma-space
            var splitted = expEntry.Split(
                ", ", StringSplitOptions.RemoveEmptyEntries
            );

            // Use the splitted (list) created above
            // to construct an expense item
            var expenseID = int.Parse(splitted[0]);
            var expenseDescr = splitted[1];
            var expenseDate = DateTime.Parse(splitted[2]);
            var expenseAmount = float.Parse(splitted[3]);
            var expenseCategoryID = int.Parse(splitted[4]);
            
            expenseList.Add(
                new Expense(expenseID, expenseDescr, expenseDate,
                    expenseAmount, expenseCategoryID)
            );
        }


        // Test the list contents for proper _AppendExpense behavior
        
        // Note1: It's critical to Delete and Recreate the file when
        // running these tests, otherwise data will be retained and counts
        // will differ.

        // Note2: list.Count returns a value that Assert.Equal cannot compare
        // to an int, so testing equality outside of Assert.Equal
        bool testListLength = expenseList.Count == 1;
        Console.WriteLine(testListLength);

        Assert.Equal(true, testListLength);
        Assert.Equal(1, expenseList[0].ID);
        
    }





    [Fact]
    public void Test_FileSaver_AppendCategory()
    {
        // Create list to append Category-like file content
        List<Category> categoryList = new List<Category>();

        // Append test Expense Entry to file
        fileSaver_Category.AppendCategoryData(testCategoryEntry);
        
        // Read file, convert to Expense-like and add to list
        var contentFromFile = File.ReadAllLines(testFileName_Category);
        foreach( var catEntry in contentFromFile ) {
            // Split given item by comma-space
            var splitted = catEntry.Split(
                ", ", StringSplitOptions.RemoveEmptyEntries
            );

            // Use the splitted (list) created above
            // to construct an expense item
            var categoryID = int.Parse(splitted[0]);
            var categoryName = splitted[1];
            var categoryEnabled = bool.Parse(splitted[2]);
            var categoryBudgetAmount = float.Parse(splitted[3]);
            
            
            categoryList.Add(
                new Category(categoryID, categoryName, categoryEnabled,
                    categoryBudgetAmount)
            );
        }


        // Test the list contents for proper _AppendCategorybehavior
        
        // Note1: It's critical to Delete and Recreate the file when
        // running these tests, otherwise data will be retained and counts
        // will differ.

        // Note2: list.Count returns a value that Assert.Equal cannot compare
        // to an int, so testing equality outside of Assert.Equal
        bool testListLength = categoryList.Count == 1;
        Console.WriteLine(testListLength);

        Assert.Equal(true, testListLength);
        Assert.Equal(5, categoryList[0].ID);
        
    }
}
