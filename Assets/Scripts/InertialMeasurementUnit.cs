using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InertialMeasurementUnit : MonoBehaviour {

	Rigidbody _rigidbody;

	[Header("GPS")]
	public bool GPS_on = true;
	float GPS_counter = 0f;
	float GPS_refresh_rate = 0.1f;
	public Vector3 GPS_output;

	[Header("Accelerometer")]
	public bool Accelerometer_on = true;
	float Accelerometer_counter = 0f;
	float Accelerometer_refresh_rate = 0.1f;
	public Vector3 Accelerometer_output;

	[Header("Gyroscope")]
	public bool Gyroscope_on = true;
	float Gyroscope_counter = 0f;
	float Gyroscope_refresh_rate = 0.1f;
	public Vector3 Gyroscope_output;

	void Start()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	void FixedUpdate () {
		if (GPS_on)
		{
			GPS();
		}
		if (Accelerometer_on)
		{
			Accelerometer();
		}
		if (Gyroscope_on)
		{
			Gyroscope();
		}

	}

	void GPS()
	{
		GPS_counter += Time.deltaTime;
		if (GPS_counter >= GPS_refresh_rate)
		{
			GPS_counter = 0;
			GPS_output = transform.position;
		}
	}

	void Accelerometer()
	{
		Accelerometer_counter += Time.deltaTime;
		if (Accelerometer_counter >= Accelerometer_refresh_rate)
		{
			Accelerometer_counter = 0;
			Accelerometer_output = _rigidbody.velocity;
		}
	}

	void Gyroscope()
	{
		Gyroscope_counter += Time.deltaTime;
		if (Gyroscope_counter >= Gyroscope_refresh_rate)
		{
			Gyroscope_counter = 0;
			Gyroscope_output = transform.eulerAngles;
		}
	}
}