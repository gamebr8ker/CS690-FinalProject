namespace ExpenseTrackerApp;

using System.IO;



public class FileSaver {

    string fileName;

    public FileSaver(string fileName) {
        /* Creates a file of {fileName} if it doesn't exist */
        this.fileName = fileName;

        if( !File.Exists(this.fileName) ) {

            File.Create(this.fileName).Close();
        }
    }



    public void AppendExpenseData(Expense data) {
        File.AppendAllText(
            this.fileName,
            data.ID + ", " + data.Description + ", " +
            data.Date + ", " + data.Amount + ", " + 
            data.ExpenseCategoryID +
            Environment.NewLine
        );
    }



    public void AppendCategoryData(Category data) {
        File.AppendAllText(
            this.fileName,
            data.ID + ", " + data.Name + ", " +
            data.Enabled + ", " + data.Budget_Amount +
            Environment.NewLine
        );
    }



    //
}

