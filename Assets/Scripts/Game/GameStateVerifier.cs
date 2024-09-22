using System;
using UnityEngine;

public class GameStateVerifier : MonoBehaviour
{
    [SerializeField] private GridMaker _gridMaker;
    [SerializeField] private Sprite _nullFigure;
    [SerializeField] private Sprite _chestFigure;

    private State _state;
    private int _rowsCount;
    private int _columnsCount;
    private Cell[,] _cells;
    private string _winnerName;

    public event Action<string> GameFinished;

    private void OnDisable()
    {
        _state.Changed -= OnStateChanged;
    }

    public void Init(State state)
    {
        _state = state;
        _state.Changed += OnStateChanged;
        _rowsCount = _gridMaker.RowsCount;
        _columnsCount = _gridMaker.ColumnsCount;
        _cells = _gridMaker.GetCells();
    }

    private void OnStateChanged()
    {
        if (VerifyColumns() || VerifyRows())
            GameFinished?.Invoke(_winnerName);
    }

    private bool VerifyColumns()
    {
        bool result = true;
        Sprite currentSprite;

        for (int i = 0; i < _columnsCount; i++)
        {
            currentSprite = _cells[0, i].GetCurrentSprite();

            if (currentSprite == null)
            {
                result = false;
                continue;
            }

            result = true;

            for (int j = 0; j < _rowsCount; j++)
            {
                if (_cells[j, i].GetCurrentSprite() != currentSprite)
                {
                    result = false;
                    break;
                }
            }

            if (result)
            {
                if (currentSprite == _nullFigure)
                    _winnerName = "Нолики";
                else
                    _winnerName = "Крестики";

                return result;
            }
        }

        return result;
    }

    private bool VerifyRows()
    {
        bool result = true;
        Sprite currentSprite;

        for (int i = 0; i < _rowsCount; i++)
        {
            currentSprite = _cells[i, 0].GetCurrentSprite();

            if (currentSprite == null)
            {
                result = false;
                continue;
            }

            result = true;

            for (int j = 0; j < _columnsCount; j++)
            {
                if (_cells[i, j].GetCurrentSprite() != currentSprite)
                {
                    result = false;
                    break;
                }
            }

            if (result)
            {
                if (currentSprite == _nullFigure)
                    _winnerName = "Нолики";
                else
                    _winnerName = "Крестики";

                return result;
            }
        }

        return result;
    }
}