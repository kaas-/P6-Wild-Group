using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject DefaultUI;
    public GameObject LockScreenUI;
    public GameObject InstructionUI;
    public GameObject StartUI;

    public GameObject Instruction1, Instruction2, Instruction3;
    private GameObject[] Instructions;

    public Camera lockScreenCamera;

    private int InstructionCounter = 0;
    public bool ScreenLocked;
    public bool TrackingLost = true;

    public GameObject InstructionObject1, InstructionObject2, InstructionObject3;
    private GameObject[] InstructionObjects;

    public GameObject InstructionsForwardButton;
    public GameObject InstructionsBackButton;
    public GameObject InstructionsDoneButton;

    private bool isStarted = false;

    // Use this for initialization
    void Awake () {

        Instructions = new GameObject[] { Instruction1, Instruction2, Instruction3 };
        InstructionObjects = new GameObject[] { InstructionObject1, InstructionObject2, InstructionObject3 };

        LockScreenUI.SetActive(false);
        InstructionUI.SetActive(false);
        DefaultUI.SetActive(false);
	}

    public void OnInstructionScreen()
    {
        DefaultUI.SetActive(false);
        
        InstructionUI.SetActive(true);

        InstructionCounter = 0;

        foreach (GameObject instruction in Instructions)
        {
            instruction.SetActive(false);
        }

        foreach (GameObject instruction in InstructionObjects)
        {
            instruction.SetActive(false);
        }

        Instructions[InstructionCounter].SetActive(true);
        InstructionObjects[InstructionCounter].SetActive(true);

        InstructionsForwardButton.SetActive(true);
        InstructionsBackButton.SetActive(false);
        InstructionsDoneButton.SetActive(false);

    }

    public void OnStartButton()
    {
        isStarted = true;
        StartUI.SetActive(false);
        DefaultUI.SetActive(true);
    }

    //based on http://answers.unity3d.com/questions/9969/convert-a-rendertexture-to-a-texture2d.html
    public void LockScreenButton()
    {
        InstructionUI.SetActive(false);
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

        ScreenLocked = true;

    }


    public void OnForwardButton()
    {

        if (InstructionCounter != Instructions.Length - 1)
        {
            if(InstructionCounter == 0)
            {
                InstructionsBackButton.SetActive(true);
            }

            Instructions[InstructionCounter].SetActive(false);
            InstructionObjects[InstructionCounter].SetActive(false);
            InstructionCounter++;
            Instructions[InstructionCounter].SetActive(true);
            InstructionObjects[InstructionCounter].SetActive(true);
        }

        if (InstructionCounter == Instructions.Length - 1)
        {
            InstructionCounter = Instructions.Length - 1;
            InstructionsForwardButton.SetActive(false);
            InstructionsDoneButton.SetActive(true);
        }
    }

    public void OnBackButton()
    {
        if (InstructionCounter != 0)
        {
            if(InstructionCounter == Instructions.Length-1)
            {
                InstructionsForwardButton.SetActive(true);
                InstructionsDoneButton.SetActive(false);
            }

            Instructions[InstructionCounter].SetActive(false);
            InstructionObjects[InstructionCounter].SetActive(false);
            InstructionCounter--;
            Instructions[InstructionCounter].SetActive(true);
            InstructionObjects[InstructionCounter].SetActive(true);
        }
        if (InstructionCounter == 0)
        {
            InstructionsBackButton.SetActive(false);
        }
    }

    public void OnReturnButton()
    {
        InstructionUI.SetActive(false);
        DefaultUI.SetActive(true);
        InstructionCounter = 0;
    }

    public void OnScreenUnlockButton()
    {
        ScreenLocked = false;
        LockScreenUI.SetActive(false);
        InstructionUI.SetActive(true);
    }

    public void OnDoneButton()
    {
        isStarted = false;
        InstructionCounter = 0;
        InstructionUI.SetActive(false);
        StartUI.SetActive(true);
    }

    void Update()
    {

        if(!TrackingLost && !ScreenLocked && !InstructionUI.activeInHierarchy && isStarted)
        {
            OnInstructionScreen();
        }

    }
}
