using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controla o movimento e restrições da cãmera
/// </summary>
public class CameraMovement : MonoBehaviour
{
	[SerializeField] private float _lerpT;

	/// <summary>
	/// Struct que repreenta as bordas da fase, as quais a câmera não pode passar
	/// </summary>
	private Bounds _camBounds;

	private Transform _playerTrs;

	/// <summary>
	/// Dimensões da câmera
	/// </summary>
	public static float camWidth;
	public static float camHeight;

	void Start()
	{
		_playerTrs = GameObject.FindGameObjectWithTag("Player").transform;
		_camBounds =
			GameObject.FindGameObjectWithTag("Cam Bounds").GetComponent<BoxCollider2D>().bounds;

		Camera cam = Camera.main;
		camHeight = 2f * cam.orthographicSize;
		camWidth = camHeight * cam.aspect;
	}

	void Update()
	{
		Vector3 destiny = _playerTrs.position;
		destiny.x = Mathf.Clamp(
			destiny.x, _camBounds.min.x + camWidth * 0.5f, _camBounds.max.x - camWidth * 0.5f);
		destiny.y = Mathf.Clamp(
			destiny.y, _camBounds.min.y + camHeight * 0.5f, _camBounds.max.y - camHeight * 0.5f);

		destiny = Vector3.Lerp(transform.position, destiny, Mathf.Pow(1 - _lerpT, Time.deltaTime));

		destiny.z = -10f;

		transform.position = destiny;
	}
}