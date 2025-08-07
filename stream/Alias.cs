/*
 * Created by SharpDevelop.
 * User: AdamS-0
 * Date: 19.06.2025
 * Time: 19:06
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace stream {
    /// <summary>
    /// Description of Alias.
    /// </summary>
    [XmlType("Alias")]
    public class Alias {
        [XmlElement("Name")]
        public string Name;
        [XmlElement("Options")]
        public Options options;
        
        public Alias() {
            
        }
        
        public Alias(string aliasName, ref Options options) {
            this.Name = aliasName;
            this.options = options;
        }
        
    }
    
    
    
    [Serializable, XmlRoot("Aliases")]
    [XmlInclude(typeof(Alias))]
    public class Aliases {
        
        [XmlArray("OptionsArray")]
        [XmlArrayItem("OptionsObject")]
        public List<Alias> items;
        
        private static void updateMaxLengths(ref Options options, ref List<int> maxFieldLen) {
            int propValLength = 0;
            int idx = 0;
            
            int optionsPropsCount = typeof(Options).GetProperties().Length;
            
            /* Increase array size, to fit all properties of Options */
            while (maxFieldLen.Count < optionsPropsCount) {
                maxFieldLen.Add(0);
            }
            
            foreach (PropertyInfo propInfo in typeof(Options).GetProperties()) {
                propValLength = 0;
                
                try {
                    /* Get property's value length */
                    propValLength = propInfo.GetValue(options).ToString().Length;
                    
                    if (propValLength > maxFieldLen [ idx ]) {
                        /* Update new max length for this property */
                        maxFieldLen [ idx ] = propValLength;
                    }
                } catch {
                }
                
                ++idx;
            }
        }
        
        public void ShowAliases() {
            List<int> maxFieldLen = new List<int>();
            int maxNameLength = 0;
            int aliasNoLength = 0;
            int idx = 0;
            int idxAlias = 0;
            string propValue = "";
            string strAliasNo = "No.";
            
            foreach (Alias als in this.items) {
                if (als.Name.Length > maxNameLength) {
                    /* Update new max length for alias name */
                    maxNameLength = als.Name.Length;
                }
                
                updateMaxLengths(ref als.options, ref maxFieldLen);
            }
            
            
            foreach (PropertyInfo propInfo in typeof(Options).GetProperties()) {
                try {
                    if( maxFieldLen[idx] < propInfo.Name.Length ) {
                        maxFieldLen[idx] = propInfo.Name.Length;
                    }
                } catch {}
                ++idx;
                
            }
            
            
            /* Print table */
            idx = 0;
            
            aliasNoLength = items.Count.ToString().Length;
            if( strAliasNo.Length > aliasNoLength ) { aliasNoLength = strAliasNo.Length; }
            
            Console.Write("{0," + aliasNoLength + "}|", strAliasNo);
            
            foreach (PropertyInfo propInfo in typeof(Options).GetProperties()) {
                try {
                    Console.Write("{0," +  maxFieldLen[idx] + "}|", propInfo.Name);
                } catch {}
                ++idx;
            }
            Console.WriteLine("");
            
            idxAlias = 1;
            foreach (Alias als in this.items) {
                idx = 0;
                Console.Write("{0," + aliasNoLength + "}|", idxAlias);
                foreach (PropertyInfo propInfo in typeof(Options).GetProperties()) {
                    try {
                        if( propInfo.GetValue(als.options) != null ) {
                            propValue = propInfo.GetValue(als.options).ToString();
                            if( propValue.Length == 0 ) { propValue = "X"; }
                        } else { propValue = ""; }
                        
                        Console.Write("{0," +  maxFieldLen[idx] + "}|", propValue);
                    } catch {}
                    ++idx;
                }
                Console.WriteLine("");
                ++idxAlias;
            }
            idx = 0;
        }
        
        
        public static Aliases ReadFromFile(string pathToFile) {
            Aliases _aliases = new Aliases();
            _aliases.items = new List<Alias>();
            
            /* File does not exists */
            if (!File.Exists(pathToFile)) { return _aliases; }
            
            _aliases = ReadFromXmlFile<Aliases>(pathToFile);
            
            return _aliases;
        }
        
        public static void SaveToFile(string pathToFile, ref Aliases aliases) {
            WriteToXmlFile<Aliases>(pathToFile, aliases);
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
        private static void WriteToXmlFile<T>(string filePath, T objectToWrite, bool append = false) where T : new() {
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
        private static T ReadFromXmlFile<T>(string filePath) where T : new() {
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
