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
    private Sprite enemySprite;
    private Coroutine enemyCoroutine;
    private PlayerStatHandler playerStatHandler;

    public float EnemySpeed { get { return enemySpeed; } }
    public EnemyAttackSO EnemyAttack { get { return enemyAttack; } }


    protected override void Awake()
    {
        base.Awake();

        enemySprite = GetComponentInChildren<SpriteRenderer>().sprite;
        playerStatHandler = GetComponent<PlayerStatHandler>();
    }

    public void Init(int enemyHealth, EnemyAttackSO enemyAttack)
    {
        this.enemyAttack = enemyAttack;
        playerStatHandler.CurrentStat.maxHealth = enemyHealth;
        enemyCoroutine = StartCoroutine(EnemyCoroutine());
    }

    // Start is called before the first frame update
    void Start()
    {
        SetRange();
    }

    protected override void Update() { }

    public void Stop()
    {
        StopCoroutine(enemyCoroutine);
        CallMoveEvent(Vector2.zero);
    }

    private IEnumerator EnemyCoroutine()
    {
        while(true)
        {
            Vector2 randomDest = new(Random.Range(randomPosRangeXMin, randomPosRangeXMax), Random.Range(randomPosRangeYMin, randomPosRangeYMax));
            CallMoveEvent(randomDest);

            if (randomDest != Vector2.zero)
            {
                yield return new WaitForSeconds(movingDuration);

                CallMoveEvent(Vector2.zero);
                CallAttackEvent();

                yield return new WaitForSeconds(moveCooltime);
            }
            else yield return null;
        }
    }

    private void SetRange()
    {
        Camera mainCamera = Camera.main;
        enemySprite = transform.GetComponentInChildren<SpriteRenderer>().sprite;

        float cameraHeight = mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * ((float)Screen.width / Screen.height);

        randomPosRangeYMin = mainCamera.transform.position.y + cameraHeight - (enemySprite.bounds.size.y / 2); // 화면 맨 위 좌표.
        randomPosRangeYMax = randomPosRangeYMin - cameraHeight; // 적이 나타날 화면 맨 아래 좌표.

        randomPosRangeXMin = mainCamera.transform.position.x - cameraWidth + (enemySprite.bounds.size.x / 2); // 화면 맨 왼쪽 좌표.
        randomPosRangeXMax = mainCamera.transform.position.x + cameraWidth - (enemySprite.bounds.size.x / 2); // 화면 맨 오른쪽 좌표.
    }
}
