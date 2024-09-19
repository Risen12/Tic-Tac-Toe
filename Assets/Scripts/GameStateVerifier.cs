using System;
using UnityEngine;

public class GameStateVerifier : MonoBehaviour
{
    [SerializeField] private GridMaker _gridMaker;

    private State _state;
    private int _rowsCount;
    private int _columnsCount;

    public event Action GameFinished;

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
    }

    private void OnStateChanged()
    {
        if (VerifyColumns() || VerifyRows())
            GameFinished?.Invoke();
    }

    private bool VerifyColumns()
    {
        bool result = true;
        Cell[] cells = _gridMaker.GetCells();
        Sprite currentSprite = null;

        for (int i = 0; i < _columnsCount; i++)
        {
            currentSprite = cells[i].GetCurrentSprite();

            if (currentSprite == null)
            {
                result = false;
                continue;
            }

            result = true;

            for (int j = i; j < cells.Length; j += _columnsCount)
            {
                if (cells[j].GetCurrentSprite() != currentSprite)
                {
                    result = false;
                    break;
                }
            }

            if (result)
                return result;
        }

        return result;
    }

    private bool VerifyRows()
    {
        bool result = true;
        Cell[] cells = _gridMaker.GetCells();
        Sprite currentSprite = null;

        for (int i = 0; i < _rowsCount; i += _columnsCount)
        {
            currentSprite = cells[i].GetCurrentSprite();

            if (currentSprite == null)
            {
                result = false;
                continue;
            }

            result = true;

            for (int j = i; j < i + _columnsCount; j++)
            {
                if (cells[j].GetCurrentSprite() != currentSprite)
                {
                    result = false;
                    break;
                }
            }

            if (result)
                return result;
        }

        return result;
    }
}