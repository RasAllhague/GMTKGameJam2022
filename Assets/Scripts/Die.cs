using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    // Texture of the cube
    public Texture2D texture;

    // id of the cube
    public string id;

    public string typeName;

    void Start()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if(renderer != null)
        {
            renderer.material.SetTexture("_MainTex", texture);
        } else
        {
            Debug.LogError("Die / Cube / Die.cs / MeshRenderer not found");
        }

        GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-15f, 15f), 0f, Random.Range(-15f, 15f)), ForceMode.Impulse);
    }

    private void Update()
    {
        if (transform.position.y < -10)
        {
            GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            if (gm != null)
            {
                gm.DieFellOfThePlane(this.id, this.typeName);
                Destroy(this.gameObject);
            }
            else
            {
                Debug.LogWarning("Die fell of the cliff, the Game Manager was not found");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            if (gm != null)
            {
                bool correct = gm.OnPlayerCollideWithDie(this.id, this.typeName);

                if (correct)
                {
                    Destroy(this.gameObject);
                }
            }
            else
            {
                Debug.LogWarning("On Collision with Die Cube, the Game Manager was not found");
            }
        }
    }
}
