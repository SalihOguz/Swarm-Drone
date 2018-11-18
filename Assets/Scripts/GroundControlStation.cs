using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundControlStation : MonoBehaviour {
	public List<GameObject> allDrones = new List<GameObject>();
	public List<Group> allGroups = new List<Group>();
	public float totalCrashCount = 0f;

	private void Start() {
		Group gr = new Group();
		for (int i = 0; i < allDrones.Count; i++) // TODO all drones goes into group 0 for now
		{
			gr.ids.Add(i);
			gr.drones.Add(allDrones[i]);
			gr.positions_current.Add(allDrones[i].transform.position);
			gr.velocities_current.Add(allDrones[i].GetComponent<Rigidbody>().velocity);
			gr.angles_current.Add(allDrones[i].transform.eulerAngles);
		}
		allGroups.Add(gr);
		LaunchingDrones();
	}

	void LaunchingDrones()
	{
		for (int i = 0; i < allGroups.Count; i++)
		{
			for (int j = 0; j < allGroups[i].drones.Count; j++)
			{
				allGroups[i].drones[j].GetComponent<FlightController>().ID = j;
				allGroups[i].drones[j].GetComponent<FlightController>().myGroupID = i;
				allGroups[i].drones[j].GetComponent<FlightController>().myGroup = allGroups[i];
				allGroups[i].drones[j].GetComponent<UnityStandardAssets.Vehicles.Aeroplane.AeroplaneController>().StartMotor(); // TODO we might delay launches
			}
		}
	}
	
	public void GetDataFromDrones(int groupID, int droneID, Vector3 dronePosition, Vector3 droneVelocity, Vector3 droneAngle)
	{
		for (int i = 0; i < allGroups[groupID].ids.Count; i++) // find drone index
		{
			if (droneID == allGroups[groupID].ids[i])
			{
				allGroups[groupID].positions_current[i] = dronePosition;
				allGroups[groupID].velocities_current[i] = droneVelocity;
				allGroups[groupID].angles_current[i] = droneAngle;
				break;
			}
		}
	}
}

[SerializeField]
public class Group
{
	public List<int> ids = new List<int>();
	public List<GameObject> drones = new List<GameObject>();
	public List<Vector3> positions_current = new List<Vector3>();
	public List<Vector3> velocities_current = new List<Vector3>();
	public List<Vector3> angles_current = new List<Vector3>();
}