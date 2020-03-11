using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Cw2
{
    [JsonObject(MemberSerialization.Fields)]
    public class StudentArray
    {
   
        [JsonProperty("createdAt")]
        private string _createdAt;
        [XmlAttribute]
        public string createdAt
        {
            get { return _createdAt; }
            set { _createdAt = value; }
        }

        [JsonProperty("author")]
        private string _author;
        [XmlAttribute]
        public string author
        {
            get { return _author; }
            set { _author = value; }
        }


        [JsonProperty("studenci")]
        public HashSet<Student> students;

        public HashSet<ActiveStudies> activeStudies;
    }

    [XmlType("studies")]
    public class ActiveStudies
    {
        [XmlAttribute]
        public string name { get; set; }
        [XmlAttribute]
        public int numberOfStudents { get; set; }
    }
}
