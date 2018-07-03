using System;
using System.Collections;
using System.Collections.Generic;
using GoogleARCore;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
	private GameObject Cube;

    // Use this for initialization
    void Start()
    {
		Cube = transform.Find("Cube").gameObject;
    }

    public void Setup(DetectedPlane plane, float floorHeight)
    {
		Transform cubeTrans = Cube.transform;

		cubeTrans.localScale = new Vector3(plane.ExtentX, transform.position.y - floorHeight, plane.ExtentZ);
		transform.position = new Vector3(transform.position.x, floorHeight + cubeTrans.localScale.y / 2, transform.position.z);
    }

    public Renderer GetRenderer()
    {
        return Cube.GetComponent<Renderer>();
    }
}
