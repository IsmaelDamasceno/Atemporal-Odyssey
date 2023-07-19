using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public static class Globals
{
    private static PlayerControls _playerControls;

    public static PlayerControls InitiateControls()
    {
		if (_playerControls == null)
		{
			_playerControls = new PlayerControls();
		}
        return _playerControls;
    }
}
