using System;
using System.Xml;
using System.Configuration;
using System.Reflection;
//...


public class ConfigSettings
{
    private ConfigSettings() { }

    public static void WriteSetting(string key, string value)
    {
        // load config document for current assembly
        XmlDocument doc = loadConfigDocument();

        // retrieve appSettings node
        XmlNode node = doc.SelectSingleNode("//connectionStrings");

        if (node == null)
            throw new InvalidOperationException("connectionStrings section not found in config file.");

        try
        {
            // select the 'add' element that contains the key
            XmlElement elem1 = (XmlElement)node.SelectSingleNode(string.Format("//add[@name='{0}']", key));

            if (elem1 != null)
            {
                // add value for key
                elem1.SetAttribute("connectionStrings", value);
            }
            else
            {
                // key was not found so create the 'add' element 
                // and set it's key/value attributes 
                elem1 = doc.CreateElement("add");
                elem1.SetAttribute("name", key);
                elem1.SetAttribute("connectionString", value);
                node.AppendChild(elem1);
            }
            doc.Save(getConfigFilePath());
        }
        catch
        {
            throw;
        }
    }

    public static void RemoveSetting(string key)
    {
        // load config document for current assembly
        XmlDocument doc = loadConfigDocument();

        // retrieve appSettings node
        XmlNode node = doc.SelectSingleNode("//connectionStrings");

        try
        {
            if (node == null)
                throw new InvalidOperationException("connectionStrings section not found in config file.");
            else
            {
                // remove 'add' element with coresponding key
                node.RemoveChild(node.SelectSingleNode(string.Format("//add[@name='{0}']", key)));
                doc.Save(getConfigFilePath());
            }
        }
        catch (NullReferenceException e)
        {
            throw new Exception(string.Format("The name {0} does not exist.", key), e);
        }
    }

    private static XmlDocument loadConfigDocument()
    {
        XmlDocument doc = null;
        try
        {
            doc = new XmlDocument();
            doc.Load(getConfigFilePath());
            return doc;
        }
        catch (System.IO.FileNotFoundException e)
        {
            throw new Exception("No configuration file found.", e);
        }
    }

    private static string getConfigFilePath()
    {
        return System.Web.HttpContext.Current.Server.MapPath("web.config");
    }
}