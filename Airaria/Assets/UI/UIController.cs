using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
	Button dockButton;
	Text m_moneyText, m_cargoText;
	private string dockButtonText;
//	private  m_market;
	// Use this for initialization
	void Start () {
		dockButton = GameObject.FindObjectOfType<HarbourButton>().GetComponent<Button>(); 
		m_moneyText = GameObject.FindObjectOfType<MoneyText>().GetComponent<Text>();
		m_cargoText = GameObject.FindObjectOfType<CargoText>().GetComponent<Text>();
		SetShowDockAction(false);
		SetShowMarketAction(false);
//		m_market = transform.GetComponentInChildren<Market>().GetComponent<

//		SetDockButtonText(false);
		dockButtonText = "";
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void UpdateHeight()
	{
		
	}

	public void UpdateMoney(float money)
	{
		m_moneyText.text = "- $ " + money.ToString(); 
	}

	public void UpdateCargo(float cargo, float maxCargo)
	{
		m_cargoText.text = "Cargo: " + cargo.ToString() + "/" + maxCargo.ToString();
	}

	public void UpdateSteer(){
		
	}

	public void SetDockButtonText(bool leave)
	{
		string text = (leave)? "Dock on Harbour" : "Leave Harbour";
		dockButton.GetComponentInChildren<Text>().text = text; 

	}

	public void SetShowDockAction(bool show)
	{
		float alpha = (show)? 1f : 0f;
		string text = (show)? "Dock on Harbour" : "";  
		dockButton.GetComponent<CanvasRenderer>().SetAlpha(alpha);
		dockButton.GetComponentInChildren<Text>().text = text;

	}

	public void SetShowMarketAction(bool show)
	{
		float add = (show) ? -1f : 1f;
		GameObject m_market = GameObject.FindObjectOfType<Market>().gameObject;
		Debug.Log("position " + m_market.GetComponent<RectTransform>().anchoredPosition3D);
		m_market.GetComponent<RectTransform>().anchoredPosition3D += (add)*  new Vector3(1000f,0f,0f);	
	}
}
