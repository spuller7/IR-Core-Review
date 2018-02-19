using UnityEngine;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;

public static class SaveLoad
{
    
    private static string SaveFilePath
    {
        get { return Application.persistentDataPath + "/savedTests.sbs"; }
    }

    private static Dictionary<int, int> tests = new Dictionary<int, int>();
    public static List<Test> savedTests = new List<Test>();

    //it's static so we can call it from anywhere
    public static void Save()
    {
        tests.Clear();
        for (int i = 0; i < savedTests.Count; i++)
        {
            tests.Add(savedTests[i].getTestID(), i);
            if (i > 7)
            {
                int min = tests.Keys.Min();
                savedTests.RemoveAt(tests[min]);
            }
        }
        
        if (tests.ContainsKey(Test.current.getTestID()))
        {
            for (int i = 0; i < savedTests.Count; i++)
            {
                if(savedTests[i].getTestID() == Test.current.getTestID())
                {
                    savedTests.RemoveAt(i);
                }
            }
        }

        savedTests.Add(Test.current);
        BinaryFormatter bf = new BinaryFormatter();
        //Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
        FileStream file = File.Create(Application.persistentDataPath + "/savedTests.sbs"); //you can call it anything you want
        bf.Serialize(file, savedTests);
        file.Close();
    }

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedTests.sbs"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedTests.sbs", FileMode.Open);
            savedTests = (List<Test>)bf.Deserialize(file);
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