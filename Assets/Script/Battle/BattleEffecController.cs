using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGet_BattleEffecController
{
	public Transform Get_AttackArea();
	public Transform Get_DontClick();
}
public class BattleEffecController : MonoBehaviour, IGet_BattleEffecController
{
	[SerializeField] private Transform attackArea;
	[SerializeField] private Transform DontClick;

	private void Awake()
	{
		Initialize_BattleEffecController();
	}

	private void OnEnable()
	{
		Setting_BattleEffecController();
	}

	private void Initialize_BattleEffecController()
	{
		attackArea = transform.GetChild(0).Find("AttackArea");
		DontClick = transform.GetChild(0).Find("Don'tClick");
	}

	private void Setting_BattleEffecController()
	{
		DontClick.gameObject.SetActive(false);
	}

	public Transform Get_AttackArea()
	{
		return attackArea;
	}

	public Transform Get_DontClick()
	{
		return DontClick; 
	}
}
