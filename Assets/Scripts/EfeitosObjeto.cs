using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfeitosObjeto : MonoBehaviour
{

    [SerializeField]
    private GameObject _objetoCriado;
    public GameObject ObjetoCriado {
        get => _objetoCriado;
        set => _objetoCriado = value;
    }

    public void AumentarTamanho()
    {
        if (_objetoCriado != null)
        {
            float tamanho = _objetoCriado.transform.localScale.x;
            tamanho += 0.01f;
            tamanho = Mathf.Clamp(tamanho, 0.01f, 1.0f);
            _objetoCriado.transform.localScale = new Vector3(tamanho, tamanho, tamanho);
        }
    }

    public void DiminuirTamanho()
    {
        if (_objetoCriado != null)
        {
            float tamanho = _objetoCriado.transform.localScale.x;
            tamanho -= 0.01f;
            tamanho = Mathf.Clamp(tamanho, 0.01f, 1f);
            _objetoCriado.transform.localScale = new Vector3(tamanho, tamanho, tamanho);
        }
    }

}
