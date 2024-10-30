using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class MainTrainingSelectPopUp : MonoBehaviour
{

	[System.Serializable]
	public class TraininghList
	{
		public string id;
		public string name;
		public int stats;
		public float time;
		public int gold;
		public int probability;
	}

	[System.Serializable]
	public class TrainingData
	{
		public List<TraininghList> training;
	}

	[SerializeField] private List<TrainingData> trainingData;

	[SerializeField] private Transform mainPopUp;
	[SerializeField] private Transform loding;
	[SerializeField] private Transform ResultPopUp;

	[SerializeField] private Transform[] TrainingPopUps;
	[SerializeField] private int currentSelectTraingingPopUpIndex;
	[SerializeField] private int currentTraingingSelcetIndex;

	[SerializeField] private Transform selectPopUp;
	[SerializeField] private int currentTraingingPlayerSelcetIndex;

	[SerializeField] private Transform trainingGuidePopUps;

	[SerializeField] private Transform trainingLackGuidePopUp;

	[SerializeField] private Transform levelUpPopUp;
	[SerializeField] private Text levelUpPopUptext;
	[SerializeField] private Transform lackGuidePopUp;

	[SerializeField] private Image lodingProgressBar;
	[SerializeField] private Image lodingBg;
	[SerializeField] private float duration;

	[SerializeField] private Image resultPopUpPlayerBG;
	[SerializeField] private Text resultPopUpText01;
	[SerializeField] private Text resultPopUpText02;

	[SerializeField] private Button[] TrainingBtns;
	[SerializeField] private Button levelBtn;

	[SerializeField] private string[] sispatchXpValues;

	private void Awake()
	{
		trainingData = new List<TrainingData>();
		Initialize_LoadTrainingData_0();
		Initialize_LoadTrainingData_1();
		Initialize_LoadTrainingData_2();
		Initialize_LoadTrainingData_3();
		Initialize_MainPopUp();
		Initialize_TrainingPopUps();
		Initialize_SelectPopUp();
		Initialize_LodingProgressBar();
		Initialize_ResultPopUp();
		Initialize_UIBtn();
		Load_DispatchXpValue();
	}

	private void OnEnable()
	{
		currentSelectTraingingPopUpIndex = 0;
		Setting_MainPopUp();
		Setting_TrainingPopUps();
		Setting_SelectPopUp();
		Setting_LodingProgressBar();
		Setting_UIBtn();
	}

	private void Initialize_LoadTrainingData_0()
	{
		TextAsset jsonFile = Resources.Load<TextAsset>("Training_0");

		if (jsonFile != null)
		{
			TrainingData trainginghData_0 = JsonUtility.FromJson<TrainingData>(jsonFile.text);
			trainingData.Add(trainginghData_0);
		}
		else
		{
			Debug.LogError("Failed to load characters_data.json from Resources folder.");
		}
	}

	private void Initialize_LoadTrainingData_1()
	{
		TextAsset jsonFile = Resources.Load<TextAsset>("Training_1");

		if (jsonFile != null)
		{
			TrainingData trainginghData_1 = JsonUtility.FromJson<TrainingData>(jsonFile.text);
			trainingData.Add(trainginghData_1);
		}
		else
		{
			Debug.LogError("Failed to load characters_data.json from Resources folder.");
		}
	}

	private void Initialize_LoadTrainingData_2()
	{
		TextAsset jsonFile = Resources.Load<TextAsset>("Training_2");

		if (jsonFile != null)
		{
			TrainingData trainginghData_2 = JsonUtility.FromJson<TrainingData>(jsonFile.text);
			trainingData.Add(trainginghData_2);
		}
		else
		{
			Debug.LogError("Failed to load characters_data.json from Resources folder.");
		}
	}

	private void Initialize_LoadTrainingData_3()
	{
		TextAsset jsonFile = Resources.Load<TextAsset>("Training_3");

		if (jsonFile != null)
		{
			TrainingData trainginghData_3 = JsonUtility.FromJson<TrainingData>(jsonFile.text);
			trainingData.Add(trainginghData_3);
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

	private void Initialize_TrainingPopUps()
	{
		TrainingPopUps = new Transform[mainPopUp.GetChild(1).childCount];
		for (int i = 0; i < TrainingPopUps.Length; i++)
		{
			TrainingPopUps[i] = mainPopUp.GetChild(1).GetChild(i);
		}

		Initialize_TrainingGuidePopUps();
		Initialize_TrainingPopUps0TextAndBtn();
		Initialize_TrainingPopUps1TextAndBtn();
		Initialize_TrainingPopUps2TextAndBtn();
		Initialize_TrainingPopUps3TextAndBtn();
		Initialize_LevelUpPopUpAndLackGuidePopUp();
		Initialize_TrainingLackGuidePopUp();
	}

	private void Initialize_TrainingPopUps0TextAndBtn()
	{
		Transform pos = TrainingPopUps[0].GetChild(0);
		for (int i = 0; i < pos.childCount; i++)
		{
			int index = i;
			Button button = pos.GetChild(index).GetComponent<Button>();
			button.onClick.AddListener(() => Click_TrainingPopUps0Btn(index));

			pos.GetChild(index).GetChild(1).GetComponent<Text>().text = trainingData[0].training[index].name;
			pos.GetChild(index).GetChild(2).GetComponent<Text>().text = "획득 : " + trainingData[0].training[index].stats + "초";
			pos.GetChild(index).GetChild(3).GetComponent<Text>().text = "소요 시간 : " + trainingData[0].training[index].time + "초";
			pos.GetChild(index).GetChild(4).GetComponent<Text>().text = "비용 : " + trainingData[0].training[index].gold;
			pos.GetChild(index).GetChild(5).GetComponent<Text>().text = "실패 확률 : " + trainingData[0].training[index].probability + "%";
		}
	}

	private void Initialize_TrainingPopUps1TextAndBtn()
	{
		Transform pos = TrainingPopUps[1].GetChild(0);
		for (int i = 0; i < pos.childCount; i++)
		{
			int index = i;
			Button button = pos.GetChild(index).GetComponent<Button>();
			button.onClick.AddListener(() => Click_TrainingPopUps0Btn(index));

			pos.GetChild(index).GetChild(1).GetComponent<Text>().text = trainingData[1].training[index].name;
			pos.GetChild(index).GetChild(2).GetComponent<Text>().text = "획득 : " + trainingData[1].training[index].stats + "초";
			pos.GetChild(index).GetChild(3).GetComponent<Text>().text = "소요 시간 : " + trainingData[1].training[index].time + "초";
			pos.GetChild(index).GetChild(4).GetComponent<Text>().text = "비용 : " + trainingData[1].training[index].gold;
			pos.GetChild(index).GetChild(5).GetComponent<Text>().text = "실패 확률 : " + trainingData[1].training[index].probability + "%";
		}
	}

	private void Initialize_TrainingPopUps2TextAndBtn()
	{
		Transform pos = TrainingPopUps[2].GetChild(0);
		for (int i = 0; i < pos.childCount; i++)
		{
			int index = i;
			Button button = pos.GetChild(index).GetComponent<Button>();
			button.onClick.AddListener(() => Click_TrainingPopUps0Btn(index));

			pos.GetChild(index).GetChild(1).GetComponent<Text>().text = trainingData[2].training[index].name;
			pos.GetChild(index).GetChild(2).GetComponent<Text>().text = "획득 : " + trainingData[2].training[index].stats + "초";
			pos.GetChild(index).GetChild(3).GetComponent<Text>().text = "소요 시간 : " + trainingData[2].training[index].time + "초";
			pos.GetChild(index).GetChild(4).GetComponent<Text>().text = "비용 : " + trainingData[2].training[index].gold;
			pos.GetChild(index).GetChild(5).GetComponent<Text>().text = "실패 확률 : " + trainingData[2].training[index].probability + "%";
		}
	}

	private void Initialize_TrainingPopUps3TextAndBtn()
	{
		Transform pos = TrainingPopUps[3].GetChild(0);
		for (int i = 0; i < pos.childCount; i++)
		{
			int index = i;
			Button button = pos.GetChild(index).GetComponent<Button>();
			button.onClick.AddListener(() => Click_TrainingPopUps0Btn(index));

			pos.GetChild(index).GetChild(1).GetComponent<Text>().text = trainingData[3].training[index].name;
			pos.GetChild(index).GetChild(2).GetComponent<Text>().text = "획득 : " + trainingData[3].training[index].stats;
			pos.GetChild(index).GetChild(3).GetComponent<Text>().text = "소요 시간 : " + trainingData[3].training[index].time + "초";
			pos.GetChild(index).GetChild(4).GetComponent<Text>().text = "비용 : " + trainingData[3].training[index].gold;
			pos.GetChild(index).GetChild(5).GetComponent<Text>().text = "실패 확률 : " + trainingData[3].training[index].probability + "%";
		}
	}

	private void Click_TrainingPopUps0Btn(int _index)
	{
		if (DataBase.Instance.Get_CurrentGold() >= trainingData[currentSelectTraingingPopUpIndex].training[currentTraingingSelcetIndex].gold)
		{
			currentTraingingSelcetIndex = _index;
			selectPopUp.gameObject.SetActive(true);
			Setting_SelectPopUp();
		}
		else
		{
			trainingLackGuidePopUp.gameObject.SetActive(true);
			StartCoroutine("Daley_TrainingLackGuidePopUpClose");
		}
	}

	private void Setting_TrainingPopUps()
	{
		for (int i = 0; i < TrainingPopUps.Length; i++)
		{
			TrainingPopUps[i].gameObject.SetActive(false);
		}
		currentTraingingSelcetIndex = 0;
		currentTraingingPlayerSelcetIndex = 0;
		TrainingPopUps[currentTraingingSelcetIndex].gameObject.SetActive(true);
		selectPopUp.gameObject.SetActive(false);
		trainingGuidePopUps.gameObject.SetActive(false);
		levelUpPopUp.gameObject.SetActive(false);
		trainingLackGuidePopUp.gameObject.SetActive(false);
		lackGuidePopUp.gameObject.SetActive(false);
	}

	private void Initialize_UIBtn()
	{
		Transform pos = mainPopUp.GetChild(0);

		Button close = pos.Find("Close").GetComponent<Button>();
		close.onClick.AddListener(() => Clikc_CloseBtn());

		TrainingBtns = new Button[pos.Find("DispatchBtn").childCount];
		Transform count = pos.Find("DispatchBtn");
		for (int i = 0; i < TrainingBtns.Length; i++)
		{
			int index = i;
			TrainingBtns[i] = count.GetChild(i).GetComponent<Button>();
			TrainingBtns[i].onClick.AddListener(() => Click_DispatchBtns(index));
		}

		levelBtn = pos.Find("LevelUp").GetComponent<Button>();
		levelBtn.onClick.AddListener(() => Click_DispatchLevelUp());

	}

	private void Setting_UIBtn()
	{
		for (int i = 0; i < TrainingBtns.Length; i++)
		{
			if (i <= DataBase.Instance.Get_TrainingLevel())
			{
				TrainingBtns[i].interactable = true;
			}
			else
			{
				TrainingBtns[i].interactable = false;
			}
		}

		if (DataBase.Instance.Get_TrainingLevel() >= 3)
		{
			levelBtn.gameObject.SetActive(false);
		}
	}

	private void Click_DispatchBtns(int _index)
	{
		if (currentSelectTraingingPopUpIndex == _index)
		{ return; }
		TrainingPopUps[currentSelectTraingingPopUpIndex].gameObject.SetActive(false);
		currentSelectTraingingPopUpIndex = _index;
		TrainingPopUps[currentSelectTraingingPopUpIndex].gameObject.SetActive(true);
	}

	private void Click_DispatchLevelUp()
	{
		levelUpPopUp.gameObject.SetActive(true);
		levelUpPopUptext.text = "해당 " + sispatchXpValues[DataBase.Instance.Get_TrainingLevel()] + " 골드를 지불하여\r\n훈련소를 업그레이드를 하겠습니까?";
	}

	private void Initialize_SelectPopUp()
	{
		selectPopUp = mainPopUp.Find("SelectPopUp");
		for(int i = 0; i < selectPopUp.GetChild(0).GetChild(0).childCount;i++)
		{
			int index = i;
			Button button = selectPopUp.GetChild(0).GetChild(0).GetChild(index).GetComponent<Button>();
			button.onClick.AddListener(() => Click_SelectPlayerBtn(index));
		}

		Button back = selectPopUp.GetChild(0).Find("Back").GetComponent<Button>();
		back.onClick.AddListener(() => Click_SelectBackBtn());
	}	

	private void Setting_SelectPopUp()
	{
		Transform pos = selectPopUp.GetChild(0).GetChild(0);
		for (int i = 0; i < pos.childCount; i++)
		{
			int _index = i;
			pos.GetChild(_index).GetChild(0).GetComponent<Image>().sprite = MGSC.Instance.get_MainController.Get_SpriteAtlas().GetSprite(DataBase.Instance.Get_CharacterData(_index).name);
			pos.GetChild(_index).GetChild(1).GetComponent<Text>().text = "레벨 : "+ DataBase.Instance.Get_CharacterData(_index).level;
			pos.GetChild(_index).GetChild(2).GetComponent<Text>().text = "공격력 : " + DataBase.Instance.Get_CharacterData(_index).baseStats.attack +  " + " + DataBase.Instance.Get_CharacterData(_index).trainingStats.attack;
			pos.GetChild(_index).GetChild(3).GetComponent<Text>().text = "방어력 : " + DataBase.Instance.Get_CharacterData(_index).baseStats.defense + " + " + DataBase.Instance.Get_CharacterData(_index).trainingStats.defense;
			pos.GetChild(_index).GetChild(4).GetComponent<Text>().text = "체력 : " + DataBase.Instance.Get_CharacterData(_index).baseStats.health + " + " + DataBase.Instance.Get_CharacterData(_index).trainingStats.health;
		}
	}

	private void Click_SelectPlayerBtn(int _index)
	{
		currentTraingingPlayerSelcetIndex = _index;
		trainingGuidePopUps.gameObject.SetActive(true);
	}

	private void Click_SelectBackBtn()
	{
		selectPopUp.gameObject.SetActive(false);
	}

	private void Initialize_TrainingGuidePopUps()
	{
		trainingGuidePopUps = mainPopUp.Find("GuidePopUp");

		Button ok = trainingGuidePopUps.GetChild(0).Find("Btn").GetChild(0).GetComponent<Button>();
		ok.onClick.AddListener(() => Click_DispatchGuidePopUpsOKNOBtn(true));
		Button no = trainingGuidePopUps.GetChild(0).Find("Btn").GetChild(1).GetComponent<Button>();
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

	private void Initialize_TrainingLackGuidePopUp()
	{
		trainingLackGuidePopUp = mainPopUp.Find("TrainingLackGuidePopUp");
	}

	private void Click_LevelUpPopUpOkBtn()
	{
		if (DataBase.Instance.Get_CurrentGold() >= int.Parse(sispatchXpValues[DataBase.Instance.Get_TrainingLevel()]))
		{
			DataBase.Instance.Funtion_RemoveGold(int.Parse(sispatchXpValues[DataBase.Instance.Get_TrainingLevel()]));
			DataBase.Instance.Funtion_AddTrainingLevel();
			if (DataBase.Instance.Get_TrainingLevel() >= 3)
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
		levelUpPopUp.gameObject.SetActive(false);
	}

	private void Click_DispatchGuidePopUpsOKNOBtn(bool _bool)
	{
		
		if (_bool)
		{
			if (DataBase.Instance.Get_CurrentGold() >= trainingData[currentSelectTraingingPopUpIndex].training[currentTraingingSelcetIndex].gold)
			{
				DataBase.Instance.Funtion_RemoveGold(trainingData[currentSelectTraingingPopUpIndex].training[currentTraingingSelcetIndex].gold);
				MGSC.Instance.get_MainController.Funtion_SettingGoldText();
				mainPopUp.gameObject.SetActive(false);
				trainingGuidePopUps.gameObject.SetActive(false);
				selectPopUp.gameObject.SetActive(false);
				loding.gameObject.SetActive(true);
				lodingBg.sprite =  MGSC.Instance.get_MainController.Get_SpriteAtlas().GetSprite(DataBase.Instance.Get_CharacterData(currentTraingingPlayerSelcetIndex).name);
				StartFilling(trainingData[currentSelectTraingingPopUpIndex].training[currentTraingingSelcetIndex].time);
			}
			else
			{
				trainingLackGuidePopUp.gameObject.SetActive(true);
				StartCoroutine("Daley_TrainingLackGuidePopUpClose");
			}
		}
		else
		{
			trainingGuidePopUps.gameObject.SetActive(false);
		}

	}

	IEnumerator Daley_TrainingLackGuidePopUpClose()
	{
		yield return new WaitForSeconds(2);
		trainingLackGuidePopUp.gameObject.SetActive(false);
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
		resultPopUpPlayerBG.sprite = MGSC.Instance.get_MainController.Get_SpriteAtlas().GetSprite(DataBase.Instance.Get_CharacterData(currentTraingingPlayerSelcetIndex).name);
		int randomValue = Random.Range(0, 100);
		if (randomValue > trainingData[currentSelectTraingingPopUpIndex].training[currentTraingingSelcetIndex].probability)
		{
			Debug.Log("파견 성공");
			resultPopUpText01.text = "훈련 성공";
			int text = 0;
			switch (currentTraingingSelcetIndex)
			{
				case 0:
					text = DataBase.Instance.Get_CharacterData(currentTraingingPlayerSelcetIndex).trainingStats.attack + trainingData[currentSelectTraingingPopUpIndex].training[currentTraingingSelcetIndex].stats;
					resultPopUpText02.text = "공격력 : " + DataBase.Instance.Get_CharacterData(currentTraingingPlayerSelcetIndex).trainingStats.attack + " -> " + text;
					DataBase.Instance.Add_CharacterDataAttack(currentTraingingPlayerSelcetIndex, text);
					break;
				case 1:
					 text = DataBase.Instance.Get_CharacterData(currentTraingingPlayerSelcetIndex).trainingStats.defense + trainingData[currentSelectTraingingPopUpIndex].training[currentTraingingSelcetIndex].stats;
					resultPopUpText02.text = "방어력 : " + DataBase.Instance.Get_CharacterData(currentTraingingPlayerSelcetIndex).trainingStats.defense + " -> " + text;
					DataBase.Instance.Add_CharacterDataDfense(currentTraingingPlayerSelcetIndex, text);
					break;
				case 2:
					text = DataBase.Instance.Get_CharacterData(currentTraingingPlayerSelcetIndex).trainingStats.health + trainingData[currentSelectTraingingPopUpIndex].training[currentTraingingSelcetIndex].stats;
					resultPopUpText02.text = "체력 : " + DataBase.Instance.Get_CharacterData(currentTraingingPlayerSelcetIndex).trainingStats.health + " -> " + text;
					DataBase.Instance.Add_CharacterDataHealth(currentTraingingPlayerSelcetIndex, text);
					break;
			}
		}
		else
		{
			Debug.Log("파견 실패");
			resultPopUpText01.text = "훈련 실패";
			switch (currentTraingingSelcetIndex)
			{
				case 0:
					resultPopUpText02.text = "공격력 : " + DataBase.Instance.Get_CharacterData(currentTraingingPlayerSelcetIndex).trainingStats.attack + " -> " + DataBase.Instance.Get_CharacterData(currentTraingingPlayerSelcetIndex).trainingStats.attack;
					break;
				case 1:
					resultPopUpText02.text = "방어력 : " + DataBase.Instance.Get_CharacterData(currentTraingingPlayerSelcetIndex).trainingStats.attack + " -> " + DataBase.Instance.Get_CharacterData(currentTraingingPlayerSelcetIndex).trainingStats.defense;
					break;
				case 2:
					resultPopUpText02.text = "체력 : " + DataBase.Instance.Get_CharacterData(currentTraingingPlayerSelcetIndex).trainingStats.attack + " -> " + DataBase.Instance.Get_CharacterData(currentTraingingPlayerSelcetIndex).trainingStats.health;
					break;
			}
		}
	}

	private void Clikc_CloseBtn()
	{
		transform.gameObject.SetActive(false);
	}

	private void Initialize_LodingProgressBar()
	{
		lodingProgressBar = loding.GetChild(0).GetChild(0).GetComponent<Image>();
		lodingBg = loding.GetChild(1).GetComponent<Image>();
	}

	private void Setting_LodingProgressBar()
	{
		lodingProgressBar.fillAmount = 0;
		lodingBg.gameObject.SetActive(true);
	}

	private void Initialize_ResultPopUp()
	{
		Button ok = ResultPopUp.GetChild(0).Find("Ok").GetComponent<Button>();
		ok.onClick.AddListener(Click_ResultPopUpClose);

		resultPopUpPlayerBG = ResultPopUp.GetChild(0).Find("Image").GetChild(0).GetComponent<Image>();

		resultPopUpText01 = ResultPopUp.GetChild(0).Find("Text").GetChild(0).GetComponent<Text>();
		resultPopUpText02 = ResultPopUp.GetChild(0).Find("Text").GetChild(1).GetComponent<Text>();
	}

	private void Click_ResultPopUpClose()
	{
		ResultPopUp.gameObject.SetActive(false);
		mainPopUp.gameObject.SetActive(true);
	}

	private void Load_DispatchXpValue()
	{
		TextAsset file = Resources.Load<TextAsset>("DispatchXpValue");
		sispatchXpValues = file.text.Split('\n');
	}
}
