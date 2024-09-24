using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IGet_BattleUiController
{
	public void OnOff_BottomBar(bool _bool);
	public Button Get_TurnOver();
	public void ReSetting_TurnText(int _index);
}

public class BattleUiController : MonoBehaviour, IGet_BattleUiController
{
    [SerializeField] private Text turnText;
    [SerializeField] private Transform bottomBar;
    [SerializeField] private Button turnOver;
	[SerializeField] private Button backBtn;

	private void Awake()
	{
		Initialize_UI();
	}

	private void OnEnable()
	{
		Setting_UI();
	}

	private void Start()
	{
		
	}

	private void Initialize_UI()
	{
		Debug.Log("Initialize_UI");
		turnText = transform.GetChild(0).Find("Title").GetChild(0).GetComponent<Text>();
		bottomBar = transform.GetChild(0).Find("BottomBar");
		Debug.Log($"bottomBar.childCount : {bottomBar.childCount}");
		for (int i = 0; i < bottomBar.childCount;i++)
		{
			int index = i;

			Debug.Log(bottomBar.GetChild(i).name);
			bottomBar.GetChild(i).GetComponent<Button>().onClick.AddListener(() => Click_BottomBar(index));
		}
		turnOver = transform.GetChild(0).Find("TurnOver").GetComponent<Button>();
		turnOver.onClick.AddListener(() => Click_TurnOver());

		backBtn = transform.GetChild(0).Find("BackBtn").GetComponent<Button>();
		backBtn.onClick.AddListener(() => Click_BackBtn());
	}

	private void Setting_UI()
	{
		turnText.text = "내 턴";
		turnText.gameObject.SetActive(true);

		bottomBar.gameObject.SetActive(false);
		for (int i = 0; i > bottomBar.childCount; i++)
		{
			bottomBar.GetChild(i).gameObject.SetActive(true);
		}

		turnOver.gameObject.SetActive(true);
		turnOver.interactable= false;

		backBtn.gameObject.SetActive(true);
	}


	private void Click_BottomBar(int _index)
	{
		Debug.Log(_index);
		BGSC.Instance.get_BattleContentController.Open_PopUp(_index);
	}



	private void Click_TurnOver()
	{
		turnOver.interactable = false;
		BGSC.Instance.get_BattleContentController.FunctionChange_TurnOver();
	}

	private void Click_BackBtn()
	{
		BGSC.Instance.get_BattleContentController.Open_PopUp(5);
	}

	public void OnOff_BottomBar(bool _bool)
	{
		bottomBar.gameObject.SetActive(_bool);
	}

	public void ReSetting_TurnText(int _index)
	{
		switch(_index)
		{
			case 0:
				turnText.text = "내 턴";
				break;
			case 1:
				turnText.text = "상대 턴";
				break;
		}
	}

	public Button Get_TurnOver()
	{
		return turnOver;
	}


}
