using Assets.scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card : MonoBehaviour
{

    public Sprite Icon;
    private bool isOpen;
    public GameObject CardBack;
    public GameObject CardFront;
    public GameObject CardIcon;
    Board BoardObj;
    public float Width;
    public float Height;

    public bool IsOpen
    {
        get { return isOpen; }
    }


    void Start()
    {
        SpriteRenderer spriteRendererObj = CardIcon.GetComponent<SpriteRenderer>();
        spriteRendererObj.sprite = Icon;
        BoardObj = FindObjectOfType<Board>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnMouseDown()
    {
        OpenCard();
    }

    public void OpenCard()
    {
        if (!BoardObj.CanCardOpen())
            return;
        
            isOpen = true;
            CardBack.SetActive(false);
            CardFront.SetActive(true);
            BoardObj.CardOpened(this);
        
    }

    public void CloseCard()
    {
        isOpen = false;
        CardBack.SetActive(true);
        CardFront.SetActive(false);
    }

}
