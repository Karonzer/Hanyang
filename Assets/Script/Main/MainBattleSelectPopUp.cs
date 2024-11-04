using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainBattleSelectPopUp : MonoBehaviour
{
	[SerializeField] private Button[] btns;
	[SerializeField] private Transform[] popUps;

	[SerializeField] private Button[] bossSelectBtn;

	[SerializeField] private int currentIndex;

	[SerializeField] private Button finalBossBtn;

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
		Setting_BossBattleSelectPopUp();
		Setting_FinalBossBattleSelectPopUp();
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
		SoundController.Instance.PlaySound_Effect(0);
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
		bossSelectBtn = new Button[enemyPos.childCount];
		for (int i = 0; i < bossSelectBtn.Length; i++)
		{
			int index = i;
			bossSelectBtn[index] = enemyPos.GetChild(i).GetComponent<Button>();
			bossSelectBtn[index].onClick.AddListener(() => Loding_LoadBattleContentScene(index));
		}
	}

	private void Setting_BossBattleSelectPopUp()
	{
		for(int i = 0;i <  DataBase.Instance.Get_BossClearCheck().Length;i++)
		{
			int index = i;
			if (!DataBase.Instance.Get_BossClearCheck()[index])
			{
				bossSelectBtn[index].interactable = true;
			}
			else
			{
				bossSelectBtn[index].interactable = false;
			}
		}
	}

	private void Initialize_FinalBossBattleSelectPopUp()
	{
		Transform finalBoss = popUps[2].GetChild(0);
		int index = 6;
		finalBossBtn = finalBoss.GetComponent<Button>();
		finalBossBtn.onClick.AddListener(() => Loding_LoadFinalBossBattleContentScene(index));

	}

	private void Setting_FinalBossBattleSelectPopUp()
	{
		bool check = true;
		for(int i = 0; i < DataBase.Instance.Get_BossClearCheck().Length;i++)
		{
			if (!DataBase.Instance.Get_BossClearCheck()[i])
			{
				check = false;
			}
		}

		if(check)
		{
			finalBossBtn.interactable = true;
			popUps[2].GetChild(1).GetComponent<Text>().text = "최종 보스 몬스터";
		}
		else
		{
			finalBossBtn.interactable = false;
			popUps[2].GetChild(1).GetComponent<Text>().text = "보스 몬스터를 전부 처리해야 버튼이 활성화 됩니다";
		}
	}

	public void Loding_LoadBattleContentScene(int _index)
	{
		SoundController.Instance.PlaySound_Effect(0);
		DataBase.Instance.Set_CurrentSelectEnemyIndex(_index);
		Loding.LoadScene("BattleContent");
		Resources.UnloadUnusedAssets();
		System.GC.Collect();
	}

	public void Loding_LoadFinalBossBattleContentScene(int _index)
	{
		SoundController.Instance.PlaySound_Effect(0);
		DataBase.Instance.Set_CurrentSelectEnemyIndex(_index);
		Loding.LoadScene("Final_BattleContent");
		Resources.UnloadUnusedAssets();
		System.GC.Collect();
	}

	private void Click_ClosePopUp()
	{
		transform.gameObject.SetActive(false);
		SoundController.Instance.PlaySound_Effect(0);
	}
}
