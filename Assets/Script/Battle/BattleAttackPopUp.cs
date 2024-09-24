using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public interface IGet_BattleAttackPopUp
{

}

public class BattleAttackPopUp : MonoBehaviour, IGet_BattleAttackPopUp
{
	[SerializeField] private Transform[] popUps;

	[SerializeField] private Outline[] outlines;

	[SerializeField] private Button nextBtn;

	private void Awake()
	{
		Initialize_PopUp();
		Initialize_SelectPopUp();
		Initialize_AttackGuidePopUp();
	}

	private void OnEnable()
	{
		Setting_PopUp();
		Setting_SelectPopUp();
	}

	private void Initialize_PopUp()
	{
		popUps = new Transform[transform.childCount];
		for(int i = 0; i < popUps.Length;i++)
		{
			popUps[i] = transform.GetChild(i);
		}
	}

	public void Setting_PopUp()
	{
		for (int i = 0; i < popUps.Length; i++)
		{
			popUps[i].gameObject.SetActive(false);
		}
		popUps[0].gameObject.SetActive(true);
	}

	public void Initialize_SelectPopUp()
	{
		Transform pos = popUps[0];
		outlines = new Outline[pos.GetChild(1).childCount];
		int num = DataBase.Instance.Get_CurrentSelectEnemyIndex();
		for (int i = 0; i < pos.GetChild(1).childCount; i++)
		{
			int index = i;
			Transform btn = pos.GetChild(1).GetChild(index);

			Button button = btn.GetComponent<Button>();
			button.onClick.AddListener(() => Click_SelectBtn(index));

			outlines[index] = btn.GetComponent<Outline>();

			btn.GetChild(0).GetComponent<Image>().sprite
		= BGSC.Instance.get_BattleContentController.Get_BattleSpriteController()
			.Get_EnemyImage(num);
		}

		nextBtn = pos.GetChild(2).GetChild(1).GetComponent<Button>();
		nextBtn.onClick.AddListener(Click_NextBtn);

		Button close = pos.GetChild(2).GetChild(0).GetComponent<Button>();
		close.onClick.AddListener(Click_CloseBtn);
	}

	private void Setting_SelectPopUp()
	{
		for (int i = 0; i < outlines.Length; i++)
		{
			outlines[i].enabled= false;
		}
		nextBtn.interactable= false;

		for(int i = 0; i < BGSC.Instance.get_BattleContentController.Get_bCurrentEnemyAlivel().Length;i++)
		{
			if (BGSC.Instance.get_BattleContentController.Get_bCurrentEnemyAlivel()[i] == true)
			{
				outlines[i].gameObject.SetActive(true);
			}
			else
			{
				outlines[i].gameObject.SetActive(false);
			}	
		}
	}

	private void Click_SelectBtn(int _index)
	{
		for (int i = 0; i < outlines.Length; i++)
		{
			outlines[i].enabled = false;
		}
		outlines[_index].enabled = true;
		nextBtn.interactable = true;
		BGSC.Instance.get_BattleContentController.Set_CurrentSelectEnemyIndex(_index);
	}

	private void Click_CloseBtn()
	{
		transform.gameObject.SetActive(false);
	}

	private void Click_NextBtn()
	{
		popUps[1].gameObject.SetActive(true);
	}

	private void Initialize_AttackGuidePopUp()
	{
		Transform pos = popUps[1].GetChild(0).GetChild(1);

		Button start = pos.GetChild(0).GetComponent<Button>();
		start.onClick.AddListener(Click_AttackGuidePopUpStartBtn);

		Button back = pos.GetChild(1).GetComponent<Button>();
		back.onClick.AddListener(Click_AttackGuidePopUpBackBtn);
	}

	private void Click_AttackGuidePopUpStartBtn()
	{
		BGSC.Instance.get_BattleContentController.Start_CharcterBattle();
		transform.gameObject.SetActive(false);
	}



	private void Click_AttackGuidePopUpBackBtn()
	{
		popUps[1].gameObject.SetActive(false);
	}
}
