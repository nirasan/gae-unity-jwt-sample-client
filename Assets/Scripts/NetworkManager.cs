using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class NetworkManager : SingletonMonoBehaviour<NetworkManager>
{
	const string SetAccessTokenHeader = "Set-AccessToken";
	const string SetRefreshTokenHeader = "Set-RefreshToken";

	public string AccessToken { get; private set; }

	public string RefreshToken { get; private set; }

	public IEnumerator Post<T> (string url, object param, bool authorization, Action<T> onSuccess)
	{
		string postString = JsonUtility.ToJson (param);
		byte[] postBytes = Encoding.Default.GetBytes (postString);
		var header = authorization ? CreateHeader () : CreateAuthorizationHeader ();

		var www = new WWW (url, postBytes, header);
		yield return www;

		if (!string.IsNullOrEmpty (www.error))
		{
			Debug.LogError (www.error);
			www.Dispose ();
			yield break;
		}

		if (www.responseHeaders.ContainsKey (SetAccessTokenHeader))
		{
			AccessToken = www.responseHeaders [SetAccessTokenHeader];
		}

		if (www.responseHeaders.ContainsKey (SetRefreshTokenHeader))
		{
			RefreshToken = www.responseHeaders [SetRefreshTokenHeader];
		}

		if (onSuccess != null)
		{
			onSuccess (JsonUtility.FromJson<T> (www.text));
		}
	}

	Dictionary<string, string> CreateHeader ()
	{
		var header = new Dictionary<string, string> ();
		header.Add ("Content-Type", "application/json; charset=UTF-8");
		header.Add ("Accept", "application/json");
		return header;
	}

	Dictionary<string, string> CreateAuthorizationHeader ()
	{
		var header = CreateHeader ();
		header.Add ("Authorization", string.Format ("token access=\"{0}\" refresh=\"{1}\"", AccessToken, RefreshToken));
		return header;
	}
}
