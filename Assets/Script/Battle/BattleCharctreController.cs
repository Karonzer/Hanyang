using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IGet_BattleCharctreController
{
	public void Change_myCharcterOutLine();
	public IGet_BattlePlayerCharcter get_BattlePlayerCharcter(int _index);
	public IGet_BattleEnemy get_BattleEnemy(int _index);
	public IGet_BattleBoss get_BattleBoss();
}

public class BattleCharctreController : MonoBehaviour, IGet_BattleCharctreController
{
	[SerializeField] private Transform myCharcter;
	[SerializeField] private Transform enemy;
	[SerializeField] private Transform middleBoss;
	[SerializeField] private Transform finalBoss;

	[SerializeField] private IGet_BattlePlayerCharcter[] battlePlayerCharcters;
	[SerializeField] private IGet_BattleEnemy[] battleEnemies;
	[SerializeField] private IGet_BattleBoss battleBoss;





	private void Awake()
	{
		Initialize_CharcterAndEnemy();
		Initialize_battlePlayerCharcters();
		Initialize_battleEnemies();
		Initialize_battleBoss();
	}

	private void OnEnable()
	{
		Setting_CharcterAndEnemy();

	}

	private void Start()
    {
		Start_SettingEnemyAndboss();
	}

	private void Initialize_CharcterAndEnemy()
	{
		myCharcter = transform.GetChild(0).Find("My");
		enemy = transform.GetChild(0).Find("Enemy");
		middleBoss = transform.GetChild(0).Find("Middle Boss Monster");
		finalBoss = transform.GetChild(0).Find("Final Boss Monster");
	}

	private void Setting_CharcterAndEnemy()
	{
		myCharcter.gameObject.SetActive(true);
		enemy.gameObject.SetActive(false);
		middleBoss.gameObject.SetActive(false);
		finalBoss.gameObject.SetActive(false);
	}

	private void Initialize_battlePlayerCharcters()
	{
		battlePlayerCharcters = new IGet_BattlePlayerCharcter[myCharcter.childCount];
		for(int i = 0; i < battlePlayerCharcters.Length;i++)
		{
			battlePlayerCharcters[i]  = myCharcter.GetChild(i).GetComponent<IGet_BattlePlayerCharcter>();
		}
	}

	private void Initialize_battleEnemies()
	{
		battleEnemies = new IGet_BattleEnemy[enemy.childCount];
		for(int i = 0; i < battleEnemies.Length;i++)
		{
			battleEnemies[i]  = enemy.GetChild(i).GetComponent<IGet_BattleEnemy>();
		}
	}	

	private void Initialize_battleBoss()
	{
		battleBoss = middleBoss.GetChild(0).GetComponent<IGet_BattleBoss>();
	}

	private void Start_SettingEnemyAndboss()
	{
		
		if (DataBase.Instance.Get_bClickBoss())
		{
			middleBoss.gameObject.SetActive(true);
			int index = DataBase.Instance.Get_CurrentSelectEnemyIndex();
			BGSC.Instance.get_BattleContentController.Initialize_bCurrentBossAlivel();
			battleBoss.Function_InitializemyBoss();
			battleBoss.Function_SettingmyBoss();
			battleBoss.Get_BossImage().sprite = BGSC.Instance.get_BattleContentController.Get_BattleSpriteController().Get_BossImage(index);
			battleBoss.Get_BattleBossObj().gameObject.SetActive(true);
			battleBoss.Setting_BossStats(DataBase.Instance.GetBossData(index));
			BGSC.Instance.get_BattleContentController.Set_currentEnemyXp(DataBase.Instance.GetBossData(index).currentXP);
		}
		else
		{
			BGSC.Instance.get_BattleContentController.Initialize_bCurrentEnemyAlivel(battleEnemies.Length);
			int index = DataBase.Instance.Get_CurrentSelectEnemyIndex();
			for (int i = 0; i < battleEnemies.Length;i++)
			{
				
				enemy.gameObject.SetActive(true);
				battleEnemies[i].Function_InitializemyCharacter();
				battleEnemies[i].Function_SettingmyCharacter();
				battleEnemies[i].Get_EnemyImage().sprite = BGSC.Instance.get_BattleContentController.Get_BattleSpriteController().Get_EnemyImage(index);
				battleEnemies[i].Get_BattleEnemyObj().gameObject.SetActive(true);
				battleEnemies[i].Setting_EnemyStats(DataBase.Instance.GetEnemyStats(index));

			}
			BGSC.Instance.get_BattleContentController.Set_currentEnemyXp(DataBase.Instance.GetEnemyStats(index).currentXP);
		}
	}

	public void Change_myCharcterOutLine()
	{
		for (int i = 0; i < battlePlayerCharcters.Length; i++)
		{
			battlePlayerCharcters[i].Set_MYCharacterOutline(false);
		}
	}

	public IGet_BattlePlayerCharcter get_BattlePlayerCharcter(int _index)
	{
		return battlePlayerCharcters[_index];
	}


	public IGet_BattleEnemy get_BattleEnemy(int _index)
	{
		return battleEnemies[_index];
	}

	public IGet_BattleBoss get_BattleBoss()
	{
		return battleBoss;
	}
}
