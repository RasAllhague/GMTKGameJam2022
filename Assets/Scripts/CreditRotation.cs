using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditRotation : MonoBehaviour
{
    [SerializeField] private GameObject currentGameObject;
    //How many degrees to rotate per second (approx)
    [SerializeField] private float rotationSpeed = 60f;
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
        Vector3 eulerRotation = currentGameObject.transform.rotation.eulerAngles;
        currentGameObject.transform.rotation = Quaternion.Euler(eulerRotation.x + randomRotationX * Time.deltaTime, eulerRotation.y + rotationSpeed * Time.deltaTime, eulerRotation.z + randomRotationZ * Time.deltaTime);
    }

    public void SetDice(GameObject dice)
    {
        currentGameObject.transform.parent = this.transform;
        currentGameObject.transform.localPosition = new Vector3();
        Rigidbody rb = currentGameObject.GetComponent<Rigidbody>();
        //Disables gravity
        rb.isKinematic = true;
        //Sets the layer to UI, so that the DiceCam can see it
        currentGameObject.layer = LayerMask.NameToLayer("UI");
    }
}