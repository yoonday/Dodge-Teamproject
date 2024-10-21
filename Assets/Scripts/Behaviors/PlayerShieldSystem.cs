using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShieldSystem : MonoBehaviour
{
    public GameObject shieldPrefab; // 방어막 프리팹
    private GameObject activeShield; // 활성화할 방어막
    public float shieldDuration = 5f; // 방어막 지속 시간
    private bool canActivateShield = false; // 방어막 활성화 가능 여부(아이템 먹었는지)

    void Update()
    {
        // R 버튼으로 방어막 활성화 (아이템 먹은 경우에만)
        if (Input.GetKeyDown(KeyCode.R) && activeShield == null && canActivateShield)
        {
            ActivateShield();
        }
    }

    public void SetShieldReady() // 아이템 먹으면 이 함수 호출
    {
        canActivateShield = true; // 방어막을 활성화할 수 있는 상태로 변경
        Debug.Log("방어막 활성화 가능");
    }

    public void ActivateShield() // 방어막 활성화
    {
        if (activeShield == null && shieldPrefab != null) // 방어막이 없을 때만 생성
        {         
            activeShield = Instantiate(shieldPrefab, transform.position, Quaternion.identity);

            ShieldFollow target = activeShield.AddComponent<ShieldFollow>();
            target.SetTarget(transform);

            // 일정 시간 후 방어막 비활성화
            Invoke("DeactivateShield", shieldDuration);

            canActivateShield = false;
        }
    }

    void DeactivateShield()
    {
        if (activeShield != null)
        {
            Destroy(activeShield);
            activeShield = null;
        }
    }
}
