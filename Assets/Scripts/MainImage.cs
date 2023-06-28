using UnityEngine;
using UnityEngine.UI;
public class MainImage : MonoBehaviour
{
    public GameObject[] images;
    private RectTransform planeRectTransform;
    private RectTransform objectRectTransform;
    private Vector3[] planeCorners;
    private float minX, minY, maxX, maxY;
    private bool isAtLimit;

    private void Start()
    {
        planeRectTransform = transform.parent.GetComponent<RectTransform>();
        objectRectTransform = transform.GetComponent<RectTransform>();
        LeanTween.scale(gameObject, Vector3.one, 0.5f).setEase(LeanTweenType.easeOutQuad);

        // Obtener las esquinas del plano en coordenadas del mundo
        planeCorners = new Vector3[4];
        planeRectTransform.GetWorldCorners(planeCorners);

        // Obtener los l�mites del plano
        minX = planeCorners[0].x;
        maxX = planeCorners[2].x;
        minY = planeCorners[0].y;
        maxY = planeCorners[2].y;
    }

    private void Update()
    {
        // Limitar la posici�n del objeto dentro de los l�mites del plano
        LimitObjectPosition();
    }

    private void LimitObjectPosition()
    {
        // Obtener la posici�n actual del objeto
        Vector3 objectPosition = objectRectTransform.position;

        // Ajustar la posici�n en funci�n de los l�mites del plano
        float clampedX = Mathf.Clamp(objectPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(objectPosition.y, minY, maxY);

        // Asignar la nueva posici�n limitada al objeto
        objectRectTransform.position = new Vector3(clampedX, clampedY, objectPosition.z);

        // Verificar si el objeto ha llegado a un l�mite
        bool isAtLimitNow = clampedX == minX || clampedX == maxX || clampedY == minY || clampedY == maxY;

        // Verificar si el estado de l�mite ha cambiado
        if (isAtLimitNow && !isAtLimit)
        {
            // Ejecutar la funci�n cuando se alcanza un l�mite por primera vez, optimiza el rendimiento
            planeRectTransform.GetComponent<ScrollRect>().StopMovement();
        }

        // Actualizar el estado de l�mite
        isAtLimit = isAtLimitNow;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //permite cambiar de color a la imagen principal y activar al color que ya fue usado
        if (collision.CompareTag("image") && collision.transform.IsChildOf(transform))
        {
            foreach (GameObject obj in images)
            {
                if (obj == collision.gameObject)
                {
                    GetComponent<Image>().color = collision.GetComponent<Image>().color;
                    obj.GetComponent<Image>().enabled = false;
                    obj.transform.localScale = Vector3.zero;
                }
                //solo enciendo componente que se encuentre apagado
                else if (obj.GetComponent<Image>().enabled == false)
                {
                    obj.GetComponent<Image>().enabled = true;
                    LeanTween.scale(obj, Vector3.one, 0.5f).setEase(LeanTweenType.easeOutQuad);
                }
            }

        }
    }
}
