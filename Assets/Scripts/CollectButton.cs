using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectButton : MonoBehaviour
{
	private void OnEnable()
	{
		CheckToDisable();
	}

	/// <summary>
	/// Cuando se incia comprueba si el objeto al que pertenece ha sido encontrado ya, de manera que si ha sido ya recolectado 
	/// se desactiva para que no se pueda volver a recolectar
	/// </summary>
	public void CheckToDisable()
	{
		if (GameManager.instance.dataObjects.Contains(GetComponentInParent<Object>().objectData))
		{
			transform.gameObject.SetActive(false);
			//Debug.Log(transform.gameObject.activeSelf);
		}
	}
}
