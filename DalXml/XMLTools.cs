using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.IO;

namespace Dal
{
    /// <summary>
    /// tools of loading data from the xml files
    /// </summary>
    public class XMLTools
    {
        #region SaveLoadWithXMLSerializer

        /// <summary>
        /// saves list to xml file, using XmlSerializer
        /// </summary>
        /// <typeparam name="T">type of object</typeparam>
        /// <param name="list">list of objects</param>
        /// <param name="filePath">file path</param>
        public static void SaveListToXMLSerializer<T>(IEnumerable<T> list, string filePath)
        {
            try
            {
                FileStream file = new FileStream(filePath, FileMode.Create);
                XmlSerializer x = new XmlSerializer(list.GetType());
                x.Serialize(file, list);
                file.Close();
            }
            catch (Exception ex)
            {
                throw new FileLoadException(filePath, $"fail to create xml file: {filePath}", ex);
            }
        }

        /// <summary>
        /// loads list of objects from xml file, using XmlSerializer
        /// </summary>
        /// <typeparam name="T">type of object</typeparam>
        /// <param name="filePath">file path</param>
        /// <returns></returns>
        public static IEnumerable<T> LoadListFromXMLSerializer<T>(string filePath)
        {

            try
            {
                if (File.Exists(filePath))
                {
                    IEnumerable<T> list;
                    XmlSerializer x = new XmlSerializer(typeof(List<T>));
                    FileStream file = new FileStream(filePath, FileMode.Open);
                    try
                    {
                        list = (IEnumerable<T>)x.Deserialize(file);
                    }
                    catch(Exception)
                    {
                        list = new List<T>();
                    }
                    file.Close();
                    return list;
                }
                else
                    throw new FileNotFoundException(filePath);

            }
            catch (Exception ex)
            {
                throw new FileNotFoundException(filePath);
            }

        }
        #endregion

        /// <summary>
        /// loads xml elements from xml file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>XElement</returns>
        public static XElement LoadData(string filePath)
        {
            try
            {
                return XElement.Load(filePath);
            }
            catch
            {
                throw new FileNotFoundException(filePath);
            }
        }
    }


}
