using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float time = 0f;
    public static Timer instance;
    public static bool playing = true;

    void Start()
    {
		text = GetComponent<TextMeshProUGUI>();
		instance = this;
	}

    void Update()
    {
        if (!playing)
        {
            return;
        }

        time += Time.deltaTime;
        string minutes = (Mathf.Floor(time / 60f) % 60f).ToString();
        if (float.Parse(minutes) <= 9)
        {
            minutes = $"0{minutes}";
        }

        string seconds = (Mathf.Floor(time) % 60f).ToString();
		if (float.Parse(seconds) <= 9)
		{
			seconds = $"0{seconds}";
		}

		string microSecs = (Mathf.Floor(time * 60f) % 60f).ToString();
		if (float.Parse(microSecs) <= 9)
		{
			microSecs = $"0{microSecs}";
		}

		text.text = $"{minutes}:{seconds}:{microSecs}";
    }
}
