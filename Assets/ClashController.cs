using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClashController : MonoBehaviour {

	bool clashed = false;

	private void OnCollisionEnter(Collision other) {

		if (other.gameObject.tag == "plane" && !clashed)
		{
			print("Clash Happend Between " + gameObject.name + " and " + other.gameObject.name);
			try
			{
				transform.GetComponent<UnityStandardAssets.Vehicles.Aeroplane.AeroplaneController>().Immobilize();
				other.transform.GetComponent<UnityStandardAssets.Vehicles.Aeroplane.AeroplaneController>().Immobilize();
				clashed = true;
			}
			catch
			{

			}	
		}
	}
}