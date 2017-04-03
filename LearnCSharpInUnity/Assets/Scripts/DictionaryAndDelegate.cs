using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryAndDelegate : MonoBehaviour {

    /// <summary>
    /// define id
    /// </summary>
    public enum TestMessageID : byte
    {
        HeadTransform = 0,
        HandTransform,
        Max
    }
    public delegate void MessageCallback();
    private Dictionary<TestMessageID, MessageCallback> messageHandlers = new Dictionary<TestMessageID, MessageCallback>();
    public Dictionary<TestMessageID, MessageCallback> MessageHandlers
    {
        get
        {
            return messageHandlers;
        }
    }

    // Use this for initialization
    // 此处如果是Start，在DictDelegateReceiver那里，可能会产生key还没有初始化（Add）就被使用的情况。
    void Awake()
    {
        for (byte index = (byte)TestMessageID.HeadTransform; index < (byte)TestMessageID.Max; index++)
        {
            Debug.Log("TestMessageID index == " + index);
            if (MessageHandlers.ContainsKey((TestMessageID)index) == false)
            {
                Debug.Log("MessageHandlers == " + index);
                MessageHandlers.Add((TestMessageID)index, null);
            }
        }

        //MessageHandlers[TestMessageID.HeadTransform] = CallbackTest1;
        //MessageHandlers[TestMessageID.HeadTransform] += CallbackTest2;
    }

    private void CallbackTest1()
    {
        Debug.Log("CallbackTest1");
    }

    private void CallbackTest2()
    {
        Debug.Log("CallbackTest2");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Update get KeyCode.Space");
            MessageCallback messageHandler = MessageHandlers[TestMessageID.HeadTransform];
            if (messageHandler != null)
            {
                messageHandler();
            }
        }
    }
}
