using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slots : MonoBehaviour
{
    /*This is just to assign to the InGameSlots object, so that it knows that 
     this is a UI element that represents a slot. It'll grab the GameObject name,
     and fill in the transform as well, and a function to add an image into that slot
     based on what's been added.*/

    Image slotImage = null;
    Transform slotLocation = null;


    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Init()
    {
        slotImage ??= GetComponent<Image>();
        slotLocation = slotImage.gameObject.transform;
    }

    public void AddObjectImage(IActionableItem item)
    {
        if (item.itemName == null) return;
        slotImage.sprite = GameManager.SpriteAtlas.GetSprite($"s_{item.itemName}");
    }
}
