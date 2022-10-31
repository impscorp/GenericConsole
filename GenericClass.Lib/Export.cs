using System.Reflection;
using System.Xml.Serialization;
using GenericConsole;
using Newtonsoft.Json;
using InvalidOperationException = System.InvalidOperationException;
using StreamReader = System.IO.StreamReader;


namespace GenericClass.Lib;

public class Export<T> : IExport<T>
{
    public List<T> ImportCSV(string path, string filename, char seperator)
    {
        try
        {
            using var reader = new StreamReader(Path.Combine(path, filename));
            var file = reader.ReadToEnd().Split(Environment.NewLine).Select(x => x.Split(',')).ToList();
            var headers = file[0];
            file.RemoveAt(0);
            var pi = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var Parameters = new List<object>();
            var returnlist = new List<T>();
            List<PropertyInfo> propertyInfos = new List<PropertyInfo>();
            foreach (var head in headers)
            {
                //Check if the header is a property of the class
                for (int i = 0; i < pi.Length; i++)
                {
                    if (head == pi[i].Name)
                    {
                        propertyInfos.Add(pi[i]);
                    }
                }
            }
            foreach (var line in file)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    switch (propertyInfos[i].PropertyType.Name)
                    {
                        case "String":
                            Parameters.Add(Convert.ToString(line[i]));
                            break;
                        case "Int32":
                            Parameters.Add(Convert.ToInt32(line[i]));
                            break;
                        case "Boolean":
                            Parameters.Add(Convert.ToBoolean(line[i]));
                            break;
                        case "DateTime":
                            Parameters.Add(Convert.ToDateTime(line[i]));
                            break;
                        case "Decimal":
                            Parameters.Add(Convert.ToDecimal(line[i]));
                            break;
                        default:
                            throw new InvalidOperationException("Type not supported");
                    }
                }
                //sort parameters to match the order of the properties
                var sortedParameters = new List<object>();
                foreach (var propertyInfo in propertyInfos)
                {
                    for (int i = 0; i < Parameters.Count; i++)
                    {
                        if (propertyInfo.Name == pi[i].Name)
                        {
                            sortedParameters.Add(Parameters[i]);
                        }
                    }
                }
                returnlist.Add((T)Activator.CreateInstance(typeof(T), sortedParameters.ToArray()));
                Parameters.Clear();
            }
            reader.Close();
            return returnlist;
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public bool ExportCSV(List<T> exportlist, string path, string filename = "export.csv")
    {
        try
        {
            Type type = typeof(T);
            var pi = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            string data = "";
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (var p in pi)
                {
                    data += p.Name + ",";
                }
                data.Remove(data.Length - 1);
                foreach (var obj in exportlist)
                { 
                    data += Environment.NewLine;
                    foreach (var p in pi)
                    {
                        data += p.GetValue(obj) + ",";
                    }
                    data.Remove(data.Length - 1);
                }
                writer.Write(data);
                return true;
            }
        }
        catch (Exception e)
        {
            return false;
        }
    }
    
    public List<T>? ImportJSON(string path, string filename)
    {
        try
        {
            var json = File.ReadAllText(path + filename);
            var list = JsonConvert.DeserializeObject<List<T>>(json);
            return list;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public bool ExportJSON(List<T> exportList, string path, string filename)
    {
        try
        {
            var json = JsonConvert.SerializeObject(exportList, Formatting.Indented);
            File.WriteAllText(path + filename, json);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public List<T>? ImportXML(string path, string filename)
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            using (StreamReader reader = new StreamReader(path + filename))
            {
                return (List<T>)serializer.Deserialize(reader)!;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public bool ExportXML(List<T> exportList, string path, string filename)
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            using (StreamWriter writer = new StreamWriter(path + filename))
            {
                serializer.Serialize(writer, exportList);
                return true;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}