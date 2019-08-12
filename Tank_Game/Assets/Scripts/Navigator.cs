using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigator : MonoBehaviour
{

    public static Navigator navi;
    public string currentNode;

    // Start is called before the first frame update
    void Start()
    {
        if (navi == null)
        {
            navi = this;
            DontDestroyOnLoad(this);
        } else
        {
            Destroy(gameObject);
        }
        currentNode = "MainMenu";
    }

    // Update is called once per frame
    public void Navigate(string from, string to)
    {
        GameObject previous = GameObject.Find(from);
        GameObject[] next = Resources.FindObjectsOfTypeAll<GameObject>();

        if (previous == null)
        {
            Debug.LogError("The parameter \"from\" is null.");
        }

        for (int i = 0; i < next.Length; i++)
        {
            if (next[i].name == to)
            {
                next[i].SetActive(true);
                previous.SetActive(false);
                currentNode = next[i].ToString();
            }
        }
    }
}
