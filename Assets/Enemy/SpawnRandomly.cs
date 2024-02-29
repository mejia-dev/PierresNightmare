using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomly : MonoBehaviour
{
    // public GameObject enemyPrefab;
    [SerializeField] GameObject[] enemyPrefabs;
    public GameObject levelingSystem;
    public int firstSpawnTime;
    public int subsequentSpawnDelay;
    int currentLevel;

    void Start()
    {
        levelingSystem = GameObject.Find("LevelingSystem");
        currentLevel = levelingSystem.GetComponent<Leveling>().GameLevel;
        Debug.Log(currentLevel);
        InvokeRepeating("MultiSpawnEnemy", firstSpawnTime, subsequentSpawnDelay);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        Vector3 randomSpawnPos=new Vector3(Random.Range(-10,11) + transform.position.x,transform.position.y + 3,Random.Range(-10,11) + transform.position.z);
        Instantiate(GetRandomEnemy(),randomSpawnPos,Quaternion.identity);
    }

    public void MultiSpawnEnemy()
    {
        currentLevel = levelingSystem.GetComponent<Leveling>().GameLevel;
        for (int i = 0; i < currentLevel; i++)
        {
            SpawnEnemy();
        }
    }

    public GameObject GetRandomEnemy()
    {
        int getRandom = Random.Range(0,4);
        return enemyPrefabs[getRandom];
    }
}
