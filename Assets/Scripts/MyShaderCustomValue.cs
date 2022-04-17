using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyShaderCustomValue : MonoBehaviour
{
    [Range(0, 1)] public float _myCustomFloat;

    private float __myCustomFloatActualValue;

    private void Awake()
    {
        __myCustomFloatActualValue = GetComponent<Text>().material.GetFloat("_TimeScale");
    }

    private void Update()
    {
        if (_myCustomFloat != __myCustomFloatActualValue)
        {
            __myCustomFloatActualValue = _myCustomFloat;
            GetComponent<Text>().material.SetFloat("_TimeScale", __myCustomFloatActualValue);
        }
    }
}
