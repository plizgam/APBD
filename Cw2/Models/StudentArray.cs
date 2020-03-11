using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Cw2.Models
{

    [Serializable]
    public class StudentArray
    {
        [XmlAttribute]
        public string author { get; set; }

        [XmlAttribute]
        public string createdAt { get; set; }


        public HashSet<Student> students;
    }
}
