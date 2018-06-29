using System;
using System.Collections;
using System.Collections.Generic;
using GoogleARCore;
using UnityEngine;

public class ShootyStateManager : MonoBehaviour
{
    public GameObject WelcomeScreen;
    public GameObject SetupScreen;

    public GameObject PlaneGenerator;

    public GameObject PointCloud;

    [SerializeField]
    private ShootyGameStates _gameState;

    // Use this for initialization
    void Start()
    {
        SwitchState(ShootyGameStates.Launch);
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
        }
    }

    public void SwitchState(ShootyGameStates state)
    {
        if (_gameState != state)
        {
            // Activate things
            switch (state)
            {
                case ShootyGameStates.Launch:
                    WelcomeScreen.SetActive(true);

                    SetupScreen.SetActive(false);
                    SetPlanePointCloudActive(false);
                    break;

                case ShootyGameStates.LevelSetup:
                    WelcomeScreen.SetActive(false);

                    SetupScreen.SetActive(true);
                    SetPlanePointCloudActive(true);
                    break;

                case ShootyGameStates.Game:
                    WelcomeScreen.SetActive(false);
                    SetPlanePointCloudActive(false);
                    break;
            }

            _gameState = state;
        }
    }

    public void SwitchState(string state)
    {
        SwitchState((ShootyGameStates)Enum.Parse(typeof(ShootyGameStates), state));
    }

    private void SetPlanePointCloudActive(bool active)
    {
        PlaneGenerator.SetActive(active);
        PointCloud.SetActive(active);
    }
}

public enum ShootyGameStates
{
    None,
    Launch,
    LevelSetup,
    Game
}
