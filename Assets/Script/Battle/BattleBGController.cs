using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IGet_BattleBGController
{

}

public class BattleBGController : MonoBehaviour, IGet_BattleBGController
{
	[SerializeField ]private Image bgImage;

	private void Awake()
	{
		Initialize_BG();
	}

	private void Start()
	{
		Setting_BG();
	}

	private void Initialize_BG()
	{
		bgImage = transform.GetChild(0).GetChild(0).GetComponent<Image>();
	}	

	private void Setting_BG()
	{
		bgImage.sprite = BGSC.Instance.get_BattleContentController.Get_BattleSpriteController().Get_BGImage(DataBase.Instance.Get_CurrentSelectEnemyIndex());
	}	


}
