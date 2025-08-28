using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[RequireComponent(typeof(WaveManagerUI))]
public class WaveManager : MonoBehaviour
{
    [Header("Elements")] 
    [SerializeField] private Player player;
    private WaveManagerUI ui;
    
    [Header("Settings")] 
    [SerializeField] private float waveDuration;
    private float waveTimer;
    private int currentWaveIndex = 0;
    private bool isTimerRunning = false;
    
    [Header("Waves")]
    [SerializeField] private Wave[] waves;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ui = GetComponent<WaveManagerUI>();
        StartWave(currentWaveIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTimerRunning) return;
        if (waveTimer < waveDuration)
        {
            ManageCurrentWave();
            string timerString = ((int)(waveDuration - waveTimer)).ToString();
            ui.UpdateTimerText(timerString);
        }
        else
            StartWaveTransition();
    }

    private void StartWaveTransition()
    {
        isTimerRunning = false;
        currentWaveIndex++;
        waveTimer = 0;

        if (currentWaveIndex >= waves.Length)
        {
            ui.UpdateTimerText("");
            ui.UpdateWaveText("Stage completed");
            return;
        }
        StartWave(currentWaveIndex);
    }

    private void StartWave(int waveIndex)
    {
        ui.UpdateWaveText("Wave " + (currentWaveIndex + 1) + " / " + waves.Length);
        isTimerRunning = true;
    }

    private void ManageCurrentWave()
    {
        Wave currentWave = waves[currentWaveIndex];

        for (int i = 0; i < currentWave.segments.Count; i++)
        {
            WaveSegment currentWaveSegment = currentWave.segments[i];

            float timeStart = currentWaveSegment.timeStartEnd.x / 100 * waveDuration;
            float timeEnd = currentWaveSegment.timeStartEnd.y / 100 * waveDuration;
            
            if(waveTimer < timeStart || waveTimer > timeEnd)
                continue;
            
            float timeSinceSegmentStart = waveTimer - timeStart;
            float spawnDelay = 1f / currentWaveSegment.spawnRate;

            if (timeSinceSegmentStart / spawnDelay > currentWaveSegment.counter)
            {
                Instantiate(currentWaveSegment.prefab, GetSpawnPosition(), Quaternion.identity, transform);
                currentWaveSegment.counter += 1;
            }
        }
        waveTimer += Time.deltaTime;
    }

    private Vector2 GetSpawnPosition()
    {
        Vector2 direction = Random.onUnitSphere;
        Vector2 offset = direction.normalized * Random.Range(8, 10);
        Vector2 targetPos = (Vector2)player.transform.position + offset;

        targetPos.x = Mathf.Clamp(targetPos.x, -18, 18);
        targetPos.y = Mathf.Clamp(targetPos.y, -10, 10);
        return targetPos;
    }
}

[System.Serializable]
public class Wave
{
    public string name;
    public List<WaveSegment> segments;
}

[System.Serializable]
public class WaveSegment
{
    [MinMaxSlider(0f, 100f)] public Vector2 timeStartEnd;
    public float spawnRate;
    public GameObject prefab;
    public int counter;
}
