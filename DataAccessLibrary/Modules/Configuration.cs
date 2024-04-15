using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace ConfigurationLoader
{
    public class Configuration
    {
        public Dictionary<string, object> Settings { get; private set; }

        public Configuration()
        {
            Settings = new Dictionary<string, object>();
        }

        public void LoadFromJson(string jsonFilePath)
        {
            if (!File.Exists(jsonFilePath))
            {
                throw new FileNotFoundException("JSON file not found.", jsonFilePath);
            }

            string json = File.ReadAllText(jsonFilePath);
            Settings = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        }

        public void LoadFromXml(string xmlFilePath)
        {
            if (!File.Exists(xmlFilePath))
            {
                throw new FileNotFoundException("XML file not found.", xmlFilePath);
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);

            XmlNodeList nodes = xmlDoc.SelectNodes("/configuration/item");
            foreach (XmlNode node in nodes)
            {
                string key = node.Attributes["key"].Value;
                string value = node.InnerText;
                Settings[key] = value;
            }
        }

        public object GetSetting(string key)
        {
            if (Settings.ContainsKey(key))
            {
                return Settings[key];
            }
            else
            {
                throw new KeyNotFoundException($"Setting '{key}' not found.");
            }
        }

        public string GetConnectionString(string name)
        {
            // Check if the connection string exists in the loaded settings
            if (Settings.ContainsKey("ConnectionStrings"))
            {
                object connectionStringsObj = Settings["ConnectionStrings"];

                // Check if the connectionStringsObj is a JObject (indicating JSON deserialization)
                if (connectionStringsObj is JObject)
                {
                    var connectionStrings = ((JObject)connectionStringsObj).ToObject<Dictionary<string, string>>();
                    if (connectionStrings.ContainsKey(name))
                    {
                        return connectionStrings[name];
                    }
                    else
                    {
                        throw new KeyNotFoundException($"Connection string '{name}' not found.");
                    }
                }
                // Check if the connectionStringsObj is a Dictionary<string, object> (indicating XML deserialization)
                else if (connectionStringsObj is Dictionary<string, object>)
                {
                    var connectionStrings = (Dictionary<string, object>)connectionStringsObj;
                    if (connectionStrings.ContainsKey(name))
                    {
                        return connectionStrings[name].ToString();
                    }
                    else
                    {
                        throw new KeyNotFoundException($"Connection string '{name}' not found.");
                    }
                }
                else
                {
                    throw new InvalidCastException("Unexpected type for ConnectionStrings section.");
                }
            }
            else
            {
                throw new KeyNotFoundException("ConnectionStrings section not found in settings.");
            }
        }
    }
}
