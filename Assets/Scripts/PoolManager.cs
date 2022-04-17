using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] Pool[] bulletPools;

    public static Dictionary<GameObject, Pool> dictionary;

    private void Start()
    {
        dictionary = new Dictionary<GameObject, Pool>();

        Initialize(bulletPools);
    }

    #if UNITY_EDITOR
    void OnDestroy()
    {
        CheckPoolSize(bulletPools);
    }
    #endif
    void CheckPoolSize(Pool[] pools)
    {
        foreach(var pool in pools)
        {
            if (pool.RuntimeSize > pool.Size)
            {
                Debug.LogWarning(string.Format("Pool: {0} has a runtime size {1} bigger than its initial size {2}!",
                    pool.Prefab.name,
                    pool.RuntimeSize,
                    pool.Size));
            }
        }
    }

    void Initialize(Pool[] pools)
    {
        foreach(var pool in pools)
        {
         #if UNITY_EDITOR
            //#if ...#endif，只會在指定的平台上運行時才會進行編譯(打包後忽略這段程式碼)
            if (dictionary.ContainsKey(pool.Prefab))//如果prefab重複，則跳過
            {
                Debug.LogError("在多個對象池裡發現了相同的prefab ! Prefab: " + pool.Prefab.name);
                continue;
            }
         #endif
            dictionary.Add(pool.Prefab, pool);

            Transform poolParent = new GameObject("Pool: " + pool.Prefab.name).transform;

            poolParent.parent = transform;
            pool.Initialize(poolParent);
        }
    }

    /// <summary>
    /// 根據傳入的prefab參數，返回對象池中預備好的遊戲對象
    /// </summary>
    /// <param name="prefab">
    /// <para>指定的遊戲對象預制體</para>
    /// </param>
    /// <returns></returns>
    public static GameObject Release(GameObject prefab)
    {
        #if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("Pool Manager 找不到prefab: " + prefab.name);

            return null;
        }
        #endif
        return dictionary[prefab].PreparedObject();
    }
    /// <summary>
    /// 根據傳入的prefab參數，在position的參數位置釋放對象池中預備好的遊戲對象
    /// </summary>
    /// <param name="prefab">
    /// <para>指定的遊戲對象預制體</para>
    /// </param>
    /// <param name="position">
    /// <para>指定釋放位置</para>
    /// </param>
    /// <returns></returns>
    public static GameObject Release(GameObject prefab, Vector3 position)
    {
        #if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("Pool Manager 找不到prefab: " + prefab.name);

            return null;
        }
        #endif
        return dictionary[prefab].PreparedObject(position);
    }
    /// <summary>
    /// 根據傳入的prefab參數和rotation參數，在position的參數位置釋放對象池中預備好的遊戲對象
    /// </summary>
    /// <param name="prefab">
    /// <para>指定的遊戲對象預制體</para>
    /// </param>
    /// <param name="position">
    /// <para>指定釋放位置</para>
    /// </param>
    /// <param name="rotation">
    /// <para>指定的旋轉值</para>
    /// </param>
    /// <returns></returns>
    public static GameObject Release(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        #if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("Pool Manager 找不到prefab: " + prefab.name);

            return null;
        }
        #endif
        return dictionary[prefab].PreparedObject(position, rotation);
    }
    /// <summary>
    /// 根據傳入的prefab參數，rotation參數和localScale參數，在position的參數位置釋放對象池中預備好的遊戲對象
    /// </summary>
    /// <param name="prefab">
    /// <para>指定的遊戲對象預制體</para>
    /// </param>
    /// <param name="position">
    /// <para>指定釋放位置</para>
    /// </param>
    /// <param name="rotation">
    /// <para>指定的旋轉值</para>
    /// </param>
    /// <param name="localScale">
    /// <para>指定的縮放值</para>
    /// </param>
    /// <returns></returns>
    public static GameObject Release(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 localScale)
    {
        #if UNITY_EDITOR
        if (!dictionary.ContainsKey(prefab))
        {
            Debug.LogError("Pool Manager 找不到prefab: " + prefab.name);

            return null;
        }
        #endif
        return dictionary[prefab].PreparedObject(position, rotation, localScale);
    }
}
