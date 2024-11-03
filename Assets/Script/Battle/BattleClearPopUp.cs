using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleClearPopUp : MonoBehaviour
{
	[SerializeField] private Transform[] popUps;
	[SerializeField] private Transform[] charcterLevelUp;
	[SerializeField] private Text[] charcterXpText;
	private void Awake()
	{
		Initialize_PopUps();
		Initialize_ClearPopUp();
		Initialize_PopUp02();
	}

	private void OnEnable()
	{
		Setting_PopUps();
	}

	private void Initialize_PopUps()
	{
		popUps = new Transform[transform.childCount];
		for(int i = 0; i < popUps.Length;i++)
		{
			popUps[i]  = transform.GetChild(i);
		}
	}

	private void Setting_PopUps()
	{
		for (int i = 0; i < popUps.Length; i++)
		{
			popUps[i].gameObject.SetActive(false);
		}

		if (DataBase.Instance.Get_CurrentSelectEnemyIndex() >= 6 && DataBase.Instance.Get_bClickBoss())
		{
			popUps[1].gameObject.SetActive(true);
		}
		else
		{
			popUps[0].gameObject.SetActive(true);
			Setting_ClearPopUp();
		}
	}

	private void Initialize_PopUp02()
	{
		Button button = transform.GetChild(1).Find("Btn").GetChild(0).GetComponent<Button>();
		button.onClick.AddListener(DataBase.Instance.Initialization);
	}

	private void Initialize_ClearPopUp()
	{
		Transform pos = transform.GetChild(0).Find("Pos");
		charcterLevelUp = new Transform[pos.childCount];
		for(int i = 0; i < charcterLevelUp.Length;i++)
		{
			charcterLevelUp[i] = pos.GetChild(i).GetChild(1);
		}

		charcterXpText = new Text[pos.childCount];
		for(int i = 0; i < charcterXpText.Length; i++)
		{
			charcterXpText[i] = pos.GetChild(i).GetChild(0).GetComponent<Text>();
		}

		Button okBtn = transform.GetChild(0).Find("Btn").GetChild(0).GetComponent<Button>();
		okBtn.onClick.AddListener(()=> Click_OkBtn());
	}

	private void Setting_ClearPopUp()
	{
		for (int i = 0; i < charcterLevelUp.Length; i++)
		{
			charcterLevelUp[i].gameObject.SetActive(false);
		}

		for (int i = 0; i < charcterXpText.Length; i++)
		{
			charcterXpText[i].gameObject.SetActive(true);
		}
		Setting_charcterXpText();
	}

	private void Setting_charcterXpText()
	{
		for (int i = 0; i < charcterXpText.Length; i++)
		{
			int index = i;
			int value = 0;
			if(DataBase.Instance.Get_bClickBoss())
			{
				value = value + DataBase.Instance.Get_BossStageExperienceValueList();
				DataBase.Instance.Set_BossClearCheck(true);
				DataBase.Instance.SaveCheckList();
			}
			else
			{
				value = value + DataBase.Instance.Get_StageExperienceValueList();
			}
			value = value + BGSC.Instance.get_BattleContentController.Get_currentEnemyXp() *  BGSC.Instance.get_BattleContentController.Get_HandlecurrentCharcterIndex()[index];
			charcterXpText[index].text = $"획득 경험치 : {value.ToString()}";
			charcterLevelUp[index].gameObject.SetActive(DataBase.Instance.Set_CharacterDataCurrentXP(index, value));
		}

		if (DataBase.Instance.Get_bClickBoss())
		{
			transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "획득 골드 : " + DataBase.Instance.FunctionGain_BossGold();
		}
		else
		{
			transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "획득 골드 : " + DataBase.Instance.FunctionGain_EnemyGold();
		}
	}	

	private void Click_OkBtn()
	{
		SoundController.Instance.PlaySound_Effect(0);
		BGSC.Instance.get_BattleContentController.FuntionClick_BackToMain();
	}


}
