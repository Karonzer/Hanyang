using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IGet_BattlePopUpController
{
	public void Open_PopUp(int _index);
	public void Close_PopUp();
	public void Set_CurrentPopUpIndex(int _index);
	public void ReSettin_GuidePopText(int _index);
}

public class BattlePopUpController : MonoBehaviour, IGet_BattlePopUpController
{
	[SerializeField] private Transform[] popUps;
	[SerializeField] private int currentPopUpIndex;

	[SerializeField] private Text guidePopText;

	private void Awake()
	{
		Initialize_PopUp();

		Initialize_StartPopUp();
		Initialize_DefensePopUpAndHealthPopUp();
		Initialize_GuidePopText();
		Initialize_DefeatPopUpBtn();
	}

	private void OnEnable()
	{
		Setting_PopUp();
		currentPopUpIndex = popUps.Length - 1;
	}

	private void Start()
	{
		
	}

	private void Initialize_PopUp()
	{
		popUps = new Transform[transform.GetChild(0).childCount];
		for(int i  = 0; i < popUps.Length;i++)
		{
			popUps[i] = transform.GetChild(0).GetChild(i);
		}
	}

	private void Setting_PopUp()
	{
		for (int i = 0; i < popUps.Length; i++)
		{
			popUps[i].gameObject.SetActive(false);
		}
		popUps[popUps.Length - 1].gameObject.SetActive(true);
	}

	private void Initialize_StartPopUp()
	{
		Transform pos = popUps[popUps.Length - 1].GetChild(0).Find("Btn");
		
		Button startBtn = pos.GetChild(0).GetComponent<Button>();
		startBtn.onClick.AddListener(() => Click_GameStart());

		Button back = pos.GetChild(1).GetComponent<Button>();
		back.onClick.AddListener(() => Click_BackToMain());
	}

	private void Click_GameStart()
	{
		popUps[currentPopUpIndex].gameObject.SetActive(false);
		BGSC.Instance.get_BattleContentController.Start_GameStart();
	}

	private void Click_BackToMain()
	{
		BGSC.Instance.get_BattleContentController.FuntionClick_BackToMain();
	}

	private void Initialize_DefensePopUpAndHealthPopUp()
	{
		Transform defensePos = popUps[1];
		Button defensePosStartBtn = defensePos.GetChild(0).GetChild(1).GetChild(0).GetComponent<Button>();
		defensePosStartBtn.onClick.AddListener(() => Click_Defense());
		Button defensePosBackBtn = defensePos.GetChild(0).GetChild(1).GetChild(1).GetComponent<Button>();
		defensePosBackBtn.onClick.AddListener(() => Click_CloseCurrnetPopUp());

		Transform healthPos = popUps[2];
		Button healthPosStartBtn = healthPos.GetChild(0).GetChild(1).GetChild(0).GetComponent<Button>();
		healthPosStartBtn.onClick.AddListener(() => Click_Health());
		Button healthPosBackBtn = healthPos.GetChild(0).GetChild(1).GetChild(1).GetComponent<Button>();
		healthPosBackBtn.onClick.AddListener(() => Click_CloseCurrnetPopUp());
	}

	private void Click_Defense()
	{
		BGSC.Instance.get_BattleContentController.FunctionClick_DefenseState();
		Click_CloseCurrnetPopUp();
		Debug.Log("방어상태");
	}

	private void Click_Health()
	{
		BGSC.Instance.get_BattleContentController.FunctionClick_RecoverCurrentHealth();
		Click_CloseCurrnetPopUp();
		Debug.Log("회복상태");
	}

	private void Initialize_GuidePopText()
	{
		guidePopText = popUps[6].GetChild(0).GetChild(0).GetComponent<Text>();
	}


	private void Click_CloseCurrnetPopUp()
	{
		popUps[currentPopUpIndex].gameObject.SetActive(false);
		Debug.Log("닫기");
	}

	public void Initialize_DefeatPopUpBtn()
	{
		Transform pos = popUps[4].GetChild(0).Find("Btn");

		Button back = pos.GetChild(0).GetComponent<Button>();
		back.onClick.AddListener(() => Click_BackToMain());
	}

	//attack select
	public void Open_PopUp(int _index)
	{
		currentPopUpIndex = _index;
		popUps[_index].gameObject.SetActive(true);
	}

	public void Close_PopUp()
	{
		Click_CloseCurrnetPopUp();
	}

	public void Set_CurrentPopUpIndex(int _index)
	{
		currentPopUpIndex = _index;
	}

	public void ReSettin_GuidePopText(int _index)
	{
		switch(_index)
		{
			case 0:
				guidePopText.text = "내 턴\r\n입나다.";
				break;
			case 1:
				guidePopText.text = "상대 턴\r\n입나다.";
				break;
		}
	}

}
