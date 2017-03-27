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
    void Start()
    {
        for (byte index = (byte)TestMessageID.HeadTransform; index < (byte)TestMessageID.Max; index++)
        {
            if (MessageHandlers.ContainsKey((TestMessageID)index) == false)
            {
                MessageHandlers.Add((TestMessageID)index, null);
            }
        }

        MessageHandlers[TestMessageID.HeadTransform] = CallbackTest1;
        MessageHandlers[TestMessageID.HeadTransform] += CallbackTest2;
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
            MessageCallback messageHandler = MessageHandlers[TestMessageID.HeadTransform];
            if (messageHandler != null)
            {
                messageHandler();
            }
        }
    }
}
