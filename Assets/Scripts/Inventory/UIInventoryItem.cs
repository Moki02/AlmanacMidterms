using System;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventoryItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private TMP_Text NameTxt;

    [SerializeField]
    private Image borderImage;

    public event Action<UIInventoryItem> OnItemClicked;

    private bool empty = true;

    public void Awake()
    {
        ResetData();
        Deselect();
    }

    public void ResetData()
    {
        this.itemImage.gameObject.SetActive(false);
        this.empty = true;
    }
    public void Deselect()
    {
        this.borderImage.enabled = false;
    }
    public void SetData(Sprite sprite, string name)
    {
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;
        this.NameTxt.text = name + "";
        this.empty = false;
    }

    public void Select()
    {
        this.borderImage.enabled = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (empty) return;
        if (eventData.button == PointerEventData.InputButton.Left)
        {

            Debug.Log($"Clicked on item: {gameObject.name}");
            

            OnItemClicked?.Invoke(this);
            Select(); 
        }
    }

    
}
