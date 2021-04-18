using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Script_Boi
{
    public static void save(Color_Name stat)
    {
        BinaryFormatter format = new BinaryFormatter();
        string path = Application.persistentDataPath + "/NameWork.FU";
        FileStream stream = new FileStream(path, FileMode.Create);

        Userdata info = new Userdata(stat);
        format.Serialize(stream, info);
        stream.Close();

    }
    public static Userdata Load()
    {
        string path = Application.persistentDataPath + "/NameWork.FU";
        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            BinaryFormatter format = new BinaryFormatter();
            Userdata data = format.Deserialize(stream) as Userdata;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("file does not exest at " +path);
            return null;
        }
        
    }
}
