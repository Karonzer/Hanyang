using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IGet_BattlePlayerCharcter
{
	public void Set_MYCharacterOutline(bool _bool);
	public Transform Get_AttackPos();
	public Transform Get_myCharacter();
	public void ReSetting_myCharacter();
	public Transform Get_CharcterPos();
	public Image Get_myCharacterBtnImage();
	public Image Get_icon();
	public void Set_bDefenseState(bool _bool);
	public void Recover_CurrentHealth();
	public int Get_AttackDamages();
}

public class BattlePlayerCharcter : MonoBehaviour, IGet_BattlePlayerCharcter
{
    [SerializeField] private Character myCharacter;

	[SerializeField] private Image myCharacterBtnImage;
	[SerializeField] private Button myCharacterBtn;
	[SerializeField] private Outline myCharacterOutline;

	[SerializeField] private int currentIndex;
	[SerializeField] private int maxHealth;
	[SerializeField] private int currentHealth;

	[SerializeField] private Image healthpar;
	[SerializeField] private Text healthText;
	[SerializeField] private Image icon;
	[SerializeField] private Transform attackPos;

	[SerializeField] private bool bDefenseState;
	private void Awake()
	{
		Initialize_myCharacter();
	}

	private void OnEnable()
	{
		Setting_myCharacter();
		Setting_myCharacterBtnImage();
		bDefenseState = false;
	}

	private void Start()
	{
		StartSetting_MyCharacterData();
	}

	private void StartSetting_MyCharacterData()
	{
		myCharacter = DataBase.Instance.Get_CharacterData(transform.GetSiblingIndex());
		maxHealth = myCharacter.baseStats.health + myCharacter.trainingStats.health;
		currentHealth = myCharacter.baseStats.health + myCharacter.trainingStats.health;
		healthText.text = currentHealth.ToString();
	}	

	private void Initialize_myCharacter()
	{
		myCharacterBtnImage = transform.GetChild(0).GetComponent<Image>();
		myCharacterBtn = transform.GetChild(0).GetComponent<Button>();
		myCharacterBtn.onClick.AddListener(() => Click_myCharacterBtn(transform.GetSiblingIndex()));
		myCharacterOutline = transform.GetChild(0).GetComponent<Outline>();

		healthpar = transform.GetChild(1).GetChild(0).GetComponent<Image>();
		healthText = transform.GetChild(1).GetChild(1).GetComponent<Text>();
		icon = transform.GetChild(2).GetComponent<Image>();
		attackPos = transform.GetChild(3);

		currentIndex = transform.GetSiblingIndex();
	}

	private void Setting_myCharacterBtnImage()
	{
		myCharacterBtnImage.color = new Color(1, 1, 1, 1);
	}

	private void Click_myCharacterBtn(int _index)
	{
		if(BGSC.Instance.get_BattleContentController.Get_bCheckCharcterAction(currentIndex) == true)
		{
			return;
		}

		Debug.Log(_index);
		BGSC.Instance.get_BattleContentController.Click_MyCharcterObject(currentIndex);
		myCharacterOutline.enabled = true;
	}

	private void Setting_myCharacter()
	{
		myCharacterBtn.gameObject.SetActive(true);
		myCharacterOutline.enabled = false;

		healthpar.fillAmount = 1;
		icon.gameObject.SetActive(false);
	}


	public void Set_MYCharacterOutline(bool _bool)
	{
		myCharacterOutline.enabled = _bool;
	}

	public Transform Get_myCharacter()
	{
		return myCharacterBtnImage.transform;
	}

	public Transform Get_AttackPos()
	{
		return attackPos;
	}

	public void ReSetting_myCharacter()
	{
		myCharacterBtnImage.transform.SetParent(transform);
		myCharacterBtnImage.transform.SetSiblingIndex(0);
	}

	public Transform Get_CharcterPos()
	{ return transform; }

	public Image Get_myCharacterBtnImage()
	{ return myCharacterBtnImage; }

	public Image Get_icon()
	{
		return icon;
	}

	public void Set_bDefenseState(bool _bool)
	{
		bDefenseState = _bool;
	}


	public void Recover_CurrentHealth()
	{
		currentHealth = currentHealth + 10;
		if(currentHealth>= maxHealth)
		{
			currentHealth = maxHealth;
		}
		healthText.text = currentHealth.ToString();
	}

	public int Get_AttackDamages()
	{
		return myCharacter.baseStats.attack;
	}

	public void Calculation_AttackDamages(int _getDamages)
	{
		int damages = _getDamages - myCharacter.baseStats.defense;
		if (damages > 0)
		{
			Debug.Log("데미지 계산");
			currentHealth = currentHealth - damages;

			if (currentHealth <= 0)
			{
				currentHealth = 0;
				healthpar.fillAmount = 0; ;
				healthText.text = "0";
			}
			else
			{
				healthpar.fillAmount = (float)currentHealth * 1 / maxHealth;
				healthText.text = currentHealth.ToString();
			}

		}
		else
		{
			Debug.Log("방어력이 높아 데미지를 넣을 수 없습니다");
			currentHealth = currentHealth - 1;
			healthpar.fillAmount = (float)currentHealth * 1 / maxHealth;
			healthText.text = currentHealth.ToString();
		}
	}

}
