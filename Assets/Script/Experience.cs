using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Experience : MonoBehaviour {

	private static void Helper() {
		using (var input = File.OpenText("Assets/Plugins/UnityPurchasing/script/IAPButton.cs"))
		using (var output = new StreamWriter("Assets/Plugins/UnityPurchasing/script/IAPButton2.cs")) {
			string line;
			while (null != (line = input.ReadLine())) {
				if (line.Contains("module.useFakeStoreUIMode = FakeStoreUIMode.StandardUser;")) {
					output.WriteLine("#if UNITY_IOS && !UNITY_EDITOR");
					output.WriteLine("module.useFakeStoreAlways = false;");
					output.WriteLine("#else");
					output.WriteLine("module.useFakeStoreAlways = true;");
					output.WriteLine("#endif");
				} else {
					output.WriteLine(line);
				}
			}
		}

		File.Delete ("Assets/Plugins/UnityPurchasing/script/IAPButton.cs");
		System.IO.File.Move("Assets/Plugins/UnityPurchasing/script/IAPButton2.cs", "Assets/Plugins/UnityPurchasing/script/IAPButton.cs");
	}
}
