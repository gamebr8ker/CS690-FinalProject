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
}

