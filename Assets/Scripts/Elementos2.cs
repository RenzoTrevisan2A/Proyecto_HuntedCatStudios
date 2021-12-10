using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine;

public class Elementos2 : MonoBehaviour
{
    public enum Orderlyness
    {
        Primary,
        Secondary,
    };

    [SerializeField] Material[] materialPoderes;
    [SerializeField] GameObject[] RecetaPoderUnico;

    [SerializeField] GameObject PoderActual;
    [SerializeField] GameObject PoderSecundario;

    [SerializeField] public SlotHandler[] slots;
    [SerializeField] public Sprite[] spritesUI;

    public bool poderDeTierraActivo = false;
    public bool poderDeFuegoActivo = false;
    public bool poderDeAguaActivo = false;

    bool CambioElem = false;

    public enum Elementos
    {
        Fuego,
        Agua,
        Tierra,
        Aire,

        NumElementos,

        Ninguno = -1,
    };
     public Elementos elementoPrincipal = Elementos.Ninguno;
     public Elementos elementoSecundario = Elementos.Ninguno;

    // Cache of the renderers
    Renderer p1;
    Renderer p2;

    // Start is called before the first frame update
    void Start()
    {
        p1 = PoderActual.GetComponent<Renderer>();
        p2 = PoderSecundario.GetComponent<Renderer>();
    }

    Elementos oldElementoPrincipal = Elementos.Ninguno;
    Elementos oldElementoSecundario = Elementos.Ninguno;
    void Update()
    {
        // Gestion de cambio
        // de elementos
        if (oldElementoPrincipal != elementoPrincipal)
        {
            p1.material = materialPoderes[(int)elementoPrincipal];
            oldElementoPrincipal = elementoPrincipal;
        }

        if (oldElementoSecundario != elementoSecundario)
        {
            p2.material = materialPoderes[(int)elementoSecundario];
            oldElementoSecundario = elementoSecundario;
        }

        if (elementoSecundario != Elementos.Ninguno)
        {
            if (CambioElem)
            {
                IntercambiaElementos();
                IntercambiaElementosUI();
                CambioElem = false;
            }
        }

        if(elementoPrincipal == Elementos.Fuego || elementoPrincipal == Elementos.Agua)
        {
            AsignSpriteElementUI();
        }


        if (elementoPrincipal == Elementos.Tierra)
        {
            poderDeTierraActivo = true;
        }
        else
        {
            poderDeTierraActivo = false;
        }

        if (elementoPrincipal == Elementos.Fuego)
        {
            poderDeFuegoActivo = true;
        }
        else if(poderDeFuegoActivo && elementoPrincipal != Elementos.Fuego)
        {
            poderDeFuegoActivo = false;
        }

        if (elementoPrincipal == Elementos.Agua)
        {
            poderDeAguaActivo = true;
        }
        else if (poderDeFuegoActivo && elementoPrincipal != Elementos.Agua)
        {
            poderDeAguaActivo = false;
        }
    }

    public void SetElemento(Orderlyness orderlyness, Elementos nuevoElemento)
    {
        switch (orderlyness)
        {
            case Orderlyness.Primary:
                if (elementoPrincipal != nuevoElemento)
                {
                    SetElementoPrincipal(nuevoElemento);
                }
                break;
            case Orderlyness.Secondary:
                SetElementoSecundario(nuevoElemento);
                break;
        }
    }

    void SetElementoPrincipal(Elementos nuevoElemento)
    {
        if (nuevoElemento != Elementos.Ninguno)
        {
            if (elementoPrincipal != Elementos.Ninguno)
            {
                elementoSecundario = elementoPrincipal;
                slots[1].AcquireImage(slots[0]);
            }
            elementoPrincipal = nuevoElemento;

            Destroy(RecetaPoderUnico[(int)elementoPrincipal]);            
        }
    }
    void SetElementoSecundario(Elementos nuevoElementoSecundario)
    {
        elementoSecundario = nuevoElementoSecundario;
    }

    void OnCambioDeElementos(InputValue input)
    {
        CambioElem = input.isPressed;
    }

    void IntercambiaElementos()
    {
        Elementos auxiliar = elementoSecundario;

        elementoSecundario = elementoPrincipal;
        elementoPrincipal = auxiliar;
    }

    void IntercambiaElementosUI()
    {
        Sprite auxiliar = slots[1].GetComponent<Image>().sprite;

        slots[1].GetComponent<Image>().sprite = slots[0].GetComponent<Image>().sprite;
        slots[0].GetComponent<Image>().sprite = auxiliar;
    }

    void AsignSpriteElementUI()
    {
        if(elementoSecundario != Elementos.Ninguno)
        {
            if (elementoSecundario == Elementos.Fuego)
            {
                slots[1].GetComponent<Image>().sprite = spritesUI[0];
            }
            else if (elementoSecundario == Elementos.Agua)
            {
                slots[1].GetComponent<Image>().sprite = spritesUI[1];
            }
        }
        else if (elementoPrincipal != Elementos.Ninguno)
        {
            if (elementoPrincipal == Elementos.Fuego)
            {
                slots[0].GetComponent<Image>().sprite = spritesUI[0];
            }
            else if (elementoPrincipal == Elementos.Agua)
            {
                slots[0].GetComponent<Image>().sprite = spritesUI[1];
            }
        }
    }
}
