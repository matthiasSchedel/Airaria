using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class Cargo {
	[SerializeField]
	string type;

	[SerializeField]
	[Range(0,100)]
	float value;

	[SerializeField]
	[Range(0,100)]
	float weight;

}

public class Zeppelin : MonoBehaviour {
	private Transform m_cargoParent;
	private Vector3 m_currentDockPosition;
	private bool m_canUnload, m_isDocked, m_docking;
	private UIController m_ui;

	private float m_currentCargo, m_currentMoney, m_currentMaxCargo;
	private float m_cargoMax = 50.0f, m_maxHeight = 500.0f;


	[SerializeField]
	private GameObject o_cargo;

	[SerializeField]
	private float m_climb, m_steer, m_height, m_speed;


	[SerializeField]
	private List<Cargo> m_cargo;

	[SerializeField]
	private const float m_max_speed = 300f, m_min_speed = 0f, m_startMoney = 100f, m_startMaxCargo = 100f;

	// [SerializeField]
	private float m_rotationSpeed = 30f, m_acceleration = 30f, m_climbSpeed = 1000f;

	// Use this for initialization
	void Start () {
		m_ui = GameObject.FindObjectOfType<UIController>();
		m_speed = m_currentCargo = 0f;
		m_currentMoney = m_startMoney;
		m_currentMaxCargo = m_startMaxCargo;
		m_canUnload = true;
		m_isDocked = m_docking = false;
		m_cargoParent = GameObject.Find("CargoParent").transform;
		m_ui.UpdateCargo(m_currentCargo, m_currentMaxCargo);
		m_ui.UpdateMoney(m_currentMoney);
		
		
	}
	void FixedUpdate () {
		if (m_docking)
			DoDocking();	
		float x,y, h = 0;
		y = Input.GetAxis("Horizontal");
		x = Input.GetAxis("Vertical");
		#if UNITY_EDITOR
			//Debug.Log("Unity editor");
			y = Input.GetAxis("Horizontal");
			x = Input.GetAxis("Vertical");
		#endif

		#if UNITY_STANDALONE
			Debug.Log("Unity standalone");
		y = Input.GetAxis("Horizontal");
		x = Input.GetAxis("Vertical");
		#endif

		#if UNITY_IOS
			x = Input.acceleration.y;
			y = Input.acceleration.x;
		#endif

		#if UNITY_ANDROID
//			x = Input.acceleration.y;
//			y = Input.acceleration.x;
		#endif

		if (Input.GetKey(KeyCode.Plus)){
			h += Time.deltaTime;
		} else if(Input.GetKey(KeyCode.Minus))
		{
			h-= Time.deltaTime;
		}

		m_speed += x * m_acceleration * Time.deltaTime;
		m_speed = Mathf.Clamp (m_speed, m_min_speed, m_max_speed);
		x = m_speed;
		y = y * m_rotationSpeed;

		if (!m_isDocked && !m_docking) 
			HandleControl(x,y, h);	
		
		SetUI();
		if (Input.GetKeyDown(KeyCode.Space)) 
		{
			//Debug.Log("Drop Cargo count" + m_cargo.Count);
			if (m_canUnload)
			{
				DropCargo();	
			}
			//DropCargo();	

		}


	}

	// Update is called once per frame
	void Update () {

	}


	void OnCollision(Collider coll)
	{
		Debug.Log("coll" + coll.name);
		if (coll.gameObject.name == "Dock")
		{
			Debug.Log("Found Dock");
		}
	}

	void SetUI()
	{
		m_height = transform.position.y;
		m_steer = transform.rotation.y;
		m_climb = transform.rotation.z;	
	}

	void HandleControl (float x, float y, float h) {
		
		transform.Rotate(Vector3.left * h * Time.deltaTime * m_climbSpeed);
		transform.position += transform.forward * Time.deltaTime * x;
		transform.Rotate (Vector3.up * y * Time.deltaTime);
		//transform.Rotate(Vector3.forward * -y * Time.deltaTime * .0f);
	}

	void DropCargo()
	{
		m_canUnload = false;
		GameObject newCargo = GameObject.Instantiate(o_cargo,m_cargoParent);
		newCargo.transform.position = transform.position - Vector3.up * 5;
		Invoke("ResetCargo",1f);

	}

	void ResetCargo()
	{
		m_canUnload = true;
	}

	public void EnterDock(Transform dock)
	{
		m_currentDockPosition = dock.position;
		m_currentDockPosition -= new Vector3(8f,5f,-5f);
		m_ui.SetShowDockAction(true);	


		Debug.Log("Enter Dock");

	}

	public void ExitDock() {
		m_ui.SetShowDockAction(false);	
//		ui.SetDockButtonText(true);
		Debug.Log("Exit Dock");
	}

	public void Dock()

	{
		if (!m_isDocked){
			//dock on
			m_docking = true;
			m_speed = .0f;
			m_isDocked = true;
		} else {
			//leave dock
			m_isDocked = false;
			m_ui.SetShowMarketAction(false);
			m_ui.SetDockButtonText(true);
//			ui.SetShowDockAction(false);

		}
		Debug.Log("Dock ");
	}


	public void DoDocking()
	{
		if (Vector3.Distance(m_currentDockPosition, transform.position) < .5f)
		{
			m_docking = false;
			m_ui.SetDockButtonText(false);
//			ui.SetDockButtonText(false);
			m_ui.SetShowMarketAction(true);
		}
		
		transform.position = Vector3.MoveTowards(transform.position, m_currentDockPosition, Time.deltaTime * 10f);

		//MoveTowards	
	}


}