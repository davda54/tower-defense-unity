using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public int Amount;
        public GameObject Enemy;
        public float SpawnTime;
        public float RestTime;
    }

    public List<Wave> Waves;
    private int waveIndex = 0;
    private Wave currentWave;
    private float spawnTime = 0.0f;

	// Use this for initialization
	void OnEnable ()
    {
        currentWave = Waves[0];
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (waveIndex >= Waves.Count) return;

        if (currentWave.RestTime < 0)
        {
            waveIndex += 1;
            if (waveIndex >= Waves.Count) return;

            currentWave = Waves[waveIndex];
            return;
        }

        if(currentWave.Amount <= 0)
        {
            currentWave.RestTime -= Time.deltaTime;
            return;
        }

        if(spawnTime < 0)
        {
            Spawn(currentWave.Enemy);

            spawnTime = currentWave.SpawnTime;
            currentWave.Amount--;
            return;
        }

        spawnTime -= Time.deltaTime;        
	}

    private void Spawn(GameObject prototype)
    {
        var spawnedEnemy = Pool.Instance.ActivateObject(prototype.tag);
        spawnedEnemy.SetActive(true);

        EnemyManagerScript.Instance.RegisterEnemy(spawnedEnemy);
    }
}

