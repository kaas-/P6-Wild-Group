using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Logger : MonoBehaviour {

    private string filename;
    private string path;

    void StartLog ()
    {

        path = Application.persistentDataPath;
    }

}
