using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditRotation : MonoBehaviour
{
    [SerializeField]private GameObject currentGameObject;
    //How many degrees to rotate per second (approx)
    [SerializeField]private float rotationSpeed = 60f;
    private float randomRotationX;
    private float randomRotationY;
    private float randomRotationZ;

    private void Start()
    {
        randomRotationX = Random.Range(-100.0f, 100.0f);
        randomRotationY = Random.Range(-500.0f, 500.0f);
        randomRotationZ = Random.Range(-100.0f, 100.0f);
    }

    private void Update()
    {
        if (currentGameObject != null)
        {
            RotateCurrentObject();
        }
    }

    private void RotateCurrentObject()
    {
        currentGameObject.transform.Rotate(randomRotationX * Time.deltaTime,rotationSpeed * Time.deltaTime, randomRotationZ * Time.deltaTime);
    }
}
