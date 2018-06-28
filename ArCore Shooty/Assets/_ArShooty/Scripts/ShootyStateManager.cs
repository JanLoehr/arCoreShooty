using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootyStateManager : MonoBehaviour
{
    public GameObject WelcomeScreen;

    [SerializeField]
    private ShootyGameStates _gameState;

    // Use this for initialization
    void Start()
    {

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
                    break;

                case ShootyGameStates.LevelSetup:
                    break;

                case ShootyGameStates.Game:
                    break;
            }

            _gameState = state;
        }
    }
}

public enum ShootyGameStates
{
    Launch,
    LevelSetup,
    Game
}
