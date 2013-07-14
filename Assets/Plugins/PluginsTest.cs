using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class PluginsTest : MonoBehaviour
{
#if UNITY_EDITOR
	private static void CallPlugins(string key)
	{
		GameObject.Find("ptest").SendMessage("PlistCallback", key) ;
	}
	private static Result GetResult(string key)
	{
		Result result;
		result.msg = "hoge";
		result.x = 120;
		result.y = 13;
			
		return result;
	}
#elif UNITY_IPHONE

	[DllImport ("__Internal")]
	private static extern void CallPlugins (string key);
	
	[DllImport("__Internal")]
	private static extern Result GetResult (string key);
	
#elif UNITY_ANDROID
	
	private static void CallPlugins(string key)
	{
		using(AndroidJavaClass testclass = new AndroidJavaClass("jp.terasurware.PluginTest"))
		{
		testclass.CallStatic("CallPlugins", key);
		}
	}
	private static Result GetResult(string key)
	{
		using ( AndroidJavaClass testclass = new AndroidJavaClass("jp.terasurware.PluginTest"))
		{
			Result r;
			AndroidJavaObject obj = testclass.CallStatic<AndroidJavaObject>("GetResult", key);
			r.msg = obj.Get<string>("msg");;
			r.x = obj.Get<int>("x");
			r.y = obj.Get<int>("y");
			return r;
		}
	}
	
#endif
	
	void Awake ()
	{
		gameObject.name = "ptest";
	}
	
	void OnGUI()
	{
		if( GUILayout.Button("call", GUILayout.Height(40)) )
		{
			CallPlugins("test");
		}
	}

	struct Result
	{
		public int x, y;
		public string msg;
	}
	

	
	void PlistCallback (string key)
	{
		Result r = GetResult (key);
		Debug.Log (string.Format ("{0}: {1}/{2}",r.msg, r.x, r.y));
	}
}
