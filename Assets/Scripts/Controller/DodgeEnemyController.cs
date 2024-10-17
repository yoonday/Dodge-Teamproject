using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeEnemyController : DodgeController
{
    [SerializeField][Range(0f, 10f)] private float enemySpeed;
    [SerializeField][Range(0f, 10f)] private float movingDuration;
    [SerializeField][Range(0f, 10f)] private float moveCooltime;
    [SerializeField] Sprite enemySprite;

    private float randomPosRangeXMin;
    private float randomPosRangeXMax;
    private float randomPosRangeYMin;
    private float randomPosRangeYMax;

    public float EnemySpeed { get { return enemySpeed; } }

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

        randomPosRangeYMin = mainCamera.transform.position.y + cameraHeight - (enemySprite.bounds.size.y / 2); // ȭ�� �� �� ��ǥ.
        randomPosRangeYMax = randomPosRangeYMin - (cameraHeight * 3 / 2); // ���� ��Ÿ�� ȭ�� �� �Ʒ� ��ǥ.

        randomPosRangeXMin = mainCamera.transform.position.x - cameraWidth + (enemySprite.bounds.size.x / 2); // ȭ�� �� ���� ��ǥ.
        randomPosRangeXMax = mainCamera.transform.position.x + cameraWidth - (enemySprite.bounds.size.x / 2); // ȭ�� �� ������ ��ǥ.

        StartCoroutine(MoveCoroutine());
    }


    private IEnumerator MoveCoroutine()
    {
        while(true)
        {
            Vector2 randomDest = new(Random.Range(randomPosRangeXMin, randomPosRangeXMax), Random.Range(randomPosRangeYMin, randomPosRangeYMax));
            CallMoveEvent(randomDest);
            yield return new WaitForSeconds(movingDuration);
            CallMoveEvent(Vector2.zero);
            yield return new WaitForSeconds(moveCooltime);
        }
    }
}
