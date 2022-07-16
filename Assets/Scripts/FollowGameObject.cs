using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGameObject : MonoBehaviour
{
    [SerializeField] private GameObject target;
    void Update()
    {
        Vector3 targetPos = target.transform.position;
        targetPos.y = transform.position.y;
        this.transform.position = targetPos;
    }
}
