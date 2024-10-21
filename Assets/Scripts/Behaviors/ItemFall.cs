using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFall : MonoBehaviour
{
    public float fallSpeed = 1.5f;  // �������� �������� �ӵ�
    private float bottonLine = 21f; // ȭ�� �ϴ� ������

    private void Update()
    {
        // �Ʒ��� �̵�
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        // ȭ�� �� - ������ �ı�
        if (transform.position.y < - bottonLine)
        {
            Destroy(gameObject);
        }
    }
}
