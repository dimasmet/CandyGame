using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour
{
	private static Color selectedColor = new Color(.5f, .5f, .5f, 1.0f);
	private static Tile previousSelected = null;

	[SerializeField] public SpriteRenderer render;

	public static bool GlobalActive = false;

	public SpriteRenderer Render
    {
        get
        {
			return render;
        }
    }

	private Vector2[] adjacentDirections = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

	private bool matchFound = false;

	public int x;
	public int y;

	public List<Tile> neighborTiles;

	[SerializeField] private Animator animator;
	[SerializeField] private Animator _animBoom;

	public enum TypeAnimation
	{
		Idle,
		Open,
		Close,
		Boom,
		False
	}

	public enum TypeTile
    {
		Classic,
		Bomb
    }

	public TypeTile currentType;


	public void SetTiteData(int x, int y)
    {
		this.x = x;
		this.y = y;

		PlayAnimations(TypeAnimation.Open);
    }

	public void SetType(Sprite sprite, TypeTile typeTile)
    {
		currentType = typeTile;
		render.sprite = sprite;
	}

	public void PlayAnimations(TypeAnimation typeAnimation)
    {
        switch (typeAnimation)
        {
			case TypeAnimation.Idle:
				animator.Play("idle");
				break;
			case TypeAnimation.Open:
				animator.Play("Open");
				break;
			case TypeAnimation.Close:
				animator.Play("Close");
				break;
			case TypeAnimation.Boom:
				_animBoom.Play("Boom");
				render.sprite = null;
				break;
			case TypeAnimation.False:
				animator.Play("False");
				break;
		}
    }

	public void Select() {
		render.color = selectedColor;
		previousSelected = this;
	}

	public void Deselect() {
		render.color = Color.white;
		previousSelected = null;
	}

	private void OnMouseDown()
	{
		if (GlobalActive)
		{
			if (render.sprite == null || BoardManager.instance.IsShifting)
			{
				return;
			}

			switch (currentType)
			{
				case TypeTile.Classic:
					HandlerMatch.Instance.ChoiceTile(this);
					break;
				case TypeTile.Bomb:
					BoardManager.instance.Exploshion(x, y);
					SoundsHandler.sound.PlayShotSound(SoundsHandler.NameSoundGame.Boom);
					break;
			}
		}
	
	}

	public void BoomTile()
    {
		PlayAnimations(TypeAnimation.Boom);

		StartCoroutine(BoardManager.instance.FindNullTiles());
	}

	public IEnumerator WaitToSwap(SpriteRenderer render2, float time)
    {
		yield return new WaitForSeconds(time);
		Sprite tempSprite = render2.sprite;
		render2.sprite = render.sprite;
		render.sprite = tempSprite;
	}

	public void SwapSprite(SpriteRenderer render2, float time = 0)
	{
		if (render.sprite == render2.sprite)
		{ 
			return;
		}
		if (time != 0)
		StartCoroutine(WaitToSwap(render2, time));
        else
        {
			Sprite tempSprite = render2.sprite;
			render2.sprite = render.sprite;
			render.sprite = tempSprite;
		}
	}

	private Tile GetAdjacent(Vector2 castDir)
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir, 1);
		if (hit.collider != null)
		{
			return hit.collider.gameObject.GetComponent<Tile>();
		}
		return null;
	}

	public List<Tile> GetAllAdjacentTiles()
	{
		neighborTiles = new List<Tile>();
		for (int i = 0; i < adjacentDirections.Length; i++)
		{
			neighborTiles.Add(GetAdjacent(adjacentDirections[i]));
		}
		return neighborTiles;
	}

	private List<Tile> FindMatch(Vector2 castDir)
	{ 
		List<Tile> matchingTiles = new List<Tile>(); 
		RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir);

		while (hit.collider != null && hit.collider.GetComponent<Tile>().Render.sprite == render.sprite && currentType != TypeTile.Bomb)
		{
			matchingTiles.Add(hit.collider.gameObject.GetComponent<Tile>());
			hit = Physics2D.Raycast(hit.collider.transform.position, castDir);
		}

		return matchingTiles; 
	}

	private int ClearMatch(Vector2[] paths) 
	{
		List<Tile> matchingTiles = new List<Tile>(); 
		for (int i = 0; i < paths.Length; i++)
		{
			matchingTiles.AddRange(FindMatch(paths[i]));
		}
		if (matchingTiles.Count >= 2)
		{
			for (int i = 0; i < matchingTiles.Count; i++)
			{
				matchingTiles[i].render.sprite = null;
			}
			matchFound = true;
		}

		return matchingTiles.Count;
	}

	public bool ClearAllMatches()
	{
		if (render.sprite == null)
			return false;

		bool isMathFind = false;

		int sumScore = 1;

		sumScore += ClearMatch(new Vector2[2] { Vector2.left, Vector2.right });
		sumScore += ClearMatch(new Vector2[2] { Vector2.up, Vector2.down });
		if (matchFound)
		{
			render.sprite = null;
			isMathFind = true; 
			matchFound = false;

			Bootstrap.OnChangeScoreValue?.Invoke(sumScore);
		}
        else
        {
			isMathFind = false;
        }

		StopCoroutine(BoardManager.instance.FindNullTiles());
		StartCoroutine(BoardManager.instance.FindNullTiles());

		return isMathFind;
	}
}