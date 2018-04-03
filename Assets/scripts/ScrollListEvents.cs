using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrollListEvents : MonoBehaviour {

	ScrollRect sr;

	// Use this for initialization
	void Start () {
		sr = GetComponent<ScrollRect> ();
		sr.onValueChanged.AddListener (OnScrollChange);
	}

	/*
	 * 
	 * Call GameLogic -> AddMoreResults() if the scroll list is at the bottom
	 * 
	 */
	void OnScrollChange(Vector2 scrollValues){
		if (scrollValues.y <= 0) {
			GameLogic.Instance.AddMoreResults ();
			Debug.Log ("End reached");
		}
	}

}