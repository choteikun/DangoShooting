using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPGauge : MonoBehaviour
{
    public Slider slider1;
    public Slider slider2;
    public Boss BossData;

    void Start()
    {
        
    }


    void Update()
    {
        //slider1.value = BossData.GetHP() / 100;

        //slider2.value = Mathf.Lerp(slider2.value, BossData.GetHP() / 100, Time.deltaTime * 10);
    }
}
