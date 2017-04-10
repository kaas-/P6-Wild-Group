using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject DefaultUI;
    public GameObject LockScreenUI;
    public GameObject InstructionUI;

    public GameObject Instruction1, Instruction2, Instruction3;
    private GameObject[] Instructions;

    public Camera lockScreenCamera;

    private int InstructionCounter = 0;

	// Use this for initialization
	void Awake () {

        Instructions = new GameObject[] { Instruction1, Instruction2, Instruction3 };

        LockScreenUI.SetActive(false);
        InstructionUI.SetActive(false);

	}

    public void OnInstructionButton()
    {
        DefaultUI.SetActive(false);
        
        InstructionUI.SetActive(true);

        InstructionCounter = 0;

        foreach (GameObject instruction in Instructions)
        {
            instruction.SetActive(false);
        }

        Instructions[InstructionCounter].SetActive(true);

    }

    //based on http://answers.unity3d.com/questions/9969/convert-a-rendertexture-to-a-texture2d.html
    public void LockScreenButton()
    {
        DefaultUI.SetActive(false);
        LockScreenUI.SetActive(true);

        lockScreenCamera.GetComponent<LockScreenCamera>().UpdateCamera();

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


    public void OnForwardButton()
    {

        if (InstructionCounter >= Instructions.Length - 1)
        {
            InstructionCounter = Instructions.Length - 1;
        }
        else
        {
            Instructions[InstructionCounter].SetActive(false);
            InstructionCounter++;
            Instructions[InstructionCounter].SetActive(true);
        }
    }

    public void OnBackButton()
    {
        if (InstructionCounter <= 0)
        {
            InstructionCounter = 0;
        }
        else
        {
            Instructions[InstructionCounter].SetActive(false);
            InstructionCounter--;
            Instructions[InstructionCounter].SetActive(true);
        }
    }

    public void OnReturnButton()
    {
        LockScreenUI.SetActive(false);
        InstructionUI.SetActive(false);
        DefaultUI.SetActive(true);
    }
}
