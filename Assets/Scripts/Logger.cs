using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Logger : MonoBehaviour {

    private static string filename;
    private static string path = Application.persistentDataPath + "/logs/";

    private static StreamWriter fileWriter;

    public static void InitLog (string UserName, string TestType)
    {
        Debug.Log("user_" + UserName + "_" + TestType);
        Debug.Log(path);

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        filename = "test_log_" + string.Format("{0:HH mm ss yyyy-MM-dd}", DateTime.Now) + "_participant_" + UserName + "_type_" + TestType + ".txt";

        fileWriter = new StreamWriter(path + filename);
        fileWriter.WriteLine("Time\tEvent");

    }

    public static void StartLog()
    {
        string time = DateTime.Now.ToString("HH:mm:ss:ffff");
        fileWriter.WriteLine(time + "\t" + "Task started");
    }

    public static void FinishLog()
    {
        string time = DateTime.Now.ToString("HH:mm:ss:ffff");
        fileWriter.WriteLine(time + "\t" + "Task finished");
        fileWriter.Close();
    }

    public static void EventLog(string eventLogged)
    {
        string time = DateTime.Now.ToString("HH:mm:ss:ffff");
        fileWriter.WriteLine(time + "\t" + eventLogged);
    }

}
