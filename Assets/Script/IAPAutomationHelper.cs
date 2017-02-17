using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IAPAutomationHelper : MonoBehaviour {

	public void PushPassScreen(GameObject g) {
		PushScreen(g.name + "Pass");
		GameObject.FindObjectOfType<IAPAutomation> ().stack.Push (g.name + "Pass");

		foreach (string s in GameObject.FindObjectOfType<IAPAutomation> ().stack) {
			print (s);
		}
	}

	public void PushFailScreen(GameObject g) {
		PushScreen(g.name + "Fail");
		GameObject.FindObjectOfType<IAPAutomation> ().stack.Push (g.name + "Fail");
	}

	public void PushScreen(string content) {
		GameObject.Find ("Log").GetComponent<Text> ().text += " " + content;
	}
}
