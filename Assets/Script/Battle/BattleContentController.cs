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
	public IGet_BattleSpriteController Get_ImageBattleSpriteController();
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
	[SerializeField] private bool[] bCurrentCharcterAlivel;
	[SerializeField] private bool[] bCheckCharcterAction;

	[SerializeField] private int currentSelectEnemyIndex;
	[SerializeField] private bool[] bCurrentEnemyAlivel;
	[SerializeField] private List<int> attcakAlivePlayers;
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
		bCurrentCharcterAlivel = new bool[3];
		bCheckCharcterAction = new bool[3];
		for (int i = 0; i < bCurrentCharcterAlivel.Length;i++)
		{
			bCurrentCharcterAlivel[i] = true;
			bCheckCharcterAction[i] = false;
		}

		attcakAlivePlayers = new List<int>();
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

	//ĳ���Ͱ� ���͸� �����ϴ� �Լ�
	IEnumerator Start_CharcterBattleDelay()
	{
		yield return new WaitForSeconds(0.5f);
		get_BattleCharctreController.get_BattlePlayerCharcter(currentCharcterIndex).Get_myCharacter().SetParent(get_BattleEffecController.Get_AttackArea());
		yield return new WaitForEndOfFrame();

		float moveSpeed = 350.0f;

		//�ش� ���� ��ġ �̵�
		while(Vector3.Distance(get_BattleCharctreController.get_BattlePlayerCharcter(currentCharcterIndex).Get_myCharacter().position, get_BattleCharctreController.get_BattleEnemy(currentSelectEnemyIndex).Get_AttackPos().position) > 0.001f)
		{
			get_BattleCharctreController.get_BattlePlayerCharcter(currentCharcterIndex).Get_myCharacter().position = 
				Vector3.MoveTowards(get_BattleCharctreController.get_BattlePlayerCharcter(currentCharcterIndex).Get_myCharacter().position, 
				get_BattleCharctreController.get_BattleEnemy(currentSelectEnemyIndex).Get_AttackPos().position,
				moveSpeed * Time.deltaTime);
			yield return new WaitForSeconds(0.001f);
		}

		Debug.Log("����");
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

			get_BattleCharctreController.get_BattleEnemy(currentSelectEnemyIndex).Get_EnemyImage().color = new Color(1, alpha, alpha, 1);
			yield return new WaitForEndOfFrame();
		}
		get_BattleCharctreController.get_BattleEnemy(currentSelectEnemyIndex).Calculation_AttackDamages(get_BattleCharctreController.get_BattlePlayerCharcter(currentSelectEnemyIndex).Get_AttackDamages());
		yield return new WaitForEndOfFrame();

		//�� ��ġ �̵�
		while (Vector3.Distance(get_BattleCharctreController.get_BattlePlayerCharcter(currentCharcterIndex).Get_myCharacter().position, get_BattleCharctreController.get_BattlePlayerCharcter(currentCharcterIndex).Get_CharcterPos().position) > 0.001f)
		{
			get_BattleCharctreController.get_BattlePlayerCharcter(currentCharcterIndex).Get_myCharacter().position =
				Vector3.MoveTowards(get_BattleCharctreController.get_BattlePlayerCharcter(currentCharcterIndex).Get_myCharacter().position,
				get_BattleCharctreController.get_BattlePlayerCharcter(currentCharcterIndex).Get_CharcterPos().position,
				moveSpeed * Time.deltaTime);
			yield return new WaitForSeconds(0.001f);
		}

		Debug.Log("�� ��ġ ����");
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
			Debug.Log("���� �ൿ�� ���� ���� �뺴�� �ֽ��ϴ�");
			return;
		}
		else
		{
			Debug.Log("���� �ൿ �Ͽ����ϴ�");
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
			//��� ������
			bcurrentMyTurn = false;
			get_BattleUiController.ReSetting_TurnText(1);
			get_BattlePopUpController.ReSettin_GuidePopText(1);
			StartCoroutine("Opne_GuidePopUp",true);

			for(int i = 0; i < 3;i++)
			{
				get_BattleCharctreController.get_BattleEnemy(i).Set_bDefenseState(false);
			}
		}
		else
		{
			//��������
			bcurrentMyTurn = true;
			get_BattleUiController.ReSetting_TurnText(0);
			get_BattlePopUpController.ReSettin_GuidePopText(0);

			for(int i = 0; i < 3;i++)
			{
				get_BattleCharctreController.get_BattlePlayerCharcter(i).Get_icon().gameObject.SetActive(false);
				get_BattleCharctreController.get_BattlePlayerCharcter(i).Set_bDefenseState(false);

				if (bCurrentCharcterAlivel[i] == true)
				{
					bCheckCharcterAction[i] = false;
				}
				else
				{
					bCheckCharcterAction[i] = true;
				}
			}
			StartCoroutine("Opne_GuidePopUp",false);
		}
	}

	IEnumerator Opne_GuidePopUp(bool _bool)
	{
		get_BattlePopUpController.Open_PopUp(6);
		yield return new WaitForSeconds(1.5f);
		get_BattlePopUpController.Close_PopUp();

		//���� �ൿ
		if(_bool == true)
		{
			StartCoroutine("Start_EnemyAction");
		}
	}

	IEnumerator Start_EnemyAction()
	{
		yield return new WaitForSeconds(1f);
		for(int i = 0; i < bCurrentEnemyAlivel.Length;i++)
		{
			int index = i;
			if (bCurrentEnemyAlivel[i] == true)
			{
				int randomValue = Random.Range(0, 100);  // 0���� 100 ������ ���� �� ����

				if (randomValue < 15)  // 75% Ȯ���� ����
				{
					Debug.Log("���� ����");
					yield return StartCoroutine("Start_EnemyAttack", index);

				}
				else if (randomValue < 55)  // 15% Ȯ���� ���
				{
					Debug.Log("���� ���");
					get_BattleCharctreController.get_BattleEnemy(index).Set_bDefenseState(true);
					yield return new WaitForEndOfFrame();
				}
				else  // 10% Ȯ���� ȸ��
				{
					Debug.Log("���� ȸ��");
					get_BattleCharctreController.get_BattleEnemy(index).Recover_CurrentHealth();
					yield return new WaitForEndOfFrame();
				}
			}
			yield return new WaitForEndOfFrame();
		}
		yield return new WaitForSeconds(1f);
		FunctionChange_TurnOver();
	}	


	//���Ͱ� �ൿ �ϴ� �Լ�
	IEnumerator Start_EnemyAttack(int _index)
	{
		attcakAlivePlayers.Clear();
		for (int i = 0; i < bCurrentCharcterAlivel.Length; i++)
		{
			if (bCurrentCharcterAlivel[i])
			{
				attcakAlivePlayers.Add(i);  // �ش� ĳ���Ͱ� ��������� �ε����� �߰�
			}
		}
		int randomPlayerIndex = 0 ;
		if (attcakAlivePlayers.Count > 0)
		{
			randomPlayerIndex = attcakAlivePlayers[Random.Range(0, attcakAlivePlayers.Count)];
		}
		get_BattleEffecController.Get_DontClick().gameObject.SetActive(true);
		yield return new WaitForSeconds(0.5f);
		get_BattleCharctreController.get_BattleEnemy(_index).Get_EnemyImage().transform.SetParent(get_BattleEffecController.Get_AttackArea());
		yield return new WaitForEndOfFrame();

		float moveSpeed = 350.0f;

		//�ش� ĳ���ͷ� �̵� ��ġ �̵�
		while (Vector3.Distance(get_BattleCharctreController.get_BattleEnemy(_index).Get_EnemyImage().transform.position, get_BattleCharctreController.get_BattlePlayerCharcter(randomPlayerIndex).Get_AttackPos().position) > 0.001f)
		{
			get_BattleCharctreController.get_BattleEnemy(_index).Get_EnemyImage().transform.position =
				Vector3.MoveTowards(get_BattleCharctreController.get_BattleEnemy(_index).Get_EnemyImage().transform.position,
				get_BattleCharctreController.get_BattlePlayerCharcter(randomPlayerIndex).Get_AttackPos().position,
				moveSpeed * Time.deltaTime);
			yield return new WaitForSeconds(0.001f);
		}

		Debug.Log("����");
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

			get_BattleCharctreController.get_BattlePlayerCharcter(randomPlayerIndex).Get_myCharacterBtnImage().color = new Color(1, alpha, alpha, 1);
			yield return new WaitForEndOfFrame();
		}
		get_BattleCharctreController.get_BattlePlayerCharcter(randomPlayerIndex).Calculation_AttackDamages(get_BattleCharctreController.get_BattleEnemy(_index).Get_AttackDamages());
		yield return new WaitForEndOfFrame();

		//�� ��ġ �̵�
		while (Vector3.Distance(get_BattleCharctreController.get_BattleEnemy(_index).Get_EnemyImage().transform.position, get_BattleCharctreController.get_BattleEnemy(_index).Get_EnemyPos().position) > 0.001f)
		{
			get_BattleCharctreController.get_BattleEnemy(_index).Get_EnemyImage().transform.position =
				Vector3.MoveTowards(get_BattleCharctreController.get_BattleEnemy(_index).Get_EnemyImage().transform.position,
				get_BattleCharctreController.get_BattleEnemy(_index).Get_EnemyPos().position,
				moveSpeed * Time.deltaTime);
			yield return new WaitForSeconds(0.001f);
		}

		Debug.Log("�� ��ġ ����");
		yield return new WaitForSeconds(1f);
		get_BattleCharctreController.get_BattleEnemy(_index).ReSetting_EnemyImage();
		get_BattleEffecController.Get_DontClick().gameObject.SetActive(false);
		yield return new WaitForEndOfFrame();
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
			Debug.Log("���� ��� ���͸� ��ġ���� ���߽��ϴ�");
			return;
		}
		else
		{
			Debug.Log("��� ���͸� ��ġ�����ϴ�");
		}
	}

	public IGet_BattleSpriteController Get_ImageBattleSpriteController()
	{
		return get_BattleSpriteController;
	}
}
