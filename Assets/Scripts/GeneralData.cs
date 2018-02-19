using UnityEngine;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class GeneralData
{

    private static string SaveFilePath
    {
        get { return Application.persistentDataPath + "/general.sbs"; }
    }

    public static General general;

    //it's static so we can call it from anywhere
    public static void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        //Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
        FileStream file = File.Create(Application.persistentDataPath + "/general.sbs"); //you can call it anything you want
        bf.Serialize(file, general);
        file.Close();
    }

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/general.sbs"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/general.sbs", FileMode.Open);
            general = (General)bf.Deserialize(file);
            file.Close();
        }
    }


    public static void Delete()
    {
        try
        {
            File.Delete(SaveFilePath);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
}