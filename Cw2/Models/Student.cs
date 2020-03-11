using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Cw2
{

    [Serializable]
    public class Student
    {

        [XmlElement (ElementName  = "indexNumber")]
        public string index { get; set; }

        [XmlElement(ElementName = "fname")]
        public string firstName { get; set; }

        [XmlElement(ElementName = "lname")]
        public string lastName { get ; set; }

        public string birthdate { get; set; }
        public string email { get; set; }
        public string mothersName { get; set; }
        public string fathersName { get; set; }


        public class Studies
        {
            [XmlElement(ElementName = "name")]
            public string studyName { get; set; }

            [XmlElement(ElementName = "mode")]
            public string studyMode { get; set; }
        }
    }
}
