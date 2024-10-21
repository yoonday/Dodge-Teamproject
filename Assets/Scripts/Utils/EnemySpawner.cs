using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;

public class EnemySpawner : Singleton<EnemySpawner>
{
    [SerializeField] EnemyInfo[] enemyInfos;
    [SerializeField] float spawnCooltime;
    [SerializeField] int levelUpCount;

    private int currentLevel;
    private int destroyedEnemyCount;

    private float randomPosRangeXMin;
    private float randomPosRangeXMax;
    private float posY;

    private bool isBoss = false;

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
            bool bossSpawned = false;

            currentLevel = destroyedEnemyCount / levelUpCount + 1;
            currentLevel = Mathf.Clamp(currentLevel, 1, 3);

            EnemyAttackSO enemySO;

            if (isBoss)
            {
                bossSpawned = true;

                int currentLevelBoss = 5 * (currentLevel - 1) - 1;

                Debug.Log(currentLevelBoss);
                enemySO = enemyInfos[currentLevelBoss].enemyAttackSO;
            }
            else
            {
                var tempEnemies = from info in enemyInfos
                                  where info.level == currentLevel
                                  select info.enemyAttackSO;

                currentEnemies = tempEnemies.ToArray();

                int rand = Random.Range(0, currentEnemies.Length - 1);

                enemySO = currentEnemies[rand];
            }

            Vector2 randInstPos = new(Random.Range(randomPosRangeXMin, randomPosRangeXMax), posY);

            var enemy = GameManager.Instance.ObjectPool.SpawnFromPool("Enemy");
            enemy.transform.position = randInstPos;

            enemy.GetComponent<DodgeEnemyController>().Init(enemySO);

            yield return new WaitForSeconds(spawnCooltime);

            if (bossSpawned)
                yield return new WaitUntil(() => isBoss == false);
        }
    }

    public void EnemyDestroyed(bool isBoss)
    {
        destroyedEnemyCount++;

        if(isBoss)
            this.isBoss = !isBoss;

        if (destroyedEnemyCount % levelUpCount == 0 && destroyedEnemyCount != 0) this.isBoss = true;
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