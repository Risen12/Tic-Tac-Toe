using System;
using UnityEngine;

[RequireComponent(typeof(EnemyPathFinder))]
public class EnemyGameManager : MonoBehaviour
{
    [SerializeField] private Null _nullPrefab;
    [SerializeField] private GameState _side;
    [SerializeField] private GridMaker _gridMaker;

    private State _gameState;
    private EnemyPathFinder _enemyPathFinder;

    public event Action MoveMaked;

    private void Awake()
    {
        _enemyPathFinder = GetComponent<EnemyPathFinder>();
    }

    public void Init(State state)
    {
        _gameState = state;
    }

    public void MakeMove() 
    {
        SetFigure();
        _gameState.Change();
        MoveMaked?.Invoke();
    }

    private void SetFigure()
    {
        Coordinate coordinates = _enemyPathFinder.GetBestPosition();

        _gridMaker.SetNullFigure(coordinates, _nullPrefab);
    }
}
