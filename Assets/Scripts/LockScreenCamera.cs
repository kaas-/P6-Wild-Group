using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockScreenCamera : MonoBehaviour {

    Camera camera;
    private void Start()
    {
        camera = GetComponent<Camera>();
    }

    public void UpdateCamera () {
        gameObject.transform.position = Camera.main.transform.position;
        gameObject.transform.localRotation = Camera.main.transform.localRotation;

        camera.depth = Camera.main.depth;
        camera.fieldOfView = Camera.main.fieldOfView;
        camera.rect = Camera.main.rect;

	}
}
