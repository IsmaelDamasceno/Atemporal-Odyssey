using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

/// <summary>
/// Métodos de utilidade global
/// </summary>
public static class Globals
{
    private static PlayerControls _playerControls;

    public static PlayerControls InitiateControls()
    {
		if (_playerControls == null)
		{
			_playerControls = new PlayerControls();
            _playerControls.Enable();
		}
        return _playerControls;
    }

    public static Material GetFlashMaterial()
    {
        return Resources.Load<Material>("Damage Matt");
    }

    public static Vector2 GetCamSize()
    {
		Camera cam = Camera.main;
		float camHeight = 2f * cam.orthographicSize;
		float camWidth = camHeight * cam.aspect;
        return new Vector2(camWidth, camHeight);
	}
}
