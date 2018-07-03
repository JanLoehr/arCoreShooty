using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public GameObject Gun;

    public GameObject GameUI;

    // Use this for initialization
    void Start()
    {

    }

	/// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        Gun.SetActive(true);
        GameUI.SetActive(true);
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        Gun?.SetActive(false);
        GameUI?.SetActive(false);
    }
}
