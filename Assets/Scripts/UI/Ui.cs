using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ui : MonoBehaviour
{
    public Elementos2 elementos2;
    [SerializeField] Elementos2.Orderlyness orderlyness;
    Sprite initialSprite;

    private void Awake()
    {
        gameObject.GetComponent<Image>().enabled = false;
        initialSprite = gameObject.GetComponent<Image>().sprite;
    }

    private void FixedUpdate()
    {
        switch (orderlyness)
        {
            case Elementos2.Orderlyness.Primary:
                gameObject.GetComponent<Image>().sprite = elementos2.slots[0].gameObject.GetComponent<Image>().sprite;
                if (initialSprite != gameObject.GetComponent<Image>().sprite) { gameObject.GetComponent<Image>().enabled = true; }
                break;
            case Elementos2.Orderlyness.Secondary:
                gameObject.GetComponent<Image>().sprite = elementos2.slots[1].gameObject.GetComponent<Image>().sprite;
                if (initialSprite != gameObject.GetComponent<Image>().sprite) { gameObject.GetComponent<Image>().enabled = true; }
                break;
        }
    }
}
