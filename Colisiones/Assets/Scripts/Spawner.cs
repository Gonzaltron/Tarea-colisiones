using UnityEngine;

public class Spawner : MonoBehaviour
{
    CollisionManager collisionManager;
    GameObject current;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collisionManager = GetComponent<CollisionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            current = GameObject.CreatePrimitive(PrimitiveType.Cube);
        }   
        else if(Input.GetKeyDown(KeyCode.C))
        {
            current = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        }

        current.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        current.transform.rotation = Quaternion.Euler(0, 0, 0);
        collisionManager.CustomColliders.Add(current);
    }
}
