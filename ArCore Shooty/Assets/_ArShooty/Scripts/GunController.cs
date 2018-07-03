using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GunController : MonoBehaviour
{
	public GameObject Ammo;

	public float ShootForce = 5;

	public int MaxAmmoCount = 50;

	public Transform GameObjects;

	private Transform _ammoRef;

	private List<GameObject> _ammos = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
		_ammoRef = transform.Find("ShootRef");
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButtonDown(0))
		{
			_ammos.Add(Instantiate(Ammo, _ammoRef.position, _ammoRef.rotation, GameObjects));

			_ammos.Last().GetComponent<Rigidbody>().AddForce(_ammoRef.forward * ShootForce, ForceMode.Impulse);

			if (_ammos.Count > MaxAmmoCount)
			{
				Destroy(_ammos.First());

				_ammos.Remove(_ammos[0]);
			}
		}
    }
}
