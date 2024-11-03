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
		public float time;
		public int gold;
		public int probability;
	}

	[System.Serializable]
	public class DispatchData
	{
		public List<DispatchList> dispatch;
	}
	[SerializeField] private List<DispatchData> dispatchData;

	[SerializeField] private Transform mainPopUp;
	[SerializeField] private Transform loding;
	[SerializeField] private Transform ResultPopUp;

	[SerializeField] private Transform[] dispatchPopUps;
	[SerializeField] private int currentSelectDispatchIndex;
	[SerializeField] private int currentDispatchPopUpsIndex;
	[SerializeField] private Transform dispatchGuidePopUps;

	[SerializeField] private Transform levelUpPopUp;
	[SerializeField] private Text levelUpPopUptext;
	[SerializeField] private Transform lackGuidePopUp;

	public SpriteAtlas spriteAtlas;

	[SerializeField] private Image lodingProgressBar;
	[SerializeField] private float duration;

	[SerializeField] private Text resultPopUpText01;
	[SerializeField] private Text resultPopUpText02;
	[SerializeField] private Text resultPopUpText03;

	[SerializeField] private Button[] dispatchBtns;
	[SerializeField] private Button levelBtn;

	[SerializeField] private string[] sispatchXpValues;

	private void Awake()
	{
		dispatchData = new List<DispatchData>();
		Initialize_LoadDispatchData_0();
		Initialize_LoadDispatchData_1();
		Initialize_LoadDispatchData_2();
		Initialize_LoadDispatchData_3();
		Initialize_MainPopUp();
		Initialize_DispatchPopUps();
		Initialize_LodingProgressBar();
		Initialize_ResultPopUp();
		Initialize_UIBtn();
		Load_DispatchXpValue();
	}

	private void OnEnable()
	{
		currentSelectDispatchIndex = 0;
		Setting_MainPopUp();
		Setting_DispatchPopUps();
		Setting_LodingProgressBar();
		Setting_UIBtn();
	}

	private void Initialize_LoadDispatchData_0()
	{
		TextAsset jsonFile = Resources.Load<TextAsset>("Dispatch_0");

		if (jsonFile != null)
		{
			DispatchData dispatchData_0 = JsonUtility.FromJson<DispatchData>(jsonFile.text);
			dispatchData.Add(dispatchData_0);
		}
		else
		{
			Debug.LogError("Failed to load characters_data.json from Resources folder.");
		}
	}

	private void Initialize_LoadDispatchData_1()
	{
		TextAsset jsonFile = Resources.Load<TextAsset>("Dispatch_1");

		if (jsonFile != null)
		{
			DispatchData dispatchData_1 = JsonUtility.FromJson<DispatchData>(jsonFile.text);
			dispatchData.Add(dispatchData_1);
		}
		else
		{
			Debug.LogError("Failed to load characters_data.json from Resources folder.");
		}
	}

	private void Initialize_LoadDispatchData_2()
	{
		TextAsset jsonFile = Resources.Load<TextAsset>("Dispatch_2");

		if (jsonFile != null)
		{
			DispatchData dispatchData_2 = JsonUtility.FromJson<DispatchData>(jsonFile.text);
			dispatchData.Add(dispatchData_2);
		}
		else
		{
			Debug.LogError("Failed to load characters_data.json from Resources folder.");
		}
	}

	private void Initialize_LoadDispatchData_3()
	{
		TextAsset jsonFile = Resources.Load<TextAsset>("Dispatch_3");

		if (jsonFile != null)
		{
			DispatchData dispatchData_3 = JsonUtility.FromJson<DispatchData>(jsonFile.text);
			dispatchData.Add(dispatchData_3);
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
		Initialize_dispatchPopUps1TextAndBtn();
		Initialize_dispatchPopUps2TextAndBtn();
		Initialize_dispatchPopUps3TextAndBtn();
		Initialize_LevelUpPopUpAndLackGuidePopUp();
	}

	private void Initialize_dispatchPopUps0TextAndBtn()
	{
		Transform pos = dispatchPopUps[0].GetChild(0).GetChild(0);
		for(int i = 0; i < pos.childCount;i++)
		{
			int index = i;
			Button button = pos.GetChild(index).GetComponent<Button>();
			button.onClick.AddListener(() => Click_dispatchPopUps0Btn(index));

			pos.GetChild(index).GetChild(0).GetComponent<Image>().sprite = spriteAtlas.GetSprite(dispatchData[0].dispatch[index].id);
			pos.GetChild(index).GetChild(1).GetComponent<Text>().text = dispatchData[0].dispatch[index].name;
			pos.GetChild(index).GetChild(2).GetComponent<Text>().text = "소요 시간 : " + dispatchData[0].dispatch[index].time + "초";
			pos.GetChild(index).GetChild(3).GetComponent<Text>().text = "획득 골드 : " + dispatchData[0].dispatch[index].gold;
			pos.GetChild(index).GetChild(4).GetComponent<Text>().text = "실패 확률 : " + dispatchData[0].dispatch[index].probability + "%";
		}
	}

	private void Initialize_dispatchPopUps1TextAndBtn()
	{
		Transform pos = dispatchPopUps[1].GetChild(0); ;
		for (int i = 0; i < pos.childCount; i++)
		{
			int index = i;
			Button button = pos.GetChild(index).GetComponent<Button>();
			button.onClick.AddListener(() => Click_dispatchPopUps0Btn(index));

			pos.GetChild(index).GetChild(0).GetComponent<Image>().sprite = spriteAtlas.GetSprite(dispatchData[1].dispatch[index].id);
			pos.GetChild(index).GetChild(1).GetComponent<Text>().text = dispatchData[1].dispatch[index].name;
			pos.GetChild(index).GetChild(2).GetComponent<Text>().text = "소요 시간 : " + dispatchData[1].dispatch[index].time + "초";
			pos.GetChild(index).GetChild(3).GetComponent<Text>().text = "획득 골드 : " + dispatchData[1].dispatch[index].gold;
			pos.GetChild(index).GetChild(4).GetComponent<Text>().text = "실패 확률 : " + dispatchData[1].dispatch[index].probability + "%";
		}
	}

	private void Initialize_dispatchPopUps2TextAndBtn()
	{
		Transform pos = dispatchPopUps[2].GetChild(0);
		for (int i = 0; i < pos.childCount; i++)
		{
			int index = i;
			Button button = pos.GetChild(index).GetComponent<Button>();
			button.onClick.AddListener(() => Click_dispatchPopUps0Btn(index));

			pos.GetChild(index).GetChild(0).GetComponent<Image>().sprite = spriteAtlas.GetSprite(dispatchData[2].dispatch[index].id);
			pos.GetChild(index).GetChild(1).GetComponent<Text>().text = dispatchData[2].dispatch[index].name;
			pos.GetChild(index).GetChild(2).GetComponent<Text>().text = "소요 시간 : " + dispatchData[2].dispatch[index].time + "초";
			pos.GetChild(index).GetChild(3).GetComponent<Text>().text = "획득 골드 : " + dispatchData[2].dispatch[index].gold;
			pos.GetChild(index).GetChild(4).GetComponent<Text>().text = "실패 확률 : " + dispatchData[2].dispatch[index].probability + "%";
		}
	}

	private void Initialize_dispatchPopUps3TextAndBtn()
	{
		Transform pos = dispatchPopUps[3].GetChild(0);
		for (int i = 0; i < pos.childCount; i++)
		{
			int index = i;
			Button button = pos.GetChild(index).GetComponent<Button>();
			button.onClick.AddListener(() => Click_dispatchPopUps0Btn(index));

			pos.GetChild(index).GetChild(0).GetComponent<Image>().sprite = spriteAtlas.GetSprite(dispatchData[3].dispatch[index].id);
			pos.GetChild(index).GetChild(1).GetComponent<Text>().text = dispatchData[3].dispatch[index].name;
			pos.GetChild(index).GetChild(2).GetComponent<Text>().text = "소요 시간 : " + dispatchData[3].dispatch[index].time + "초";
			pos.GetChild(index).GetChild(3).GetComponent<Text>().text = "획득 골드 : " + dispatchData[3].dispatch[index].gold;
			pos.GetChild(index).GetChild(4).GetComponent<Text>().text = "실패 확률 : " + dispatchData[3].dispatch[index].probability + "%";
		}
	}

	private void Click_dispatchPopUps0Btn(int _index)
	{
		SoundController.Instance.PlaySound_Effect(0);
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
		levelUpPopUp.gameObject.SetActive(false);
		lackGuidePopUp.gameObject.SetActive(false);
	}

	private void Initialize_UIBtn()
	{
		Transform pos = mainPopUp.GetChild(0);

		Button close = pos.Find("Close").GetComponent<Button>();
		close.onClick.AddListener(() => Clikc_CloseBtn());

		dispatchBtns = new Button[pos.Find("DispatchBtn").childCount];
		Transform count = pos.Find("DispatchBtn") ;
		for (int i = 0; i < dispatchBtns.Length; i++)
		{
			int index = i;
			dispatchBtns[i] = count.GetChild(i).GetComponent<Button>();
			dispatchBtns[i].onClick.AddListener(() => Click_DispatchBtns(index));
		}

		levelBtn = pos.Find("LevelUp").GetComponent<Button>();
		levelBtn.onClick.AddListener(() => Click_DispatchLevelUp());

	}

	private void Setting_UIBtn()
	{
		for(int i = 0; i < dispatchBtns.Length;i++)
		{
			if(i <= DataBase.Instance.Get_DispatchLevel())
			{
				dispatchBtns[i].interactable = true;
			}
			else
			{
				dispatchBtns[i].interactable = false;
			}
		}

		if (DataBase.Instance.Get_DispatchLevel() >= 3)
		{
			levelBtn.gameObject.SetActive(false);
		}
	}

	private void Click_DispatchBtns(int _index)
	{
		if(currentSelectDispatchIndex == _index)
		{ return; }
		SoundController.Instance.PlaySound_Effect(0);
		dispatchPopUps[currentSelectDispatchIndex].gameObject.SetActive(false);
		currentSelectDispatchIndex = _index;
		dispatchPopUps[currentSelectDispatchIndex].gameObject.SetActive(true);
	}

	private void Click_DispatchLevelUp()
	{
		SoundController.Instance.PlaySound_Effect(0);
		levelUpPopUp.gameObject.SetActive(true);
		levelUpPopUptext.text = "해당 " + sispatchXpValues[DataBase.Instance.Get_DispatchLevel()] + " 골드를 지불하여\r\n파견소를 업그레이드를 하겠습니까?";
	}

	private void Initialize_DispatchGuidePopUps()
	{
		dispatchGuidePopUps = mainPopUp.Find("GuidePopUp");

		Button ok = dispatchGuidePopUps.GetChild(0).Find("Btn").GetChild(0).GetComponent<Button>();
		ok.onClick.AddListener(() => Click_DispatchGuidePopUpsOKNOBtn(true));
		Button no = dispatchGuidePopUps.GetChild(0).Find("Btn").GetChild(1).GetComponent<Button>();
		no.onClick.AddListener(() => Click_DispatchGuidePopUpsOKNOBtn(false));
	}	

	private void Initialize_LevelUpPopUpAndLackGuidePopUp()
	{
		levelUpPopUp = mainPopUp.Find("LevelUpPopUp");

		levelUpPopUptext = levelUpPopUp.GetChild(0).GetChild(0).GetComponent<Text>();

		Button ok = levelUpPopUp.GetChild(0).GetChild(1).GetChild(0).GetComponent<Button>();
		ok.onClick.AddListener(() => Click_LevelUpPopUpOkBtn());
		Button No = levelUpPopUp.GetChild(0).GetChild(1).GetChild(1).GetComponent<Button>();
		No.onClick.AddListener(() => Click_LevelUpPopUpNoBtn());

		lackGuidePopUp = mainPopUp.Find("LackGuidePopUp");
	}

	private void Click_LevelUpPopUpOkBtn()
	{
		SoundController.Instance.PlaySound_Effect(0);
		if (DataBase.Instance.Get_CurrentGold() >= int.Parse(sispatchXpValues[DataBase.Instance.Get_DispatchLevel()]))
		{
			DataBase.Instance.Funtion_RemoveGold(int.Parse(sispatchXpValues[DataBase.Instance.Get_DispatchLevel()]));
			DataBase.Instance.Funtion_AddDispatchLevel();
			if (DataBase.Instance.Get_DispatchLevel() >= 3)
			{
				levelBtn.gameObject.SetActive(false);
			}
			levelUpPopUp.gameObject.SetActive(false);
			Setting_UIBtn();
			MGSC.Instance.get_MainController.Funtion_SettingGoldText();
		}
        else
        {
			levelUpPopUp.gameObject.SetActive(false);
			lackGuidePopUp.gameObject.SetActive(true);
			StartCoroutine("Daley_lackGuidePopUpClose");
		}
	}

	IEnumerator Daley_lackGuidePopUpClose()
	{
		yield return new WaitForSeconds(2);
		lackGuidePopUp.gameObject.SetActive(false);
	}

	private void Click_LevelUpPopUpNoBtn()
	{
		SoundController.Instance.PlaySound_Effect(0);
		levelUpPopUp.gameObject.SetActive(false);
	}

	private void Click_DispatchGuidePopUpsOKNOBtn(bool _bool)
	{
		SoundController.Instance.PlaySound_Effect(0);
		if (_bool)
		{
			mainPopUp.gameObject.SetActive(false);
			dispatchGuidePopUps.gameObject.SetActive(false);
			loding.gameObject.SetActive(true);
			StartFilling(dispatchData[currentSelectDispatchIndex].dispatch[currentDispatchPopUpsIndex].time);
		}
		else
		{
			dispatchGuidePopUps.gameObject.SetActive(false);
		}
	}

	public void StartFilling(float time)
	{
		duration = time;
		StartCoroutine(LodingImageFillOverTime());
	}

	private IEnumerator LodingImageFillOverTime()
	{
		float elapsedTime = 0f;

		lodingProgressBar.fillAmount = 0f;

		while (elapsedTime < duration)
		{
			elapsedTime += Time.deltaTime;
			lodingProgressBar.fillAmount = Mathf.Clamp01(elapsedTime / duration);
			yield return null;
		}

		lodingProgressBar.fillAmount = 1f;
		loding.gameObject.SetActive(false);
		ResultPopUp.gameObject.SetActive(true);

		int randomValue = Random.Range(0, 100);
		if(randomValue > dispatchData[currentSelectDispatchIndex].dispatch[currentDispatchPopUpsIndex].probability)
		{
			Debug.Log("파견 성공");
			resultPopUpText01.text = "파견 성공";
			resultPopUpText02.text = "용병단이 해당 파견을 \r\n성공하였습니다";
			resultPopUpText03.text = "획득 골드 : " + dispatchData[currentSelectDispatchIndex].dispatch[currentDispatchPopUpsIndex].gold.ToString();
			DataBase.Instance.Funtion_AddGold(dispatchData[currentSelectDispatchIndex].dispatch[currentDispatchPopUpsIndex].gold);
			MGSC.Instance.get_MainController.Funtion_SettingGoldText();
		}
		else
		{
			Debug.Log("파견 실패");
			resultPopUpText01.text = "파견 실패";
			resultPopUpText02.text = "용병단이 해당 파견을 \r\n실패하였습니다";
			resultPopUpText03.text = "획득 골드 : " + 0;
		}
	}

	private void Clikc_CloseBtn()
	{
		SoundController.Instance.PlaySound_Effect(0);
		transform.gameObject.SetActive(false);
	}

	private void Initialize_LodingProgressBar()
	{
		lodingProgressBar = loding.GetChild(0).GetChild(0).GetComponent<Image>();
	}

	private void Setting_LodingProgressBar()
	{
		lodingProgressBar.fillAmount = 0;
	}

	private void Initialize_ResultPopUp()
	{
		Button ok = ResultPopUp.GetChild(0).Find("Ok").GetComponent<Button>();
		ok.onClick.AddListener(Click_ResultPopUpClose);

		resultPopUpText01 = ResultPopUp.GetChild(0).GetChild(0).GetComponent<Text>();
		resultPopUpText02 = ResultPopUp.GetChild(0).GetChild(1).GetComponent<Text>();
		resultPopUpText03 = ResultPopUp.GetChild(0).GetChild(2).GetComponent<Text>();
	}


	private void Click_ResultPopUpClose()
	{
		SoundController.Instance.PlaySound_Effect(0);
		ResultPopUp.gameObject.SetActive(false);
		mainPopUp.gameObject.SetActive(true);
	}

	private void Load_DispatchXpValue()
	{
		TextAsset file = Resources.Load<TextAsset>("DispatchXpValue");
		sispatchXpValues = file.text.Split('\n');
	}


}
