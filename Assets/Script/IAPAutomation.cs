using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.IO;

public class IAPAutomation : MonoBehaviour {

	public List<TestCase> resultTable = new List<TestCase>();
	public List<IEnumerator> testSuite = new List<IEnumerator>();

	public GameObject processDisplayer;
	public UnityEvent onCompelete;
	public Stack<string> stack = new Stack<string>();

	private int maxTest = 0;
	private int runOrder = 0;

	private bool QAstatus = false;

	string myLog;


	private void click(string name, string needed = null) {
		StartCoroutine (clickHelper (name, needed));
	}

	private IEnumerator clickHelper(string name, string needed = null) {
		// Click a button
		if (needed == null) {
			GameObject.Find (name).GetComponent<Button> ().onClick.Invoke ();
		}

		while (GameObject.Find (needed) == null) {
			yield return new WaitForEndOfFrame ();
		}

		GameObject.Find (name).GetComponent<Button> ().onClick.Invoke ();
	}

	private bool compareInfo(string expected) {
		// Get text from a text UI
		return expected == stack.Peek();
	}

	private bool compareInfo(string expected, string expected2) {
		// Get text from a text UI

		string real1 = stack.Peek ();
		stack.Pop ();
		string real2 = stack.Peek ();

		if (real1 == expected && real2 == expected2) {
			return true;
		}

		if (real1 == expected2 && real2 == expected) {
			return true;
		}

		return false;
	}

	private void showProgess(int progress) {
		// Show progree to the screen
		processDisplayer.GetComponent<Text>().text = "Please wait, running " + progress + " / " + (maxTest - 1) + "...";
	}
}
