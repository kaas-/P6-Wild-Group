﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // Rotate the object around its local X axis at 1 degree per second
        transform.Rotate(Vector3.right * 10);

        // ...also rotate around the World's Y axis
        transform.Rotate(Vector3.up * 10, Space.World);
    }
}