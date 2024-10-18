using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeEnemyController : DodgeController
{
    [SerializeField][Range(0f, 10f)] private float enemySpeed;
    [SerializeField][Range(0f, 10f)] private float movingDuration;
    [SerializeField][Range(0f, 10f)] private float moveCooltime;
    [SerializeField] Sprite enemySprite;
    [SerializeField] private EnemyAttackSO enemyAttack;

    private float randomPosRangeXMin;
    private float randomPosRangeXMax;
    private float randomPosRangeYMin;
    private float randomPosRangeYMax;

    Coroutine enemyCoroutine;


    public float EnemySpeed { get { return enemySpeed; } }
    public EnemyAttackSO EnemyAttack { get { return enemyAttack; } }

    public void Init(float enemySpeed)
    {
        this.enemySpeed = enemySpeed;
    }

    // Start is called before the first frame update
    void Start()
    {
        Camera mainCamera = Camera.main;
        enemySprite = transform.GetComponentInChildren<SpriteRenderer>().sprite;

        float cameraHeight = mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * ((float)Screen.width / Screen.height);

        randomPosRangeYMin = mainCamera.transform.position.y + cameraHeight - (enemySprite.bounds.size.y / 2); // 화면 맨 위 좌표.
        randomPosRangeYMax = randomPosRangeYMin - cameraHeight; // 적이 나타날 화면 맨 아래 좌표.

        randomPosRangeXMin = mainCamera.transform.position.x - cameraWidth + (enemySprite.bounds.size.x / 2); // 화면 맨 왼쪽 좌표.
        randomPosRangeXMax = mainCamera.transform.position.x + cameraWidth - (enemySprite.bounds.size.x / 2); // 화면 맨 오른쪽 좌표.

        enemyCoroutine = StartCoroutine(EnemyCoroutine());

        CallAttackEvent();
    }

    protected override void Update() { }

    public void Stop()
    {
        StopCoroutine(enemyCoroutine);
        CallMoveEvent(Vector2.zero);
    }

    private IEnumerator EnemyCoroutine()
    {
        yield return new WaitForSeconds(moveCooltime);
        while(true)
        {
            Vector2 randomDest = new(Random.Range(randomPosRangeXMin, randomPosRangeXMax), Random.Range(randomPosRangeYMin, randomPosRangeYMax));
            CallMoveEvent(randomDest);

            yield return new WaitForSeconds(movingDuration);

            CallMoveEvent(Vector2.zero);
            CallAttackEvent();

            yield return new WaitForSeconds(moveCooltime);
        }
    }
}
