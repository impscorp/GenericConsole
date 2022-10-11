using System.Xml.Serialization;
using GenericConsole;
using Newtonsoft.Json;


namespace GenericClass.Lib;

public class Export<T> : IExport<T>
{
    public List<T> ImportCSV(string path, string filename)
    {
        throw new NotImplementedException();
    }

    public bool ExportCSV(List<T> exportlist, string path, string filename = "export.csv")
    {
        throw new NotImplementedException();
    }

    public List<T>? ImportJSON(string path, string filename)
    {
        List<T>? importlist;
        using (StreamReader file = File.OpenText(path + filename))
        {
            JsonSerializer serializer = new JsonSerializer();
            importlist = (List<T>)serializer.Deserialize(file, typeof(List<T>))!;
        }
        return importlist;
    }

    public bool ExportJSON(List<T> exportList, string path, string filename)
    {
        using (StreamWriter file = File.CreateText(path + filename))
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(file, exportList);
        }
        return true;
     }

    public List<T>? ImportXML(string path, string filename)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
        using (StreamReader reader = new StreamReader(path + filename))
        {
            return (List<T>)serializer.Deserialize(reader)!;
        }
    }

    public bool ExportXML(List<T> exportList, string path, string filename)
    {

        XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
        using (StreamWriter writer = new StreamWriter(path + filename))
        {
            serializer.Serialize(writer, exportList);
        }
        return true;
    }
}