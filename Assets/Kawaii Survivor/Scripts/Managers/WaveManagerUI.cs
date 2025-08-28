using UnityEngine;
using TMPro;


public class WaveManagerUI : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private TextMeshProUGUI waveTimerText;
    [SerializeField] private TextMeshProUGUI waveText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    public void UpdateWaveText(string waveString) =>  waveText.text = waveString;
    public void UpdateTimerText(string timerString) =>  waveTimerText.text = timerString;
}
