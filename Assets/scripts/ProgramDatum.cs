using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ProgramDatum : MonoBehaviour, IPointerClickHandler {

	public Datum program;

	/*
	 * 
	 * Instantiate the Program Details UI when the user clicks a list item
	 * 
	 */
	public void OnPointerClick(PointerEventData Data){
		Debug.Log ("Showing Program Details");

		// Instantiating the Details UI and setting the "Canvas" GameObject as it parent
		GameObject program_details =  GameObject.Instantiate (Resources.Load ("program_details")) as GameObject;
		program_details.transform.SetParent (GameLogic.Instance.canvas);

		// Fixing the UI Scale and Position
		RectTransform rt = program_details.GetComponent<RectTransform> ();
		rt.offsetMax = Vector2.zero;
		rt.offsetMin = Vector2.zero;
		rt.localScale = Vector3.one;

		// Passing the program values to show to the UI GameObject Script
		ProgramDetails detailsScr = program_details.GetComponent<ProgramDetails> ();
		detailsScr.program = program;
		detailsScr.SetDetails ();
	}

}
