using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    // Texture of the cube
    public Texture2D texture;

    // id of the cube
    public string id;

    void Start()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if(renderer != null)
        {
            renderer.material.SetTexture("_MainTex", texture);
        } else
        {
            Debug.Log("Die / Cube / Die.cs / MeshRenderer not found");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //GameManager gm = GameObject.find("GameManager").GetComponent<GameManager>();
            //if(gm != null){
            //  gm.OnPlayerCollideWithDie(this.id);
            //} else {
            //  Debug.Log("On Collision with Die Cube, the Game Manager was not found");
            //}
        }
    }
}
