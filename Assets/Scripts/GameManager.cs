using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GridMaker _gridMaker;
    [SerializeField] private GameState _side;
    [SerializeField] private Null _nullPrefab;
    [SerializeField] private Chest _chestPrefab;
    [SerializeField] private GameStateVerifier _gameStateVerifier;
    [SerializeField] private EnemyGameManager _enemyGameManager;

    private State _gameState;

    private void Start()
    {
        _gameState = new State();
        _gameState.SetFirstState(_side);
        _gameStateVerifier.Init(_gameState);

        _gameStateVerifier.GameFinished += OnGameFinished;

        SubscribeCellsEvents();
    }

    private void OnDisable()
    {
        UnsubscribeCellsEvents();
        _gameStateVerifier.GameFinished -= OnGameFinished;
    }

    private void OnCellClicked(Cell cell)
    {
        if (_gameState.GetCurrent() == GameState.NullPlayerState)
            cell.SetNullFigure(_nullPrefab);
        else
            cell.SetChestFigure(_chestPrefab);

        _gameState.Change();
        _enemyGameManager.SetFigure();
    }

    private void SubscribeCellsEvents()
    {
        Cell[] cells = _gridMaker.GetCells();

        foreach (Cell cell in cells)
        { 
            cell.Clicked += OnCellClicked;
        }
    }

    private void UnsubscribeCellsEvents()
    {
        Cell[] cells = _gridMaker.GetCells();

        foreach (Cell cell in cells)
        {
            cell.Clicked -= OnCellClicked;
        }
    }

    private void OnGameFinished()
    {
        Debug.Log($"Игра завершена, победили крестики!");
    }
}
