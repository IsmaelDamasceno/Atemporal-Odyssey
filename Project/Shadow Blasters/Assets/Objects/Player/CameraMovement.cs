using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

	[SerializeField] private float _lerpT;

	private Bounds _camBounds;
	private Transform _playerTrs;

	private float _camWidth;
	private float _camHeight;

	void Start()
	{
		_playerTrs = GameObject.FindGameObjectWithTag("Player").transform;
		_camBounds =
			GameObject.FindGameObjectWithTag("Cam Bounds").GetComponent<BoxCollider2D>().bounds;

		Camera cam = Camera.main;
		_camHeight = 2f * cam.orthographicSize;
		_camWidth = _camHeight * cam.aspect;
	}

	void Update()
	{
		Vector3 destiny = _playerTrs.position;
		destiny.x = Mathf.Clamp(
			destiny.x, _camBounds.min.x + _camWidth * 0.5f, _camBounds.max.x - _camWidth * 0.5f);
		destiny.y = Mathf.Clamp(
			destiny.y, _camBounds.min.y + _camHeight * 0.5f, _camBounds.max.y - _camHeight * 0.5f);

		destiny = Vector3.Lerp(transform.position, destiny, 1 - Mathf.Pow(_lerpT, Time.deltaTime));

		destiny.z = -10f;

		transform.position = destiny;
	}
}