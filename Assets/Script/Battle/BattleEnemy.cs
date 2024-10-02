using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IGet_BattleEnemy
{
	public void Function_InitializemyCharacter();
	public void Function_SettingmyCharacter();
	public Transform Get_BattleEnemyObj();
	public void Setting_EnemyStats(EnemyStats _data);
	public Image Get_EnemyImage();
	public Transform Get_AttackPos();
	public void ReSetting_EnemyImage();
	public Transform Get_EnemyPos();
	public int Get_AttackDamages();
	public void Calculation_AttackDamages(int _getDamages);
	public void Recover_CurrentHealth();
	public void Set_bDefenseState(bool _bool);
	public int Get_EnemyXp();
}

public class BattleEnemy : MonoBehaviour, IGet_BattleEnemy
{
	[SerializeField] private EnemyStats enemyStats;

	[SerializeField] private int currentIndex;
	[SerializeField] private int maxHealth;
	[SerializeField] private int currentHealth;

	[SerializeField] private Image enemyImage;
	[SerializeField] private Image stateImage;
	[SerializeField] private Image healthpar;
	[SerializeField] private Text healthText;
	[SerializeField] private Transform attackPos;

	[SerializeField] private bool bDefenseState;


	private void Awake()
	{

	}

	private void OnEnable()
	{

	}

	private void Start()
	{
		
	}

	public void Function_InitializemyCharacter()
	{
		Initialize_myCharacter();
	}

	public void Function_SettingmyCharacter()
	{
		Setting_myCharacter();
		Setting_EnemyImage();
		bDefenseState = false;

	}

	private void Initialize_myCharacter()
	{
		enemyImage = transform.GetChild(0).GetComponent<Image>();
		stateImage = enemyImage.transform.GetChild(0).GetComponent<Image>();
		healthpar = transform.GetChild(1).GetChild(0).GetComponent<Image>();
		healthText = transform.GetChild(1).GetChild(1).GetComponent<Text>();
		attackPos = transform.GetChild(2);

		currentIndex = transform.GetSiblingIndex();
	}

	private void Setting_EnemyImage()
	{
		enemyImage.color = new Color(1, 1, 1, 1);
		stateImage.gameObject.SetActive(false);
	}

	private void Setting_myCharacter()
	{
		healthpar.fillAmount = 1;
	}

	public Transform Get_BattleEnemyObj()
	{
		return transform;
	}

	public Image Get_EnemyImage()
	{ return enemyImage; }

	public void Setting_EnemyStats(EnemyStats _data)
	{
		enemyStats = _data;
		maxHealth = enemyStats.baseStats.health;
		currentHealth = enemyStats.baseStats.health;
		healthText.text = currentHealth.ToString();
	}

	public Transform Get_AttackPos()
	{
		return attackPos;
	}

	public void ReSetting_EnemyImage()
	{
		enemyImage.transform.SetParent(transform);
		enemyImage.transform.SetSiblingIndex(0);
	}

	public Transform Get_EnemyPos()
	{ return transform; }


	public int Get_AttackDamages()
	{
		return enemyStats.baseStats.attack;
	}

	public void Set_bDefenseState(bool _bool)
	{
		bDefenseState = _bool;
		if(bDefenseState) 
		{
			stateImage.gameObject.SetActive(true);
			stateImage.sprite = BGSC.Instance.get_BattleContentController.Get_ImageBattleSpriteController().Get_IconImage(1);
		}
		else
		{
			stateImage.gameObject.SetActive(false);
		}
	}


	public void Recover_CurrentHealth()
	{
		currentHealth = currentHealth + 10;
		if (currentHealth >= maxHealth)
		{
			currentHealth = maxHealth;
		}
		healthText.text = currentHealth.ToString();
		stateImage.gameObject.SetActive(true);
		stateImage.sprite = BGSC.Instance.get_BattleContentController.Get_ImageBattleSpriteController().Get_IconImage(2);

	}

	public void Calculation_AttackDamages(int _getDamages)
	{
		
		int damages = _getDamages - enemyStats.baseStats.defense;

		Debug.Log($"damages : {damages} , _getDamages : {_getDamages} , defense : {enemyStats.baseStats.defense} ");
		if (damages > 0)
		{
			if(bDefenseState)
			{
				Debug.Log("방어 상태 데미지 계산");
				currentHealth = currentHealth - (int)(damages * 0.1);

				if (currentHealth <= 0)
				{
					currentHealth = 0;
					healthpar.fillAmount = 0; ;
					healthText.text = "0";
					StartCoroutine("Die_Enemy");
				}
				else
				{
					healthpar.fillAmount = (float)currentHealth / maxHealth;
					healthText.text = currentHealth.ToString();
				}
			}
			else
			{
				Debug.Log("데미지 계산");
				currentHealth = currentHealth - damages;

				if (currentHealth <= 0)
				{
					currentHealth = 0;
					healthpar.fillAmount = 0; ;
					healthText.text = "0";
					StartCoroutine("Die_Enemy");
				}
				else
				{
					healthpar.fillAmount = (float)currentHealth / maxHealth;
					healthText.text = currentHealth.ToString();
				}
			}


		}
		else
		{
			currentHealth = currentHealth - 1;
			Debug.Log("방어력이 높아 데미지를 넣을 수 없습니다");
			if (currentHealth <= 0)
			{
				currentHealth = 0;
				healthpar.fillAmount = 0; ;
				healthText.text = "0";
				StartCoroutine("Die_Enemy");
			}
			else
			{
				healthpar.fillAmount = (float)currentHealth / maxHealth;
				healthText.text = currentHealth.ToString();
			}

		}
	}

	IEnumerator Die_Enemy()
	{
		float blinkSpeed = 1f;
		float alpha = 1;
		bool stop = true;
		while (stop)
		{
			alpha -= Time.deltaTime * blinkSpeed;
			if (alpha <= 0f)
			{
				alpha = 0f;
				stop = false;
			}
			enemyImage.color = new Color(1, 1, 1, alpha);
			yield return new WaitForEndOfFrame();
		}
		BGSC.Instance.get_BattleContentController.FunctionGain_handlecurrentCharcterIndexXP();
		BGSC.Instance.get_BattleContentController.FuntionStatsCnage_bCurrentEnemyAlivel(currentIndex);
		yield return new WaitForEndOfFrame();
		transform.gameObject.SetActive(false);
	}

	public int Get_EnemyXp()
	{
		return enemyStats.currentXP;
	}

}
