using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private Transform projectileSpawnPosition;
    [SerializeField] private GameObject projectilePrefab;

    private DodgeEnemyController controller;
    private EnemyAttackSO enemyAttack;

    private readonly float distance_Straight = 1f;


    private void Awake()
    {
        controller = GetComponent<DodgeEnemyController>();
    }

    void Start()
    {
        controller.OnAttackEvent += StartAttackCoroutine;
    }

    public void EnemyShootingInit()
    {
        enemyAttack = controller.EnemyAttack;
    }

    private void StartAttackCoroutine()
    {
        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        if (enemyAttack == null) yield break;

        for (int i = 0; i < enemyAttack.attackCountAtOnce; i++)
        {

            switch (enemyAttack.angle)
            {
                // 수직 패턴
                case EnemyAttackSO.AttackType.STRAIGHT:

                    float startPosX = projectileSpawnPosition.position.x; // 총알 발사 위치

                    if (enemyAttack.amount % 2 == 0) { startPosX -= distance_Straight / 2; } // 한 행에 총알 개수가 짝수이면 가운데에서 나가면 안되기 때문에 간격의 반 만큼 옆으로 밀어줌
                    int setStartPos = (enemyAttack.amount - 1) / 2; // 아니면 출발위치를 그냥 옆으로 밀어줌. setStartPos 는 얼마나 옆으로 밀지 개수.

                    startPosX -= distance_Straight * setStartPos; // 옆으로 밀어줌.

                    for (int j = 0; j < enemyAttack.amount; j++)
                    {
                        Vector2 spawnPosition = new(startPosX + (distance_Straight * j), projectileSpawnPosition.position.y); // 맨 옆 위치에서 하나씩 더해가면서 생성

                        var projectile = GameManager.Instance.ObjectPool.SpawnFromPool("EnemyBullet");
                        projectile.transform.SetPositionAndRotation(spawnPosition, transform.rotation);

                        projectile.GetComponent<EnemyProjectileController>().Init(transform.TransformDirection(Vector2.up).normalized, enemyAttack); // 총알 발사 방향 설정.
                    }
                    break;


                // 퍼져나가는 패턴
                case EnemyAttackSO.AttackType.ANGLED:

                    float startThetaAngled = (180 - enemyAttack.angleDeg) / 2; // 0도에서 가장 가까운 위치. 발사 각도의 중심을 90도로 놓는다고 침.
                    float thetaAngled = enemyAttack.angleDeg / (enemyAttack.amount - 1); // 발사 각도를 발사체의 수로 나눈 값.

                    for (int j = 0; j < enemyAttack.amount; j++)
                    {
                        var projectile = GameManager.Instance.ObjectPool.SpawnFromPool("EnemyBullet");
                        projectile.transform.SetPositionAndRotation(projectileSpawnPosition.position, transform.rotation);

                        float curTheta = startThetaAngled + (thetaAngled * j); // 시작 각도에서 세타 만큼 옆으로 이동하면서 발사.
                        float cos = Mathf.Cos(curTheta * Mathf.Deg2Rad);
                        float sin = Mathf.Sin(curTheta * Mathf.Deg2Rad);

                        projectile.GetComponent<EnemyProjectileController>().Init(transform.TransformDirection(new(cos, sin)).normalized, enemyAttack); // 총알 방향 세팅.
                    }
                    break;


                // 드르륵 패턴
                case EnemyAttackSO.AttackType.SPREAD:

                    // 오른쪽에서 왼쪽으로 발사할지 반대로 발사할지 설정 (층 수 = i 에 따라서 설정함)
                    float startThetaSpread;
                    if (i % 2 != 0)
                    {
                        startThetaSpread = (180 - enemyAttack.angleDeg) / 2 + enemyAttack.angleDeg;
                    }
                    else
                    {
                        startThetaSpread = (180 - enemyAttack.angleDeg) / 2;
                    }

                    // 각도 패턴과 똑같이 한 발사체당 각도 설정
                    float thetaSpread = enemyAttack.angleDeg / (enemyAttack.amount - 1);

                    for (int j = 0; j < enemyAttack.amount; j++)
                    {
                        var projectile = GameManager.Instance.ObjectPool.SpawnFromPool("EnemyBullet");
                        projectile.transform.SetPositionAndRotation(projectileSpawnPosition.position, transform.rotation);

                        // 발사 방향대로 각도 빼거나 더함.
                        float curTheta;
                        if (i % 2 != 0)
                        {
                            curTheta = startThetaSpread - (thetaSpread * j);
                        }
                        else
                        {
                            curTheta = startThetaSpread + (thetaSpread * j);
                        }

                        float cos = Mathf.Cos(curTheta * Mathf.Deg2Rad);
                        float sin = Mathf.Sin(curTheta * Mathf.Deg2Rad);

                        projectile.GetComponent<EnemyProjectileController>().Init(transform.TransformDirection(new(cos, sin)).normalized, enemyAttack); // 총알 방향 설정.

                        yield return new WaitForSeconds(enemyAttack.spreadDelay);
                    }
                    break;


                // 뾰족이 패턴
                case EnemyAttackSO.AttackType.STAR:

                    float startThetaStar = (180 - enemyAttack.angleDeg) / 2; // 위와 똑같이 시작 각도 설정.
                    float thetaStar = enemyAttack.angleDeg / (enemyAttack.amount * 2); // 조금 특별하게 발사체 개수의 2배로 나눔. (이유는 나중에 후술)
                    int middleCount = enemyAttack.attackCountAtOnce - 2; // 꼭대기 층(뾰족한 부분)과 맨 아래 층(반대 부분)을 뺀 중간 층의 개수.

                    // 각 층에 따라 발사체 개수가 다르다. 중간 층은 삼각형의 옆면을 담당해야 하기 때문에 2배가 된다.
                    int amount;
                    if (i == 0)
                    {
                        amount = enemyAttack.amount;
                    }
                    else if (i < enemyAttack.attackCountAtOnce - 1)
                    {
                        amount = enemyAttack.amount * 2;
                    }
                    else
                    {
                        amount = enemyAttack.amount + 1;
                    }


                    for (int j = 0; j < amount; j ++)
                    {
                        var projectile = GameManager.Instance.ObjectPool.SpawnFromPool("EnemyBullet");
                        projectile.transform.SetPositionAndRotation(projectileSpawnPosition.position, transform.rotation);


                        // 층에 따라 현재 발사 각도를 정한다.
                        float curTheta;
                        if (i == 0)
                        {
                            // 맨 위층은 단순하게 뾰족한 부분의 위치를 잡아주면 된다.
                            curTheta = startThetaStar + (thetaStar * (j * 2 + 1));
                        }
                        else if (i < enemyAttack.attackCountAtOnce - 1)
                        {
                            // 중간 층이 중요한데, 옆면에 따라서 한 면은 오른쪽으로 내려가면 나머지 한 면은 왼쪽으로 내려가야 하기 때문에 나머지의 각은 1의 보수가 된다.. 여기서 왼쪽일수록 각이 크고 오른쪽일수록 각이 작다.
                            float middleCoeff;

                            if (j % 2 == 0) // 삼각형의 오른쪽
                            {
                                middleCoeff = 1 - (i / (float)(middleCount + 1)); // 삼각형의 오른쪽은 점점 오른쪽으로 내려가야 하기 때문에 위층일수록 각도가 왼쪽에 있음. 그러므로 1의 보수 (위쪽일수록 각이 크므로 i = 층이 작을 때 값이 커야 한다.)
                            }
                            else // 삼각형의 왼쪽
                            {
                                middleCoeff = i / (float)(middleCount + 1); // 삼각형의 왼쪽은 점점 왼쪽으로 내려가야 하기 때문에 위층일수록 각도가 오른쪽에 있음. 그러므로 그냥 계산 (오른쪽과 반대.)
                            }

                            curTheta = startThetaStar + thetaStar * (j + middleCoeff); // 이렇게 나온 중간 층의 계수를 한 칸당 각도에 곱해서 시작 세타에 더해준다.
                        }
                        else
                        {
                            curTheta = startThetaStar + 2 * j * thetaStar; // 마지막 층은 삼각형의 밑변 부분에서 꼭짓점이 이어지게 그린다. 간단하니 생략함.
                        }

                        float cos = Mathf.Cos(curTheta * Mathf.Deg2Rad);
                        float sin = Mathf.Sin(curTheta * Mathf.Deg2Rad);

                        projectile.GetComponent<EnemyProjectileController>().Init(transform.TransformDirection(new(cos, sin)).normalized, enemyAttack); // 총알 방향 설정.
                    }

                    break;
            }

            yield return new WaitForSeconds(enemyAttack.delay);
        }
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Fire);
    }
}
