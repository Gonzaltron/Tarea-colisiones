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
    [SerializeField] GameObject rCube;
    [SerializeField]bool cubeB;
    [SerializeField]bool cube1B;
    float pUp;
    float pRight;
    GameObject current;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sphereTolerance = 0.5f;
        current = null;
    }

    // Update is called once per frame
    void Update()
    {
        cubeXmax = cube.transform.position.x + 0.5f;
        cubeYmax = cube.transform.position.y + 0.5f;
        cubeYmin = cube.transform.position.y - 0.5f;
        cubeXmin = cube.transform.position.x - 0.5f;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            transform.position = hit.point;
        }
        Vector3 delta = transform.position - rCube.transform.position;
        Vector3 up = rCube.transform.up.normalized;
        Vector3 side = rCube.transform.right.normalized;
        pUp = Vector3.Dot(delta, up);
        pRight = Vector3.Dot(delta, side);
        if (!Input.GetKey(KeyCode.Mouse0))
        {
            current = null;
            SelectDrag();
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            current.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }

    bool Inside(int i)
    {
        switch (i)
        {
            case 0:
                if (transform.position.x <= cubeXmax && transform.position.x >= cubeXmin && transform.position.y <= cubeYmax && transform.position.y >= cubeYmin) {return true;}
                else {return false;}

            case 1:
                if (Mathf.Abs(pUp) <= rCube.transform.localScale.y * 0.5f && Mathf.Abs(pRight) <= rCube.transform.localScale.x * 0.5f) {return true;}
                else {return false;}

            case 2:
                sphereDistance = Mathf.Sqrt((transform.position.x - sphere.transform.position.x) * (transform.position.x - sphere.transform.position.x) + (transform.position.y - sphere.transform.position.y) * (transform.position.y - sphere.transform.position.y));
                if (sphereDistance < sphereTolerance) {return true;}
                else {return false;}

             default:
                return false;
        }
    }

    GameObject SelectDrag()
    {
        if (Inside(0) && current == null)
        {
            current = cube;
            return current;
        }
        else if (Inside(1) && current == null)
        {
            current = rCube;
            return current;
        }
        else if (Inside(2) && current == null)
        {
            current = sphere;
            return current;
        }
        return null;
    }
}
