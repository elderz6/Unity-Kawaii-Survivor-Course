using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void StartGame() => SetGameState(GameState.GAME);
    public void StartWeaponSelect() => SetGameState(GameState.WEAPONSELECTION);
    public void StartShop() => SetGameState(GameState.SHOP);

    public void WaveCompletedCallback()
    {
        if (Player.instance.HasLeveledUp())
            SetGameState(GameState.WAVETRANSITION);
        else
            SetGameState(GameState.SHOP);
    }

    public void SetGameState(GameState state)
    {
        IEnumerable<IGameStateListener> gameStateListeners =
            FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IGameStateListener>();

        foreach (IGameStateListener gameStateListener in gameStateListeners)
            gameStateListener.GameStateChangedCallback(state);
    }

    public void ManageGameOver()
    {
        SceneManager.LoadScene(0);
    }
}


public interface IGameStateListener
{
    void GameStateChangedCallback(GameState state);
}