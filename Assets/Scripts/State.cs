using System;

public class State
{
    private GameState _currentGameState;

    public event Action Changed;

    public GameState GetCurrent() => _currentGameState;

    public void Change()
    {
        if (_currentGameState == GameState.ChestPlayerState)
            _currentGameState = GameState.NullPlayerState;
        else
            _currentGameState = GameState.ChestPlayerState;

        Changed?.Invoke();
    }

    public void SetFirstState(GameState state)
    {
        _currentGameState = state;
    }
}
