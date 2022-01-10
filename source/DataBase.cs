using DataBank;
using Newtonsoft.Json;

public class DataBase
{
    public DataBase(string name,bool cndb)
    {
        Name = name;
        DirName = Engine.home + "\\" + Name;
        if (cndb)
        {
            Engine.CreateNewDataBase(Name);
            Console.WriteLine("Create new Database (" + Name + ") at " + DirName);
        }
    }
    
    public static string Name;
    public static string DirName;
    
    public void CreateDocument(string documentName,Dictionary<string, Object> dictionary)
    {
        string filename = DirName + "\\" + documentName + ".json";
        if (File.Exists(filename))
        {
            Console.WriteLine("Document " + documentName + " in DataBase (" + Name + "),so it will be overwritten");
        }
        string json = JsonConvert.SerializeObject(dictionary, Formatting.Indented);
        File.WriteAllTextAsync(filename, json);
        Console.WriteLine("Create new Document (" + documentName + ") in DataBase " + Name + " at " + filename);
    }

    public void DeleteDocument(string documentName)
    {
        string filename = DirName + "\\" + documentName + ".json";
        if (File.Exists(filename))
        {
            Console.WriteLine("Deleted Document " + documentName + " from DataBase (" + Name + ")");
            File.Delete(filename);
        }
        else
        {
            Console.WriteLine("Document " + documentName + " not Found in DataBase (" + Name + ")");
        }
    }
    
    public string GetPath() { return DirName; }
    
    public void EditDocument(string documentName,string key,string value)
    {
        string filename = DirName + "\\" + documentName + ".json";
        if (File.Exists(filename))
        {
            var text = File.ReadAllText(filename); 
            Dictionary<string, Object> dictionary = JsonConvert.DeserializeObject<Dictionary<string, Object>>(text);
            if (dictionary.Keys.Contains(key))
            {
                dictionary[key] = value; // Overwrite
                Console.WriteLine("Document " + documentName + " in DataBase (" + Name + ") already has the key " + key + ", so it will be overwritten ");
            }
            else
            {
                dictionary.Add(key,value); // Add new
            }
            string json = JsonConvert.SerializeObject(dictionary, Formatting.Indented);
            File.WriteAllTextAsync(filename, json);
        }
        else
        {
            Console.WriteLine("Document " + documentName + " not Found in DataBase (" + Name + ")");
        }
    }
    
    public void GetAllDocument(string documentName)
    {
        string filename = DirName + "\\" + documentName + ".json";
        if (File.Exists(filename))
        {
            Console.WriteLine("Listing all Keys/Values for Document : " + documentName + " in DataBase (" + Name + ") :");
            var text = File.ReadAllText(filename); Dictionary<string, Object> mydictionary = JsonConvert.DeserializeObject<Dictionary<string,Object>>(text);
            foreach (var key_value in mydictionary)
            {
                string key = key_value.Key;
                var value = key_value.Value;
                string type = value.GetType().ToString();
                
                Console.WriteLine($"{key} : {type} = {value}");
            }
        }
        else
        {
            Console.WriteLine("Document " + documentName + " not Found in DataBase (" + Name + ")");
        }
    }

    public string[] GetAllDocuments()
    {
        Console.WriteLine("Listing all Doucments for DataBase (" + Name + ") :");
        return Directory.GetFiles(DirName);
    }
}