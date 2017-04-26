using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class MyTrackableEventHandler : MonoBehaviour, ITrackableEventHandler{

    private TrackableBehaviour mTrackableBehaviour;
    private UIManager uimanager;

    public void OnTrackableStateChanged(
                                    TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackingFound();
        }
        else
        {
            OnTrackingLost();
        }
    }

    // Use this for initialization
    void Start () {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }

    }

    void Awake()
    {
        uimanager = GameObject.FindGameObjectWithTag("uimanager").GetComponent<UIManager>();
    }

    private void OnTrackingFound()
    {
        if (UIManager.ImageTarget != mTrackableBehaviour.TrackableName)
        {
            UIManager.ImageTarget = mTrackableBehaviour.TrackableName;
        }

        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
        Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

        // Enable rendering:
        foreach (Renderer component in rendererComponents)
        {
            component.enabled = true;
        }

        // Enable colliders:
        foreach (Collider component in colliderComponents)
        {
            component.enabled = true;
        }

        uimanager.TrackingLost = false;

        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
    }


    private void OnTrackingLost()
    {

        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
        Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

        // Disable rendering:
        foreach (Renderer component in rendererComponents)
        {
            component.enabled = false;
        }

        // Disable colliders:
        foreach (Collider component in colliderComponents)
        {
            component.enabled = false;
        }

        uimanager.TrackingLost = true;

        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
    }

}
