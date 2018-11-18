using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightController : MonoBehaviour {
	[Header("General")]
	public int ID;
	public int myGroupID;
	public Group myGroup;
	public GameObject targetObject;

	[Header("IMU")]
	InertialMeasurementUnit IMU;
	public Vector3 _position;
	public Vector3 _velocity;
	Vector3 _angles;

	[Header("Ground Control")]
	public GroundControlStation groundControlStation;

	private void Awake() {
		GetComponent<UnityStandardAssets.Vehicles.Aeroplane.AeroplaneAiControl>().m_Target = targetObject.transform;

	}

	private void Start() {
		IMU = GetComponent<InertialMeasurementUnit>();
		groundControlStation = GameObject.Find("GroundControlStation").GetComponent<GroundControlStation>();
	}

	void GetIMUData()
	{
		_position = IMU.GPS_output;
		_velocity = IMU.Accelerometer_output;
		_angles = IMU.Gyroscope_output;
	}

	void SendData()
	{
		groundControlStation.GetDataFromDrones(myGroupID, ID, _position, _velocity, _angles);
	}

	private void Update() {
		GetIMUData();
		SendData();
	}

	// public void GetDataFromDrones(int groupID, int droneID, Vector3 dronePosition, Vector3 droneVelocity, Vector3 droneAngle)
	// {
	// 	if (myGroupID == groupID)
	// 	{
			// TODO get data and save
	// 	}
	// 	else
	// 	{
	// 		return;
	// 	}
	// }
}
