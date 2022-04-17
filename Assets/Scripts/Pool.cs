using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable] public class Pool//未繼承MonoBehaviour所以添加[System.Serializable]特性才能將Pool類中的序列化暴露出來
{
    public GameObject Prefab => prefab;
    public int Size => size;
    public int RuntimeSize => queue.Count;

    Transform parent;

    [SerializeField] GameObject prefab;
    [SerializeField] int size = 1;

    Queue<GameObject> queue;


    public void Initialize(Transform parent)//隊列需要先初始化，否則它將會一直是個空值
    {
        queue = new Queue<GameObject>();
        this.parent = parent;

        for(int i = 0; i < size; i++)
        {
            queue.Enqueue(Copy());//存儲所有複製體
        }
    }
    GameObject Copy()
    {
        var copy = GameObject.Instantiate(prefab, parent);

        copy.SetActive(false);

        return copy;
    }

    GameObject AvailableObject()//可用對象
    {
        GameObject availableObject = null;

        if (queue.Count > 0 && !queue.Peek().activeSelf) //當隊列元素的數量大於0並且隊列的第一個元素不是被啟用狀態時(Peek函數將返回隊列中最前面的第一個元素，但不會將它從隊列中移除)
        {
            availableObject = queue.Dequeue();//出列函數來取出隊列中的元素，這個函數將返回隊列中最前面的第一個元素，並將它從隊列中移除
        }
        else
        {
            availableObject = Copy();
        }

        queue.Enqueue(availableObject);

        return availableObject;
    }
    public GameObject PreparedObject()
    {
        GameObject preparedObject = AvailableObject();

        preparedObject.SetActive(true);

        return preparedObject;
    }
    public GameObject PreparedObject(Vector3 position)//將位置參數的值賦給預備好的對象再將它返回
    {
        GameObject preparedObject = AvailableObject();

        preparedObject.SetActive(true);
        preparedObject.transform.position = position;

        return preparedObject;
    }
    public GameObject PreparedObject(Vector3 position, Quaternion rotation)//將位置參數的值賦給預備好的對象再將它返回(增加一個四元數的旋轉值參數)
    {
        GameObject preparedObject = AvailableObject();

        preparedObject.SetActive(true);
        preparedObject.transform.position = position;
        preparedObject.transform.rotation = rotation;

        return preparedObject;
    }
    public GameObject PreparedObject(Vector3 position, Quaternion rotation, Vector3 localScale)//將位置參數的值賦給預備好的對象再將它返回
    {
        GameObject preparedObject = AvailableObject();

        preparedObject.SetActive(true);
        preparedObject.transform.position = position;
        preparedObject.transform.rotation = rotation;
        preparedObject.transform.localScale = localScale;

        return preparedObject;
    }
    //public void Return(GameObject gameObject)
    //{
    //    queue.Enqueue(gameObject);
    //}
}
