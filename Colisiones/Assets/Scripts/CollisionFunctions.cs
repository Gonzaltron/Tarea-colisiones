using UnityEngine;

public class CollisionFunctions : MonoBehaviour
{
    [SerializeField] GameObject cube;
    [SerializeField] GameObject sphere;
    [SerializeField] GameObject rCube;
    [SerializeField]Vector3 mousePos;
    [SerializeField] Camera mainCamera;
    float cubeXmax;
    float cubeYmax;
    float cubeYmin;
    float cubeXmin;
    float sphereTolerance;
    [SerializeField] float sphereDistance;
    [SerializeField] bool sphereB;
    bool cubeB;
    float cubeRot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sphereTolerance = 0.5f;
        sphereB = false;
        cubeB = false;
        cubeRot = rCube.transform.rotation.z;
    }

    // Update is called once per frame
    void Update()
    {
        PointToAABB();
        //PointToCircle();
        PointToOBB();
    }


    void PointToAABB()
    {
        cubeXmax = cube.transform.position.x + 0.5f;
        cubeYmax = cube.transform.position.y + 0.5f;
        cubeYmin = cube.transform.position.y - 0.5f;
        cubeXmin = cube.transform.position.x - 0.5f;
        if(Input.GetKey(KeyCode.Mouse0))
        {
            if(transform.position.x <= cubeXmax && transform.position.x >= cubeXmin && transform.position.y <= cubeYmax && transform.position.y >= cubeYmin && !sphereB)
            {
                cube.transform.position = new Vector3 (transform.position.x, transform.position.y, 0);
            }
        }
    }
/*
    void PointToCircle()
    {
        sphereDistance = Mathf.Sqrt((transform.position.x - sphere.transform.position.x) * (transform.position.x - sphere.transform.position.x) + (transform.position.y - sphere.transform.position.y) * (transform.position.y - sphere.transform.position.y));
    }
*/
    void PointToOBB()
    {
        Vector3 delta = transform.position - rCube.transform.position;
        Vector3 up = rCube.transform.up.normalized;
        Vector3 side = rCube.transform.right.normalized;
        float pUp = Vector3.Dot(delta, up);
        float pRight = Vector3.Dot(delta, side);
        if (Mathf.Abs(pUp) <= rCube.transform.localScale.y * 0.5f && Mathf.Abs(pRight) <= rCube.transform.localScale.x * 0.5f && Input.GetKey(KeyCode.Mouse0))
        {
            rCube.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }
}
