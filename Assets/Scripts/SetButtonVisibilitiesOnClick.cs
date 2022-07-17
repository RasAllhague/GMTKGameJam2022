using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetButtonVisibilitiesOnClick : MonoBehaviour
{
    [SerializeField] private GameObject[] ObjectsToShow;
    [SerializeField] private GameObject[] ObjectsToHide;
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetVisibilities);
    }

    private void SetVisibilities()
    {
        Debug.Log("SETTING");
        foreach (GameObject obj in ObjectsToShow)
        {
            obj.SetActive(true);
        }
        foreach (GameObject obj in ObjectsToHide)
        {
            obj.SetActive(false);
        }
    }

    
}
