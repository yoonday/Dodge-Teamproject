using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private float screenTopMax;

    void Start()
    {
        SetRange();
    }

    void Update()
    {
        transform.position += Vector3.up * 0.2f;

        if (transform.position.y > screenTopMax)
        {
            gameObject.SetActive(false);
        }
    }

    private void SetRange()
    {
        Camera mainCamera = Camera.main;

        float cameraHeight = mainCamera.orthographicSize;

        screenTopMax = mainCamera.transform.position.y + cameraHeight; // ȭ�� �� �� ��ǥ.
    }
}

