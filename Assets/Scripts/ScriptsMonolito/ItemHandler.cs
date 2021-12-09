using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Vector2 position;
    [SerializeField] public Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public Transform panel;
    public GameObject slot;

    private Image img;
    private Image imgInstanced;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        img = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        position = transform.position;

        img = eventData.pointerDrag.GetComponent<Image>();
        eventData.pointerDrag.transform.SetParent(panel.transform);

        
        imgInstanced = Instantiate(img, panel.transform);
        imgInstanced.name = $"instanced{tag}";
        canvasGroup.blocksRaycasts = false;
    }

    // Drag the selected item.
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        Destroy(eventData.pointerDrag);
    }
}
