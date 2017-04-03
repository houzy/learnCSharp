using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictDelegateReceiver : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
        //MessageHandlers[TestMessageID.HeadTransform] = CallbackTest1;

		GameObject testDD = GameObject.Find("TestDictionaryAndDelegate");
		DictionaryAndDelegate testDDComp = testDD.GetComponent<DictionaryAndDelegate>();
        testDDComp.MessageHandlers[DictionaryAndDelegate.TestMessageID.HeadTransform] += CallbackTest;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void CallbackTest()
    {
        Debug.Log(gameObject.name + " CallbackTest");
    }
}
