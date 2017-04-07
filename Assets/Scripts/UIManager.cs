using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Text text;

	// Use this for initialization
	void Start () {
        //text = GetComponentInChildren<Text>();
	}

    public void buttonTest()
    {
        text.text = "button clicked";
    }
	
}
