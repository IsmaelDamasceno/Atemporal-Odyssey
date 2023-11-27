using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum MenuMode
{
    Keyboard,
    Mouse,
}

public class MenuController : MonoBehaviour
{

    private List<MenuButton> menuButton = new();
    private int selected = 0;
	private static PlayerControls _controls;

    private bool inputLastFrame = false;

    private static float menuInput;
    private static bool selectInput;

    private MenuMode mode = MenuMode.Keyboard;
    private Vector2 mouseLastPos;

	void Start()
    {
        foreach(Transform trs in transform)
        {
            menuButton.Add(trs.GetComponent<MenuButton>());
        }

        menuButton[0].SetSelected(true);
	}

    private void Awake()
    {
		if (_controls == null)
		{
			_controls = Globals.InitiateControls();

			_controls.Player.Ladder.performed += (ctx) => { menuInput = -ctx.ReadValue<float>(); };
			_controls.Player.Ladder.canceled += (_) => { menuInput = 0f; };

			_controls.Player.Jump.performed += (_) => { selectInput = true; };
			_controls.Player.Jump.canceled += (_) => { selectInput = false; };
		}
		else
		{
			Debug.Log(_controls);
		}

		mouseLastPos = Input.mousePosition;
	}

	void Update()
    {
        Vector2 curMousePos = Input.mousePosition;
        if (curMousePos != mouseLastPos)
        {
            mode = MenuMode.Mouse;
        }
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
			mode = MenuMode.Keyboard;
		}
		mouseLastPos = curMousePos;

		switch(mode)
        {
            case MenuMode.Keyboard:
                {
					if (menuInput != 0f && !inputLastFrame)
					{
						menuButton[selected].SetSelected(false);
						selected = selected + (int)menuInput;
						if (selected < 0)
						{
							selected = menuButton.Count - 1;
						}
						else if (selected == menuButton.Count)
						{
							selected = 0;
						}

						menuButton[selected].SetSelected(true);
					}
					inputLastFrame = menuInput != 0f;

					if (selectInput)
					{
						menuButton[selected].GetComponent<IButtonAction>().ButtonPress();
					}
				}
				break;
            case MenuMode.Mouse:
                {
					int index = 0;
					bool broke = false;
					foreach(MenuButton curMenuBtn in menuButton)
					{
						if (curMenuBtn.active)
						{
							menuButton[selected].SetSelected(false);
							selected = index;
							curMenuBtn.SetSelected(true);

							broke = true;
							break;
						}
						index ++;
					}
					if (!broke)
					{
						menuButton[selected].SetSelected(false);
					}

					if (Input.GetMouseButtonDown(0))
					{
						menuButton[selected].GetComponent<IButtonAction>().ButtonPress();
					}
                }break;
        }

		if (selectInput || Input.GetMouseButtonDown(0))
		{
			menuButton[selected].GetComponent<IButtonAction>().ButtonPress();
		}
	}
}
