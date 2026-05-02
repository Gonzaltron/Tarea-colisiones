using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Raton : MonoBehaviour
{
    [SerializeField] GameObject cube;
    [SerializeField] GameObject sphere;
    [SerializeField]Vector3 mousePos;
    [SerializeField] Camera mainCamera;
    float cubeXmax;
    float cubeYmax;
    float cubeYmin;
    float cubeXmin;
    float sphereTolerance;
    [SerializeField] float sphereDistance;
    [SerializeField] bool sphereB;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sphereTolerance = 0.5f;
        sphereB = false;
    }

    // Update is called once per frame
    void Update()
    {
        sphereDistance = Mathf.Sqrt((transform.position.x - sphere.transform.position.x) * (transform.position.x - sphere.transform.position.x) + (transform.position.y - sphere.transform.position.y) * (transform.position.y - sphere.transform.position.y));
        if(Input.GetKey(KeyCode.Mouse0))
        {
            if(transform.position.x <= cubeXmax && transform.position.x >= cubeXmin && transform.position.y <= cubeYmax && transform.position.y >= cubeYmin && !sphereB)
            {
                cube.transform.position = new Vector3 (transform.position.x, transform.position.y, 0);
            }
            else if(sphereDistance < sphereTolerance)
            {
                sphereB = true;
                sphere.transform.position = new Vector3 (transform.position.x, transform.position.y, 0);
            }
        }

        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            sphereB = false;
        }
        cubeXmax = cube.transform.position.x + 0.5f;
        cubeYmax = cube.transform.position.y + 0.5f;
        cubeYmin = cube.transform.position.y - 0.5f;
        cubeXmin = cube.transform.position.x - 0.5f;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            transform.position = hit.point;
        } 
    }
}
