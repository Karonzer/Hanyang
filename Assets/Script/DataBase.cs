using System.Collections;
using System.Collections.Generic;
using System.IO;
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
	public int gold;
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

[System.Serializable]
public class CheckListData
{
	public int DispatchLevel;
	public int TrainingLevel;
	public bool[] bossClear;
}

public class DataBase : GenericSingletonClass<DataBase>
{
    
    [SerializeField] private CharacterData characterData;

    [SerializeField] private EnemyData enemyData;

    [SerializeField] private EnemyData bossData;

    [SerializeField] private bool bClickBoss;
    [SerializeField] private int currentSelectEnemyIndex;

    [SerializeField] private int currentGold;

    private Dictionary<int, int> stageExperienceValueList;
	private Dictionary<int, int> bossStageExperienceValueList;

	private Dictionary<int, int> xpList;

	[SerializeField] CheckListData checkListData;


	private void Awake()
	{
		Application.targetFrameRate = 60;
		DontDestroyOnLoad(gameObject);
	}

	private void Start()
    {
        LoadCharacterData();
        Load_EnemyData();
        Load_BossData();
		Load_CurrnetGold();
        Load_StageExperienceValue();
		Load_BossStageExperienceValueList();
		Load_XpList();
		Load_CheckList();
	}

    private void LoadCharacterData()
    {
		string path = Path.Combine(Application.persistentDataPath, "characters_data.json");

		// Check if the file exists in persistentDataPath
		if (File.Exists(path))
		{
			// Load from persistentDataPath if it exists
			string json = File.ReadAllText(path);
			characterData = JsonUtility.FromJson<CharacterData>(json);
			Debug.Log("Character data loaded from persistentDataPath.");
		}
		else
		{
			// Otherwise, load the default data from Resources
			TextAsset jsonFile = Resources.Load<TextAsset>("characters_data");

			if (jsonFile != null)
			{
				characterData = JsonUtility.FromJson<CharacterData>(jsonFile.text);
				Debug.Log("Character data loaded from Resources.");
			}
			else
			{
				Debug.LogError("Failed to load characters_data.json from Resources folder.");
				return;
			}
		}

		// Display the loaded character data
		foreach (var character in characterData.characters)
		{
			Debug.Log($"Character ID: {character.id}, Name: {character.name}, Level: {character.level}");
		}
	}

	public void SaveCharacterData()
	{
		string json = JsonUtility.ToJson(characterData, true);

		// You cannot write to the Resources folder directly in a build, so save to persistentDataPath
		string path = Path.Combine(Application.persistentDataPath, "characters_data.json");

		File.WriteAllText(path, json);
		Debug.Log($"Character data saved to {path}");
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

	private void Load_BossData()
	{
		Debug.Log("Load_BossData");
		TextAsset enemyjsonFile = Resources.Load<TextAsset>("Boss_data");
		if (enemyjsonFile != null)
		{
			Debug.Log("enemyjsonFile");
			bossData = JsonUtility.FromJson<EnemyData>(enemyjsonFile.text);

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
		string path = Path.Combine(Application.persistentDataPath, "Gold.json");

		// Check if the file exists in persistentDataPath
		if (File.Exists(path))
		{
			// Load the saved gold data from persistentDataPath
			string json = File.ReadAllText(path);
			GoldData goldData = JsonUtility.FromJson<GoldData>(json);
			currentGold = goldData.currentGold;
			Debug.Log("Gold data loaded from persistentDataPath.");
		}
		else
		{
			// Otherwise, load the default gold data from Resources
			TextAsset jsonFile = Resources.Load<TextAsset>("Gold");

			if (jsonFile != null)
			{
				GoldData goldData = JsonUtility.FromJson<GoldData>(jsonFile.text);
				currentGold = goldData.currentGold;
				Debug.Log("Gold data loaded from Resources.");
			}
			else
			{
				Debug.LogError("Failed to load Gold.json from Resources folder.");
			}
		}
	}

	public void SaveCurrentGold()
	{
		// Create a GoldData object to save the current gold
		GoldData goldData = new GoldData { currentGold = currentGold };

		// Serialize the gold data to JSON format
		string json = JsonUtility.ToJson(goldData, true);

		// Save the JSON data to persistentDataPath
		string path = Path.Combine(Application.persistentDataPath, "Gold.json");

		File.WriteAllText(path, json);
		Debug.Log($"Gold data saved to {path}");
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

	private void Load_BossStageExperienceValueList()
	{
		bossStageExperienceValueList = new Dictionary<int, int>();
		TextAsset file = Resources.Load<TextAsset>("BossStageExperienceValue");
		string[] Sequence = file.text.Split('\n');
		char sp = ' ';
		foreach (var num in Sequence)
		{
			string[] word = num.Split(sp);
			bossStageExperienceValueList.Add(int.Parse(word[0].ToString()), int.Parse(word[1].ToString()));
			Debug.Log($"BossStageExperienceValue : key : {int.Parse(word[0].ToString())} , value : {int.Parse(word[1].ToString())}");

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

	private void Load_CheckList()
	{
		string path = Path.Combine(Application.persistentDataPath, "CheckList.json");

		// Check if the file exists in persistentDataPath
		if (File.Exists(path))
		{
			// Load the saved checklist data from persistentDataPath
			string json = File.ReadAllText(path);
			checkListData = JsonUtility.FromJson<CheckListData>(json);
			Debug.Log("Checklist data loaded from persistentDataPath.");
		}
		else
		{
			// Otherwise, load the default checklist data from Resources
			TextAsset jsonFile = Resources.Load<TextAsset>("CheckList");

			if (jsonFile != null)
			{
				checkListData = JsonUtility.FromJson<CheckListData>(jsonFile.text);
				Debug.Log("Checklist data loaded from Resources.");
			}
			else
			{
				Debug.LogError("Failed to load CheckList.json from Resources folder.");
			}
		}
	}

	public void SaveCheckList()
	{
		// Serialize the CheckListData to JSON format
		string json = JsonUtility.ToJson(checkListData, true);

		// Save the JSON data to persistentDataPath
		string path = Path.Combine(Application.persistentDataPath, "CheckList.json");

		File.WriteAllText(path, json);
		Debug.Log($"Checklist data saved to {path}");
	}

	public Character Get_CharacterData(int _index)
    {
        return characterData.characters[_index];
	}

	public void Add_CharacterDataAttack(int _index,int _value)
	{
		characterData.characters[_index].trainingStats.attack = _value;
		SaveCharacterData();
	}

	public void Add_CharacterDataDfense(int _index, int _value)
	{
		characterData.characters[_index].trainingStats.defense = _value;
		SaveCharacterData();
	}

	public void Add_CharacterDataHealth(int _index, int _value)
	{
		characterData.characters[_index].trainingStats.health = _value;
		SaveCharacterData();
	}


	public EnemyStats GetEnemyStats(int _index) 
    {
        return enemyData.enemyStats[_index];
    }

	public EnemyStats GetBossData(int _index)
	{
		return bossData.enemyStats[_index];
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


    public string FunctionGain_EnemyGold()
    {
		Debug.Log($"currentSelectEnemyIndex : {currentSelectEnemyIndex} , 획득 골드 : {enemyData.enemyStats[currentSelectEnemyIndex].gold}");
		currentGold += enemyData.enemyStats[currentSelectEnemyIndex].gold;
		SaveCurrentGold();
		return enemyData.enemyStats[currentSelectEnemyIndex].gold.ToString();

	}
	public string FunctionGain_BossGold()
	{
		Debug.Log($"currentSelectEnemyIndex : {currentSelectEnemyIndex} , 획득 골드 : {bossData.enemyStats[currentSelectEnemyIndex].gold}");
		currentGold += bossData.enemyStats[currentSelectEnemyIndex].gold;
		SaveCurrentGold();
		return bossData.enemyStats[currentSelectEnemyIndex].gold.ToString();
	}

	public void Funtion_AddGold(int _value)
	{
		currentGold += _value;
		SaveCurrentGold();
	}

	public void Funtion_RemoveGold(int _value)
	{
		currentGold -= _value;
		SaveCurrentGold();
	}	

	public int Get_CurrentGold()
	{
		return currentGold;
	}

    public int Get_StageExperienceValueList()
    {
        return stageExperienceValueList[currentSelectEnemyIndex];
	}

	public int Get_BossStageExperienceValueList()
	{
		return bossStageExperienceValueList[currentSelectEnemyIndex];
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
		SaveCharacterData();
		return check;
	}

	public void UpdateCharacterLevel(int _index)
	{
        if(characterData.characters[_index].level >= 100)
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

	public int Get_DispatchLevel()
	{
		return checkListData.DispatchLevel;
	}

	public int Get_TrainingLevel()
	{
		return checkListData.TrainingLevel;
	}

	public void Funtion_AddDispatchLevel()
	{
		checkListData.DispatchLevel += 1;
		SaveCheckList();
	}

	public void Funtion_AddTrainingLevel()
	{
		checkListData.TrainingLevel += 1;
		SaveCheckList();
	}

	public void Initialization()
	{
		Debug.Log("Initialization");
		Initialization_LoadCharacterData();
		Initialization_LoadCurrnetGold();
		Initialization_LoadCheckList();
		Loding.LoadScene("ReTitle");
		Resources.UnloadUnusedAssets();
		System.GC.Collect();
	}

	public bool[] Get_BossClearCheck()
	{
		return checkListData.bossClear;
	}

	public void Set_BossClearCheck(bool _bool)
	{
		checkListData.bossClear[currentSelectEnemyIndex] = _bool;
	}

	private void Initialization_LoadCharacterData()
	{
		TextAsset jsonFile = Resources.Load<TextAsset>("characters_data");

		if (jsonFile != null)
		{
			characterData = JsonUtility.FromJson<CharacterData>(jsonFile.text);

			string savejson = JsonUtility.ToJson(characterData, true);
			string savepath = Path.Combine(Application.persistentDataPath, "characters_data.json");

			File.WriteAllText(savepath, savejson);
			Debug.Log($"Character data saved to {savepath}");
		}
		else
		{
			Debug.LogError("Failed to load characters_data.json from Resources folder.");
			return;
		}
	}

	private void Initialization_LoadCurrnetGold()
	{
		TextAsset jsonFile = Resources.Load<TextAsset>("Gold");

		if (jsonFile != null)
		{
			GoldData goldData = JsonUtility.FromJson<GoldData>(jsonFile.text);
			currentGold = goldData.currentGold;
			Debug.Log("Gold data loaded from Resources.");

			string json = JsonUtility.ToJson(goldData, true);
			string path = Path.Combine(Application.persistentDataPath, "Gold.json");
			File.WriteAllText(path, json);
			Debug.Log($"Gold data saved to {path}");
		}
		else
		{
			Debug.LogError("Failed to load Gold.json from Resources folder.");
		}
	}


	private void Initialization_LoadCheckList()
	{
		TextAsset jsonFile = Resources.Load<TextAsset>("CheckList");

		if (jsonFile != null)
		{
			checkListData = JsonUtility.FromJson<CheckListData>(jsonFile.text);
			Debug.Log("Checklist data loaded from Resources.");

			string json = JsonUtility.ToJson(checkListData, true);
			string path = Path.Combine(Application.persistentDataPath, "CheckList.json");
			File.WriteAllText(path, json);
			Debug.Log($"Checklist data saved to {path}");
		}
		else
		{
			Debug.LogError("Failed to load CheckList.json from Resources folder.");
		}
	}



}
