using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zeppelin : MonoBehaviour {
	[SerializeField]
	private float m_speed = 200f;

	[SerializeField]
	private const float m_max_speed = 300f;

	[SerializeField]
	private const float m_min_speed = 0f;


	[SerializeField]
	private float m_rotationSpeed = 10f;
	[SerializeField]
	private float m_acceleration;

	// Use this for initialization
	void Start () {
		m_speed = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		HandleControl();
	}

	void HandleControl() 
	{
		m_speed += Input.GetAxis("Vertical") * Time.deltaTime;
		m_speed = Mathf.Clamp(m_speed, m_min_speed, m_max_speed);
		float x = m_speed;
		float y = Input.GetAxis("Horizontal") * m_rotationSpeed;
		transform.position += transform.forward * Time.deltaTime * x;
		//		transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.forward * 5, step);
		transform.Rotate(Vector3.up * y * Time.deltaTime);
	}
}
