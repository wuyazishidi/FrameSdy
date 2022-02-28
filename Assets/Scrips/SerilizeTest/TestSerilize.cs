using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

[System.Serializable]
public class TestSerilize
{
    void Start()
    {
        
    }
    [XmlAttribute("Id")]
    public int Id { get; set; }
    [XmlAttribute("Name")]
    public string Name { get; set; }
    [XmlElement("List")]
    public List<int> List { get; set; }

    [XmlElement("ABList")]
    public List<ABBase> ABList { get; set; }

}
