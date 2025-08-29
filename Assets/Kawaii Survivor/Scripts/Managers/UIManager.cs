using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour, IGameStateListener
{
    [Header("Panels")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject waveTransitionPanel;
    
    private List<GameObject> menuPanels = new List<GameObject>();

    private void Awake()
    {
        menuPanels.AddRange(new GameObject[] 
            { menuPanel, gamePanel, shopPanel, waveTransitionPanel });
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStateChangedCallback(GameState state)
    {
        switch (state)
        {
            case GameState.MENU:
                EnablePanel(menuPanel);
                break;
            case GameState.GAME:
                EnablePanel(gamePanel);
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
