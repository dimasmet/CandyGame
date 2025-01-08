using UnityEngine;

public class HandlerMatch : MonoBehaviour
{
    public static HandlerMatch Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public Tile _firstChoiceTile;
    public Tile _secondChoiceTile;

    private bool isHammerActive = false;

    public void ActiveBonusHammer()
    {
        isHammerActive = true;
    }

    public void ChoiceTile(Tile tile)
    {
        SoundsHandler.sound.PlayShotSound(SoundsHandler.NameSoundGame.Click);

        if (isHammerActive == true)
        {
            isHammerActive = false;
            ButtonBonus.globalActive = false;
            tile.BoomTile();
            BonusesHandler.I.ActionCompleted();
        }
        else
        {
            if (_firstChoiceTile == null)
            {
                _firstChoiceTile = tile;

                _firstChoiceTile.Select();
            }
            else
            {
                if (_secondChoiceTile == _firstChoiceTile)
                {
                    _firstChoiceTile.Deselect();
                }
                else
                {
                    _secondChoiceTile = tile;

                    if (_secondChoiceTile.GetAllAdjacentTiles().Contains(_firstChoiceTile))
                    {
                        _secondChoiceTile.SwapSprite(_firstChoiceTile.Render);

                        _firstChoiceTile.Deselect();
                        _secondChoiceTile.Deselect();

                        bool isSuccFirst = _firstChoiceTile.ClearAllMatches();
                        bool isSuccSecond = _secondChoiceTile.ClearAllMatches();

                        if (isSuccFirst == false && isSuccSecond == false)
                        {
                            _secondChoiceTile.PlayAnimations(Tile.TypeAnimation.False);
                            _firstChoiceTile.PlayAnimations(Tile.TypeAnimation.False);
                            _firstChoiceTile.SwapSprite(_secondChoiceTile.Render, 0.5f);

                            SoundsHandler.sound.PlayShotSound(SoundsHandler.NameSoundGame.False);
                        }
                        else
                        {
                            SoundsHandler.sound.PlayShotSound(SoundsHandler.NameSoundGame.Success);
                        }

                        _firstChoiceTile = null;
                        _secondChoiceTile = null;

                        //StartCoroutine(BoardManager.instance.WaitToClearAllMatchesOnScene());
                    }
                    else
                    {
                        _firstChoiceTile.Deselect();
                        _secondChoiceTile.Deselect();

                        _firstChoiceTile = _secondChoiceTile;
                        _firstChoiceTile.Select();

                        _secondChoiceTile = null;
                    }
                }
            }
        }
    }
}
