using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

/*
 * 
 * Classname: GameLogic
 * Implements: Singleton pattern
 * Description: Singleton class to instantiate once to facilitate
 * calls between other gameObjects and components, also to maintain
 * a clean environment with the game variables.
 * 
 */
public class GameLogic : Singleton<GameLogic> {

	protected GameLogic() {}

	[Header("UI")]
	public Transform canvas;

	[Header("Input Field")]
	public GameObject loading;
	public Text query;
	public int resultsLimit;

	[Header("List Item")]
	public GameObject listItemPrefab;
	public int maxCharacters;

	[Header("Scroll View")]
	public Transform programList;

	// 'offset' is where the pagination begins on the api call
	private int offset;
	// 'searchingPrograms' validates if a search is being performed to avoid call overflows
	private bool searchingPrograms;

	// all the current results are stored in 'results'
	private List<Datum> results;
	// 'lastQuery' store the last query to control the first call on 'AddMoreResults'
	private string lastQuery;

	void Start(){
		InitValues ();
	}

	/*
	 * 
	 *  The initial variable values
	 * 
	 */
	protected void InitValues(){
		query.text = lastQuery = "";
		offset = 0;
		searchingPrograms = false;
		results = new List<Datum> ();
	}


	/*
	 * 
	 *	Public function that is invoked by the search button 
	 * 
	 */
	public void SearchProgram(){
		if (!searchingPrograms) {
			CleanList ();
			if (!String.IsNullOrEmpty(query.text)) {

				this.lastQuery = this.query.text;
				StartCoroutine (GetPrograms ());

			}
		}
	}

	/*
	 * 
	 * Add more results if the scroll list is at the bottom 
	 * 
	 */
	public void AddMoreResults(){
		if (!searchingPrograms) {

			// offset get +10 results at every onBottom event
			offset += resultsLimit;
			if (!String.IsNullOrEmpty (this.lastQuery)){
				StartCoroutine (GetPrograms ());
			}
		}
	}

	/*
	 * 
	 * Coroutine that invokes a network call
	 * to use the API and then populate
	 * the program list
	 * 
	 */
	private IEnumerator GetPrograms()
	{
		Debug.Log ("Searching Programs from YLE");

		this.ShowLoading ();

		// Constructing the URI
		string apiUri = GetAPIUri ();

		Debug.Log ("APIUri: " + apiUri);

		using (UnityWebRequest www = UnityWebRequest.Get(apiUri))
		{
			searchingPrograms = true;

			yield return www.Send();

			if (www.isNetworkError || www.isHttpError)
			{
				Debug.Log(www.error);
			}
			else
			{
				// Show results as text
				Debug.Log(www.downloadHandler.text);

				// YLEProgram is a model that deserialize the json values obtained by the API call
				YLEProgram response = JsonUtility.FromJson<YLEProgram> (www.downloadHandler.text);

				// Lets test the response
				Debug.Log("YLE Api Version: " + response.apiVersion);

				// Now create the program list
				this.CreateList (response);

				// Or retrieve results as binary data
				//byte[] results = www.downloadHandler.data;
			}

			searchingPrograms = false;
		}
			
		this.HideLoading ();
	}

	/*
	 * 
	 * Constructs the API URI based in the current query and offset 
	 * 
	 */
	private string GetAPIUri(){
		return string.Format ("https://external.api.yle.fi/v1/programs/items.json?q={0}&offset={1}&limit={2}&app_id=6624bd89&app_key=8d9282be0198cffd5a40d99f1bb37094&region=world",
			this.query.text,
			this.offset,
			this.resultsLimit);
	}

	/*
	 * 
	 * Params: yleProgram
	 * Description: Create item list with an item prefab, then set its values.
	 * 
	 */
	private void CreateList(YLEProgram yleProgram){

		if (yleProgram.data != null) {

			results.AddRange (yleProgram.data);

			Debug.Log ("data is not null");

			foreach (Datum program in yleProgram.data) {

				// Instantiate the item list
				GameObject item = GameObject.Instantiate (listItemPrefab);
				// Set the item list parent as 'programList' then the scale is fixed
				item.transform.SetParent(programList);
				item.transform.localScale = Vector3.one;

				// Pass 'Datum' object to ProgramDatum component that is used then to show the details
				item.GetComponentInChildren<ProgramDatum> ().program = program;

				// Setting the item title
				Text itemTxt = item.GetComponentInChildren<Text> ();

				if (program.itemTitle.fi!=null) {
					itemTxt.text = program.itemTitle.fi;
				} else if (program.itemTitle.sv!=null) {
					itemTxt.text = program.itemTitle.sv;
				}else if(program.itemTitle.en!=null){
					itemTxt.text = program.itemTitle.en;
				}

				if (itemTxt.text.Length > maxCharacters) {
					itemTxt.text = itemTxt.text.Substring (0, maxCharacters) + "...";
				}
					
			}
		} else {
			Debug.Log ("data is NULL");
		}

	}

	private void ShowLoading(){
		loading.SetActive (true);
	}

	private void HideLoading(){
		loading.SetActive (false);
	}

	/*
	 * 
	 * Description: Destroy all GameObjects with tag: "ListItem"
	 * and reset values.
	 * 
	 */
	private void CleanList(){
		GameObject[] listItems = GameObject.FindGameObjectsWithTag ("ListItem");
		foreach (GameObject go in listItems) {
			Destroy (go);
		}

		offset = 0;
		results = new List<Datum> ();
	}

}