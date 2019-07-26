using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Linea para crear el objeto ObjectData en el proyecto
[CreateAssetMenu(fileName = "New ObjectData", menuName = "Object Data", order = 51)]

/*Los ScriptableObjects funcionan de la siguiente manera: creas una clase (esta) con toda su información, después
 * mediante la linea de arriba, puedes crear dentro del proyecto (está en la carpeta Scripts -> ObjectData) 
 * un archivo modificable desde inspector, poniendo todos sus datos. Después a cada objeto se le añade una variable ObjectData
 * y se le asigna la suya, de esa manera, tiene acceso a toda su información insitu y ahorramos memoria, además de todos los componentes que quieran acceder a ella.
 * Por ejemplo: Los SlotInfo tienen un ObjectData del objeto que tienen asignado, de esa manera comprueban si el objeto recogido (el currentObject.objectData) es igual
 * al suyo, y se activan.
 */
public class ObjectData : ScriptableObject
{
	[SerializeField]
	private int _id;
	[SerializeField]
	private string _objectName;
	[SerializeField]
	private string _description;
	[SerializeField]
	private Sprite _icon;
	[SerializeField]
	private GameObject _target;

	public int id { get { return _id; } }

	public string objectName { get { return _objectName; } }

	public string description { get { return _description; } }

	public Sprite icon { get { return _icon; } }

	public GameObject target { get { return _target; } }
}
