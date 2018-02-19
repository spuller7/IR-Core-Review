using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAppearance : MonoBehaviour {

    public bool isActive = true;
    public Color disableColor;

    public void Start()
    {
        changeImage();
    }

    public void changeStatus(bool active)
    {
        if(active)
        {
            isActive = true;
        }
        else
        {
            isActive = false;
        }
        changeImage();
    }
    public void changeImage()
    {
        gameObject.GetComponent<Image>().color = disableColor;
    }
}
