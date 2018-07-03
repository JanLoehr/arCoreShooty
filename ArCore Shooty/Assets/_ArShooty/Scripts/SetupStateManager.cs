using System;
using System.Collections;
using System.Collections.Generic;
using GoogleARCore;
using UnityEngine;
using UnityEngine.UI;

public class SetupStateManager : MonoBehaviour
{
    public Text SetupStepDescription;

    public GameObject ConfirmFloorPanel;

    public GameObject FloorPrefab;

    public GameObject ObstaclePrefab;

    public Transform PlayfieldObjects;

    [SerializeField]
    private SetupStates SetupState;

    private float _suggestedFloorHeight;

    private List<Renderer> _renderers;

    // Use this for initialization
    void Start()
    {
        SwitchState(SetupStates.FindFloor);
    }

    // Update is called once per frame
    void Update()
    {
        Touch touch;
        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        }

        // Raycast against the location the player touched to search for planes.
        TrackableHit hit;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
            TrackableHitFlags.FeaturePointWithSurfaceNormal;

        if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
        {
            if (SetupState == SetupStates.FindFloor)
            {
                _suggestedFloorHeight = hit.Pose.position.y;

                SwitchState(SetupStates.ConfirmFloor);
            }
            else if (SetupState == SetupStates.AddObstacles)
            {
                if (hit.Trackable is DetectedPlane)
                {
                    DetectedPlane plane = hit.Trackable as DetectedPlane;

                    GameObject go = Instantiate(ObstaclePrefab, hit.Pose.position, hit.Pose.rotation, PlayfieldObjects);

                    ObstacleController controller = go.GetComponent<ObstacleController>();
                    controller.Setup(plane, _suggestedFloorHeight);
                    _renderers.Add(controller.GetRenderer());
                }
            }
        }
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        foreach (Renderer item in _renderers)
        {
            item.enabled = false;
        }
    }

    public void SwitchState(SetupStates state)
    {
        if (SetupState != state)
        {
            switch (state)
            {
                case SetupStates.FindFloor:
                    SetupStepDescription.gameObject.SetActive(true);

                    ConfirmFloorPanel.SetActive(false);
                    break;

                case SetupStates.ConfirmFloor:
                    SetupStepDescription.gameObject.SetActive(false);

                    ConfirmFloorPanel.SetActive(true);
                    break;

                case SetupStates.AddObstacles:
                    ConfirmFloorPanel.SetActive(false);

                    SetupStepDescription.gameObject.SetActive(true);
                    SetupStepDescription.text = "Now add some environment objects by scanning and clicking on things";
                    break;
            }

            SetupState = state;
        }
    }

    public void FloorConfirmed(bool confirmed)
    {
        if (confirmed)
        {
            StartCoroutine(AddFloorCube());

            SwitchState(SetupStates.AddObstacles);
        }
        else
        {
            _suggestedFloorHeight = 0;

            SwitchState(SetupStates.FindFloor);
        }
    }

    private IEnumerator AddFloorCube()
    {
        GameObject floor = Instantiate(FloorPrefab, new Vector3(0, _suggestedFloorHeight - 0.05f, 0), Quaternion.identity, PlayfieldObjects);
        floor.transform.localScale += new Vector3(30, 0, 30);

        yield return new WaitForSeconds(2);

        floor.GetComponent<Renderer>().enabled = false;
    }
}

public enum SetupStates
{
    None,
    FindFloor,
    ConfirmFloor,
    AddObstacles
}
