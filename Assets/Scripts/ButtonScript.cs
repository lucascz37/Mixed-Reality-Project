using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    private string text;
    [SerializeField]
    private SpawnObject _script;
    void Start()
    {
        text = GetComponentInChildren<Text>().text;
        _script = GameObject.FindGameObjectWithTag("Respawn").GetComponent<SpawnObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void passFile()
    {
        _script.setObject(text);
    }
}
