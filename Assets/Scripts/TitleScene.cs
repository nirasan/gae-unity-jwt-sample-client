using UnityEngine;
using System.Collections;

public class TitleScene : MonoBehaviour
{
	class EmptyRequest
	{

	}

	IEnumerator Start ()
	{
		var nm = NetworkManager.Instance;

		RegistrationHandlerResponse res1 = null;
		yield return StartCoroutine (nm.Post<RegistrationHandlerResponse> (
			"http://localhost:8080/registration", 
			new RegistrationHandlerRequest{ Username = "user3", Password = "pass1" },
			false, 
			(res) => res1 = res
		));

		Debug.Log (res1.Success);

		AuthenticationHandlerResponse res2 = null;
		yield return StartCoroutine (nm.Post<AuthenticationHandlerResponse> (
			"http://localhost:8080/authentication", 
			new AuthenticationHandlerRequest{ Username = "user3", Password = "pass1" },
			false, 
			(res) => res2 = res
		));

		Debug.Log (res2.Success);
		Debug.Log (nm.AccessToken);
		Debug.Log (nm.RefreshToken);

		HelloWorldHandlerResponse res3 = null;
		yield return StartCoroutine (nm.Post<HelloWorldHandlerResponse> (
			"http://localhost:8080/authorized_hello", 
			new EmptyRequest{ },
			false, 
			(res) => res3 = res
		));

		Debug.Log (res3.Success);
		Debug.Log (res3.Message);
	}
}
