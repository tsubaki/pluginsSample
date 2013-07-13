package jp.terasurware;

import com.unity3d.player.*;

public class PluginTest {
	
	
	public static void CallPlugins(String key)
	{
		UnityPlayer.UnitySendMessage("ptest", "PlistCallback", key);
	}
	public static Result GetResult(String key)
	{
		Result r = new Result();
		r.msg = "hoge";
		r.x = 128;
		r.y = 256;
		return r;
	}

}
