using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
	#region Attributes
	[SerializeField]
	private ObjectData _objectData;

	public ObjectData objectData { get { return _objectData; } }

	[SerializeField]
	private float _speedRot;

	private Animator _anim;
	public bool scene3D;
	public bool interactable;

	private GameObject collectButton;

	private Vector3 _originalARPos, _originalARScale;

	#endregion

	#region UnityCallBack
	/// <summary>
	/// Si se encuentra, pone a default sus propiades e inicializa sus métodos
	/// </summary>
	private void OnEnable()
	{
		if(!scene3D)
			Initialize();

		transform.rotation = Quaternion.Euler(Vector3.zero);
		_originalARPos = transform.position;
		_originalARScale = transform.localScale;

		_anim = GetComponent<Animator>();
		_anim.enabled = false;

		GameManager.instance.currentObject = transform.gameObject;

		Invoke("AnimInit", 1.2f);
		
	}

	private void Start()
	{
		GameManager.instance.DisplayName(scene3D, objectData.objectName);
	}

	/// <summary>
	/// Al pulsar activa el botón de coleccionarlo
	/// </summary>
	private void OnMouseDown()
	{
		if (interactable && !scene3D)
		{
			_anim.enabled = false;
			collectButton.SetActive(true);
		}
	}

	/// <summary>
	/// Al arrastrar sobre él lo rota para verlo mejor
	/// </summary>
	private void OnMouseDrag()
	{
		if (Input.GetMouseButton(0) && interactable)
		{
			transform.Rotate(Vector3.up * _speedRot * Time.deltaTime * Input.GetAxis("Mouse X") * -1);
		}
	}

	/// <summary>
	/// Si se desactiva, desactiva todos sus parámetros establecidos
	/// </summary>
	private void OnDisable()
	{
		GameManager.instance.currentObject = null;
		_anim.enabled = false;
		collectButton.SetActive(false);
		GetComponent<MeshRenderer>().enabled = false;
	}

	#endregion

	#region Methods
	/// <summary>
	/// Inicializa el objeto
	/// </summary>
	private void Initialize()
	{
		GetComponent<MeshRenderer>().enabled = false;
		collectButton = transform.GetChild(0).gameObject;
	}

	/// <summary>
	/// Inicializa las animaciones
	/// </summary>
	private void AnimInit()
	{
		GetComponent<MeshRenderer>().enabled = true;
		GetComponent<BoxCollider>().enabled = true;

		if (scene3D)
		{
			_anim.enabled = false;
		}
		else
		{
			_anim.enabled = true;
		}
	}
	
	public void ResetForAR()
	{
		transform.position = _originalARPos;
		transform.localScale = _originalARScale;
		scene3D = false;
		_anim.enabled = false;
	}
	#endregion
}
