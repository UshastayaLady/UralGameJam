using UnityEngine;
using System.Xml.Serialization;     //запись и чтение xml файла
using System.IO;

[XmlRoot("xmlDialogue")]
public class ReadXmlDialogue
{
    //[XmlElement("text")]
    //public string name;

    [XmlElement("node")]
    public Node[] nodes;

    public static ReadXmlDialogue Load(TextAsset _xml)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ReadXmlDialogue));
        StringReader reader = new StringReader(_xml.text);
        ReadXmlDialogue dial = serializer.Deserialize(reader) as ReadXmlDialogue;        
        return dial;
    }

    public void Remove()
    {
        for (int i = 0; i < nodes.Length; i++)
        {
            for (int j = 0; j < nodes[i].answers.Length; j++)
            {
                nodes[i].answers[j].text = "";
            }
            nodes[i].npcText = "";
        }
    }
}

[System.Serializable]
public class Node
{
    [XmlElement("npcText")]
    public string npcText;

    [XmlArray("answers")]
    [XmlArrayItem("answer")]
    public Answer[] answers; 
}

public class Answer
{
    [XmlAttribute("toNode")]
    public int nextNode;
    [XmlElement("text")]
    public string text;    
    [XmlElement("end")] // Конец диалога без перехода на следующий, т.е. данный диалог будет проигран еще раз
    public string endRestart;
    
    [XmlElement("nextDialogue")] // Конец диалога и переход на следующий
    public ЕndNextDialogue nextDialogue;

    //[XmlAttribute("nextNumberDialogue")]
    //public int nextNumberDialogue;
}

public class ЕndNextDialogue
{
    [XmlAttribute("nextNumberDialogue")]
    public int nextNumberDialogue;
}