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
            " " + this.Date.Month + "/" + this.Date.Day +
                "/" + this.Date.Year +
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



    public Category (
        int ID, string Name, bool Enabled, float Budget_Amount = (float)0
    ) {
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
    public int ID { get; }
    public string Description { get; set; }
    public int Due_Day { get; set; }
    public float Amount { get; set; }
    public bool Enabled { get; set; }


    public Notification_Bill (
        int ID, string Description, int Due_Day, float Amount, bool Enabled
    ) {
        this.ID = ID;
        this.Description = Description;
        this.Due_Day = Due_Day;
        this.Amount = Amount;
        this.Enabled = Enabled;
    }



    public override string ToString() {
        return ( Convert.ToString(this.ID) + 
            "_" + this.Description +
            "_Day: " + Convert.ToString(this.Due_Day) + 
            "_Amount: " + Convert.ToString(this.Amount) +
            "_Enabled: " + Convert.ToString(this.Enabled)
        );
    }



}



public class Notification_Budget {

    public int ID { get; }
    public int Threshold_Day { get; set; }
    public float Tolerance_Percent { get; set; }
    public int ExpenseCategoryID { get; set; }
    public bool Enabled { get; set; }


    public Notification_Budget (
        int ID, int Threshold_Day, float Tolerance_Percent, 
        int ExpenseCategoryID, bool Enabled
    ) {
        this.ID = ID;
        this.Threshold_Day = Threshold_Day;
        this.Tolerance_Percent = Tolerance_Percent;
        this.ExpenseCategoryID = ExpenseCategoryID;
        this.Enabled = Enabled;
    }



    public override string ToString() {
        return ( Convert.ToString(this.ID) + 
            "_Thresh Day: " + Convert.ToString(this.Threshold_Day) +
            "_Pct: " + Convert.ToString(this.Tolerance_Percent) + 
            "_CategoryID: " + Convert.ToString(this.ExpenseCategoryID) +
            "_Enabled: " + Convert.ToString(this.Enabled)
        );
    }



}




/// <summary>
///  To aid in calculations to display Budget Notifications
/// </summary>
public class Notification_Budget_Aggregate {

    public int expenseCategoryID { get; set; }
    public string expenseCategoryName { get; set; }
    public float budgetAmount { get; set; }
    public float expenseAmountSum { get; set; }
    public int thresholdDay { get; set; }
    public float tolerancePercent { get; set; }
    public bool notificationEnabled { get; set; }

    public Notification_Budget_Aggregate (
        int expenseCategoryID, 
        string expenseCategoryName,
        float budgetAmount,
        float expenseAmountSum,
        int thresholdDay,
        float tolerancePercent,
        bool notificationEnabled
    ) {
        this.expenseCategoryID = expenseCategoryID;
        this.expenseCategoryName = expenseCategoryName;
        this.budgetAmount = budgetAmount;
        this.expenseAmountSum = expenseAmountSum;
        this.thresholdDay = thresholdDay;
        this.tolerancePercent = tolerancePercent;
        this.notificationEnabled = notificationEnabled;
    }



    public override string ToString() {
        return ( 
            Convert.ToString(this.expenseCategoryID) +
            "_" + this.expenseCategoryName +
            "_" + Convert.ToString(this.budgetAmount) +
            "_" + Convert.ToString(this.expenseAmountSum) +
            "_" + Convert.ToString(this.thresholdDay) +
            "_" + Convert.ToString(this.tolerancePercent) +
            "_" + Convert.ToString(this.notificationEnabled)
        );
    }


}