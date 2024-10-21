using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFall : MonoBehaviour
{
    public float fallSpeed = 1.5f;  // 아이템이 떨어지는 속도
    private float bottomMax;

    private void Start()
    {
        SetRange();
    }

    private void Update()
    {
        // 아래로 이동
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        // 화면 밖 - 아이템 파괴
        if (transform.position.y < bottomMax)
        {
            Destroy(gameObject);
        }
    }

    private void SetRange()
    {
        Camera mainCamera = Camera.main;

        float cameraHeight = mainCamera.orthographicSize;

        bottomMax = mainCamera.transform.position.y - cameraHeight; // 화면 맨 아래 좌표.
    }
}
