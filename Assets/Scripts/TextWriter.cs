using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextWriter : MonoBehaviour
{
    [SerializeField] private float timeBetweenLetters = 0.01f;
    private TextMeshProUGUI textMesh;
    private int charIndex = 0;
    private float lastLetterTime = 0;
    private string textToWrite = "";
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        textToWrite = textMesh.text.Replace("\\n", "\n");
        textMesh.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Updating" + Time.time + " " + lastLetterTime + timeBetweenLetters + " "+ Time.timeScale);
        if (Time.time < lastLetterTime + timeBetweenLetters)
            return;
        Debug.Log("IUpdating");
        lastLetterTime = Time.time;
        if (charIndex < textToWrite.Length)
        {
            Debug.Log("RUpdating");
            textMesh.text += textToWrite[charIndex];
            charIndex++;
        }
        else
        {
            Debug.Log("DustroyUpdating");
            Destroy(this);
        }
        Debug.Log("FUpdating");
    }
}
