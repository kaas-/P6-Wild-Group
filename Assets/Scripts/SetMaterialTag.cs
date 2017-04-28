using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMaterialTag : MonoBehaviour {

	// Use this for initialization
	void Awake()
    {
        Renderer m_renderer = GetComponent<Renderer>();

        m_renderer.material.renderQueue = 1800;
    }
}
