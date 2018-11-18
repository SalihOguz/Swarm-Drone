using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClashController : MonoBehaviour {

	public bool isClashed = false;

	private void OnCollisionEnter(Collision other) {

		if (other.gameObject.tag == "plane" && !isClashed)
		{
			//print("Clash Happend Between " + gameObject.name + " and " + other.gameObject.name);
			GetComponent<FlightController>().groundControlStation.totalCrashCount += 0.5f;
			if (GetComponent<FlightController>().groundControlStation.totalCrashCount % 1 == 0)
			{
				print("Total Crash Count: " + GetComponent<FlightController>().groundControlStation.totalCrashCount + " & Total Fallen Plane Count: " + GetComponent<FlightController>().groundControlStation.totalCrashCount*2);
			}
			try
			{
				transform.GetComponent<UnityStandardAssets.Vehicles.Aeroplane.AeroplaneController>().Immobilize();
				other.transform.GetComponent<UnityStandardAssets.Vehicles.Aeroplane.AeroplaneController>().Immobilize();
				isClashed = true;
			}
			catch
			{

			}	
		}
	}
}