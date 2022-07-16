using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectOuterWalls : MonoBehaviour
{
    [SerializeField] private List<GameObject> outerWalls = new List<GameObject>();
    [SerializeField] private float outerWallMinScale = 1;
    [SerializeField] private float outerWallHeight = 3;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 position = transform.position;
        Vector3 scale = transform.localScale;
        //Iterating through all outer walls to set their scale and position
        for (int i = 0; i < outerWalls.Count; i++)
        {
            //setting default scale and position
            Vector3 outerWallPos = new Vector3(position.x, outerWallHeight/2 + scale.y/2, position.z);
            Vector3 outerWallScale = new Vector3(outerWallMinScale, outerWallHeight, outerWallMinScale);
            //for the side walls
            if ( i%2 == 0)
            {
                //left
                outerWallScale.z = scale.z;
                outerWallPos.x = scale.x/2 - (outerWallMinScale/2);
                //right
                if(i%4 == 0)
                    outerWallPos.x = -outerWallPos.x;
            }
            else
            {
                //for the front and back walls
                //front
                outerWallScale.x = scale.x;
                outerWallPos.z = scale.z / 2 - (outerWallMinScale/2);
                //back
                if ((i +1) % 4 == 0)
                    outerWallPos.z = -outerWallPos.z;
            }
            outerWalls[i].transform.position = outerWallPos;
            outerWalls[i].transform.localScale = outerWallScale;
        }
    }
}
