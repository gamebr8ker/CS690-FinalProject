namespace ExpenseTrackerApp;


public class Expense {
    public int ID { get; }

    public string Description { get; set; }

    public DateTime Date { get; set; }

    public float Amount { get; set; }

    public int ExpenseCategoryID { get; set; }



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

    public string Name { get; set; }

    public bool Enabled { get; set; }

    public float Budget_Amount { get; set; }



    public Category (int ID, string Name, bool Enabled, float Budget_Amount) {
        this.ID = ID;
        this.Name = Name;
        this.Enabled = Enabled;
        this.Budget_Amount = Budget_Amount;
    }


    public override string ToString() {
        return ( Convert.ToString(this.ID) + 
            "_" + this.Name +
            " " + Convert.ToString(this.Budget_Amount) + 
            " " + Convert.ToString(this.Enabled) + " "
        );
    }



}



public class Notification_Bill {



}



public class Notification_Budget {



}