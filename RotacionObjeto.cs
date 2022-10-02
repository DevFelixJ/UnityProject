using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacionObjeto : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayer;
    private float rotationRate = 0.8f;
    [SerializeField] private bool touchAnywhere;
    private float m_previousX;
    private Camera m_camera;
    private bool m_rotating = false;

    private void Awake()
    {
        m_camera = Camera.main;
    }

    private void Update()
    {
        if (!touchAnywhere)
        {
            //No need to check if already rotating
            if (!m_rotating)
            {
                RaycastHit hit;
                Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);
                if (!Physics.Raycast(ray, out hit, 1000, targetLayer))
                {
                    return;
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            m_rotating = true;
            m_previousX = Input.mousePosition.x;
        }
        // get the user touch input

        //Al mantener pulsado el raton primario(click izquierdo) se cumple la condicion de que se puede rotar el objeto
        if (Input.GetMouseButton(0))
        {
            var touch = Input.mousePosition;

            //Logica para capturar el valor de la rotacion del eje X
            var deltaX = -(Input.mousePosition.x - m_previousX) * rotationRate;

            //Modificar la posicion X con un transform para que se actualice la rotacion
            transform.Rotate(0, deltaX, 0, Space.World);

            //Se actualiza la rotacion del eje X
            m_previousX = Input.mousePosition.x;
        }
        //Al dejar de mantener pulsado el boton primario(Click izquierdo) Se cumple la condicion de que ya no hay que rotar el objeto
        if (Input.GetMouseButtonUp(0))
            m_rotating = false;
    }
}