using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceHintVisualizer : MonoBehaviour
{
    private GameObject currentGameObject;
    [SerializeField] private Camera diceCam;
    private float rotationSpeed = 60f;

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
        currentGameObject.transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y + rotationSpeed * Time.deltaTime, 0);
    }

    public void SetDice(GameObject dice)
    {
        if (currentGameObject != null)
        {
            Destroy(currentGameObject);
        }
        currentGameObject = Instantiate(dice);
        currentGameObject.transform.parent = this.transform;
        currentGameObject.transform.localPosition = new Vector3();
        Rigidbody rb = currentGameObject.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        currentGameObject.layer = LayerMask.NameToLayer("UI");
    }
}
