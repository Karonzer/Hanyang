using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
	private void Awake()
	{
		Initialize_Btn();
	}


	private void Initialize_Btn()
	{
		Button button = transform.GetChild(0).GetChild(0).Find("StartBtn").GetComponent<Button>();
		button.onClick.AddListener(Click_Btn);
	}


	private void Click_Btn()
	{
		Loding.LoadScene("Main");
		Resources.UnloadUnusedAssets();
		System.GC.Collect();
	}
}
