using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
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
	private DatabaseReference reference;

	string myLog;

	void Awake () {
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://automationiapsimple.firebaseio.com/");
		reference = FirebaseDatabase.DefaultInstance.RootReference;
	}

	public void Start() {
		// testRun ();
	}

	public void testRun() {

		// Add test cases to test suite
		testSuite.Add (test01 ());
		testSuite.Add (test02 ());
		testSuite.Add (test03 ());
		testSuite.Add (test04 ());
		testSuite.Add (test05 ());
		testSuite.Add (test06 ());
		testSuite.Add (test07 ());
		testSuite.Add (finalization ());

		maxTest = testSuite.Count;

		// Start test run
		StartCoroutine(testSuite[runOrder]);
	}

	// Consumable Successfully
	public IEnumerator test01() {
		runOrder++;
		showProgess (runOrder);

		yield return new WaitForSeconds(0.3f);
		click ("Consumable");

		yield return new WaitForSeconds(0.3f);
		click ("Button1");

		yield return new WaitForSeconds (0.1f);
		resultTable.Add(new TestCase(
			"C2777-1",
			compareInfo ("ConsumablePass"), 
			"Consumable Successfully Event",
			System.DateTime.Now,
			stack.Peek()));

		if (runOrder < maxTest) {
			yield return StartCoroutine (testSuite [runOrder]);
		} 
	}

	//Consumable Fail
	public IEnumerator test02() {
		runOrder++;
		showProgess (runOrder);

		yield return new WaitForSeconds(0.3f);
		click ("ConsumableFake");

		yield return new WaitForSeconds (0.3f);
		click ("Button2");

		yield return new WaitForSeconds (0.1f);
		resultTable.Add(new TestCase(
			"C2777-2",
			compareInfo ("ConsumableFail"), 
			"Consumable Fail Event",
			System.DateTime.Now,
			stack.Peek()));

		if (runOrder < maxTest) {
			yield return StartCoroutine (testSuite [runOrder]);
		} 
	}

	//Non-Consumable Successfully
	public IEnumerator test03() {
		runOrder++;
		showProgess (runOrder);

		yield return new WaitForSeconds(0.3f);
		click ("NonConsumable");

		yield return new WaitForSeconds (0.3f);
		click ("Button1");

		yield return new WaitForSeconds (0.1f);
		resultTable.Add(new TestCase(
			"C2778-1",
			compareInfo ("NonConsumablePass"), 
			"Non-Consumable Successfully Event",
			System.DateTime.Now,
			stack.Peek()));

		if (runOrder < maxTest) {
			yield return StartCoroutine (testSuite [runOrder]);
		} 
	}

	//Non-Consumable Fail
	public IEnumerator test04() {
		runOrder++;
		showProgess (runOrder);

		yield return new WaitForSeconds(0.3f);
		click ("NonConsumableFake");

		yield return new WaitForSeconds (0.3f);
		click ("Button2");

		yield return new WaitForSeconds (0.1f);
		resultTable.Add(new TestCase(
			"C2778-2",
			compareInfo ("NonConsumableFail"), 
			"Non-Consumable Fail Event",
			System.DateTime.Now,
			stack.Peek()));

		if (runOrder < maxTest) {
			yield return StartCoroutine (testSuite [runOrder]);
		} 
	}

	//Subscription Successfully
	public IEnumerator test05() {
		runOrder++;
		showProgess (runOrder);

		yield return new WaitForSeconds(0.3f);
		click ("Subscription");


		yield return new WaitForSeconds (0.3f);
		click ("Button1");

		yield return new WaitForSeconds (0.1f);
		resultTable.Add(new TestCase(
			"C2779-1",
			compareInfo ("SubscriptionPass"), 
			"Subscription Successfully Event",
			System.DateTime.Now,
			stack.Peek()));

		if (runOrder < maxTest) {
			yield return StartCoroutine (testSuite [runOrder]);
		} 
	}

	//Subscription Fail
	public IEnumerator test06() {
		runOrder++;
		showProgess (runOrder);

		yield return new WaitForSeconds(0.3f);
		click ("SubscriptionFake");

		yield return new WaitForSeconds (0.3f);
		click ("Button2");

		yield return new WaitForSeconds (0.1f);
		resultTable.Add(new TestCase(
			"C2779-2",
			compareInfo ("SubscriptionFail"), 
			"Subscription Fail Event",
			System.DateTime.Now,
			stack.Peek()));

		if (runOrder < maxTest) {
			yield return StartCoroutine (testSuite [runOrder]);
		} 
	}

	//Restore
	public IEnumerator test07() {
		runOrder++;
		showProgess (runOrder);

#if UNITY_IOS && !UNITY_EDITOR

		yield return new WaitForSeconds(0.3f);
		click ("Restore");

		yield return new WaitForSeconds (0.1f);
		resultTable.Add(new TestCase(
			"C2803",
			compareInfo ("NonConsumablePass"), 
			"Restore Event",
			System.DateTime.Now,
			stack.Peek()));

#endif

		if (runOrder < maxTest) {
			yield return StartCoroutine (testSuite [runOrder]);
		} 
	}

	public IEnumerator finalization() {
		showReport ();
		yield return new WaitForSeconds (0.3f);
	}
		
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
		return stack.Peek() == expected;
	}

	private void showProgess(int progress) {
		// Show progree to the screen
		processDisplayer.GetComponent<Text>().text = "Please wait, running " + progress + " / " + (maxTest - 1) + "...";
	}

	private void showReport() {
		// Push the result to the server and reminds user
		int failed = 0;

		foreach(TestCase result in resultTable) {
			if (result.getResult () == false) {
				failed++;
			}
			pushResultToServer (result);
		}
			
		processDisplayer.GetComponent<Text> ().text = "Finished! " + (maxTest - failed - 1) + " / " + (maxTest - 1)  + " Passed!";
	}

	private void pushResultToServer(TestCase testCase) {
		string key = reference.Child("QAReport").Push().Key;
		Dictionary<string, object> childUpdates = new Dictionary<string, object>();
		childUpdates ["/QAReport/" + System.DateTime.Now.ToString("MMM d, yyyy") + "/" + testCase.getDescitpion()  + "/" + testCase.getDate().ToString("HH:mm:ss tt zz") + "/Result/"] = testCase.getResult () == true ? "Pass" : "Fail";
		childUpdates ["/QAReport/" + System.DateTime.Now.ToString("MMM d, yyyy") + "/" + testCase.getDescitpion()  + "/" + testCase.getDate().ToString("HH:mm:ss tt zz") + "/RealResult/"] = testCase.getResult () == true ? null : testCase.getResultContent();
		childUpdates ["/QAReport/" + System.DateTime.Now.ToString("MMM d, yyyy") + "/" + testCase.getDescitpion()  + "/" + testCase.getDate().ToString("HH:mm:ss tt zz") + "/DeviceInfo/DeviceType"] = UnityEngine.SystemInfo.deviceType.ToString();
		childUpdates ["/QAReport/" + System.DateTime.Now.ToString("MMM d, yyyy") + "/" + testCase.getDescitpion()  + "/" + testCase.getDate().ToString("HH:mm:ss tt zz") + "/DeviceInfo/DeviceModel"] = UnityEngine.SystemInfo.deviceModel.ToString();;
		childUpdates ["/QAReport/" + System.DateTime.Now.ToString("MMM d, yyyy") + "/" + testCase.getDescitpion()  + "/" + testCase.getDate().ToString("HH:mm:ss tt zz") + "/DeviceInfo/OperatingSystem"] = UnityEngine.SystemInfo.operatingSystem.ToString();;

		reference.UpdateChildrenAsync(childUpdates);
	}
}
