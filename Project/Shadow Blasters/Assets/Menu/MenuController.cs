using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{

    private List<MenuButton> menuButton = new();
    private int selected = 0;
	private PlayerControls _controls;

    private bool inputLastFrame = false;

    private float menuInput;
    private bool selectInput;

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
		_controls = Globals.InitiateControls();

		_controls.Player.Ladder.performed += (ctx) => { menuInput = ctx.ReadValue<float>(); };
		_controls.Player.Ladder.canceled += (_) => { menuInput = 0f; };

		_controls.Player.Jump.performed += (_) => { selectInput = true; };
		_controls.Player.Jump.canceled += (_) => { selectInput = false; };
	}

    private void OnEnable() => _controls.Enable();
	private void OnDisable() => _controls.Disable();

	void Update()
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
}
