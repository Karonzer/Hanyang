using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public interface IGet_BattleSpriteController
{
	public Sprite Get_IconImage(int _index);
	public Sprite Get_EnemyImage(int _index);
	public Sprite Get_BGImage(int _index);
	public Sprite Get_BossImage(int _index);
}

public class BattleSpriteController : MonoBehaviour, IGet_BattleSpriteController
{
	public SpriteAtlas iconSpriteAtlas;
	public SpriteAtlas enemySpriteAtlas;
	public SpriteAtlas bgSpriteAtlas;
	public SpriteAtlas bossSpriteAtlas;

	public Sprite Get_IconImage(int _index)
	{
		string name = null;
		switch(_index)
		{
			case 0:
				name = "icons_19";
				break;
			case 1:
				name = "icons_10";
				break;
			case 2:
				name = "icons_5";
				break;
		}
		return iconSpriteAtlas.GetSprite(name);
	}

	public Sprite Get_EnemyImage(int _index)
	{
		string name = "E_" + _index.ToString();
		return enemySpriteAtlas.GetSprite(name);
	}

	public Sprite Get_BGImage(int _index)
	{
		string name = "BG_" + _index.ToString();
		return bgSpriteAtlas.GetSprite(name);
	}

	public Sprite Get_BossImage(int _index)
	{
		string name = "Boss_" + _index.ToString();
		return bossSpriteAtlas.GetSprite(name);
	}
}
