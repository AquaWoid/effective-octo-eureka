using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{

    public static void SavePlayer(PlayerStats player)
    {

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.wld";
        FileStream stream = new FileStream(path, FileMode.Create);
        
        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("wrote to: " + path);

    }

    public static PlayerData LoadPlayer()
    {

        string path = Application.persistentDataPath + "/player.wld";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            Debug.Log("read from: " + path);

            return data;

        }
        else
        {

            Debug.LogError("save file not found in" + path);
            return null;

        }

    }


    public static void SaveWorld(WorldStats worldStats)
    {

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/world.wld";
        FileStream stream = new FileStream(path, FileMode.Create);

        WorldData data = new WorldData(worldStats);

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("wrote to: " + path);

    }

    public static WorldData LoadWorld()
    {

        string path = Application.persistentDataPath + "/world.wld";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            WorldData data = formatter.Deserialize(stream) as WorldData;
            stream.Close();

            Debug.Log("read from: " + path);

            return data;

        }
        else
        {
            Debug.LogError("save file not found in" + path);
            return null;
        }

    }

}
