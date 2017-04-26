using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour {

    public GameObject DefaultUI;
    public GameObject LockScreenUI;
    public GameObject InstructionUI;
    public GameObject StartUI;

    public GameObject InstructionA1, InstructionA2, InstructionA3;
    public GameObject InstructionB1, InstructionB2, InstructionB3;
    private GameObject[] InstructionsA, InstructionsB;

    public Camera lockScreenCamera;

    private int InstructionCounter = 0;
    public bool ScreenLocked;
    public bool TrackingLost = true;

    public GameObject InstructionObjectA1, InstructionObjectA2, InstructionObjectA3;
    public GameObject InstructionObjectB1, InstructionObjectB2, InstructionObjectB3;
    private GameObject[] InstructionObjectsA, InstructionObjectsB;

    public GameObject InstructionsForwardButton;
    public GameObject InstructionsBackButton;
    public GameObject InstructionsDoneButton;

    public GameObject DefineUserUI;
    public TextMeshProUGUI DefineUserInputField; 

    private bool isStarted = false;

    public static string ImageTarget;

    private bool testWithApp;

    // Use this for initialization
    void Awake () {

        InstructionsA = new GameObject[] { InstructionA1, InstructionA2, InstructionA3 };
        InstructionObjectsA = new GameObject[] { InstructionObjectA1, InstructionObjectA2, InstructionObjectA3 };

        InstructionsB = new GameObject[] { InstructionB1, InstructionB2, InstructionB3 };
        InstructionObjectsB = new GameObject[] { InstructionObjectB1, InstructionObjectB2, InstructionObjectB3 };

        LockScreenUI.SetActive(false);
        InstructionUI.SetActive(false);
        DefaultUI.SetActive(false);
        StartUI.SetActive(false);

	}

    public void OnInstructionScreen()
    {
        Logger.EventLog("Marker " + ImageTarget + " found");
        DefaultUI.SetActive(false);
        
        InstructionUI.SetActive(true);

        InstructionCounter = 0;

        foreach (GameObject instruction in InstructionsA)
        {
            instruction.SetActive(false);
        }

        foreach (GameObject instruction in InstructionObjectsA)
        {
            instruction.SetActive(false);
        }

        foreach (GameObject instruction in InstructionsB)
        {
            instruction.SetActive(false);
        }

        foreach (GameObject instruction in InstructionObjectsB)
        {
            instruction.SetActive(false);
        }

        if (ImageTarget == "blue_aau")
        {
            InstructionsA[InstructionCounter].SetActive(true);
            InstructionObjectsA[InstructionCounter].SetActive(true);
        }

        if (ImageTarget == "red_aau")
        {
            InstructionsB[InstructionCounter].SetActive(true);
            InstructionObjectsB[InstructionCounter].SetActive(true);
        }


        InstructionsForwardButton.SetActive(true);
        InstructionsBackButton.SetActive(false);
        InstructionsDoneButton.SetActive(false);

    }

    public void OnStartButton()
    {
        isStarted = true;

        Logger.StartLog();
        StartUI.SetActive(false);

        if (testWithApp)
        {
            DefaultUI.SetActive(true);
        }
        else
        {
            InstructionUI.SetActive(true);

            foreach (GameObject instruction in InstructionsA)
            {
                instruction.SetActive(false);
            }

            foreach (GameObject instruction in InstructionObjectsA)
            {
                instruction.SetActive(false);
            }

            foreach (GameObject instruction in InstructionsB)
            {
                instruction.SetActive(false);
            }

            foreach (GameObject instruction in InstructionObjectsB)
            {
                instruction.SetActive(false);
            }

            InstructionsDoneButton.SetActive(true);
        }
    }

    //based on http://answers.unity3d.com/questions/9969/convert-a-rendertexture-to-a-texture2d.html
    public void LockScreenButton()
    {
        Logger.EventLog("Screen Locked");

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
        Logger.EventLog("Forward Instruction");

        if (InstructionCounter != 2)
        {
            if(InstructionCounter == 0)
            {
                InstructionsBackButton.SetActive(true);
            }

            

            if (ImageTarget == "blue_aau")
            {
                InstructionsA[InstructionCounter].SetActive(false);
                InstructionObjectsA[InstructionCounter].SetActive(false);
                InstructionCounter++;
                InstructionsA[InstructionCounter].SetActive(true);
                InstructionObjectsA[InstructionCounter].SetActive(true);
            }

            if (ImageTarget == "red_aau")
            {
                InstructionsB[InstructionCounter].SetActive(false);
                InstructionObjectsB[InstructionCounter].SetActive(false);
                InstructionCounter++;
                InstructionsB[InstructionCounter].SetActive(true);
                InstructionObjectsB[InstructionCounter].SetActive(true);

            }
        }

        if (InstructionCounter == 2)
        {
            
            InstructionsForwardButton.SetActive(false);
            InstructionsDoneButton.SetActive(true);
        }
    }

    public void OnBackButton()
    {
        Logger.EventLog("Back Instruction");

        if (InstructionCounter != 0)
        {
            if(InstructionCounter == 2)
            {
                InstructionsForwardButton.SetActive(true);
                InstructionsDoneButton.SetActive(false);
            }

            if (ImageTarget == "blue_aau")
            {
                InstructionsA[InstructionCounter].SetActive(false);
                InstructionObjectsA[InstructionCounter].SetActive(false);
                InstructionCounter--;
                InstructionsA[InstructionCounter].SetActive(true);
                InstructionObjectsA[InstructionCounter].SetActive(true);
            }

            if (ImageTarget == "red_aau")
            {
                InstructionsB[InstructionCounter].SetActive(false);
                InstructionObjectsB[InstructionCounter].SetActive(false);
                InstructionCounter--;
                InstructionsB[InstructionCounter].SetActive(true);
                InstructionObjectsB[InstructionCounter].SetActive(true);
            }

        }
        if (InstructionCounter == 0)
        {
            InstructionsBackButton.SetActive(false);
        }
    }

    public void OnReturnButton()
    {
        Logger.EventLog("Returned to scan screen");
        InstructionUI.SetActive(false);
        DefaultUI.SetActive(true);
        InstructionCounter = 0;
    }

    public void OnScreenUnlockButton()
    {
        Logger.EventLog("Screen Unlocked");
        ScreenLocked = false;
        LockScreenUI.SetActive(false);
        InstructionUI.SetActive(true);
    }

    public void OnDoneButton()
    {
        isStarted = false;
        InstructionCounter = 0;
        Logger.FinishLog();

        InstructionUI.SetActive(false);
        StartUI.SetActive(true);
    }

    public void OnWithAppButton()
    {
        testWithApp = true;
        Logger.InitLog(DefineUserInputField.text, "with_app");
        DefineUserUI.SetActive(false);
        StartUI.SetActive(true);
    }

    public void OnWithoutAppButton()
    {
        testWithApp = false;
        Logger.InitLog(DefineUserInputField.text, "without_app");
        DefineUserUI.SetActive(false);
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
