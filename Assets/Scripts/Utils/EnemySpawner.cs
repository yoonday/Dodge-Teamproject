using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] EnemyInfo[] enemyInfos;
    [SerializeField] float spawnCooltime;
    [SerializeField] float levelUpTime;

    private float gameTime;
    private int currentLevel;

    private float randomPosRangeXMin;
    private float randomPosRangeXMax;
    private float posY;

    EnemyAttackSO[] currentEnemies;


    void Start()
    {
        Camera mainCamera = Camera.main;

        float cameraHeight = mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * ((float)Screen.width / Screen.height);

        posY = mainCamera.transform.position.y + cameraHeight * 2; // 화면 맨 위 좌표.

        randomPosRangeXMin = mainCamera.transform.position.x - cameraWidth; // 화면 맨 왼쪽 좌표.
        randomPosRangeXMax = mainCamera.transform.position.x + cameraWidth; // 화면 맨 오른쪽 좌표.

        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            currentLevel = (int)(gameTime / levelUpTime) + 1;

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
}

[System.Serializable]
public struct EnemyInfo
{
    public int level;
    public EnemyAttackSO enemyAttackSO;
}