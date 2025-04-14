namespace ExpenseTrackerApp;


public class Expense {
    public int ID { get; }

    public string Description { get; }

    public DateTime Date { get; }

    public float Amount { get; }

    public int ExpenseCategoryID { get; }



    public Expense(
        int ID , string Description, DateTime Date, 
        float Amount, int ExpenseCategoryID ) {
        this.ID = ID;
        this.Description = Description;
        this.Date = Date;
        this.Amount = Amount;
        this.ExpenseCategoryID = ExpenseCategoryID;
    }


    public override string ToString() {
        return ( Convert.ToString(this.ID) + 
            "_" + this.Description + 
            " " + this.Date + 
            " " + this.Amount + 
            " " + Convert.ToString(this.ExpenseCategoryID)
        );
    }

}



public class Category {
    public int ID { get; }

    public string Name { get; }

    public bool Enabled { get; }

    public float Budget_Amount { get; }



    public Category (int ID, string Name, bool Enabled, float Budget_Amount) {
        this.ID = ID;
        this.Name = Name;
        this.Enabled = Enabled;
        this.Budget_Amount = Budget_Amount;
    }

    public override string ToString() {
        return ( Convert.ToString(this.ID) + "_" + this.Name );
    }



}



public class Notification_Bill {



}



public class Notification_Budget {



}