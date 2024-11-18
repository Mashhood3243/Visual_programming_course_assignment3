using System;
using System.Xml;

class Program
{
    static void Main(string[] args)
    {
        create_xml_file();
        read_and_display_xml_file();
    }

    static void create_xml_file()
    {
        Console.WriteLine("Please enter the details to create the XML file.");

        // Collecting user input
        Console.Write("Enter the DateTime (e.g., 1/26/2017 5:08:59 PM): ");
        string date_time = Console.ReadLine();

        Console.Write("Enter the x coordinate (e.g., 65.8973342): ");
        string x_coordinate = Console.ReadLine();

        Console.Write("Enter the y coordinate (e.g., 72.3452346): ");
        string y_coordinate = Console.ReadLine();

        Console.Write("Enter the Speed (e.g., 40): ");
        string speed = Console.ReadLine();

        Console.Write("Enter the Number of Satellites (NoSatt, e.g., 7): ");
        string no_satt = Console.ReadLine();

        Console.Write("Enter the Path of the image (e.g., \\images\\1.jpg): ");
        string image_path = Console.ReadLine();

        // Creating the XML file with the user inputs
        XmlWriterSettings xml_settings = new XmlWriterSettings();
        xml_settings.Indent = true;
        xml_settings.IndentChars = "\t";
        xml_settings.Encoding = System.Text.Encoding.UTF8;

        XmlWriter xml_writer = XmlWriter.Create("gps_log.xml", xml_settings);

        xml_writer.WriteStartDocument();
        xml_writer.WriteStartElement("GPS_Log");

        xml_writer.WriteStartElement("Position");
        xml_writer.WriteAttributeString("DateTime", date_time);
        xml_writer.WriteElementString("x", x_coordinate);
        xml_writer.WriteElementString("y", y_coordinate);

        xml_writer.WriteStartElement("SatteliteInfo");
        xml_writer.WriteElementString("Speed", speed);
        xml_writer.WriteElementString("NoSatt", no_satt);
        xml_writer.WriteEndElement();

        xml_writer.WriteEndElement();

        xml_writer.WriteStartElement("Image");
        xml_writer.WriteAttributeString("Resolution", "1024x800");
        xml_writer.WriteElementString("Path", image_path);
        xml_writer.WriteEndElement();

        xml_writer.WriteEndElement();
        xml_writer.WriteEndDocument();
        xml_writer.Close();

        Console.WriteLine("The XML file 'gps_log.xml' has been created successfully.");
    }

    static void read_and_display_xml_file()
    {
        Console.WriteLine("\nReading the XML file:");

        XmlDocument xml_document = new XmlDocument();
        xml_document.Load("gps_log.xml");

        XmlElement root_element = xml_document.DocumentElement;

        if (root_element != null)
        {
            Console.WriteLine("<" + root_element.Name + ">");
            
            foreach (XmlNode node in root_element.ChildNodes)
            {
                if (node.Name == "Position")
                {
                    Console.WriteLine($"\t<{node.Name} DateTime=\"{node.Attributes["DateTime"].Value}\">");

                    foreach (XmlNode child in node.ChildNodes)
                    {
                        Console.WriteLine($"\t\t<{child.Name}>{child.InnerText}</{child.Name}>");
                    }

                    XmlNode satellite_info_node = node["SatteliteInfo"];
                    Console.WriteLine($"\t\t<{satellite_info_node.Name}>");

                    foreach (XmlNode sat_info_child in satellite_info_node.ChildNodes)
                    {
                        Console.WriteLine($"\t\t\t<{sat_info_child.Name}>{sat_info_child.InnerText}</{sat_info_child.Name}>");
                    }

                    Console.WriteLine($"\t\t</{satellite_info_node.Name}>");
                    Console.WriteLine($"\t</{node.Name}>");
                }
                else if (node.Name == "Image")
                {
                    Console.WriteLine($"\t<{node.Name} Resolution=\"{node.Attributes["Resolution"].Value}\">");
                    Console.WriteLine($"\t\t<Path>{node["Path"].InnerText}</Path>");
                    Console.WriteLine($"\t</{node.Name}>");
                }
            }

            Console.WriteLine("</" + root_element.Name + ">");
        }
    }
}
