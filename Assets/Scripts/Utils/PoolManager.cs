using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> Pools;
    public Dictionary<string, Queue<GameObject>> PoolDictionary;

    private void Awake()
    {
        // �ν�����â�� Pools�� �������� ������ƮǮ�� ���� ��. 
        // ������ƮǮ�� ������Ʈ���� �����̸�, pool������ �Ѿ�� ������ ���� ���ο� ������Ʈ���� �Ҵ�.
        PoolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (var pool in Pools)
        {
            // ť�� FIFO(First-in First-out) �����μ�, ���� ���� ��ó�� ���� ���� �� ��(enqueue) ��ü�� ���� ���� ���� ����(dequeue) �� �ִ� ����
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                // Awake�ϴ� ���� ������ƮǮ�� �� Instantitate �Ͼ�� ������ �͹��Ͼ��� ������ ����
                GameObject obj = Instantiate(pool.prefab, transform);
                obj.SetActive(false);
                // ���� ���� �������� ����.
                objectPool.Enqueue(obj);
            }
            // ������ ���� Dictionary�� ���
            PoolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag)
    {
        // ���ʿ� Pool�� �������� �ʴ� ���
        if (!PoolDictionary.ContainsKey(tag))
            return null;

        // ���� ������ ��ü�� ��Ȱ��
        GameObject obj = PoolDictionary[tag].Dequeue();
        PoolDictionary[tag].Enqueue(obj);
        obj.SetActive(true);
        return obj;
    }


}
