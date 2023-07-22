using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{

    public bool Full = true;

    [SerializeField] private Sprite _emptySprite;
    [SerializeField] private Sprite _fullSprite;

    private Image _image;

    void Awake()
    {
        _image = GetComponent<Image>();
    }

    void Update()
    {
        
    }

    public void ChangeHeart(bool value)
    {
        Full = value;
        _image.sprite = (Full == true) ? _fullSprite : _emptySprite;
    }
}
