using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainBattleSelectPopUp : MonoBehaviour
{
	private Button[] btns;
    private Transform[] popUps;

	private int currentIndex;

	private void Awake()
	{
		Initialize_PopUps();
		Initialize_EnemyBattleSelectPopUp();
		Initialize_BossBattleSelectPopUp();
		Initialize_FinalBossBattleSelectPopUp();
		Initialize_Btns();
	}

	private void OnEnable()
	{
		DataBase.Instance.Set_bClickBoss(false);
		currentIndex = 0;
		Setting_PopUps();
		Setting_BtnsImageColor();
	}

	private void Initialize_PopUps()
	{
		Transform pos = transform.GetChild(0).GetChild(1);
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
		popUps[0].gameObject.SetActive(true);
	}

	private void Initialize_Btns()
	{
		Transform pos = transform.GetChild(0).GetChild(0);
		btns = new Button[pos.childCount - 1];
		for(int i = 0; i < btns.Length;i++)
		{
			int index = i;
			btns[i] = pos.GetChild(i).GetComponent<Button>();
			btns[i].onClick.AddListener(() => Click_Btn(index));
		}

		Button close = transform.GetChild(0).GetChild(0).GetChild(3).GetComponent<Button>();
		close.onClick.AddListener(() => Click_ClosePopUp());
	}

	private void Setting_BtnsImageColor()
	{
		for (int i = 0; i < btns.Length; i++)
		{
			btns[i].interactable = true;
		}
		btns[0].interactable = false;
	}

	private void Click_Btn(int _index)
	{
		popUps[currentIndex].gameObject.SetActive(false);
		currentIndex = _index;
		popUps[currentIndex].gameObject.SetActive(true);


		for (int i = 0; i < btns.Length; i++)
		{
			btns[i].interactable = true;
		}
		btns[currentIndex].interactable = false;

		if(currentIndex == 1 || currentIndex == 2)
		{
			DataBase.Instance.Set_bClickBoss(true);
		}
		else
		{
			DataBase.Instance.Set_bClickBoss(false);
		}
	}

	private void Initialize_EnemyBattleSelectPopUp()
	{
		Transform enemyPos = popUps[0].GetChild(0).GetChild(0);
		for (int i = 0; i < enemyPos.childCount; i++)
		{
			int index = i;
			Button button = enemyPos.GetChild(i).GetComponent<Button>();
			button.onClick.AddListener(() => Loding_LoadBattleContentScene(index));
		}
	}

	private void Initialize_BossBattleSelectPopUp()
	{
		Transform enemyPos = popUps[1].GetChild(0).GetChild(0);
		for (int i = 0; i < enemyPos.childCount; i++)
		{
			int index = i;
			Button button = enemyPos.GetChild(i).GetComponent<Button>();
			button.onClick.AddListener(() => Loding_LoadBattleContentScene(index));
		}
	}

	private void Initialize_FinalBossBattleSelectPopUp()
	{
		Transform finalBoss = popUps[2].GetChild(0);
		int index = 6;
		Button button = finalBoss.GetComponent<Button>();
		button.onClick.AddListener(() => Loding_LoadFinalBossBattleContentScene(index));

	}

	public void Loding_LoadBattleContentScene(int _index)
	{
		DataBase.Instance.Set_CurrentSelectEnemyIndex(_index);
		Loding.LoadScene("BattleContent");
		Resources.UnloadUnusedAssets();
		System.GC.Collect();
	}

	public void Loding_LoadFinalBossBattleContentScene(int _index)
	{
		DataBase.Instance.Set_CurrentSelectEnemyIndex(_index);
		Loding.LoadScene("Final_BattleContent");
		Resources.UnloadUnusedAssets();
		System.GC.Collect();
	}

	private void Click_ClosePopUp()
	{
		transform.gameObject.SetActive(false);
	}
}
