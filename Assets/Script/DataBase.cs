using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
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
	public int currentXP;
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

    private Dictionary<int, int> stageExperienceValueList;

    private Dictionary<int, int> xpList;

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
        Load_StageExperienceValue();
        Load_XpList();
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

    private void Load_StageExperienceValue()
    {
        stageExperienceValueList = new Dictionary<int, int>();
		TextAsset file = Resources.Load<TextAsset>("StageExperienceValue");
		string[] Sequence = file.text.Split('\n');
		char sp = ' ';
        foreach (var num in Sequence)
        {
            string[] word = num.Split(sp);
            stageExperienceValueList.Add(int.Parse(word[0].ToString()), int.Parse(word[1].ToString()));
            Debug.Log($"stageExperienceValueList : key : {int.Parse(word[0].ToString())} , value : {int.Parse(word[1].ToString())}");

        }
	}

    private void Load_XpList()
    {
        xpList = new Dictionary<int, int>();
		TextAsset file = Resources.Load<TextAsset>("XpValue");
		string[] Sequence = file.text.Split('\n');
		char sp = ' ';
		foreach (var num in Sequence)
		{
			string[] word = num.Split(sp);
			xpList.Add(int.Parse(word[0].ToString()), int.Parse(word[1].ToString()));
			Debug.Log($"stageExperienceValueList : key : {int.Parse(word[0].ToString())} , value : {int.Parse(word[1].ToString())}");

		}
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
                currentGold += 50;
				break;
            case 1:
				currentGold += 100;
				break;
            case 2:
				currentGold += 150;
				break;
            case 3:
				currentGold += 200;
				break;
            case 4:
				currentGold += 250;
				break;
        }
    }

    public int Get_StageExperienceValueList()
    {
        return stageExperienceValueList[currentSelectEnemyIndex];
	}

    public bool Set_CharacterDataCurrentXP(int _index,int _value)
    {
        bool check = false;
        int currentLevel = characterData.characters[_index].level;
		characterData.characters[_index].currentXP = characterData.characters[_index].currentXP + _value;
        UpdateCharacterLevel(_index);

        if(currentLevel < characterData.characters[_index].level)
        {
            check = true;
		}
        else
        {
            check = false;
        }
		return check;
	}

	public void UpdateCharacterLevel(int _index)
	{
        if(characterData.characters[_index].level >= 101)
        {
            return;
        }
        
		if (characterData.characters[_index].currentXP >= xpList[characterData.characters[_index].level])
		{
            characterData.characters[_index].level += 1;
            characterData.characters[_index].baseStats.attack +=3;
			characterData.characters[_index].baseStats.defense += 3;
			characterData.characters[_index].baseStats.health += 3;
			UpdateCharacterLevel(_index);
		}
	}

}
