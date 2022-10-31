namespace GenericConsole;

public interface IExport<T>
{
    public List<T> ImportCSV(string path, string filename, char seperator);
    
    public bool ExportCSV(List<T> exportlist, string path,
        string filename = "export.csv");
    
    public List<T>? ImportJSON(string path, string filename);
    
    public bool ExportJSON(List<T> exportList, string path, string filename);
    
    public List<T>? ImportXML(string path, string filename);
    
    public bool ExportXML(List<T> exportList, string path, string filename);
    
}