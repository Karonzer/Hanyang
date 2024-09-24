using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BGSC : GenericSingletonClass<BGSC>
{
	public IGet_BattleContentController get_BattleContentController;
}

public interface IGet_BattleContentController
{
	public void Start_GameStart();
	public void Click_MyCharcterObject(int _index);
	public void FuntionClick_BackToMain();
	public IGet_BattleSpriteController Get_BattleSpriteController();
	public int Get_CurrentSelectEnemyIndex();
	public void Set_CurrentSelectEnemyIndex(int _index);
	public void Initialize_bCurrentEnemyAlivel(int _index);
	public void Open_PopUp(int _index);
	public void Start_CharcterBattle();
	public bool Get_bCheckCharcterAction(int _index);
	public bool[] Get_bCurrentEnemyAlivel();
	public void FunctionClick_RecoverCurrentHealth();
	public void FunctionClick_DefenseState();
	public void Set_CurrentPopUpIndex(int _index);
	public void FunctionChange_TurnOver();
	public void FuntionStatsCnage_bCurrentEnemyAlivel(int _index);

}

public class BattleContentController : MonoBehaviour, IGet_BattleContentController
{
	[SerializeField] private Transform bgsCanvas;
	[SerializeField] private Transform charctersCanvas;
	[SerializeField] private Transform effectCanvas;
	[SerializeField] private Transform uiCanvas;
	[SerializeField] private Transform popUpCanvas;

	private IGet_BattleBGController get_BattleBGController;
	private IGet_BattleCharctreController get_BattleCharctreController;
	private IGet_BattleEffecController get_BattleEffecController;
	private IGet_BattleUiController get_BattleUiController;
	private IGet_BattlePopUpController get_BattlePopUpController;

	private IGet_BattleSpriteController get_BattleSpriteController;

	[SerializeField] private int currentCharcterIndex;
	[SerializeField] private bool[] currentCharcterAlivel;
	[SerializeField] private bool[] bCheckCharcterAction;

	[SerializeField] private int currentSelectEnemyIndex;
	[SerializeField] private bool[] bCurrentEnemyAlivel;

	[SerializeField] private bool bcurrentMyTurn;

	private void Awake()
	{
		BGSC.Instance.get_BattleContentController = this;
		Initialize_Canvas();
		Initialize_interface();
		Initialize_Value();
	}

	private void OnEnable()
	{
		Setting_Canvas();

		bcurrentMyTurn = true;
	}

	private void Start()
	{

	}

	private void Initialize_Canvas()
	{
		bgsCanvas = transform.Find("BGCanvas");
		charctersCanvas = transform.Find("CharctersCanvas");
		effectCanvas = transform.Find("EffectCanvas");
		uiCanvas = transform.Find("UICanvas");
		popUpCanvas = transform.Find("PopUpCanvas");
	}

	private void Setting_Canvas()
	{
		bgsCanvas.gameObject.SetActive(true);
		charctersCanvas.gameObject.SetActive(true);
		effectCanvas.gameObject.SetActive(true);
		uiCanvas.gameObject.SetActive(true);
		popUpCanvas.gameObject.SetActive(true);
	}

	private void Initialize_interface()
	{
		get_BattleBGController = bgsCanvas.GetComponent<IGet_BattleBGController>();
		get_BattleCharctreController = charctersCanvas.GetComponent<IGet_BattleCharctreController>();
		get_BattleEffecController = effectCanvas.GetComponent<IGet_BattleEffecController>();
		get_BattleUiController = uiCanvas.GetComponent<IGet_BattleUiController>();
		get_BattlePopUpController = popUpCanvas.GetComponent<IGet_BattlePopUpController>();

		get_BattleSpriteController = transform.GetComponent<IGet_BattleSpriteController>();
	}

	private void Initialize_Value()
	{
		currentCharcterAlivel = new bool[3];
		bCheckCharcterAction = new bool[3];
		for (int i = 0; i < currentCharcterAlivel.Length;i++)
		{
			currentCharcterAlivel[i] = true;
			bCheckCharcterAction[i] = false;
		}
	}

	public void Start_GameStart()
	{

	}

	public void Click_MyCharcterObject(int _index)
	{
		currentCharcterIndex = _index;
		get_BattleCharctreController.Change_myCharcterOutLine();
		get_BattleUiController.OnOff_BottomBar(true);
	}


	public void FuntionClick_BackToMain()
	{
		Loding.LoadScene("Main");
		Resources.UnloadUnusedAssets();
		System.GC.Collect();
	}

	public IGet_BattleSpriteController Get_BattleSpriteController()
	{ return get_BattleSpriteController; }

	public int Get_CurrentSelectEnemyIndex()
	{
		return currentSelectEnemyIndex;
	}

	public void Set_CurrentSelectEnemyIndex(int _index)
	{
		currentSelectEnemyIndex = _index;
	}

	public void Open_PopUp(int _index)
	{
		get_BattlePopUpController.Open_PopUp(_index);
	}

	public void Initialize_bCurrentEnemyAlivel(int _index)
	{
		bCurrentEnemyAlivel = new bool[_index];
		for (int i = 0; i < bCurrentEnemyAlivel.Length; i++)
		{
			bCurrentEnemyAlivel[i] = true;
		}
	}

	public void Start_CharcterBattle()
	{
		get_BattleUiController.OnOff_BottomBar(false);
		get_BattleEffecController.Get_DontClick().gameObject.SetActive(true);
		get_BattleCharctreController.get_BattlePlayerCharcter(currentCharcterIndex).Get_icon().gameObject.SetActive(true);
		get_BattleCharctreController.get_BattlePlayerCharcter(currentCharcterIndex).Get_icon().sprite = get_BattleSpriteController.Get_IconImage(0);
		StartCoroutine("Start_CharcterBattleDelay");
		Debug.Log("Start_CharcterBattle");
	}

	IEnumerator Start_CharcterBattleDelay()
	{
		yield return new WaitForSeconds(0.5f);
		get_BattleCharctreController.get_BattlePlayerCharcter(currentCharcterIndex).Get_myCharacter().SetParent(get_BattleEffecController.Get_AttackArea());
		yield return new WaitForEndOfFrame();

		float moveSpeed = 300.0f;

		//해당 몬스터 위치 이동
		while(Vector3.Distance(get_BattleCharctreController.get_BattlePlayerCharcter(currentCharcterIndex).Get_myCharacter().position, get_BattleCharctreController.get_BattleEnemy(currentSelectEnemyIndex).Get_AttackPos().position) > 0.001f)
		{
			get_BattleCharctreController.get_BattlePlayerCharcter(currentCharcterIndex).Get_myCharacter().position = 
				Vector3.MoveTowards(get_BattleCharctreController.get_BattlePlayerCharcter(currentCharcterIndex).Get_myCharacter().position, 
				get_BattleCharctreController.get_BattleEnemy(currentSelectEnemyIndex).Get_AttackPos().position,
				moveSpeed * Time.deltaTime);
			yield return new WaitForSeconds(0.001f);
		}

		Debug.Log("도착");
		float blinkSpeed = 7.5f;
		bool increasing = true;
		float alpha = 1;
		bool stop = true;
		while (stop)
		{
			if (increasing)
			{
				alpha -= Time.deltaTime * blinkSpeed;
				if (alpha <= 0f)
				{
					alpha = 0f;
					increasing = false; 
				}
			}
			else
			{
				alpha += Time.deltaTime * blinkSpeed;
				if (alpha >= 1f)
				{
					alpha = 1f;
					increasing = true;
					stop = false;
				}
			}

			get_BattleCharctreController.get_BattleEnemy(currentSelectEnemyIndex).Get_enemyImage().color = new Color(1, alpha, alpha, 1);
			yield return new WaitForEndOfFrame();
		}
		get_BattleCharctreController.get_BattleEnemy(currentSelectEnemyIndex).Calculation_AttackDamages(get_BattleCharctreController.get_BattlePlayerCharcter(currentSelectEnemyIndex).Get_AttackDamages());
		yield return new WaitForEndOfFrame();

		//원 위치 이동
		while (Vector3.Distance(get_BattleCharctreController.get_BattlePlayerCharcter(currentCharcterIndex).Get_myCharacter().position, get_BattleCharctreController.get_BattlePlayerCharcter(currentCharcterIndex).Get_CharcterPos().position) > 0.001f)
		{
			get_BattleCharctreController.get_BattlePlayerCharcter(currentCharcterIndex).Get_myCharacter().position =
				Vector3.MoveTowards(get_BattleCharctreController.get_BattlePlayerCharcter(currentCharcterIndex).Get_myCharacter().position,
				get_BattleCharctreController.get_BattlePlayerCharcter(currentCharcterIndex).Get_CharcterPos().position,
				moveSpeed * Time.deltaTime);
			yield return new WaitForSeconds(0.001f);
		}

		Debug.Log("원 위치 도착");
		yield return new WaitForSeconds(1f);
		get_BattleCharctreController.get_BattlePlayerCharcter(currentCharcterIndex).ReSetting_myCharacter();
		get_BattleCharctreController.Change_myCharcterOutLine();
		get_BattleEffecController.Get_DontClick().gameObject.SetActive(false);
		Check_bCheckCharcterAction();
		yield return new WaitForEndOfFrame();
	}


	public bool Get_bCheckCharcterAction(int _index)
	{
		return bCheckCharcterAction[_index];
	}

	public void Check_bCheckCharcterAction()
	{
		bCheckCharcterAction[currentCharcterIndex] = true;
		Check_AllbCheckCharcterAction();
	}

	public void Check_AllbCheckCharcterAction()
	{
		bool hasFalse = bCheckCharcterAction.Any(value => value == false);

		if(hasFalse)
		{
			Debug.Log("아직 행동은 하지 않은 용병이 있습니다");
			return;
		}
		else
		{
			Debug.Log("전부 행동 하였습니다");
			get_BattleUiController.Get_TurnOver().interactable = true;
		}
	}

	public void FunctionClick_RecoverCurrentHealth()
	{
		get_BattleCharctreController.get_BattlePlayerCharcter(currentCharcterIndex).Recover_CurrentHealth();
		get_BattleCharctreController.get_BattlePlayerCharcter(currentCharcterIndex).Get_icon().gameObject.SetActive(true);
		get_BattleCharctreController.get_BattlePlayerCharcter(currentCharcterIndex).Get_icon().sprite = get_BattleSpriteController.Get_IconImage(2);
		get_BattleCharctreController.Change_myCharcterOutLine();
		get_BattleUiController.OnOff_BottomBar(false);
		Check_bCheckCharcterAction();
	}

	public void FunctionClick_DefenseState()
	{
		get_BattleCharctreController.get_BattlePlayerCharcter(currentCharcterIndex).Set_bDefenseState(true);
		get_BattleCharctreController.get_BattlePlayerCharcter(currentCharcterIndex).Get_icon().gameObject.SetActive(true);
		get_BattleCharctreController.get_BattlePlayerCharcter(currentCharcterIndex).Get_icon().sprite = get_BattleSpriteController.Get_IconImage(1);
		get_BattleCharctreController.Change_myCharcterOutLine();
		get_BattleUiController.OnOff_BottomBar(false);
		Check_bCheckCharcterAction();
	}


	public void FunctionChange_TurnOver()
	{
		if (bcurrentMyTurn)
		{
			//상대 턴으로
			bcurrentMyTurn = false;
			get_BattleUiController.ReSetting_TurnText(1);
			get_BattlePopUpController.ReSettin_GuidePopText(1);
			StartCoroutine("Opne_GuidePopUp");
		}
		else
		{
			//내턴으로
			bcurrentMyTurn = true;
			get_BattleUiController.ReSetting_TurnText(0);
			get_BattlePopUpController.ReSettin_GuidePopText(0);

			for(int i = 0; i < 3;i++)
			{
				get_BattleCharctreController.get_BattlePlayerCharcter(i).Get_icon().gameObject.SetActive(true);
				get_BattleCharctreController.get_BattlePlayerCharcter(i).Set_bDefenseState(false);
			}
			StartCoroutine("Opne_GuidePopUp");
		}
	}

	IEnumerator Opne_GuidePopUp()
	{
		get_BattlePopUpController.Open_PopUp(6);
		yield return new WaitForSeconds(1.5f);
		get_BattlePopUpController.Close_PopUp();
	}

	//get,set
	public void Set_CurrentPopUpIndex(int _index)
	{
		get_BattlePopUpController.Set_CurrentPopUpIndex(_index);
	}

	public bool[] Get_bCurrentEnemyAlivel()
	{
		return bCurrentEnemyAlivel;
	}

	public void FuntionStatsCnage_bCurrentEnemyAlivel(int _index)
	{
		bCurrentEnemyAlivel[_index] = false;

		bool hasFalse = bCurrentEnemyAlivel.Any(value => value == true);

		if (hasFalse)
		{
			Debug.Log("아직 모든 몬스터를 해치우지 못했습니다");
			return;
		}
		else
		{
			Debug.Log("모든 몬스터를 해치웠습니다");
		}
	}

}
