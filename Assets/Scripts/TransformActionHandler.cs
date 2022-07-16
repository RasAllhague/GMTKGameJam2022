using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformActionHandler : MonoBehaviour
{
    public bool loop = true;
    private TransformAction[] transformActionList;
    // Start is called before the first frame update
    private int current = 0;
    void Start()
    {
        transformActionList = GetComponents<TransformAction>();
        transformActionList[current].Finished += OnFinish;
        transformActionList[current].Activate();
    }

    private void OnFinish(TransformAction transformAction)
    {
        transformAction.Finished -= OnFinish;
        current++;
        if (current >= transformActionList.Length)
            if (loop)
                current = 0;
            else return;
        transformActionList[current].Finished += OnFinish;
        transformActionList[current].Activate();
    }

}
