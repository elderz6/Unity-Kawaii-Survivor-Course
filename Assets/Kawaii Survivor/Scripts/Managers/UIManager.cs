using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour, IGameStateListener
{
    [Header("Panels")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject weaponSelectionPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject stageCompletePanel;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject waveTransitionPanel;
    
    private List<GameObject> menuPanels = new List<GameObject>();

    private void Awake()
    {
        menuPanels.AddRange(new GameObject[] 
            { menuPanel, gamePanel, shopPanel, waveTransitionPanel, 
                gameOverPanel, stageCompletePanel, weaponSelectionPanel });
    }

    public void GameStateChangedCallback(GameState state)
    {
        switch (state)
        {
            case GameState.MENU:
                EnablePanel(menuPanel);
                break;
            case GameState.WEAPONSELECTION:
                EnablePanel(weaponSelectionPanel);
                break;
            case GameState.GAME:
                EnablePanel(gamePanel);
                break;      
            case GameState.GAMEOVER:
                EnablePanel(gameOverPanel);
                break;
            case GameState.STAGECOMPLETE:
                EnablePanel(stageCompletePanel);
                break;
            case GameState.SHOP:
                EnablePanel(shopPanel);
                break;
            case GameState.WAVETRANSITION:
                EnablePanel(waveTransitionPanel);
                break;
        }
    }

    private void EnablePanel(GameObject panel)
    {
        foreach (GameObject obj in menuPanels)
            obj.SetActive(obj == panel);
    }

}
