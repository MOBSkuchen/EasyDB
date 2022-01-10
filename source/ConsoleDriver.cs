using System.Text.RegularExpressions;

namespace DataBank;

public class ConsoleDriver
{

    public static void Main()
    {
        Console.Title = "EasyDB CLI v1.0";
        while (true)
        {
            Dictionary<string, Object> dbs = Engine.ImportAll();
            if (dbs != null)
            {
                Engine.DataBases = dbs;
            }
            string Input = GetInput();
            Match(Input);
        }
    }

    public static string GetInput()
    {
        Console.Write("> "); return Console.ReadLine();
    }

    public static void Match(string cmd)
    {
        if (cmd.StartsWith("create "))
        {
            var regex = new Regex(Regex.Escape("create "));var newText = regex.Replace(cmd, "", 1);
            Engine.TechNewDataBase(newText);
        } else if (cmd.StartsWith("doc "))
        {
            var regex = new Regex(Regex.Escape("doc "));var newText = regex.Replace(cmd, "", 1);
            string[] PathAndArgs = newText.Split(" ");
            int c = 0; string args = ""; string path = ""; foreach (var paa in PathAndArgs) {c += 1;if (c == 1){    path = paa;}else if(c == 2){    args += paa;}else{    args += " " + paa;} }
            if (args == "")
            {
                Console.WriteLine("Missing Argument : [Document]");
                return;
            }
            DataBase ?db = Engine.GetDataBase(path);
            if (db != null)
            {
                db.GetAllDocument(args);
            }
        } else if (cmd.StartsWith("list "))
        {
            var regex = new Regex(Regex.Escape("list "));var newText = regex.Replace(cmd, "", 1);
            DataBase ?db = Engine.GetDataBase(newText);
            if (db != null)
            {
                string[] docs = db.GetAllDocuments();
                foreach (var doc in docs)
                {
                    FileInfo file = new FileInfo(doc);
                    string filename = file.Name;
                    string extension = file.Extension;
                    if (extension != ".ignore")
                    {
                        Console.WriteLine(filename);
                    }
                }
            }
        }else if (cmd.Equals("dbs"))
        {
            foreach (var dataBase in Engine.GetDataBases())
            {
                FileInfo file = new FileInfo(dataBase);
                Console.WriteLine($"- {file.Name} ({file.FullName})");
            }
        }else if (cmd.Equals("help"))
        {
            Console.WriteLine("Listing all EasyDB commands :                                                                            ");
            Console.WriteLine("- create <DataBase>                           | Creates <DataBase> in the %home% directory               ");
            Console.WriteLine("- new <DataBase> <Document>                   | Creates <Document>.json in <DataBase>                    ");
            Console.WriteLine("- doc <DataBase> <Document>                   | Views all the Keys/Values in <Document>  (<DataBase> DB) ");
            Console.WriteLine("- inject <DataBase> <Document> <Key> <Value>  | Creates <Key> with <Value> in <Document> (<DataBase> DB) ");
            Console.WriteLine("- erase <DataBase>                            | Deletes <DataBase> in the %home% directory               ");
            Console.WriteLine("- del <DataBase> <Document>                   | Deletes <Document> in <DataBase>                         ");
            Console.WriteLine("- list <DataBase>                             | Lists all Documents in <DataBase>                        ");
            Console.WriteLine("- dbs                                         | Lists all DataBases in the %home% directory              ");
            Console.WriteLine("- help                                        | Lists all EasyDB                                         ");
        }
        else if (cmd.StartsWith("erase "))
        {
            var regex = new Regex(Regex.Escape("erase "));var newText = regex.Replace(cmd, "", 1);
            DataBase ?db = Engine.GetDataBase(newText);
            if (db != null)
            {
                Directory.Delete(db.GetPath(),true);
                Engine.DeleteDataBase(newText);
            }
        }
        else if (cmd.StartsWith("new "))
        {
            var regex = new Regex(Regex.Escape("new "));var newText = regex.Replace(cmd, "", 1);
            string[] PathAndArgs = newText.Split(" ");
            int c = 0; string args = ""; string path = ""; foreach (var paa in PathAndArgs) {c += 1;if (c == 1){    path = paa;}else if(c == 2){    args += paa;}else{    args += " " + paa;} }
            if (args == "")
            {
                Console.WriteLine("Missing Argument : [Document Name]");
                return;
            }
            DataBase ?db = Engine.GetDataBase(path);
            if (db != null)
            {
                Dictionary<string, Object> emptyDB = new Dictionary<string, Object>();
                db.CreateDocument(args,emptyDB);
            }
        }else if (cmd.StartsWith("del "))
        {
            var regex = new Regex(Regex.Escape("del "));var newText = regex.Replace(cmd, "", 1);
            string[] PathAndArgs = newText.Split(" ");
            int c = 0; string args = ""; string path = ""; foreach (var paa in PathAndArgs) {c += 1;if (c == 1){    path = paa;}else if(c == 2){    args += paa;}else{    args += " " + paa;} }
            if (args == "")
            {
                Console.WriteLine("Missing Argument : [Document Name]");
                return;
            }
            DataBase ?db = Engine.GetDataBase(path);
            if (db != null)
            {
                db.DeleteDocument(args);
            }
        }
        else if (cmd.StartsWith("inject "))
        {
            var regex = new Regex(Regex.Escape("inject "));var newText = regex.Replace(cmd, "", 1);
            string[] vStrings = newText.Split(" ");
            if (vStrings.Length < 4)
            {
                Console.WriteLine("Missing Argument(s)");
                return;
            }
            string database = "";
            string document = "";
            string key = "";
            int c = 0;
            string value = "";
            foreach (var VARIABLE in vStrings)
            {
                c += 1;
                if (c == 1)
                {
                    database = VARIABLE;
                }
                else if (c == 2)
                {
                    document = VARIABLE;
                }
                else if (c == 3)
                {
                    key = VARIABLE;
                }
                else if (c  == 4)
                {
                    value += VARIABLE;
                }
                else if (c  > 4)
                {
                    value += " " + VARIABLE;
                }
            }
            DataBase ?db = Engine.GetDataBase(database);
            if (db != null)
            {
                db.EditDocument(document,key,value);
            }
        }
        else
        {
            Console.WriteLine("Command not found");
        }
    }
}