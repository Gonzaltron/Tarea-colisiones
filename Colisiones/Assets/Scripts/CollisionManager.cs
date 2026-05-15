using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    [SerializeField] List<GameObject> CustomColliders = new List<GameObject>();
    [SerializeField] Camera mainCamera;
    [SerializeField] CollisionFunctions collisionFunctions;
    RaycastHit hit;
    Ray ray;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit))
        {
            if(hit.transform.gameObject.GetComponent<MeshFilter>().mesh.name == "Cube")
            {
                
                foreach(GameObject obj in CustomColliders)
                {
                    float angle = Vector3.Dot(hit.transform.up, Vector3.up);
                    float angle1 = Vector3.Dot(obj.transform.up, Vector3.up);
                    float cos = Mathf.Cos(angle);
                    float cos1 = Mathf.Cos(angle1);
                    if(obj == hit.transform.gameObject)
                    {
                        continue;
                    }
                    else if(obj.GetComponent<MeshFilter>().mesh.name == "Cube")
                    {
                        
                        if(cos == 1 || cos == -1 || cos == 0)
                        {
                            if(cos1 == 1 || cos1 == -1 || cos1 == 0)
                            {
                                if(collisionFunctions.ABBToABB(hit.transform.gameObject, obj))
                                {
                                    Debug.Log("Collision between " + hit.transform.gameObject.name + " and " + obj.name);
                                }
                            }
                            else if(collisionFunctions.ABBToOBB(hit.transform.gameObject, obj))
                            {
                                Debug.Log("Collision between " + hit.transform.gameObject.name + " and " + obj.name);
                            }
                        }

                    }
                    else if(obj.GetComponent<MeshFilter>().mesh.name == "Sphere")
                    {
                        if(cos == 1 || cos == -1 || cos == 0 && collisionFunctions.CircleToABB(hit.transform.gameObject, obj))
                        {
                            Debug.Log("Collision between " + hit.transform.gameObject.name + " and " + obj.name);
                        }
                        else if(collisionFunctions.CircleToOBB(hit.transform.gameObject, obj, obj.transform.localScale.x * 0.5f))
                        {
                            Debug.Log("Collision between " + hit.transform.gameObject.name + " and " + obj.name);
                        }
                    }
                }
            }
            else if(hit.transform.gameObject.GetComponent<MeshFilter>().mesh.name == "Sphere")
            {
                foreach(GameObject obj in CustomColliders)
                {
                    float angle = Vector3.Dot(hit.transform.up, Vector3.up);
                    float angle1 = Vector3.Dot(obj.transform.up, Vector3.up);
                    float cos = Mathf.Cos(angle);
                    float cos1 = Mathf.Cos(angle1);
                    if(obj == hit.transform.gameObject)
                    {
                        continue;
                    }
                    else if(obj.GetComponent<MeshFilter>().mesh.name == "Cube")
                    {
                        if(cos == 1 || cos == -1 || cos == 0 && collisionFunctions.CircleToABB(obj, hit.transform.gameObject))
                        {
                            Debug.Log("Collision between " + hit.transform.gameObject.name + " and " + obj.name);
                        }
                        else if(collisionFunctions.CircleToOBB(obj, hit.transform.gameObject, hit.transform.localScale.x * 0.5f))
                        {
                            Debug.Log("Collision between " + hit.transform.gameObject.name + " and " + obj.name);
                        }
                    }
                    else if(obj.GetComponent<MeshFilter>().mesh.name == "Sphere")
                    {
                        if(collisionFunctions.CircleToCircle(hit.transform.gameObject, obj, hit.transform.localScale.x * 0.5f))
                        {
                            Debug.Log("Collision between " + hit.transform.gameObject.name + " and " + obj.name);
                        }
                    }
                }
            }
        }
    }
}
