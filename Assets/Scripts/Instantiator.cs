using UnityEngine;
using UnityEngine.EventSystems;
public class Instantiator : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject objectPrefab; //Objeto a instanciar
    private RectTransform panelRectTransform; //Objeto que contiene el script
    private void Start()
    {
        panelRectTransform = GetComponent<RectTransform>();
    }
    private GameObject spawnedObject; //Objeto ya instanciado

    public void OnPointerDown(PointerEventData eventData)
    {
        //Instancia prefab si es tocado el objeto
        if (eventData.pointerCurrentRaycast.gameObject == gameObject)
        {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRectTransform, eventData.position, eventData.pressEventCamera, out localPoint);
            spawnedObject = Instantiate(objectPrefab, transform.localPosition, Quaternion.identity, transform);
            spawnedObject.transform.localPosition = Vector3.zero;
            if (spawnedObject != null)
            {
                Transform childTransform = spawnedObject.transform.GetChild(0);
                childTransform.localPosition = localPoint; // Modifica la posición del hijo
            }
        }
    }
}
