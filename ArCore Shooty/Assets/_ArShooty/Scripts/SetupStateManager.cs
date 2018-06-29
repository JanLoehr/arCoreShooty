using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupStateManager : MonoBehaviour
{
    public GameObject FloorPlaneDescription;

    [SerializeField]
    private SetupStates SetupState;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SwitchState(SetupStates state)
    {
        switch (state)
        {
            case SetupStates.FindFloor:
                FloorPlaneDescription.SetActive(true);
                break;

            case SetupStates.AddObstacles:
                break;
        }
    }
}

public enum SetupStates
{
    None,
    FindFloor,
    AddObstacles
}
