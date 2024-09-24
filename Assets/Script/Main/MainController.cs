using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Loding_LoadBattleContentScene()
    {
        DataBase.Instance.Set_bClickBoss(false);
        DataBase.Instance.Set_CurrentSelectEnemyIndex(2);
		Loding.LoadScene("BattleContent");
		Resources.UnloadUnusedAssets();
		System.GC.Collect();
	}
}
