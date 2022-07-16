using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TransformActionHandler))]
public class GameWallController : MonoBehaviour
{
    public enum FadeType
    {
        None,
        FadeIn,
        FadeOut,
        Repetetive
    }
    [SerializeField] private FadeType fadeType;
    public List<TransformAction> transformActions = new List<TransformAction>();
    public List<Vector3> vectorShit = new ();
    public TransformAction t;
    private int currentTransformActionIndex = 0;
    private float moveStartTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        //Expected usage is, that the listitems are created in the Inspector, so that no change will occur afterwards
        foreach (TransformAction transformAction in transformActions)
        {
            //transformAction.Finished += TransformAction_Finished;
        }
    }


    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (transformActions.Count == 0) return;
        TransformAction currentTransformAction = transformActions[currentTransformActionIndex];
        //currentTransformAction.Update(this.transform, moveStartTime);
    }

    private void TransformAction_Finished()
    {
        currentTransformActionIndex++;
        if (currentTransformActionIndex > transformActions.Count - 1)
        {
            currentTransformActionIndex = 0;
            moveStartTime = Time.time;
        }
    }
}
