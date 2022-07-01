using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    [SerializeField] Life targetLife;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    void Update()
    {
         _image.fillAmount = targetLife.Amount / targetLife.maximumLife;
    }
}
