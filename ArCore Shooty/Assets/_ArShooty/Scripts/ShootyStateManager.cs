using System;
using System.Collections;
using System.Collections.Generic;
using GoogleARCore;
using UnityEngine;

public class ShootyStateManager : MonoBehaviour
{
    public GameObject WelcomeScreen;
    public GameObject SetupScreen;

    public GameObject GameObjects;

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

                    GameObjects.SetActive(false);
                    break;

                case ShootyGameStates.LevelSetup:
                    WelcomeScreen.SetActive(false);

                    SetupScreen.SetActive(true);
                    SetPlanePointCloudActive(true);
                    break;

                case ShootyGameStates.Game:
                    WelcomeScreen.SetActive(false);
                    SetPlanePointCloudActive(false);

                    GameObjects.SetActive(true);
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
