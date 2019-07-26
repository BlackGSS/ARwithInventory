using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionController : MonoBehaviour
{
	#region Attributes
	private RaycastHit _hitInfo;

	[SerializeField]
	GameObject _slotsPanel;

	[SerializeField]
	GameObject _slot;

	public int slotsAmount;
	private List<GameObject> _allSlots = new List<GameObject>();

	[SerializeField]
	private GameObject[] _allItems;

	#endregion

	#region UnityCallbacks

	void Start()
	{
		InitializeItems();
	}
	#endregion

	#region Methods
	/// <summary>
	/// Creamos la colección dinámicamente
	/// </summary>
	private void InitializeItems()
	{
		_allItems = Resources.LoadAll<GameObject>("Items");

		for (int i = 0; i < slotsAmount; i++)
		{
			_allSlots.Add(Instantiate(_slot, _slotsPanel.transform));

			GameObject goitem = Instantiate(_allItems[i], _allSlots[i].transform);
		}

	}
	#endregion
}
