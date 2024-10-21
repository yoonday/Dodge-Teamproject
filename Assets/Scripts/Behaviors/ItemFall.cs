using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFall : MonoBehaviour
{
    public float fallSpeed = 1.5f;  // �������� �������� �ӵ�
    private float bottomMax;

    private void Start()
    {
        SetRange();
    }

    private void Update()
    {
        // �Ʒ��� �̵�
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        // ȭ�� �� - ������ �ı�
        if (transform.position.y < bottomMax)
        {
            Destroy(gameObject);
        }
    }

    private void SetRange()
    {
        Camera mainCamera = Camera.main;

        float cameraHeight = mainCamera.orthographicSize;

        bottomMax = mainCamera.transform.position.y - cameraHeight; // ȭ�� �� �Ʒ� ��ǥ.
    }
}
