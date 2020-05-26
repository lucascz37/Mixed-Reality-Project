using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GetFiles : MonoBehaviour
{
    private files _objetos;

    [SerializeField]
    private GameObject _Buttonprefab;
    void Start()
    {
        StartCoroutine(GetRequest("http://192.168.0.102:3000/"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();


            if (webRequest.isNetworkError)
            {
                Debug.Log("Error: " + webRequest.error);
            }
            else
            {
                _objetos = JsonUtility.FromJson<files>(webRequest.downloadHandler.text);
                createButtons();
            }
        }
    }

    void createButtons()
    {
        foreach(Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        for(int i = 0; i < _objetos.file.Length; i++)
        {
            string file = _objetos.file[i];
            if (file.Contains(".obj"))
            {
                _Buttonprefab.GetComponentInChildren<Text>().text = file.Replace(".obj", "");
                Instantiate(_Buttonprefab, transform).transform.position = new Vector3(307,-(i*25)+200,0);
            }
        }
    }
}

[System.Serializable]
public class files
{
    public string[] file;
}
