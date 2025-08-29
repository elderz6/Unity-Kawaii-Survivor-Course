using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    
    void Start()
    {
        Application.targetFrameRate = 60;
        SetGameState(GameState.MENU);
    }

    public void StartGame()
    {
        Debug.Log("Starting Game");
        SetGameState(GameState.GAME);
    }

    public void StartShop()
    {
        Debug.Log("StartShop");
        SetGameState(GameState.SHOP);
    }

    public void WaveCompletedCallback()
    {
        Debug.Log("Wave Completed Level up " + Player.instance.HasLeveledUp());
        if (Player.instance.HasLeveledUp())
        {
            SetGameState(GameState.WAVETRANSITION);
        }
        else
        {
            SetGameState(GameState.SHOP);
        }
    }

    public void SetGameState(GameState state)
    {
        IEnumerable<IGameStateListener> gameStateListeners =
            FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IGameStateListener>();

        foreach (IGameStateListener gameStateListener in gameStateListeners)
            gameStateListener.GameStateChangedCallback(state);
    }
}


public interface IGameStateListener
{
    void GameStateChangedCallback(GameState state);
}