/*
 * Created by SharpDevelop.
 * User: AdamS-0
 * Date: 07.08.2025
 * Time: 13:36
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace streamNode {
    
    [XmlType("NodeCfgConn")]
    public class NodeCfgConn {
        
        [XmlElement("Name")]
        public string name;
        
        [XmlElement("AllowIn")]
        public bool allowIn;
        
        [XmlElement("AllowOut")]
        public bool allowOut;
        
        [XmlElement("StreamCfg")]
        public string streamCfg;
    }
    
    [Serializable, XmlRoot("NodeCfg")]
    [XmlInclude(typeof(NodeCfgConn))]
    public class NodeCfg {
        
        [XmlArray("OptionsArray")]
        [XmlArrayItem("OptionsObject")]
        public List<NodeCfgConn> conns = new List<NodeCfgConn>();
        
        public NodeCfg() { }
        
        public static NodeCfg ReadFromFile(string pathToFile) {
            NodeCfg _aliases = new NodeCfg();
            _aliases.conns = new List<NodeCfgConn>();
            
            /* File does not exists */
            if (!File.Exists(pathToFile)) { return _aliases; }
            
            _aliases = CfgManager.ReadFromXmlFile<NodeCfg>(pathToFile);
            
            return _aliases;
        }
        
        public static void SaveToFile(string pathToFile, ref NodeCfg nodeCfg) {
            CfgManager.WriteToXmlFile<NodeCfg>(pathToFile, nodeCfg);
        }
        
    }
    
    
    /// <summary>
    /// Description of CfgManager.
    /// </summary>
    public class CfgManager {
        public CfgManager() {
        }
        
        
        
        /// <summary>
        /// Writes the given object instance to an XML file.
        /// <para>Only Public properties and variables will be written to the file. These can be any type though, even other classes.</para>
        /// <para>If there are public properties/variables that you do not want written to the file, decorate them with the [XmlIgnore] attribute.</para>
        /// <para>Object type must have a parameterless constructor.</para>
        /// </summary>
        /// <typeparam name="T">The type of object being written to the file.</typeparam>
        /// <param name="filePath">The file path to write the object instance to.</param>
        /// <param name="objectToWrite">The object instance to write to the file.</param>
        /// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
        public static void WriteToXmlFile<T>(string filePath, T objectToWrite, bool append = false) where T : new() {
            using( TextWriter writer = new StreamWriter(filePath, append) ) {
                try {
                    var serializer = new XmlSerializer(typeof(T));
                    serializer.Serialize(writer, objectToWrite);
                    writer.Close();
                } finally {}
            }

        }
        
        /// <summary>
        /// Reads an object instance from an XML file.
        /// <para>Object type must have a parameterless constructor.</para>
        /// </summary>
        /// <typeparam name="T">The type of object to read from the file.</typeparam>
        /// <param name="filePath">The file path to read the object instance from.</param>
        /// <returns>Returns a new instance of the object read from the XML file.</returns>
        public static T ReadFromXmlFile<T>(string filePath) where T : new() {
            T dataType;
            string strBlock = "";
            
            using (StreamReader sr = new StreamReader(filePath)) {
                strBlock = sr.ReadToEnd();
            }
            
            using( TextReader reader = new StringReader(strBlock) ) {
                try {
                    var serializer = new XmlSerializer(typeof(T));
                    dataType = (T)serializer.Deserialize(reader);
                    reader.Close();
                    return dataType;
                } finally { }
            }

        }
        
    }
}
