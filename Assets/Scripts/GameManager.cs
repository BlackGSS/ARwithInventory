using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
	#region Attributes
	public static GameManager instance; //Singleton
	
	public GameObject currentObject;

	[SerializeField]
	private Transform pivotViewer;
	[SerializeField]
	private GameObject arCamera;
	[SerializeField]
	private GameObject viewerCamera;

	private List<GameObject> _targets = new List<GameObject>();
	private List<GameObject> _listedObjects = new List<GameObject>();
	public List<ObjectData> dataObjects = new List<ObjectData>();

	private RaycastHit _hitInfo;

	public GameObject collectionCanvas;
	public GameObject canvas3D;
	public GameObject canvasAR;
	
	public GameObject collectButton;

	private TextMesh nameObject; //Texto3D

	private int _currentModel3dID;

	public TextMeshProUGUI nameText, descriptionText; //TextoUI3D

	#endregion

	#region Unity Callback

	void Start()
    {
		currentObject = null;
		instance = this;

		CreateInitialObjects();
	}
	
	void Update()
    {
		if(arCamera.activeSelf)
			CollectObject();
	}

	#endregion

	#region Methods
	/// <summary>
	/// Busca si donde hemos pulsado puede recoger el objeto
	/// </summary>
	private void CollectObject()
	{
		if (Input.GetMouseButtonDown(0) && currentObject.GetComponent<Object>().interactable)
		{
			Ray RayInfo = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(RayInfo, out _hitInfo))
			{
				Debug.Log(_hitInfo);
				if (_hitInfo.collider.tag == "Collect")
				{
					collectButton = _hitInfo.collider.gameObject;
					AddFoundedObject(_hitInfo.collider.gameObject.transform.parent.gameObject); //Añade el objeto encontrado
					collectionCanvas.SetActive(true);
				}
			}
		}
	}
	
	/// <summary>
	/// Inicializa los imagetargets y los objetos, ambos de forma dinámica
	/// </summary>
	private void CreateInitialObjects()
	{
		 GameObject[] _allItems = Resources.LoadAll<GameObject>("Targets");

		for (int i = 0; i < _allItems.Length; i++)
		{
			for (int j = 0; j < _allItems[i].transform.childCount; j++) //Desactivar todos los hijos de los targets 
			{
				_allItems[i].transform.GetChild(j).gameObject.SetActive(false);
			}

			GameObject goTarget = Instantiate(_allItems[i]); //Instancia el target
			_targets.Add(goTarget);

			if (!_listedObjects.Contains(goTarget.transform.GetChild(0).gameObject)) //Comprueba si ya está añadido
				_listedObjects.Add(goTarget.transform.GetChild(0).gameObject); //Si no, añade el hijo (el objeto) a la lista

			//Debug.Log(_listedObjects);
		}
	}

	/// <summary>
	/// Muestra el modelo en el visor 3D
	/// </summary>
	/// <param name="id"></param>
	internal void ShowModelId(int id)
	{
		//Activar y desactivar 
		canvasAR.SetActive(false);
		viewerCamera.SetActive(true);
		canvas3D.SetActive(true);
		arCamera.SetActive(false);
	
		_listedObjects[id].SetActive(true);
		_listedObjects[id].GetComponent<Object>().scene3D = true;
		collectionCanvas.SetActive(false);

		_listedObjects[id].transform.parent = pivotViewer;
		_listedObjects[id].transform.position = pivotViewer.position;
		_listedObjects[id].transform.rotation = pivotViewer.rotation;
		_listedObjects[id].transform.localScale = Vector3.one;
		_listedObjects[id].GetComponent<MeshRenderer>().enabled = true;
		_listedObjects[id].GetComponent<BoxCollider>().enabled = true;

		_currentModel3dID = id;
	}

	/// <summary>
	/// Muestra el nombre del texto en AR
	/// </summary>
	/// <param name="active3D"></param>
	/// <param name="name"></param>
	internal void DisplayName(bool active3D, string name)
	{
		if (!active3D)
		{
			nameObject = FindObjectOfType<TextMesh>();
			nameObject.GetComponent<TextMesh>().text = name;
		}
	}

	/// <summary>
	/// Añade el objeto encontrado
	/// </summary>
	/// <param name="currentObj"></param>
	public void AddFoundedObject(GameObject currentObj)
	{
		Debug.Log(currentObj);

		if (!dataObjects.Contains(currentObj.GetComponent<Object>().objectData))
		{
			Debug.Log(dataObjects);
			dataObjects.Add(currentObj.GetComponent<Object>().objectData);
		}
	}

	/// <summary>
	/// Cambia a la escena AR
	/// </summary>
	public void ChangeAR()
	{
		canvasAR.SetActive(true);

		if (viewerCamera.activeSelf)
			viewerCamera.SetActive(false);

		arCamera.SetActive(true);
		collectionCanvas.SetActive(false);
		currentObject.SetActive(false);

	}
	
	/// <summary>
	/// Botón para volver a la colección desde la escena 3D
	/// </summary>
	public void BackToCollection()
	{
		canvas3D.SetActive(false);
		collectionCanvas.SetActive(true);
		canvasAR.SetActive(false);
		_listedObjects[_currentModel3dID].SetActive(false);

		_listedObjects[_currentModel3dID].transform.parent = _targets[_currentModel3dID].transform;
		_listedObjects[_currentModel3dID].GetComponent<Object>().ResetForAR();
	}

	/// <summary>
	/// Muestra la información del objeto en la escena 3D
	/// </summary>
	/// <param name="name"></param>
	/// <param name="description"></param>
	internal void DisplayUI3D(string name, string description)
	{
		nameText.text = name;
		descriptionText.text = description;
	}

	/// <summary>
	/// Muestra la colección
	/// </summary>
	public void ShowCollection()
	{
		collectionCanvas.SetActive(true);
		canvasAR.SetActive(false);
		arCamera.SetActive(false);
	}

	#endregion
}
