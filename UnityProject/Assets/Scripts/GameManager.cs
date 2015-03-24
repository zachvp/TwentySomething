using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public int p_money; 
	public float p_maxStamina; 
	public float p_stamina; 
	public float p_staminaRate; 
	public float timer; 

	public int hourOfDay = 0; 

	public int stepsTaken = 0;
	TextMesh [] uitext;

	void Awake () {
		DontDestroyOnLoad (this); 
		}

	// Use this for initialization
	void Start () { 
		uitext = GetComponentsInChildren<TextMesh> (); 
		p_money = 100; 
		p_maxStamina = 100f;
		p_stamina = 100f; 
		p_staminaRate = 1.0f; 
		timer = 0f; 
		Application.LoadLevel ("Demo"); 
	}
	
	// Update is called once per frame
	void Update () {
		//prevent the players stamina from exceeding current max stamina. 
		if (p_stamina > p_maxStamina) {
			p_stamina =  p_maxStamina; 
				}

		//handle UI objects

	}

	void OnLevelWasLoaded(

	void FixedUpdate() {
		timer += 1f;
		p_stamina -= 4;
		//Debug.Log (timer); 

		if ((int)timer % 60 == 0)
			hourOfDay++; 
		if (hourOfDay >= 24)
			hourOfDay = 0; 

		uitext [0].text = "Stamina: " + p_stamina; 
		uitext [1].text = "Money: " + p_money; 
		uitext [2].text = "Time: " + hourOfDay; 
		 
		Debug.Log (hourOfDay); 
	}

	void OnLevelWasLoaded(int level) {

	}

	public void addMoney(int toAdd){
		p_money += toAdd; 
	}

	public void spendMoney(int toSpend){
		p_money -= toSpend; 
	}

	public void eat (float staminaGain){
		p_stamina += staminaGain;
	}
}
