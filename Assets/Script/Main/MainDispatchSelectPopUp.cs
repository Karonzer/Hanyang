using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class MainDispatchSelectPopUp : MonoBehaviour
{
	[System.Serializable]
	public class DispatchList
	{
		public string id;
		public string name;
		public int time;
		public int gold;
		public int probability;
	}

	[System.Serializable]
	public class DispatchData_0
	{
		public List<DispatchList> dispatch_0;
	}

	[SerializeField] private DispatchData_0 dispatchData_0;

	[SerializeField] private Transform mainPopUp;
	[SerializeField] private Transform loding;
	[SerializeField] private Transform ResultPopUp;

	[SerializeField] private Transform[] dispatchPopUps;
	[SerializeField] private int currentDispatchPopUpsIndex;
	[SerializeField] private Transform dispatchGuidePopUps;

	public SpriteAtlas spriteAtlas;

	private void Awake()
	{
		Initialize_LoadDispatchData_0();
		Initialize_MainPopUp();
		Initialize_DispatchPopUps();
		Initialize_UIBtn();
	}

	private void OnEnable()
	{
		Setting_MainPopUp();
		Setting_DispatchPopUps();
	}

	private void Initialize_LoadDispatchData_0()
	{
		TextAsset jsonFile = Resources.Load<TextAsset>("Dispatch_0");

		if (jsonFile != null)
		{
			dispatchData_0 = JsonUtility.FromJson<DispatchData_0>(jsonFile.text);
		}
		else
		{
			Debug.LogError("Failed to load characters_data.json from Resources folder.");
		}
	}

	private void Initialize_MainPopUp()
	{
		mainPopUp = transform.GetChild(0);
		loding = transform.GetChild(1);
		ResultPopUp = transform.GetChild(2);
	}

	private void Setting_MainPopUp()
	{
		mainPopUp.gameObject.SetActive(true);
		loding.gameObject.SetActive(false);
		ResultPopUp.gameObject.SetActive(false);
	}

	private void Initialize_DispatchPopUps()
	{
		dispatchPopUps = new Transform[mainPopUp.GetChild(1).childCount];
		for(int i = 0; i < dispatchPopUps.Length;i++)
		{
			dispatchPopUps[i] = mainPopUp.GetChild(1).GetChild(i);
		}

		Initialize_DispatchGuidePopUps();
		Initialize_dispatchPopUps0TextAndBtn();
	}

	private void Initialize_dispatchPopUps0TextAndBtn()
	{
		Transform pos = dispatchPopUps[0].GetChild(0).GetChild(0);
		for(int i = 0; i < pos.childCount;i++)
		{
			int index = i;
			Button button = pos.GetChild(index).GetComponent<Button>();
			button.onClick.AddListener(() => Click_dispatchPopUps0Btn(index));

			pos.GetChild(index).GetChild(0).GetComponent<Image>().sprite = spriteAtlas.GetSprite(dispatchData_0.dispatch_0[index].id);
			pos.GetChild(index).GetChild(1).GetComponent<Text>().text = dispatchData_0.dispatch_0[index].name;
			pos.GetChild(index).GetChild(2).GetComponent<Text>().text = "소요 시간 : " + dispatchData_0.dispatch_0[index].time + "초";
			pos.GetChild(index).GetChild(3).GetComponent<Text>().text = "획득 골드 : " + dispatchData_0.dispatch_0[index].gold;
			pos.GetChild(index).GetChild(4).GetComponent<Text>().text = "실패 확률 : " + dispatchData_0.dispatch_0[index].probability + "%";
		}
	}	

	private void Click_dispatchPopUps0Btn(int _index)
	{
		currentDispatchPopUpsIndex = _index;
		dispatchGuidePopUps.gameObject.SetActive(true);
	}

	private void Setting_DispatchPopUps()
	{
		for (int i = 0; i < dispatchPopUps.Length; i++)
		{
			dispatchPopUps[i].gameObject.SetActive(false);
		}
		currentDispatchPopUpsIndex = 0;
		dispatchPopUps[currentDispatchPopUpsIndex].gameObject.SetActive(true);
		dispatchGuidePopUps.gameObject.SetActive(false);
	}

	private void Initialize_UIBtn()
	{
		Transform pos = mainPopUp.GetChild(0);

		Button close = pos.Find("Close").GetComponent<Button>();
		close.onClick.AddListener(() => Clikc_CloseBtn());
	}

	private void Initialize_DispatchGuidePopUps()
	{
		dispatchGuidePopUps = mainPopUp.Find("GuidePopUp");

		Button ok = dispatchGuidePopUps.GetChild(0).Find("Btn").GetChild(0).GetComponent<Button>();
		ok.onClick.AddListener(() => Click_DispatchGuidePopUpsOKNOBtn(true));
		Button no = dispatchGuidePopUps.GetChild(0).Find("Btn").GetChild(1).GetComponent<Button>();
		no.onClick.AddListener(() => Click_DispatchGuidePopUpsOKNOBtn(false));
	}	

	private void Click_DispatchGuidePopUpsOKNOBtn(bool _bool)
	{
		if(_bool)
		{
			mainPopUp.gameObject.SetActive(false);
			loding.gameObject.SetActive(true);
		}
		else
		{
			dispatchGuidePopUps.gameObject.SetActive(false);
		}
	}

	private void Clikc_CloseBtn()
	{
		transform.gameObject.SetActive(false);
	}
}
