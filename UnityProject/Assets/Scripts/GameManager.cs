using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public int p_money; 
	public float p_maxStamina; 
	public float p_stamina; 
	public float p_staminaRate; 
	public float timeLeft; 

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
		if (p_stamina > p_maxStamina) {
			p_stamina =  p_maxStamina; 
				}
		//Debug.Log("Update time :" + Time.deltaTime);
	}

	void FixedUpdate() {
		//Debug.Log("FixedUpdate time: " + Time.deltaTime); 
		timeLeft -= 1f; 
		Debug.Log (timeLeft); 
	}
}
