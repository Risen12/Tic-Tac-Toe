using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup), typeof(RectTransform))]
public class GridMaker : MonoBehaviour
{
    [SerializeField] private Cell _cellPrefab;
    [SerializeField] private int _rowCount;
    [SerializeField] private int _columnCount;
    [SerializeField] private int _offset;

    private GridLayoutGroup _gridLayoutGroup;
    private RectTransform _rectTransform;
    private int _cellHeight;
    private int _cellWidth;
    private Cell[] _cells;
    private int _cellIndex;

    public int RowsCount => _rowCount;
    public int ColumnsCount => _columnCount;

    private void Awake()
    {
        _gridLayoutGroup = GetComponent<GridLayoutGroup>();
        _rectTransform = GetComponent<RectTransform>();
        _cells = new Cell[_rowCount * _columnCount];
        _cellIndex = 0;

        DefineParameters();
        GenerateGrid();
    }

    private void DefineParameters()
    { 
        int height = (int)_rectTransform.rect.height;
        int width = (int)_rectTransform.rect.width;

        _cellHeight = (height / _rowCount) - (_offset * _rowCount);
        _cellWidth = (width / _columnCount) - (_offset * _columnCount);

        SetParameters(_cellHeight, _cellWidth, _offset);
    }

    private void GenerateGrid()
    {
        for (int i = 0; i < _rowCount; i++)
        {
            for (int j = 0; j < _columnCount; j++)
            {
               Cell cell = Instantiate(_cellPrefab, transform);
                _cells[_cellIndex++] = cell;
            }
        }
    }

    private void SetParameters(float cellHeight, float cellWidth, float offset)
    {
        _gridLayoutGroup.cellSize = new Vector2(cellWidth, cellHeight);
        _gridLayoutGroup.spacing = new Vector2(offset, offset);
    }

    public Cell[] GetCells()
    {
        Cell[] gridCells = new Cell[_cells.Length];

        for (int i = 0; i < gridCells.Length; i++)
        {
            gridCells[i] = _cells[i];
        }

        return gridCells;
    }
}
