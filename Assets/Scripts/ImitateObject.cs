using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImitateObject : MonoBehaviour
{

    [SerializeField] private GameObject target;
    // Update is called once per frame
    void Update()
    {
        //Follows the target on every frame update
        this.transform.position = target.transform.position;
        this.transform.rotation = target.transform.rotation;
    }
}
