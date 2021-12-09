using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System;

public class SlotHandler : MonoBehaviour,  IDropHandler
{
    public Elementos2 elementos2;
    [SerializeField] Elementos2.Orderlyness orderlyness;
    [SerializeField] SlotHandler fallbackSlot;

    Color actualSlotColor;

    private void Start()
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Elementos2.Elementos elementoRecienCogido = Elementos2.Elementos.Ninguno;

            for (int i = 0; i < (int)Elementos2.Elementos.NumElementos; i++)
            {
                if (eventData.pointerDrag.CompareTag(((Elementos2.Elementos)i).ToString()) && (elementoRecienCogido != (Elementos2.Elementos)i))
                {
                    elementoRecienCogido = (Elementos2.Elementos)i;
                }
            }

            Debug.Log(elementoRecienCogido);

            elementos2.SetElemento(orderlyness, elementoRecienCogido);

            AcquireImageFromDragged(eventData.pointerDrag);
            Destroy(eventData.pointerDrag);
        }
    }

    internal void AcquireImageFromDragged(GameObject dragged)
    {
        gameObject.GetComponent<Image>().sprite = dragged.GetComponent<Image>().sprite;
    }

    internal void AcquireImage(SlotHandler slotHandler)
    {
        elementos2.slots[1].gameObject.GetComponent<Image>().sprite = slotHandler.gameObject.GetComponent<Image>().sprite;
    }
}
