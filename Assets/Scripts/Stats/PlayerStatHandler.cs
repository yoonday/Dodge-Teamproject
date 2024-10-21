using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatHandler : MonoBehaviour
{
    // ���� Stat ��� ���� : �⺻ Stat + �߰� Stat

    [SerializeField] private PlayerStat baseStats; // �⺻ Stat
    public PlayerStat CurrentStat {  get; private set; }
    public List<PlayerStat> statModifiers = new List<PlayerStat>(); // �߰� Stat ���� ����Ʈ ����

    private void Awake()
    {
        UpdateCharacterStat();
    }

    private void UpdateCharacterStat() // ���� ������Ʈ
    {
        PlayerAttackSO playerSO = null;
        if (baseStats.playerSO != null) // ���� ���� �ʱ�ȭ
        {
            playerSO = Instantiate(baseStats.playerSO);
        }

        // ���� �÷��̾� Stat
        CurrentStat = new PlayerStat { playerSO = playerSO };
        CurrentStat.maxHealth = baseStats.maxHealth;
        CurrentStat.speed = baseStats.speed;
        Debug.Log("�⺻ ���ǵ� ����: " + CurrentStat.speed);

    }


    public void ChangeSpeedStat(float increasedSpeed, float duration) // �ӵ� ����
    {
        float originalSpeed = baseStats.speed; // �⺻ ���ǵ� �� ����

        // ������ ���� ����
        CurrentStat.speed = increasedSpeed;
        Debug.Log("����� �ӵ� Ȯ��: " + CurrentStat.speed);

        // �ڷ�ƾ ��� : ���� �ð� �� ���� �ӵ��� ���ư�
        StartCoroutine(ResetSpeed(originalSpeed, duration));
    }

    private IEnumerator ResetSpeed(float originalSpeed, float duration) // �ڷ�ƾ (���� ���ǵ�, ������ ������ �ð�)
    {
        yield return new WaitForSeconds(duration); // ������ ���� �ð�

        CurrentStat.speed = originalSpeed; // �⺻ ���ǵ� ����
        Debug.Log("������ ȿ�� ����: " + CurrentStat.speed);
    }

}
