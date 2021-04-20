using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Script_Boi
{
    //this is to save and load
    public static void save(Color_Name stat)
    {
        //format into binary have a plication persistant path file name of NameWork.FU serlize and close stream
        BinaryFormatter format = new BinaryFormatter();
        string path = Application.persistentDataPath + "/NameWork.FU";
        FileStream stream = new FileStream(path, FileMode.Create);

        Userdata info = new Userdata(stat);
        format.Serialize(stream, info);
        stream.Close();

    }
    public static Userdata Load()
    {
        //load the data if it is ther and unserailize it and return the datclass that stores the values
        string path = Application.persistentDataPath + "/NameWork.FU";
        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            BinaryFormatter format = new BinaryFormatter();
            Userdata data = format.Deserialize(stream) as Userdata;
            stream.Close();
           // Debug.Log(path);
            return data;
        }
        else
        {
            Debug.LogError("file does not exest at " +path);
            return null;
        }
        
    }
  

}
