using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class EnemySpawner : Singleton<EnemySpawner>
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] EnemyInfo[] enemyInfos;
    [SerializeField] float spawnCooltime;
    [SerializeField] int eliteMobFreq;
    [SerializeField] int bossMobFreq;

    private int currentLevel;
    private int destroyedEnemyCount;

    private float randomPosRangeXMin;
    private float randomPosRangeXMax;
    private float posY;

    EnemyAttackSO[] currentEnemies;


    void Start()
    {
        destroyedEnemyCount = 0;
        SetRange();

        StartCoroutine(SpawnEnemy());

        SceneManager.sceneLoaded += DestroyEnemySpawner;
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            currentLevel = 1;

            var tempEnemies = from info in enemyInfos
                                 where info.level <= currentLevel
                                 select info.enemyAttackSO;

            currentEnemies = tempEnemies.ToArray();

            int rand = Random.Range(0, currentEnemies.Length);

            Vector2 randInstPos = new(Random.Range(randomPosRangeXMin, randomPosRangeXMax), posY);

            var enemy = Instantiate(enemyPrefab, randInstPos, enemyPrefab.transform.rotation);
            enemy.GetComponent<DodgeEnemyController>().Init(1, currentEnemies[rand]);

            yield return new WaitForSeconds(spawnCooltime);
        }
    }

    public void EnemyDestroyed()
    {
        destroyedEnemyCount++;
    }

    private void SetRange()
    {
        Camera mainCamera = Camera.main;

        float cameraHeight = mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * ((float)Screen.width / Screen.height);

        posY = mainCamera.transform.position.y + cameraHeight * 2; // 화면 맨 위 좌표.

        randomPosRangeXMin = mainCamera.transform.position.x - cameraWidth; // 화면 맨 왼쪽 좌표.
        randomPosRangeXMax = mainCamera.transform.position.x + cameraWidth; // 화면 맨 오른쪽 좌표.
    }

    private void DestroyEnemySpawner(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Dodge_Project_Start")
        {
            SceneManager.sceneLoaded -= DestroyEnemySpawner;
            Destroy(gameObject);
        }
    }
}

[System.Serializable]
public struct EnemyInfo
{
    public int level;
    public EnemyAttackSO enemyAttackSO;
}