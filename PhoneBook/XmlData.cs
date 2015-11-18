using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace PhoneBook
{
    class XmlData
    {
        public void WriteSaveXml(string path, string name, string phone, string mail)
        {
            try
            {
                var streamReader = new StreamReader(path);
                var line = streamReader.ReadLine();
                streamReader.Close();
                if (line == null)
                {
                    var writer = new XmlTextWriter(new StreamWriter(path, true)) { Formatting = Formatting.Indented };
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Users");
                    writer.WriteStartElement("user");
                    writer.WriteAttributeString("id", name);
                    writer.WriteElementString("name", name);
                    writer.WriteElementString("phone", phone);
                    writer.WriteElementString("mail", mail);
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                    writer.Flush();
                    writer.Close();
                }
                else
                {
                    var doc = new XmlDocument();
                    doc.Load(path);

                    var newUser = doc.CreateElement("user");
                    newUser.SetAttribute("id", name);

                    var newLoginElement = doc.CreateElement("name");
                    newLoginElement.InnerText = name;
                    newUser.AppendChild(newLoginElement);

                    var newPhoneElement = doc.CreateElement("phone");
                    newPhoneElement.InnerText = phone;
                    newUser.AppendChild(newPhoneElement);

                    var newMailElement = doc.CreateElement("mail");
                    newMailElement.InnerText = mail;
                    newUser.AppendChild(newMailElement);

                    doc.DocumentElement.AppendChild(newUser);
                    doc.Save(path);
                }
            }
            catch (Exception exception)
            {
                Console.Write(exception.Message);
            }
        }

        public List<XmlWithData> ReadSaveXml(string path)
        {
            var list = new List<XmlWithData>();
            try
            {
                var xDoc = new XmlDocument();
                xDoc.Load(path);
                var xRoot = xDoc.DocumentElement;
                if (xRoot != null)
                    foreach (XmlElement xnode in xRoot)
                    {
                        var dataGrid = new XmlWithData();
                        foreach (XmlNode childnode in xnode.ChildNodes)
                        {

                            if (childnode.Name == "name")
                                dataGrid.Name = childnode.InnerText;
                            if (childnode.Name == "phone")
                                dataGrid.Phone = childnode.InnerText;
                            if (childnode.Name == "mail")
                                dataGrid.Mail = childnode.InnerText;
                        }
                        list.Add(dataGrid);
                    }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            return list;
        }

        public static void XmlRemoveAll(string path)
        {
            var file = new StreamWriter(path, false);
            file.Close();
        }
    }
}
