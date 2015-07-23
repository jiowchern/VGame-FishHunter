using UnityEngine;
using System.Collections;

public class NGUISpriteRotator : MonoBehaviour
{

	
	public UISprite Sprite;

	
	[HideInInspector]
	[SerializeField]
	private  System.Collections.Generic.List<string> _SpriteNames;

	private int _Index;


    public UnityEngine.Texture2D TestTxr;

	public string[] SpriteNames 
    {
		get
		{
			return _SpriteNames.ToArray();
		}
	}

	public UIAtlas Atlas {
		get
		{
			if (this.Sprite != null)
				return this.Sprite.atlas;
			return null;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	    
	}   

	public void Next()
	{
		if (_HasSprite())
		{
			this.Sprite.spriteName = _Next();                            
		}
			
	}

	private bool _HasSprite()
	{
		return _SpriteNames.Count > 0;
	}

	private string _Next()
	{
		return _SpriteNames[_NextIndex()];
	}

	private int _NextIndex()
	{        
		_Index ++;
		_Index %= _SpriteNames.Count;
		return _Index;
	}

	public void Remove(string sprite_name)
	{
		_SpriteNames.Remove(sprite_name);
	}

	public void Add(string sprite)
	{
		_SpriteNames.Add(sprite);
	}
}
