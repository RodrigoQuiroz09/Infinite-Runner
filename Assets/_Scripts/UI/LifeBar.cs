using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    
    [Tooltip("Life the UI is going to represent")][SerializeField] Life targetLife;

    /**Visual representation of the life
     * ! It must be a Radial image
    */
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    /// <summary>
    /// Update the propotion of the filled image depending of the amount of life left
    /// </summary>
    void Update()
    {
         _image.fillAmount = targetLife.Amount / targetLife.maximumLife;
    }
}
