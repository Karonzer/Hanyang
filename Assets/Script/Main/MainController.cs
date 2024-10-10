using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class MainController : MonoBehaviour
{
	[SerializeField] private Transform[] popUps;
	[SerializeField] private int currentPopUpIndex;

	private void Awake()
	{
		Initialize_SelectBtn();
		Initialize_PopUps();
		Initialize_BattleSelectPopUp();
	}

	private void OnEnable()
	{
		Setting_PopUps();
		currentPopUpIndex = 0;
	}

	private void Initialize_SelectBtn()
	{
		Transform pos = transform.GetChild(0).GetChild(0).GetChild(1);
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
	}

	private void Click_ClosePopUp()
	{
		popUps[currentPopUpIndex].gameObject.SetActive(false);
	}


	private void Initialize_PopUps()
	{
		Transform pos = transform.GetChild(0).GetChild(0);
		popUps = new Transform[3];
		for(int i = 0; i < popUps.Length;i++)
		{
			popUps[i] = pos.GetChild(i + 2);
		}
	}

	private void Setting_PopUps()
	{
		for (int i = 0; i < popUps.Length; i++)
		{
			popUps[i].gameObject.SetActive(false);
		}
	}

	private void Initialize_BattleSelectPopUp()
	{
		Transform pos = popUps[0].GetChild(0);
		Transform enemyPos = pos.GetChild(1).GetChild(0).GetChild(0).GetChild(0);
		for (int i = 0; i< enemyPos.childCount;i++)
		{
			int index = i;
			Button button = enemyPos.GetChild(i).GetComponent<Button>();
			button.onClick.AddListener(() => Loding_LoadBattleContentScene(index));
		}

		Button close = pos.GetChild(0).GetChild(3).GetComponent<Button>();
		close.onClick.AddListener(() => Click_ClosePopUp());
	}



	public void Loding_LoadBattleContentScene(int _index)
    {
        DataBase.Instance.Set_bClickBoss(false);
        DataBase.Instance.Set_CurrentSelectEnemyIndex(_index);
		Loding.LoadScene("BattleContent");
		Resources.UnloadUnusedAssets();
		System.GC.Collect();
	}
}
