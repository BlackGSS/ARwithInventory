using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas3DController : MonoBehaviour
{
	#region Attributes

	public GameObject panel;
	public GameObject pos;

	private bool showPanel;
	private Vector3 originalPanelPos;
	#endregion

	#region Unity Callbacks

	void Start()
	{
		originalPanelPos = panel.transform.position;
	}
	
	void Update()
	{
		PanelControl(); //Controla el desplazamiento del panel
	}

	/// <summary>
	/// Resetea la posición y la booleana para al abrirlo de nuevo aparezca sin desplegar
	/// </summary>
	private void OnDisable()
	{
		panel.transform.position = originalPanelPos;
		showPanel = false;
	}

	#endregion

	#region Methods
	/// <summary>
	/// Controla el desplazamiento del panel desplegable
	/// </summary>
	private void PanelControl()
	{
		if (showPanel == true)
		{
			panel.transform.position = Vector3.MoveTowards(panel.transform.position, pos.transform.position, 1 * Time.deltaTime);
		}
		else
		{
			if (originalPanelPos != panel.transform.position)
			{
				panel.transform.position = Vector3.MoveTowards(panel.transform.position, originalPanelPos, 1 * Time.deltaTime);
			}
		}
	}

	/// <summary>
	/// Función para botón para activar o desactivar el panel
	/// </summary>
	public void ShowPanel()
	{
		if (!showPanel)
			showPanel = true;
		else
			showPanel = false;
	}
	#endregion
}
