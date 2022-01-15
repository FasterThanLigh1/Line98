using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    float time = 0;
    public TextMeshProUGUI textField;
    void Update()
    {
        textField = GetComponent<TextMeshProUGUI>();
        UpdateTime();
        time = time + Time.deltaTime;
        if(time >= 999) {
            time = 999;
        }
    }

    void UpdateTime() {
        textField.text = ((int)time).ToString("000");
    }
}
