using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleClearPopUp : MonoBehaviour
{

	[SerializeField] private Transform[] charcterLevelUp;
	[SerializeField] private Text[] charcterXpText;
	private void Awake()
	{
		Initialize_ClearPopUp();
	}

	private void OnEnable()
	{
		Setting_ClearPopUp();
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
			DataBase.Instance.FunctionGain_BossGold();
		}
		else
		{
			DataBase.Instance.FunctionGain_EnemyGold();
		}
	}	

	private void Click_OkBtn()
	{
		BGSC.Instance.get_BattleContentController.FuntionClick_BackToMain();
	}


}
