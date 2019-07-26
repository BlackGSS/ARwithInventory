using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotInfo : MonoBehaviour
{
	#region Attributes
	public ObjectData objectInfo;

	#endregion

	#region UnityCallBacks
	private void OnEnable()
	{
		CheckIfObjectWasFounded();
	}

	void Start()
	{
		GetComponent<Button>().onClick.AddListener(ShowModel);
	}
	#endregion

	#region Methods
	/// <summary>
	/// Comprueba si el objeto al que pertenece ha sido encontrado
	/// </summary>
	private void CheckIfObjectWasFounded()
	{
		if (GameManager.instance.dataObjects.Contains(objectInfo))
		{
			transform.tag = "Interactable";
			Color temp = Color.green;
			GetComponent<Image>().color = temp;
		}
	}

	/// <summary>
	/// Muestra el modelo en 3D
	/// </summary>
	private void ShowModel()
	{
		if (transform.tag == "Interactable")
		{
			GameManager.instance.ShowModelId(objectInfo.id);
			GameManager.instance.DisplayUI3D(objectInfo.objectName, objectInfo.description);
		}
	}
	#endregion
}
