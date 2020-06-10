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

    private bool _rotacionar = false;
    private void Update()
    {
        if (_rotacionar && _objetoCriado != null)
        {
            _objetoCriado.transform.Rotate(0, 5 * Time.deltaTime, 0);
        }
    }

    public void Rotacionar()
    {
        if (_rotacionar)
        {
            _rotacionar = false;
        }
        else
        {
            _rotacionar = true;
        }
    }

    public void AumentarTamanho()
    {
        if (_objetoCriado != null)
        {
            float tamanho = _objetoCriado.transform.localScale.x;
            tamanho += 0.001f;
            tamanho = Mathf.Clamp(tamanho, 0.001f, 0.01f);
            _objetoCriado.transform.localScale = new Vector3(tamanho, tamanho, tamanho);
        }
    }

    public void DiminuirTamanho()
    {
        if (_objetoCriado != null)
        {
            float tamanho = _objetoCriado.transform.localScale.x;
            tamanho -= 0.001f;
            tamanho = Mathf.Clamp(tamanho, 0.001f, 0.01f);
            _objetoCriado.transform.localScale = new Vector3(tamanho, tamanho, tamanho);
        }
    }

}
