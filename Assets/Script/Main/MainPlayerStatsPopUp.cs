using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class MainPlayerStatsPopUp : MonoBehaviour
{

	private void Awake()
	{
		Initialize_PopUp();
	}

	private void OnEnable()
	{
		Setting_PopUp();
	}


	private void Initialize_PopUp()
	{
		Button close = transform.GetChild(0).Find("Close").GetComponent<Button>();
		close.onClick.AddListener(Click_Close);
	}

	private void Click_Close()
	{
		transform.gameObject.SetActive(false);
	}


	private void Setting_PopUp()
	{
		Transform pos = transform.GetChild(0).GetChild(0);
		for (int i = 0; i < pos.childCount; i++)
		{
			int _index = i;
			pos.GetChild(_index).GetChild(0).GetComponent<Image>().sprite = MGSC.Instance.get_MainController.Get_SpriteAtlas().GetSprite(DataBase.Instance.Get_CharacterData(_index).name);
			pos.GetChild(_index).GetChild(1).GetComponent<Text>().text = "레벨 : " + DataBase.Instance.Get_CharacterData(_index).level;
			pos.GetChild(_index).GetChild(2).GetComponent<Text>().text = "공격력 : " + DataBase.Instance.Get_CharacterData(_index).baseStats.attack + " + " + DataBase.Instance.Get_CharacterData(_index).trainingStats.attack;
			pos.GetChild(_index).GetChild(3).GetComponent<Text>().text = "방어력 : " + DataBase.Instance.Get_CharacterData(_index).baseStats.defense + " + " + DataBase.Instance.Get_CharacterData(_index).trainingStats.defense;
			pos.GetChild(_index).GetChild(4).GetComponent<Text>().text = "체력 : " + DataBase.Instance.Get_CharacterData(_index).baseStats.health + " + " + DataBase.Instance.Get_CharacterData(_index).trainingStats.health;
		}
	}
}
