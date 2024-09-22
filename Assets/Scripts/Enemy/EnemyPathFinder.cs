using JetBrains.Annotations;
using UnityEngine;

public class EnemyPathFinder : MonoBehaviour
{
    private const int RowIndex = 0;
    private const int ColumnIndex = 1;

    [SerializeField] private GridMaker _gridMaker;
    [SerializeField] private Sprite _nullSprite;
    [SerializeField] private Sprite _chestSprite;

    private Cell[,] _cells;
    private int _bestRow;
    private int _bestColumn;

    private void Start()
    {
        _cells = _gridMaker.GetCells();
        _bestRow = 0;
        _bestColumn = 0;
    }

    public Coordinate GetBestPosition()
    {
        int minValue = 0;
        int maxValue = 10;
        int middleValue = 5;

        Coordinate coordinates;

        int rowPriority = VerifyRows();
        int columnPriority = VerifyColumns();

        if (rowPriority > columnPriority)
        {
            coordinates = GetEmptySpaceCoordinatesByRow(_bestRow);
        }
        else if (columnPriority > rowPriority)
        {
            coordinates = GetEmptySpaceCoordinatesByColumn(_bestColumn);
        }
        else
        {
            if (Random.Range(minValue, maxValue) >= middleValue)
            {
                coordinates = GetEmptySpaceCoordinatesByRow(_bestRow);
            }
            else
            {
                coordinates = GetEmptySpaceCoordinatesByColumn(_bestRow);
            }
        }

        return coordinates;
    }

    private int VerifyRows()
    {
        int maxPriority = 2;
        int priority = 0;

        for (int i = 0; i < _cells.GetLength(RowIndex); i++)
        {
            Cell[] currentRow = new Cell[_cells.GetLength(ColumnIndex)];

            for (int j = 0; j < _cells.GetLength(ColumnIndex); j++)
            {
                currentRow[j] = _cells[i, j];
            }

            int currentPriority = VerifyArray(currentRow);

            if (currentPriority > priority)
            { 
                priority = currentPriority;
                _bestRow = i;
            }

            if (priority == maxPriority)
                return priority;
        }

        return priority;
    }

    private int VerifyColumns()
    {
        int maxPriority = 2;
        int priority = 0;

        for (int i = 0; i < _cells.GetLength(ColumnIndex); i++)
        {
            Cell[] currentColumn = new Cell[_cells.GetLength(RowIndex)];

            for (int j = 0; j < _cells.GetLength(RowIndex); j++)
            {
                currentColumn[j] = _cells[j, i];
            }

            int currentPriority = VerifyArray(currentColumn);

            if (currentPriority > priority)
            {
                priority = currentPriority;
                _bestColumn = i;

                if (priority == maxPriority)
                    return priority;
            }
        }

        return priority;
    }

    private int VerifyArray(Cell[] cells)
    {
        int priority;
        int maxCount = 2;

        if (VerifyEmptySpaces(cells))
        {
            if (VerifyNullFigures(cells) && VerifyChestFigures(cells))
            {
                priority = 0;
            }
            else if (VerifyChestFigures(cells) == false && VerifyNullFigures(cells))
            {
                if (GetNullFiguresCount(cells) == maxCount)
                {
                    priority = 2;
                }
                else
                {
                    priority = 1;
                }
            }
            else if (VerifyChestFigures(cells) && VerifyNullFigures(cells) == false)
            {
                if (GetChestFiguresCount(cells) == maxCount)
                {
                    priority = 2;
                }
                else
                {
                    priority = 0;
                }
            }
            else
            {
                priority = 0;
            }
        }
        else
        {
            priority = -1;
        }

        return priority;
    }

    private bool VerifyEmptySpaces(Cell[] cells)
    {
        foreach (Cell cell in cells)
        {
            if (cell.GetCurrentSprite() == null)
                return true;
        }

        return false;
    }

    private bool VerifyNullFigures(Cell[] cells)
    {
        foreach (Cell cell in cells)
        { 
            if(cell.GetCurrentSprite() == _nullSprite)
                return true;
        }

        return false;
    }

    private bool VerifyChestFigures(Cell[] cells)
    {
        foreach (Cell cell in cells)
        {
            if (cell.GetCurrentSprite() == _chestSprite)
                return true;
        }

        return false;
    }

    private int GetNullFiguresCount(Cell[] cells)
    {
        int count = 0;
        foreach (Cell cell in cells)
        {
            if (cell.GetCurrentSprite() == _nullSprite)
                count++;
        }

        return count;
    }

    private int GetChestFiguresCount(Cell[] cells)
    {
        int count = 0;
        foreach (Cell cell in cells)
        {
            if (cell.GetCurrentSprite() == _chestSprite)
                count++;
        }

        return count;
    }

    private int GetEmptyPosition(Cell[] cells)
    {
        int index = 0;

        for (int i = 0; i < cells.Length; i++)
        {
            if (cells[i].IsBusy == false)
            {
                index = i;
                return index;
            }
        }

        return index;
    }

    private Coordinate GetEmptySpaceCoordinatesByRow(int bestRow)
    {
        int column;

        Cell[] tempCells = new Cell[_cells.GetLength(ColumnIndex)];

        for (int j = 0; j < _cells.GetLength(ColumnIndex); j++)
        {
            tempCells[j] = _cells[bestRow, j];
        }

        column = GetEmptyPosition(tempCells);

        return new Coordinate(bestRow, column);
    }

    private Coordinate GetEmptySpaceCoordinatesByColumn(int bestColumn)
    {
        int row;

        Cell[] tempCells = new Cell[_cells.GetLength(RowIndex)];

        for (int i = 0; i < _cells.GetLength(RowIndex); i++)
        {
            tempCells[i] = _cells[i, bestColumn];
        }

        row = GetEmptyPosition(tempCells);

        return new Coordinate(row, bestColumn);
    }
}
