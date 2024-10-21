using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShieldSystem : MonoBehaviour
{
    public GameObject shieldPrefab; // �� ������
    private GameObject activeShield; // Ȱ��ȭ�� ��
    public float shieldDuration = 5f; // �� ���� �ð�
    private bool canActivateShield = false; // �� Ȱ��ȭ ���� ����(������ �Ծ�����)

    void Update()
    {
        // R ��ư���� �� Ȱ��ȭ (������ ���� ��쿡��)
        if (Input.GetKeyDown(KeyCode.R) && activeShield == null && canActivateShield)
        {
            ActivateShield();
        }
    }

    public void SetShieldReady() // ������ ������ �� �Լ� ȣ��
    {
        canActivateShield = true; // ���� Ȱ��ȭ�� �� �ִ� ���·� ����
        Debug.Log("�� Ȱ��ȭ ����");
    }

    public void ActivateShield() // �� Ȱ��ȭ
    {
        if (activeShield == null && shieldPrefab != null) // ���� ���� ���� ����
        {         
            activeShield = Instantiate(shieldPrefab, transform.position, Quaternion.identity);

            ShieldFollow target = activeShield.AddComponent<ShieldFollow>();
            target.SetTarget(transform);

            // ���� �ð� �� �� ��Ȱ��ȭ
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
