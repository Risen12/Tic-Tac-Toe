using UnityEngine;

public class EnemyGameManager : MonoBehaviour
{
    [SerializeField] private Null _nullPrefab;
    [SerializeField] private GameState _side;
    [SerializeField] private GridMaker _gridMaker;

    public void SetFigure()
    {
        Cell[] cells = _gridMaker.GetCells();
        int position = ChooseNextPosition();

        cells[position].SetNullFigure(_nullPrefab);
    }

    private int ChooseNextPosition()
    {
        Cell[] cells = _gridMaker.GetCells();
        int rowsCount = _gridMaker.RowsCount;
        int columnsCount = _gridMaker.ColumnsCount;

        for (int i = 0; i < rowsCount; i += columnsCount)
        { 
            
        }
    }
}
