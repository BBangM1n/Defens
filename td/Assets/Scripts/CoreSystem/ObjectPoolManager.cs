using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;

    // 여러 프리팹과 그에 해당하는 풀을 관리할 딕셔너리
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
    /// 특정 프리팹에 대한 풀 생성
    /// </summary>
    /// <param name="prefab">풀링할 프리팹</param>
    /// <param name="defaultCapacity">기본 개수</param>
    /// <param name="maxSize">최대 개수</param>
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
    /// 특정 프리팹에 대한 오브젝트 가져오기
    /// </summary>
    /// <param name="prefab">요청할 프리팹</param>
    /// <returns>풀에서 가져온 오브젝트</returns>
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
    /// 특정 프리팹에 대한 오브젝트 반환
    /// </summary>
    /// <param name="prefab">반환할 프리팹</param>
    /// <param name="obj">반환할 오브젝트</param>
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

    // 오브젝트 생성
    private GameObject CreatePooledObject(GameObject prefab)
    {
        GameObject obj = Instantiate(prefab);
        obj.SetActive(false);
        return obj;
    }

    // 풀에서 오브젝트를 가져올 때
    private void OnTakeFromPool(GameObject obj)
    {
        obj.SetActive(true);
    }

    // 풀로 반환할 때
    private void OnReturnedToPool(GameObject obj)
    {
        obj.SetActive(false);
    }

    // 오브젝트가 파괴될 때
    private void OnDestroyPoolObject(GameObject obj)
    {
        Destroy(obj);
    }
}
