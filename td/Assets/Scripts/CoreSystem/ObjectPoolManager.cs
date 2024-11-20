using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;

    // ���� �����հ� �׿� �ش��ϴ� Ǯ�� ������ ��ųʸ�
    private Dictionary<GameObject, ObjectPool<GameObject>> poolDictionary = new Dictionary<GameObject, ObjectPool<GameObject>>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Ư�� �����տ� ���� Ǯ ����
    /// </summary>
    /// <param name="prefab">Ǯ���� ������</param>
    /// <param name="defaultCapacity">�⺻ ����</param>
    /// <param name="maxSize">�ִ� ����</param>
    public void CreatePool(GameObject prefab, int defaultCapacity = 10, int maxSize = 50)
    {
        if (poolDictionary.ContainsKey(prefab))
            return;

        poolDictionary[prefab] = new ObjectPool<GameObject>(
            () => CreatePooledObject(prefab),
            OnTakeFromPool,
            OnReturnedToPool,
            OnDestroyPoolObject,
            true, defaultCapacity, maxSize);
    }

    /// <summary>
    /// Ư�� �����տ� ���� ������Ʈ ��������
    /// </summary>
    /// <param name="prefab">��û�� ������</param>
    /// <returns>Ǯ���� ������ ������Ʈ</returns>
    public GameObject GetObject(GameObject prefab)
    {
        if (!poolDictionary.ContainsKey(prefab))
        {
            Debug.LogError($"Pool for prefab '{prefab.name}' does not exist! Call CreatePool first.");
            return null;
        }

        return poolDictionary[prefab].Get();
    }

    /// <summary>
    /// Ư�� �����տ� ���� ������Ʈ ��ȯ
    /// </summary>
    /// <param name="prefab">��ȯ�� ������</param>
    /// <param name="obj">��ȯ�� ������Ʈ</param>
    public void ReturnObject(GameObject prefab, GameObject obj)
    {
        if (!poolDictionary.ContainsKey(prefab))
        {
            Debug.LogError($"Pool for prefab '{prefab.name}' does not exist!");
            Destroy(obj);
            return;
        }

        poolDictionary[prefab].Release(obj);
    }

    // ������Ʈ ����
    private GameObject CreatePooledObject(GameObject prefab)
    {
        GameObject obj = Instantiate(prefab);
        obj.SetActive(false);
        return obj;
    }

    // Ǯ���� ������Ʈ�� ������ ��
    private void OnTakeFromPool(GameObject obj)
    {
        obj.SetActive(true);
    }

    // Ǯ�� ��ȯ�� ��
    private void OnReturnedToPool(GameObject obj)
    {
        obj.SetActive(false);
    }

    // ������Ʈ�� �ı��� ��
    private void OnDestroyPoolObject(GameObject obj)
    {
        Destroy(obj);
    }
}
