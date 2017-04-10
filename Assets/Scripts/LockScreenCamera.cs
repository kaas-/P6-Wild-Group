using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockScreenCamera : MonoBehaviour {

    new Camera camera;
    private void Start()
    {
        camera = GetComponent<Camera>();
    }

    public void UpdateCamera () {
        gameObject.transform.position = Camera.main.transform.parent.position;
        gameObject.transform.localRotation = Camera.main.transform.parent.localRotation;

        camera.depth = Camera.main.depth;
        camera.fieldOfView = Camera.main.fieldOfView;
        camera.rect = Camera.main.rect;
        camera.aspect = Camera.main.aspect;
        camera.farClipPlane = Camera.main.farClipPlane;
        camera.nearClipPlane = Camera.main.nearClipPlane;

	}
}
