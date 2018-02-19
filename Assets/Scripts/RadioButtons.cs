using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MaterialUI
{

    public class RadioButtons : MonoBehaviour
    {

        public GameObject[] toggleBut;
        public Color selectedColor;
        public Color notSelectedColor;
        public Color selectedTextColor;
        public Color notSelectedTextColor;

        public void Start()
        {
            foreach (GameObject tog in toggleBut)
            {
                if (tog == toggleBut[0])
                {
                    tog.GetComponent<Image>().color = selectedColor;
                    Transform go = tog.transform.Find("Text");
                    go.GetComponent<Text>().color = selectedTextColor;
                }
                else
                {
                    tog.GetComponent<Image>().color = notSelectedColor;
                    Transform go = tog.transform.Find("Text");
                    go.GetComponent<Text>().color = notSelectedTextColor;
                }

            }
        }

        public void triggerToggle(int button)
        {
            foreach (GameObject tog in toggleBut)
            {
                if(tog == toggleBut[button])
                {
                    tog.GetComponent<Image>().color = selectedColor;
                    Transform go = tog.transform.Find("Text");
                    go.GetComponent<Text>().color = selectedTextColor;
                }
                else
                {
                    tog.GetComponent<Image>().color = notSelectedColor;
                    Transform go = tog.transform.Find("Text");
                    go.GetComponent<Text>().color = notSelectedTextColor;
                }
                
            }
        }

        public string getSelected(string radio)
        {
            foreach (GameObject tog in toggleBut)
            {
                if (tog.GetComponent<Image>().color == selectedColor)
                {
                    Transform go = tog.transform.Find("Text");
                    return go.GetComponent<Text>().text;
                }
            }
            if (radio == "Question Types")
                return "All";
            else if (radio == "Quiz Mode")
                return "Tutor";
            else
                return "";
        }
    }

}
