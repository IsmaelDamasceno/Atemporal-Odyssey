using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private Sprite unselected;
    [SerializeField] private Sprite selected;

    private Image image;
    public bool active = false;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        
    }

    public void SetSelected(bool active)
    {
		image.sprite = active ? selected : unselected; 
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        active = true;
    }

	public void OnPointerExit(PointerEventData eventData)
	{
        active = false;
	}
}
