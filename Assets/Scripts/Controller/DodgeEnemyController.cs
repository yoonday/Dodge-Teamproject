using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeEnemyController : DodgeController
{
    [SerializeField][Range(0f, 10f)] private float enemySpeed;
    [SerializeField][Range(0f, 10f)] private float movingDuration;
    [SerializeField][Range(0f, 10f)] private float moveCooltime;

    private float randomPosRangeXMin;
    private float randomPosRangeXMax;
    private float randomPosRangeYMin;
    private float randomPosRangeYMax;

    private EnemyAttackSO enemyAttack;
    private SpriteRenderer enemySprite;
    private Coroutine enemyCoroutine;
    private HealthSystem health;
    private PlayerStatHandler handler;
    private Animator animator;

    

    public bool IsBoss { get; private set; }
    public float EnemySpeed { get { return enemySpeed; } }
    public EnemyAttackSO EnemyAttack { get { return enemyAttack; } }


    protected override void Awake()
    {
        base.Awake();

        enemySprite = GetComponentInChildren<SpriteRenderer>();
        health = GetComponent<HealthSystem>();
        handler = GetComponent<PlayerStatHandler>();
        animator = GetComponentInChildren<Animator>();
    }

    public void Init(EnemyAttackSO enemyAttack)
    {
        this.enemyAttack = enemyAttack;

        health.InitHealth(enemyAttack.enemyHealth);
        handler.CurrentStat.maxHealth = enemyAttack.enemyHealth;
        enemySprite.sprite = EnemyAttack.enemySprite;
        animator.runtimeAnimatorController = enemyAttack.enemyAnimatorController;
        animator.enabled = true;
        IsBoss = enemyAttack.isBoss;

        SetRange();

        if(enemyAttack.isBoss)
        {
            enemyCoroutine = StartCoroutine(EnemyAttackingCoroutine());
        }
        else
        {
            enemyCoroutine = StartCoroutine(EnemyMovingAndAttackingCoroutine());
        }
    }

    protected override void Update() { }

    public void Stop()
    {
        StopCoroutine(enemyCoroutine);
        CallMoveEvent(Vector2.zero);
    }

    private IEnumerator EnemyMovingAndAttackingCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(moveCooltime);

            Vector2 randomDest = new(Random.Range(randomPosRangeXMin, randomPosRangeXMax), Random.Range(randomPosRangeYMin, randomPosRangeYMax));
            CallMoveEvent(randomDest);

            if (randomDest != Vector2.zero)
            {
                yield return new WaitForSeconds(movingDuration);

                CallMoveEvent(Vector2.zero);
                CallAttackEvent();
            }
            else yield return null;
        }
    }

    private IEnumerator EnemyAttackingCoroutine()
    {
        yield return new WaitForSeconds(moveCooltime);

        Vector2 centerVec = Vector3.Lerp(new(randomPosRangeXMin, randomPosRangeYMin), new(randomPosRangeXMax, randomPosRangeYMax), 0.5f);
        CallMoveEvent(centerVec);

        while (true)
        {
            yield return new WaitForSeconds(moveCooltime);

            CallAttackEvent();

            yield return new WaitForSeconds(moveCooltime);

        }
    }

    private void SetRange()
    {
        Camera mainCamera = Camera.main;
        var sprite = enemySprite.sprite;

        float cameraHeight = mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * ((float)Screen.width / Screen.height);

        randomPosRangeYMin = mainCamera.transform.position.y + cameraHeight - (enemySprite.bounds.size.y / 2); // 화면 맨 위 좌표.
        randomPosRangeYMax = randomPosRangeYMin - cameraHeight; // 적이 나타날 화면 맨 아래 좌표.

        randomPosRangeXMin = mainCamera.transform.position.x - cameraWidth + (enemySprite.bounds.size.x / 2); // 화면 맨 왼쪽 좌표.
        randomPosRangeXMax = mainCamera.transform.position.x + cameraWidth - (enemySprite.bounds.size.x / 2); // 화면 맨 오른쪽 좌표.
    }
}
