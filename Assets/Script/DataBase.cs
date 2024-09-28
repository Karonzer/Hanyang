using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterStats
{
    public int attack;
    public int defense;
    public int health;
}

[System.Serializable]
public class Character
{
    public int id;
    public string name;
    public int level;
    public int currentXP;
    public CharacterStats baseStats;
    public CharacterStats trainingStats;
}

[System.Serializable]
public class CharacterData
{
    public List<Character> characters;
}

[System.Serializable]
public class EnemyStats
{
	public int id;
	public string name;
	public CharacterStats baseStats;
}

[System.Serializable]
public class EnemyData
{
	public List<EnemyStats> enemyStats;
}

[System.Serializable]
public class GoldData
{
	public int currentGold;
}

public class DataBase : GenericSingletonClass<DataBase>
{
    
    [SerializeField] private CharacterData characterData;

    [SerializeField] private EnemyData enemyData;

    [SerializeField] private bool bClickBoss;
    [SerializeField] private int currentSelectEnemyIndex;

    [SerializeField] private int currentGold;

	private void Awake()
	{
		Application.targetFrameRate = 60;
		DontDestroyOnLoad(gameObject);
	}

	private void Start()
    {
        LoadCharacterData();
        Load_EnemyData();
        Load_CurrnetGold();
	}

    private void LoadCharacterData()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("characters_data");

        if (jsonFile != null)
        {
            characterData = JsonUtility.FromJson<CharacterData>(jsonFile.text);

            foreach (var character in characterData.characters)
            {
                Debug.Log($"Character ID: {character.id}, Name: {character.name}, Level: {character.level}");
            }
        }
        else
        {
            Debug.LogError("Failed to load characters_data.json from Resources folder.");
        }


	
	}

    private void Load_EnemyData()
    {
        Debug.Log("Load_EnemyData");
		TextAsset enemyjsonFile = Resources.Load<TextAsset>("Enemy_data");
		if (enemyjsonFile != null)
		{
			Debug.Log("enemyjsonFile");
			enemyData = JsonUtility.FromJson<EnemyData>(enemyjsonFile.text);

			foreach (var enemyStats in enemyData.enemyStats)
			{
				Debug.Log($"Character ID: {enemyStats.id}, Name: {enemyStats.name}");
			}
		}
		else
		{
			Debug.LogError("Failed to load characters_data.json from Resources folder.");
		}
	}

    private void Load_CurrnetGold()
    {
		TextAsset jsonFile = Resources.Load<TextAsset>("Gold");
        GoldData goldData =  JsonUtility.FromJson<GoldData>(jsonFile.text);
        currentGold = goldData.currentGold;
	}    

    public Character Get_CharacterData(int _index)
    {
        return characterData.characters[_index];
	}

    public EnemyStats GetEnemyStats(int _index) 
    {
        return enemyData.enemyStats[_index];
    }

	public bool Get_bClickBoss()
    { return bClickBoss; }

    public void Set_bClickBoss(bool _bool)
    {
        bClickBoss = _bool;
    }

	public int Get_CurrentSelectEnemyIndex()
    {
        return currentSelectEnemyIndex;
    }

	public void Set_CurrentSelectEnemyIndex(int _index)
    {
		currentSelectEnemyIndex = _index;
	}


    public void FunctionGain_Gold()
    {
        switch(currentSelectEnemyIndex)
        {
            case 0:
				currentGold =  currentGold + 50;
				break;
            case 1:
				currentGold = currentGold + 100;
				break;
            case 2:
				currentGold = currentGold + 150;
				break;
            case 3:
				currentGold = currentGold + 200;
				break;
            case 4:
				currentGold = currentGold + 250;
				break;
        }
    }

}
