using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Raton : MonoBehaviour
{
    [SerializeField]Camera mainCamera;
    GameObject current;
    RaycastHit hit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        current = null;
    }

    // Update is called once per frame
    void Update()
    { 
        if (mainCamera == null)
        {
            return;
        }

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            transform.position = hit.point;
        }
        if (!Input.GetKey(KeyCode.Mouse0))
        {
            current = null;
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            if(current == null && hit.transform != null)
            {
                current = hit.transform.gameObject;
            }
            if(current != null)
            {
                current.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            }
        }
    }
}
