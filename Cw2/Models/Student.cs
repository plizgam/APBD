using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;


namespace Cw2
{

    [Serializable]
    public class Student
    {
        [XmlAttribute (AttributeName = "indexNumber")]
        [JsonProperty("indexNumber")]
        public string index { get; set; }

        [XmlElement(ElementName = "fname")]
        [JsonProperty("fname")]
        public string firstName { get; set; }

        [XmlElement(ElementName = "lname")]
        [JsonProperty("lname")]
        public string lastName { get ; set; }

        public string birthdate { get; set; }
        public string email { get; set; }
        public string mothersName { get; set; }
        public string fathersName { get; set; }

        public Studies studies { get; set; }

     
    }

    public class Studies
    {
        [XmlElement(ElementName = "name")]
        [JsonProperty("name")]
        public string studyName { get; set; }

        [XmlElement(ElementName = "mode")]
        [JsonProperty("mode")]
        public string studyMode { get; set; }
    }
}
