using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldFollow : MonoBehaviour
{
    private Transform target;
    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }

    void Update()
    {
        if (target != null)
        {
            // ���� �÷��̾��� ��ġ�� ���󰡵��� ����
            transform.position = target.position;
        }
    }
}
