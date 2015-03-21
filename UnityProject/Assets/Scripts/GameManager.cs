using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public int p_money; 
	public float p_maxStamina; 
	public float p_stamina; 
	public float p_staminaRate; 
	public float timeLeft; 

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
		timeLeft = 600f; 
		Application.LoadLevel ("Demo"); 
	}
	
	// Update is called once per frame
	void Update () {
		//prevent the players stamina from exceeding current max stamina. 
		if (p_stamina > p_maxStamina) {
			p_stamina =  p_maxStamina; 
				}
		//decrement stamina VERY TEMP SOLUTION, JUST TESTING IF THIS WORKS
		if (stepsTaken >= 10) {
			stepsTaken = 0; 
			p_stamina--; 
				}


		//test fail conditons, change to appropiate situations. 
		if (timeLeft <= 0f || p_stamina <= 0f) {
			Debug.Log("Out of Time/Stamina"); 
			Application.LoadLevel("START_GAME_HERE"); 
				}
	}

	void FixedUpdate() {
		//Debug.Log("FixedUpdate time: " + Time.deltaTime); 
		timeLeft -= 1f; 
		Debug.Log (timeLeft); 
	}
}
