using ExtWatcher.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ExtWatcher.Common.Service
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot( ElementName = "configuration", Namespace ="", IsNullable = false)]
    public partial class WCFServices
    {
        private ServiceModel _serviceModel;

        [XmlElement("system.serviceModel")]
        public ServiceModel ServiceModel
        {
            get { return _serviceModel; }
            set { _serviceModel = value; }
        }

        public static WCFServices Create(string configFile)
        {
            var serializer = new XmlSerializer(typeof(WCFServices));
            using (var reader = XmlReader.Create(configFile))
            {
                return (WCFServices)serializer.Deserialize(reader);
            }
        }
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public partial class ServiceModel
    {
        private List<Service> _serviceList = new List<Service>();

        [XmlArray("services", IsNullable = false)]
        [XmlArrayItem("service", IsNullable = false)]
        public Service[] Services
        {
            get { return _serviceList.ToArray(); }
            set
            {
                if (_serviceList == null)
                {
                    _serviceList = new List<Service>();
                }
                _serviceList.AddRange(value);
            }
        }
    }

    [Serializable]
    [XmlType(AnonymousType = true)]
    public partial class Service
    {
        private string _assemblyName;
        private string _className;
        private string _name;

        [XmlAttribute(AttributeName = "name")]
        public string Name
        {
            get { return _name; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _name = value;
                    // name="<assemblyName>:<className>"
                    string[] components = _name.Split(new[] { ':' });

                    switch(components.Length)
                    {
                        case 2:
                            // Assembly name and class name specified.  This allows us to have the class located in a differently named assembly.
                            _assemblyName = components[0];
                            _className = components[1];
                            break;
                        default:
                            Logger.WriteToLog("Invalid appConfig <system.serviceModel\\services\\service> node name param."
                                + "The name param must follow the name=\"<assemblyName>:<className\" naming convention\n");
                            break;
                    }
                }
            }
        }

        [XmlIgnore]
        public string AssemblyName
        {
            get
            {
                // Append .Dll if required
                return _assemblyName.IndexOf(".dll", StringComparison.CurrentCultureIgnoreCase) == -1 ? _assemblyName + ".dll" : _assemblyName;
            }

            set { _assemblyName = value; }
        }

        [XmlIgnore]
        public string ClassName
        {
            get { return _className; }
            set { _className = value; }
        }
    }
}
