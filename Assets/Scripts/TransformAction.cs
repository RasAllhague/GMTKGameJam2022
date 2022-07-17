using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[ExecuteInEditMode]
[Serializable]
public class TransformAction : MonoBehaviour
{
    public float relativeActionStartTime  = 0;
    public float actionTime = 1;
    public Vector3 actionMovement = new Vector3();
    public Vector3 actionRotation = new Vector3();

    public Action<TransformAction> Finished { get; set; }
    public float startTime { get; set; } = 0;
    private Vector3 movementPerSecond = new Vector3();
    private Vector3 rotationPerSecond = new Vector3();
    private bool active = false;

    public void Update()
    {
        if (!active) return;
        if (Time.time < startTime + relativeActionStartTime) return;
        this.transform.position += movementPerSecond * Time.deltaTime;
        this.transform.Rotate(rotationPerSecond * Time.deltaTime);
        if (Time.time > startTime + relativeActionStartTime + actionTime)
        {
            active = false;
            OnFinished();
        }
    }

    public void Activate()
    {
        active = true;
        startTime = Time.time;
        movementPerSecond = actionMovement / actionTime;
        rotationPerSecond = actionRotation / actionTime;
    }

    private void OnFinished()
    {
        Finished?.Invoke(this);
    }
}

