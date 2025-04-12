namespace ExpenseTrackerApp;

class Program
{

    static void Main(string[] args)
    {
        Console.WriteLine(Environment.CurrentDirectory);

        //DataManager dataManager = new DataManager();

        ConsoleUI theUI = new ConsoleUI();
        theUI.Show();
 
    }
}
