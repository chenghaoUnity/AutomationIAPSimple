using UnityEngine.Events;
using UnityEngine.UI;
using UnityEditor;
using System.IO;
using System.Reflection;
using System.Collections.Generic;

public static class BuildHelper {

	public static void SwitchPlatformsGooglePlay(UnityEngine.CloudBuild.BuildManifestObject manifest) {
		UnityEngine.Purchasing.UnityPurchasingEditor.TargetAndroidStore(UnityEngine.Purchasing.AndroidStore.GooglePlay);
	}

	public static void SwitchPlatformsCloudMoolah(UnityEngine.CloudBuild.BuildManifestObject manifest) {
		UnityEngine.Purchasing.UnityPurchasingEditor.TargetAndroidStore(UnityEngine.Purchasing.AndroidStore.CloudMoolah);
	}

	public static void SwitchPlatformsAmazonAppStore(UnityEngine.CloudBuild.BuildManifestObject manifest) {
		UnityEngine.Purchasing.UnityPurchasingEditor.TargetAndroidStore(UnityEngine.Purchasing.AndroidStore.AmazonAppStore);
	}

	public static void SwitchPlatformsSamsungApps(UnityEngine.CloudBuild.BuildManifestObject manifest) {
		UnityEngine.Purchasing.UnityPurchasingEditor.TargetAndroidStore(UnityEngine.Purchasing.AndroidStore.SamsungApps);
	}
}


#if !UNITY_CLOUD_BUILD
namespace UnityEngine.CloudBuild
{
	public class BuildManifestObject : ScriptableObject
	{
		// Tries to get a manifest value - returns true if key was found and could be cast to type T, false otherwise.
		public bool TryGetValue<T> (string key, out T result) 
		{
			result = default(T);
			return false;
		}

		// Retrieve a manifest value or throw an exception if the given key isn't found.
		public T GetValue<T> (string key)
		{
			return default(T);
		}

		// Sets the value for a given key.
		public void SetValue (string key, object value) { }

		// Copy values from a dictionary. ToString() will be called on dictionary values before being stored.
		public void SetValues (Dictionary<string, object> sourceDict) { }

		// Remove all key/value pairs
		public void ClearValues () { }

		// Returns a Dictionary that represents the current BuildManifestObject
		public Dictionary<string, object> ToDictionary ()
		{
			return new Dictionary<string, object>();
		}

		// Returns a JSON formatted string that represents the current BuildManifestObject
		public string ToJson ()
		{
			return null;
		}

		// Returns an INI formatted string that represents the current BuildManifestObject
		public override string ToString ()
		{
			return null;
		}
	}
}
#endif
