using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
	[SerializeField] private Transform[] popUps;
	[SerializeField] private int currentPopUpIndex;

	[SerializeField] private Text goldText;

	private void Awake()
	{
		Initialize_SelectBtn();
		Initialize_PopUps();
		Initialize_GoldText();
	}

	private void OnEnable()
	{
		Setting_PopUps();
		Funtion_SettingGoldText();
		currentPopUpIndex = 0;
	}

	private void Initialize_SelectBtn()
	{
		Transform pos = transform.GetChild(0).GetChild(0).Find("SelectBtn");
		for(int i = 0; i < pos.childCount;i++)
		{
			int index = i;
			Button button = pos.GetChild(i).GetComponent<Button>();
			button.onClick.AddListener(() => Click_OpenPopUpSelectBtn(index));
		}
	}

	private void Click_OpenPopUpSelectBtn(int _index)
	{
		currentPopUpIndex = _index;
		popUps[currentPopUpIndex].gameObject.SetActive(true);

		if(currentPopUpIndex ==0)
		{
			DataBase.Instance.Set_bClickBoss(false);
		}
	}

	private void Click_ClosePopUp()
	{
		popUps[currentPopUpIndex].gameObject.SetActive(false);
	}


	private void Initialize_PopUps()
	{
		Transform pos = transform.GetChild(0).GetChild(0).Find("PopUps");
		popUps = new Transform[pos.childCount];
		for(int i = 0; i < popUps.Length;i++)
		{
			popUps[i] = pos.GetChild(i);
		}
	}

	private void Setting_PopUps()
	{
		for (int i = 0; i < popUps.Length; i++)
		{
			popUps[i].gameObject.SetActive(false);
		}
	}

	private void Initialize_GoldText()
	{
		goldText = transform.GetChild(0).GetChild(0).Find("UI").GetChild(0).GetChild(0).GetComponent<Text>();
	}

	public void Funtion_SettingGoldText()
	{
		goldText.text = "현재 소지금 :  " + DataBase.Instance.Get_CurrentGold().ToString();
	}

}
