using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject DefaultUI;
    public GameObject LockScreenUI;

    public Camera lockScreenCamera;



	// Use this for initialization
	void Start () {
        LockScreenUI.SetActive(false);
	}

    public void buttonTest()
    {

    }

    //based on http://answers.unity3d.com/questions/9969/convert-a-rendertexture-to-a-texture2d.html
    public void LockScreenButton()
    {
        DefaultUI.SetActive(false);
        LockScreenUI.SetActive(true);

        RenderTexture tempRT = new RenderTexture(Screen.width, Screen.height, 24);
        lockScreenCamera.targetTexture = tempRT;
        lockScreenCamera.Render();

        RenderTexture.active = tempRT;

        Texture2D snapshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        snapshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        snapshot.Apply();

        RenderTexture.active = null;
        lockScreenCamera.targetTexture = null;

        RawImage lockScreenTexture = LockScreenUI.GetComponentInChildren<RawImage>();

        lockScreenTexture.texture = snapshot;
        lockScreenTexture.color = Color.white;

    }

    public void UnlockScreenButton()
    {
        LockScreenUI.SetActive(false);
        DefaultUI.SetActive(true);
    }
}
