using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ImageFadeState
{
    FadeIn,
    Inside,
    FadeOut
}

public class ImageFade : MonoBehaviour
{

    [SerializeField] private float fadeInDuration;
    [SerializeField] private float inDuration;
    [SerializeField] private float fadeOutDuration;

    private Image image;
    private ImageFadeState currentState = ImageFadeState.FadeIn;
    private float p = 0f;

    void Start()
    {
        image = GetComponent<Image>();

		Color newColor = image.color;
		newColor.a = 0f;
		image.color = newColor;
	}

    void Update()
    {
        switch(currentState)
        {
            case ImageFadeState.FadeIn:
                {
                    p += Time.deltaTime;
                    Color newColor = image.color;
                    newColor.a = p / fadeInDuration;
                    image.color = newColor;
                    if (p >= fadeInDuration)
                    {
                        p = 0f;
                        currentState = ImageFadeState.Inside;
                    }
                }break;
			case ImageFadeState.Inside:
				{
					p += Time.deltaTime;
					if (p >= inDuration)
					{
						p = 0f;
						currentState = ImageFadeState.FadeOut;
					}
				}
				break;
			case ImageFadeState.FadeOut:
				{
					p += Time.deltaTime;
					Color newColor = image.color;
					newColor.a = 1f - (p / fadeInDuration);
					image.color = newColor;
					if (p >= fadeOutDuration)
					{
                        Destroy(gameObject);
					}
				}
				break;
		}
    }
}
