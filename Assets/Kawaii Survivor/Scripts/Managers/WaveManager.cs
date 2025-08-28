using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class WaveManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float waveDuration;
    private float waveTimer;
    
    [Header("Waves")]
    [SerializeField] private Wave[] waves;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(waveTimer < waveDuration)
            ManageCurrentWave();
        
    }

    private void ManageCurrentWave()
    {
        Wave currentWave = waves[0];

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
                Instantiate(currentWaveSegment.prefab, Vector2.zero, Quaternion.identity, transform);
                currentWaveSegment.counter += 1;
            }
        }
        waveTimer += Time.deltaTime;
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
