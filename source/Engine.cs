using Newtonsoft.Json;

namespace DataBank;

public class Engine
{
    public static string home = Load();
    public static Dictionary<string, Object> DataBases = ImportAll();
    public static void CreateNewDataBase(string name)
    {
        name = name.Replace("/", "\\");
        Directory.CreateDirectory(home+"\\"+name);
        File.WriteAllTextAsync(home+"\\"+name+"\\index.ignore","");
    }

    public static string[] GetDataBases()
    {
        Console.WriteLine("Listing all DataBases :");
        return Directory.GetDirectories(home);
    }
    
    public static void ExportAll()
    {
        string json = JsonConvert.SerializeObject(DataBases, Formatting.Indented);
        File.WriteAllTextAsync(home + "\\databases.json", json);
    }
    
    public static void ExportSpef(Dictionary<string,Object> dictionary)
    {
        string json = JsonConvert.SerializeObject(dictionary, Formatting.Indented);
        File.WriteAllTextAsync(home + "\\databases.json", json);
    }
    
    public static Dictionary<string, Object>? ImportAll()
    {
        try
        {
            var text = File.ReadAllText(home + "\\databases.json");
            return JsonConvert.DeserializeObject<Dictionary<string, Object>>(text);
        } catch(IOException) // File is being used by another Process
        {
            return null;
        }
    }

    public static bool Exists(string name)
    {
        if (DataBases.Keys.Contains(name))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void TechNewDataBase(string name) // Used by ConsoleDriver
    {
        if (Exists(name))
        {
            Console.WriteLine("DataBase (" + name + ") can not be created, because it already exists");
            return;
        }
        DataBase db = new DataBase(name,true);
        DataBases.Add(name,name);
        ExportAll();
    }

    public static void DeleteDataBase(string database)
    {
        if (Exists(database))
        {
            Dictionary<string, Object> DBs = ImportAll();
            DBs.Remove(database);
            ExportSpef(DBs);
            Console.WriteLine("Deleted DataBase (" + database + ")");
        }
        else
        {
            Console.WriteLine("DataBase (" + database + ") was not found");
        }
    }

    public static DataBase? GetDataBase(string name)
    {
        if (Exists(name))
        {
            var fml = DataBases[name];
            DataBase db = new DataBase(fml.ToString(), false);
            return db;
        }
        else
        {
            Console.WriteLine("DataBase (" + name + ") was not found");
            return null;
        }
    }

    public static void Setup(string home)
    {
        home = home.Replace("/", "\\");
        File.WriteAllTextAsync(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\easydb.txt", home);
        Directory.CreateDirectory(home);
        File.WriteAllTextAsync(home + "\\databases.json", "{}");
    }
    
    public static string Load()
    {
        if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\easydb.txt") != true)
        {
            Setup(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\EasyDB");
            Console.WriteLine("Auto Setup done at " + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\EasyDB");
        }
        return File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\easydb.txt"); 
    }

}
