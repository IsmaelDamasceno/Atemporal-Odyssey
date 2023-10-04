using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{

    [SerializeField] private Sprite unselected;
    [SerializeField] private Sprite selected;

    private Image image;

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
}
