using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingController : MonoBehaviour {
	FlightController flightController;
	public float minDistance = 100f;
	public float cohesionMultiplier = 1f;
	public float allignmentMultiplier = 1f;
	public float seperationMultiplier = 3f;
	public float targetMultiplier = 2f;
	public float meanMultiplier = 20f;

	void Start()
	{
		flightController = GetComponent<FlightController>();
	}

	private void FixedUpdate() {
		if (!GetComponent<ClashController>().isClashed)
		{
			Flock();
		}
	}

	void Flock()
	{
		// TODO should I normalize the mean??
		Vector3 mean = (GetMeanPosition().normalized*cohesionMultiplier + GetMeanVelocity().normalized*allignmentMultiplier + GetSeperationPos().normalized*seperationMultiplier + GetTargetPos().normalized*targetMultiplier).normalized*meanMultiplier;
		Debug.DrawRay(transform.position, mean, Color.magenta);
		GetComponent<UnityStandardAssets.Vehicles.Aeroplane.AeroplaneAiControl>().targetPosition = flightController._position + mean;
	}

	Vector3 GetMeanPosition()
	{
		Vector3 meanPos = Vector3.zero;
		int healthyCount = 0;
		for (int i = 0; i < flightController.myGroup.positions_current.Count; i++)
		{
			if (!flightController.myGroup.drones[i].GetComponent<ClashController>().isClashed)
			{
				healthyCount++;
				meanPos += flightController.myGroup.positions_current[i];
			}
		}
		meanPos = meanPos/healthyCount;
		Debug.DrawRay(transform.position, meanPos - transform.position, Color.green);
		return meanPos;
	}

	// Vector3 GetMeanAngles()
	// {
	// 	Vector3 meanAngle = Vector3.zero;

	// 	for (int i = 0; i < flightController.myGroup.angles_current.Count; i++)
	// 	{
	// 		meanAngle += flightController.myGroup.angles_current[i];
	// 	}
	// 	meanAngle = meanAngle/(flightController.myGroup.angles_current.Count);
	// 	// Debug.DrawRay(transform.position, direction, Color.red);
	// 	return meanAngle;
	// }

	Vector3 GetMeanVelocity()
	{
		Vector3 meanVelocity = Vector3.zero;
		int healthyCount = 0;
		for (int i = 0; i < flightController.myGroup.velocities_current.Count; i++)
		{
			if (!flightController.myGroup.drones[i].GetComponent<ClashController>().isClashed)
			{
				healthyCount++;
				meanVelocity += flightController.myGroup.velocities_current[i];	
			}
		}
		meanVelocity = meanVelocity/healthyCount;
		Debug.DrawRay(transform.position, meanVelocity, Color.blue);
		return meanVelocity;
	}

	Vector3 GetSeperationPos()
	{
		Vector3 seperationPos = Vector3.zero;
		int closeCount = 0;
		for (int i = 0; i < flightController.myGroup.positions_current.Count; i++)
		{
			if (flightController.myGroup.ids[i] != flightController.ID) // not itself
			{
				float distance = Vector3.Distance(flightController._position, flightController.myGroup.positions_current[i]);
				if (distance <= minDistance)
				{
					if (!flightController.myGroup.drones[i].GetComponent<ClashController>().isClashed)
					{
						closeCount++;
						//float sqrDistance = 1/Mathf.Max(distance, 0.001f);
						seperationPos +=  (flightController._position - flightController.myGroup.positions_current[i]) ;//* sqrDistance;
					}	
				}
			}
		}
		if (closeCount > 0)
		{
			seperationPos = seperationPos / closeCount;
		}
		else
		{
			seperationPos = Vector3.zero;
		}
		Debug.DrawRay(transform.position, seperationPos, Color.red);
		return seperationPos;
	}

	Vector3 GetTargetPos()
	{
		return flightController.targetObject.transform.position - flightController._position;
	}
}