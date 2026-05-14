using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Raton : MonoBehaviour
{
    [SerializeField] GameObject cube;
    [SerializeField] GameObject sphere;
    [SerializeField]Vector3 mousePos;
    [SerializeField] Camera mainCamera;
    [SerializeField] float sphereDistance;
    [SerializeField] bool sphereB;
    [SerializeField] GameObject rCube;
    [SerializeField]bool cubeB;
    [SerializeField]bool cube1B;
    GameObject current;
    RaycastHit hit;
    [SerializeField]float angle;
    [SerializeField]float cos;
    CollisionFunctions collisions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        current = null;
        collisions = GetComponent<CollisionFunctions>();
    }

    // Update is called once per frame
    void Update()
    { 
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            transform.position = hit.point;
        }
        if (!Input.GetKey(KeyCode.Mouse0))
        {
            current = null;
            current = SelectDrag();

        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            current.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }

    GameObject SelectDrag()
    {
        if (hit.transform.gameObject.CompareTag("Cube") && current == null)
        {
            angle = Vector3.Dot(hit.transform.up, Vector3.up);
            cos = Mathf.Cos(angle);
            if (angle == 1 || angle == 0)
            {
                if(collisions.PointToAABB())
                {
                    return cube;
                }
            }
            else if(collisions.PointToOBB())
            {
                return rCube;
            }
        }
        else if(collisions.PointToCircle())
        {
            return sphere;
        }
        return null;
    }
}
