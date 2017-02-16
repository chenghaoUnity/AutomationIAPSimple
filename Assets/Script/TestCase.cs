using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCase {

	private string ID;
	private bool result;
	private string descitpion;
	private System.DateTime date;
	private string resultContent;

	public TestCase() {
	}

	public TestCase(string ID, bool result, string descitpion, System.DateTime date, string resultContent) {
		this.ID = ID;
		this.result = result;
		this.descitpion = descitpion;
		this.date = date;
		this.resultContent = resultContent;
	}

	public string getID() {
		return ID;
	}

	public bool getResult() {
		return result;
	}

	public string getDescitpion() {
		return descitpion;
	}

	public System.DateTime getDate() {
		return date;
	}

	public string getResultContent() {
		return resultContent;
	}

	public void setID(string ID) {
		this.ID = ID;
	}

	public void setResult(bool result) {
		this.result = result;
	}

	public void setDescitpion(string descitpion) {
		this.descitpion = descitpion;
	}

	public void setdate(System.DateTime date) {
		this.date = date;
	}

	public void setResultContent(string resultContent) {
		this.resultContent = resultContent;
	}
}