using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harbour : MonoBehaviour {
	[SerializeField]
	float m_cargoCount;

	[SerializeField]
	Zeppelin m_zeppelin;

	[SerializeField]
	float m_CargoPrize = 10.0f, m_maxCargo = 100f;
	// Use this for initialization
	void Start () {
		m_zeppelin = GameObject.Find("Zeppelin").GetComponent<Zeppelin>();
		m_cargoCount = m_maxCargo;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public float BuyCargo(float amount)
	{
		m_cargoCount -= amount;
		return m_cargoCount;
	}
}
