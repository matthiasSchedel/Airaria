using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dock : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider coll)
	{
		if (coll.tag == "Player") {
			Debug.Log("coll on dock enter" + coll.GetComponent<Collider>().name);

			//coll.gameObject.GetComponent<Zeppelin>().EnterDock();
			//GameObject.FindGameObjectWithTag("Zeppelin").gameObject.GetComponent<Zeppelin>().EnterDock();
			GameObject.FindGameObjectWithTag("Zeppelin").gameObject.SendMessage("EnterDock",transform);
			//GameObject.Find("Zeppelin").GetComponent<Zeppelin>().EnterDock();

		}


	}

	void OnTriggerExit(Collider coll)
	{
		
		if (coll.tag == "Player") {
			Debug.Log("coll on dock exit" +  coll.GetComponent<Collider>().name);
			//coll.gameObject.SendMessage("ExitDock");
			GameObject.FindGameObjectWithTag("Zeppelin").gameObject.SendMessage("ExitDock");

			//coll.gameObject.SendMessage("ExitDock");
		}
	}

	void OnTriggerStay(Collider coll)
	{
		//Debug.Log("coll on dock stay" + coll.GetComponent<Collider>().name);
	}
}
