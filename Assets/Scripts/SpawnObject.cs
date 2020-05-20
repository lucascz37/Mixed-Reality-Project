using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class SpawnObject : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefab;
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
            _posicaoToque = Input.GetTouch(0).position;
            return true;
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

            if(_criado == null)
            {
                _criado = Instantiate(_prefab, lugar.position, lugar.rotation);
                _gameManager.ObjetoCriado = _criado;
            }
            else
            {
                _criado.transform.position = lugar.position;
            }
        }
    }
}
