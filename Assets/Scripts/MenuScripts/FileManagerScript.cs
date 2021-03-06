﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class FileManagerScript {
    private const string saveName = "Saves/colorsSave.binary";

    public static void WriteStringToFile(string str, string filename)
    {
#if !WEB_BUILD
        string path = pathForDocumentsFile(filename);
        FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);

        StreamWriter sw = new StreamWriter(file);
        sw.WriteLine(str);

        sw.Close();
        file.Close();
#endif
    }


    public static string ReadStringFromFile(string filename)//, int lineIndex )
    {
#if !WEB_BUILD
        string path = pathForDocumentsFile(filename);

        if (File.Exists(path))
        {
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(file);

            string str = null;
            str = sr.ReadLine();

            sr.Close();
            file.Close();

            return str;
        }

        else
        {
            return null;
        }
#else
return null;
#endif
    }


    public static string pathForDocumentsFile(string filename)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            string path = Application.dataPath.Substring(0, Application.dataPath.Length - 5);
            path = path.Substring(0, path.LastIndexOf('/'));
            return Path.Combine(Path.Combine(path, "Documents"), filename);
        }

        else if (Application.platform == RuntimePlatform.Android)
        {
            string path = Application.persistentDataPath;
            path = path.Substring(0, path.LastIndexOf('/'));
            return Path.Combine(path, filename);
        }

        else
        {
            string path = Application.dataPath;
            path = path.Substring(0, path.LastIndexOf('/'));
            return Path.Combine(path, filename);
        }
    }

    public static void SaveData(DataScript data)
    {
        String fileName = FileManagerScript.pathForDocumentsFile(saveName);
        if (!File.Exists(fileName))
            Directory.CreateDirectory(FileManagerScript.pathForDocumentsFile("Saves"));

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Create(fileName);

        formatter.Serialize(saveFile, data);

        saveFile.Close();
    }

    public static DataScript LoadData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        String fileName = FileManagerScript.pathForDocumentsFile(saveName);
        if (!File.Exists(fileName))
        {
            return new DataScript();
        }
        FileStream saveFile = File.Open(fileName, FileMode.Open);
        
        DataScript result = (DataScript)formatter.Deserialize(saveFile);

        saveFile.Close();

        return result;
    }
}
