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

	void Awake () {
		DontDestroyOnLoad (this); 
		}

	// Use this for initialization
	void Start () { 
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
		//decrement stamina VERY TEMP SOLUTION, JUST TESTING IF THIS WORKS
		/*if (stepsTaken >= 10) {
			stepsTaken = 0; 
			p_stamina--; 
				}


		//test fail conditons, change to appropiate situations. 
	/*	if (timeLeft <= 0f || p_stamina <= 0f) {
			Debug.Log("Out of Time/Stamina"); 
			Application.LoadLevel("START_GAME_HERE"); 
				}*/
	}

	void FixedUpdate() {
		timer += 1f; 
		//Debug.Log (timer); 

		if ((int)timer % 60 == 0)
			hourOfDay++; 
		if (hourOfDay >= 24)
			hourOfDay = 0; 

		 
		Debug.Log (hourOfDay); 
	}

	public void addMoney(int toAdd){
		p_money += toAdd; 
	}

	public void spendMoney(int toSpend){
		p_money -= toSpend; 
	}
}
