using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour,IEndDragHandler, IDragHandler
{
    private Canvas canvas;
    private RectTransform recTransform;
    private Vector3 initialLocalPosition;
    private void Start()
    {
        // Guardar la posición local inicial del objeto
        initialLocalPosition = transform.localPosition;
    }
    private void Awake()
    {
        recTransform = GetComponent<RectTransform>();
        canvas = FindObjectOfType<Canvas>();
    }  
    public void OnDrag(PointerEventData eventData)
    {
        //mueve el objeto en el canvas
        recTransform.anchoredPosition += eventData.delta / canvas.scaleFactor; 
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        //devuelve el objeto a su posicion inicial
        LeanTween.moveLocal(gameObject, initialLocalPosition, 0.3f).setEase(LeanTweenType.easeOutQuad); 
    }
 
}
