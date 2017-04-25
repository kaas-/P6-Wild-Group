using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Logger : MonoBehaviour {

    private static string filename;
    private static string path = Application.persistentDataPath;

    public static void StartLog (string UserName, string TestType)
    {
        Debug.Log("user_" + UserName + "_" + TestType);
    }

}
