using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	public string targetScene; 
	public GameObject player; 
	// Use this for initialization
	void Start () {
//		player = GameObject.FindGameObjectWithTag ("player"); 
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.transform.position.x == player.transform.position.x && 
		   gameObject.transform.position.y == player.transform.position.y ){
			Application.LoadLevel(targetScene); 
		}
	}
}
