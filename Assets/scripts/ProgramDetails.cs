using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgramDetails : MonoBehaviour {

	public Datum program;
	public Text title;
	public Text description;
	public Text type;
	public Text duration;
	public Text modified;

	public void SetDetails(){
		title.text = !String.IsNullOrEmpty(program.itemTitle.fi) ? program.itemTitle.fi : program.itemTitle.sv;
		description.text = !String.IsNullOrEmpty(program.description.fi) ? program.description.fi : program.description.sv;
		type.text = program.type;
		duration.text = program.duration;
		modified.text = program.indexDataModified.ToString();
	}

	public void CloseDetails(){
		Destroy (gameObject);
	}

}
