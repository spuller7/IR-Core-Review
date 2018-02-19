using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconTextPopup : MonoBehaviour {
    
	void OnEnable () {
		if(Test.current.isComplete())
        {
            this.GetComponent<Text>().text = "Would you like to review Test " + Test.current.getTestID() + "?";
        }
        else
        {
            this.GetComponent<Text>().text = "Would you like to continue Test " + Test.current.getTestID() + "?";
        }
	}
}
