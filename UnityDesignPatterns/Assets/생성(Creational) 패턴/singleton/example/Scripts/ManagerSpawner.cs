using System.Collections;
using System.Collections.Generic;
using NG.Patterns.Structure.Singleton;
using UnityEngine;
using UnityEngine.UI;

public class ManagerSpawner : MonoBehaviour
{
	public GameObject Manager;
	public Text DebugText;

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Instantiate(Manager);
			DebugText.text = "5 이상이 되면 게임매니저 오브젝트는 파괴됩니다.";
		}	
	}
}
