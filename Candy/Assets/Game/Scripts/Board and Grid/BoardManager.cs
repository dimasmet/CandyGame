
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour {
	public static BoardManager instance;
	[SerializeField] private List<Sprite> characters = new List<Sprite>();
	[SerializeField] private Sprite _bombSprite;

	[SerializeField] Tile tile;
	[SerializeField] int xSize, ySize;

	private Tile[,] tiles;

	public bool IsShifting { get; set; }

	[SerializeField] private int _countBomb;

	private Vector2 offsetSprite;

	[SerializeField] private Button _ClearOtherBtn;

	private int[,] matrixClassic = {
		{1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1}
	};

	private int[,] currentMatrix = new int[7,7];

	private void Awake()
    {
		_ClearOtherBtn.onClick.AddListener(() =>
		{
			StopAllCoroutines();
			StartCoroutine(WaitToClearAllMatchesOnScene());
		});
	}

    void Start () {
		if (instance == null)
			instance = this;

		offsetSprite = tile.GetComponent<SpriteRenderer>().bounds.size;
		offsetSprite.x += 0.05f;
		offsetSprite.y += 0.05f;

		float startX = transform.position.x;
		float startY = transform.position.y;

		tiles = new Tile[xSize, ySize];

		for (int x = 0; x < xSize; x++)
		{
			for (int y = 0; y < ySize; y++)
			{
				Tile newTile = Instantiate(tile, new Vector3(startX + (offsetSprite.x * x), startY + (offsetSprite.y * y), 0), tile.transform.rotation);
				tiles[x, y] = newTile;

				newTile.transform.parent = transform;

				newTile.SetTiteData(x, y);
				newTile.SetType(null, Tile.TypeTile.Classic);
			}
		}

		Bootstrap.OnStartGame += CreateBoard;
	}

    private void OnDisable()
    {
		Bootstrap.OnStartGame -= CreateBoard;
	}


	private void CreateBoard ()
	{
		_countBomb = 5;

		Tile.GlobalActive = true;

		float startX = transform.position.x;
		float startY = transform.position.y;

		Sprite[] previousLeft = new Sprite[ySize];
		Sprite previousBelow = null;

		currentMatrix = matrixClassic;

		for (int x = 0; x < xSize; x++)
		{
			for (int y = 0; y < ySize; y++)
			{
				List<Sprite> possibleCharacters = new List<Sprite>();
				possibleCharacters.AddRange(characters);

				possibleCharacters.Remove(previousLeft[y]);
				possibleCharacters.Remove(previousBelow);

				Sprite newSprite = null;
				if (currentMatrix[x, y] == 1)
				{
					int rnd = Random.Range(0, possibleCharacters.Count);
					newSprite = possibleCharacters[rnd];

					tiles[x, y].SetTiteData(x, y);
					tiles[x, y].SetType(newSprite, Tile.TypeTile.Classic);
				}
                else
                {
					newSprite = null;
					tiles[x, y].SetTiteData(x, y);
					tiles[x, y].SetType(newSprite, Tile.TypeTile.Classic);
				}

				previousLeft[y] = newSprite;
				previousBelow = newSprite;
			}
		}		
    }

	public void RespawnField(bool isRestartGame)
    {
		if (isRestartGame == true)
		{
			Bootstrap.OnStartGame?.Invoke();
		}
        else
        {
			CreateBoard();
        }
	}

	public void DestroyCrystalSprite(Sprite sprite)
    {
		int countTile = 0;
		for (int x = 0; x < xSize; x++)
		{
			for (int y = 0; y < ySize; y++)
			{
				if (tiles[x, y].Render.sprite == sprite)
                {
					tiles[x, y].PlayAnimations(Tile.TypeAnimation.Boom);
					countTile++;
				}
			}
		}

		Bootstrap.OnChangeScoreValue(countTile);

		StopAllCoroutines();
		StartCoroutine(FindNullTiles());
	}

	public void Exploshion(int x, int y)
    {
		tiles[x, y].PlayAnimations(Tile.TypeAnimation.Boom);

		List<Tile> tilesAdjacent = tiles[x, y].GetAllAdjacentTiles();

		int countBoomItem = 1;
		for(int i = 0; i < tilesAdjacent.Count; i++)
        {
			if (tilesAdjacent[i] != null)
			{
				tilesAdjacent[i].PlayAnimations(Tile.TypeAnimation.Boom);
				countBoomItem++;

				/*if (tilesAdjacent[i].currentType == Tile.TypeTile.Bomb)
                {
					Exploshion(tilesAdjacent[i].x, tilesAdjacent[i].y);
				}*/
			}
		}

		Bootstrap.OnChangeScoreValue(countBoomItem);

		StartCoroutine(FindNullTiles());
	}

	public IEnumerator FindNullTiles()
	{
		for (int x = 0; x < xSize; x++)
		{
			for (int y = 0; y < ySize; y++)
			{
				if (tiles[x, y].Render.sprite == null && currentMatrix[x,y] != 0)
				{
					Tile.GlobalActive = false;
					yield return StartCoroutine(ShiftTilesDown(x, y));
					break;
				}
			}
		}
	}

	public IEnumerator WaitToClearAllMatchesOnScene()
    {
		yield return new WaitForSeconds(0.5f);
		for (int x = 0; x < xSize; x++)
		{
			for (int y = 0; y < ySize; y++)
			{
				if (tiles[x, y].Render.sprite != null && tiles[x, y].currentType != Tile.TypeTile.Bomb)
					tiles[x, y].ClearAllMatches();
			}
		}

		Tile.GlobalActive = true;
	}

	private IEnumerator ShiftTilesDown(int x, int yStart, float shiftDelay = .03f)
	{
		IsShifting = true;
		List<Tile> tilesToMove = new List<Tile>();
		int nullCount = 0;

		for (int y = yStart; y < ySize; y++)
		{  
			if (this.tiles[x, y].Render.sprite == null && this.tiles[x, y] && currentMatrix[x,y] != 0)
			{ 
				nullCount++;
			}
			if (currentMatrix[x, y] == 1)
			{
					tilesToMove.Add(this.tiles[x, y]);
			}
		}

		for (int i = 0; i < nullCount; i++)
		{
			//Debug.Log("CreateNEw = " + tilesToMove.Count);
			yield return new WaitForSeconds(shiftDelay);
			for (int k = 0; k < tilesToMove.Count - 1; k++)
			{ 
				tilesToMove[k].SetType(tilesToMove[k + 1].Render.sprite, tilesToMove[k + 1].currentType);
				tilesToMove[k + 1].PlayAnimations(Tile.TypeAnimation.Open);
			}

			// New Sprite
			if (currentMatrix[x, ySize - i - 1] == 1)
			{
				int rndValue = Random.Range(0, 100);

				if (rndValue < 10 && _countBomb > 0)
				{
					_countBomb--;
					this.tiles[x, ySize - i - 1].SetType(_bombSprite, Tile.TypeTile.Bomb);
				}
				else
				{
					this.tiles[x, ySize - i - 1].SetType(GetNewSprite(x, ySize - 1), Tile.TypeTile.Classic);
				}
			}
		}
		IsShifting = false;

		StopAllCoroutines();
		StartCoroutine(WaitToClearAllMatchesOnScene());
	}

	public void FillField()
    {
		for (int x = 0; x < xSize; x++)
		{
			for (int y = 0; y < ySize; y++)
			{
				if (currentMatrix[x,y] == 1 && tiles[x,y].Render.sprite == null)
                {
					tiles[x, y].Render.sprite = GetNewSprite(x, y);
				}
			}
		}
	}

	private Sprite GetNewSprite(int x, int y)
	{
		List<Sprite> possibleCharacters = new List<Sprite>();
		Sprite spriteTile;
		possibleCharacters.AddRange(characters);
		if (x > 0)
		{
			possibleCharacters.Remove(tiles[x - 1, y].Render.sprite);
		}
		if (x < xSize - 1)
		{
			possibleCharacters.Remove(tiles[x + 1, y].Render.sprite);
		}
		if (y > 0)
		{
			possibleCharacters.Remove(tiles[x, y - 1].Render.sprite);
		}
		spriteTile = possibleCharacters[Random.Range(0, possibleCharacters.Count)];
		return spriteTile;
	}
}
