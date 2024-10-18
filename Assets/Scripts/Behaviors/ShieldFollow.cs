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
            // 방어막이 플레이어의 위치를 따라가도록 설정
            transform.position = target.position;
        }
    }
}
