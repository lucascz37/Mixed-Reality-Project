using Dummiesman;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class SpawnObject : MonoBehaviour
{
    [SerializeField]
    private GameObject _loading;
    [SerializeField]
    private GameObject _done;
    [SerializeField]
    private GameObject _criado;
    private ARRaycastManager _gerenciadorRayCast;
    private EfeitosObjeto _gameManager;

    static List<ARRaycastHit> acertos = new List<ARRaycastHit>();
    private void Awake()
    {
        _gerenciadorRayCast = GetComponent<ARRaycastManager>();
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EfeitosObjeto>();
    }

    bool PegarPosicaoToque(out Vector2 _posicaoToque)
    {
        if(Input.touchCount > 0)
        {
            if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)){
                _posicaoToque = Input.GetTouch(0).position;
                return true;
            }
        }
        _posicaoToque = default;
        return false;
    }

    private void Update()
    {
        if(!PegarPosicaoToque(out Vector2 _posicaoToque))
        {
            return;
        }
 
        if(_gerenciadorRayCast.Raycast(_posicaoToque, acertos, TrackableType.PlaneWithinPolygon))
        {
            var lugar = acertos[0].pose;

            if(_criado != null)
            {
                _criado.transform.position = lugar.position;
                if(_criado.activeSelf == false)
                {
                    _criado.SetActive(true);
                }
            }

        }
    }

    public void setObject(string prefab)
    {
        Vector3 position = new Vector3(0,0,0);
        Quaternion rotation = new Quaternion(0,0,0,0);
        bool nulo = true;
        if (_criado != null)
        {
            position = _criado.transform.position;
            rotation = _criado.transform.rotation;
            Destroy(_criado);
            nulo = false;
        }
        string path = "http://192.168.0.102:3000/" + prefab;
        StartCoroutine(newObject(path + ".obj", path + ".mtl", position, rotation, nulo));
    }

    IEnumerator newObject(string uri, string mtl,Vector3 position, Quaternion rotation, bool nulo)
    {
        string obj = "Teste";
        string mtlObj = "Teste";
        _loading.SetActive(true);
        _done.SetActive(false);
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
                obj = webRequest.downloadHandler.text;
            }
        }

        using (UnityWebRequest webRequest1 = UnityWebRequest.Get(mtl))
        {
            yield return webRequest1.SendWebRequest();

            if (webRequest1.isNetworkError)
            {
                Debug.Log("Error: " + webRequest1.error);
            }
            else
            {
                mtlObj = webRequest1.downloadHandler.text;
            }
        }
        

        if (obj != "Teste" && mtlObj != "Teste" )
        {
            var textStream = new MemoryStream(Encoding.UTF8.GetBytes(obj));
            var mtlStream = new MemoryStream(Encoding.UTF8.GetBytes(mtlObj));
            _criado = new OBJLoader().Load(textStream, mtlStream);
            _criado.transform.localScale = new Vector3(0.005f, 0.005f, 0.005f);
            _gameManager.ObjetoCriado = _criado;
            if (nulo)
            {
                _criado.SetActive(false);
            }
            else
            {
                _criado.transform.position = position;
                _criado.transform.rotation = rotation;
            }
            _loading.SetActive(false);
            _done.SetActive(true);
        }

    }
}
