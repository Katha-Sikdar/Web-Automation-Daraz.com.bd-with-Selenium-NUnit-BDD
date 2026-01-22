using System.IO;
using Newtonsoft.Json.Linq;

namespace Daraz.Automation.BDD.Utils
{
    public static class JsonReader
    {
public static string GetTestData(string key)
{
    // Using the path you confirmed
    string filePath = "/Users/katha/Desktop/Automation Assessment/Web-Automation-Daraz.com.bd-with-Selenium-NUnit-BDD/Data/testData.json";
    
    string json = File.ReadAllText(filePath);
    var data = Newtonsoft.Json.Linq.JObject.Parse(json);
    
    return data["RegistrationData"]?[key]?.ToString();
}
    }
}
