using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReviewText : MonoBehaviour {
    
	void OnEnable () {
        this.GetComponent<Text>().text = "Test " + Test.current.testID;
	}
}
