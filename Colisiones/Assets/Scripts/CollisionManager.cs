using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public List<GameObject> CustomColliders = new List<GameObject>();
    [SerializeField] Camera mainCamera;
    [SerializeField] CollisionFunctions collisionFunctions;
    RaycastHit hit;
    Ray ray;
    [SerializeField]float rotSpeed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collisionFunctions = GetComponent<CollisionFunctions>();
    }

    // Update is called once per frame
    void Update()
    {
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit))
        {
            if(hit.transform.gameObject.GetComponent<MeshFilter>().mesh.name.Contains("Cube"))
            {
                
                foreach(GameObject obj in CustomColliders)
                {
                    float dot = Vector3.Dot(hit.transform.up.normalized, Vector3.up);
                    float dot1 = Vector3.Dot(obj.transform.up.normalized, Vector3.up);
                    bool dotIsAxisAligned = Mathf.Approximately(Mathf.Abs(dot), 1f) || Mathf.Approximately(dot, 0f);
                    bool dot1IsAxisAligned = Mathf.Approximately(Mathf.Abs(dot1), 1f) || Mathf.Approximately(dot1, 0f);
                    if(obj == hit.transform.gameObject)
                    {
                        continue;
                    }
                    else if(obj.GetComponent<MeshFilter>().mesh.name.Contains("Cube"))
                    {
                        
                        if(dotIsAxisAligned)
                        {
                            if(dot1IsAxisAligned)
                            {
                                if(collisionFunctions.ABBToABB(hit.transform.gameObject, obj))
                                {
                                    obj.GetComponent<Renderer>().material.color = Color.red;
                                    hit.transform.gameObject.GetComponent<Renderer>().material.color = Color.green;
                                }
                                else
                                {
                                    obj.GetComponent<Renderer>().material.color = Color.white;
                                    hit.transform.gameObject.GetComponent<Renderer>().material.color = Color.white;
                                }
                            }
                            else if(collisionFunctions.ABBToOBB(hit.transform.gameObject, obj))
                            {
                                obj.GetComponent<Renderer>().material.color = Color.red;
                                hit.transform.gameObject.GetComponent<Renderer>().material.color = Color.green;
                            }
                            else{
                                obj.GetComponent<Renderer>().material.color = Color.white;
                                hit.transform.gameObject.GetComponent<Renderer>().material.color = Color.white;
                            }
                        }

                    }
                    else if(obj.GetComponent<MeshFilter>().mesh.name.Contains("Sphere"))
                    {
                        if(dotIsAxisAligned && collisionFunctions.CircleToABB(hit.transform.gameObject, obj))
                        {
                            obj.GetComponent<Renderer>().material.color = Color.red;
                            hit.transform.gameObject.GetComponent<Renderer>().material.color = Color.green;
                        }
                        else if(collisionFunctions.CircleToOBB(hit.transform.gameObject, obj, obj.transform.localScale.x * 0.5f))
                        {
                            obj.GetComponent<Renderer>().material.color = Color.red;
                            hit.transform.gameObject.GetComponent<Renderer>().material.color = Color.green;
                        }
                        else 
                        {
                            obj.GetComponent<Renderer>().material.color = Color.white;
                            hit.transform.gameObject.GetComponent<Renderer>().material.color = Color.white;
                        }
                    }
                }
            }
            else if(hit.transform.gameObject.GetComponent<MeshFilter>().mesh.name.Contains("Sphere"))
            {
                foreach(GameObject obj in CustomColliders)
                {
                    float dot = Vector3.Dot(hit.transform.up.normalized, Vector3.up);
                    float dot1 = Vector3.Dot(obj.transform.up.normalized, Vector3.up);
                    bool dotIsAxisAligned = Mathf.Approximately(Mathf.Abs(dot), 1f) || Mathf.Approximately(dot, 0f);
                    bool dot1IsAxisAligned = Mathf.Approximately(Mathf.Abs(dot1), 1f) || Mathf.Approximately(dot1, 0f);
                    if(obj == hit.transform.gameObject)
                    {
                        continue;
                    }
                    else if(obj.GetComponent<MeshFilter>().mesh.name.Contains("Cube"))
                    {
                        if(dotIsAxisAligned && collisionFunctions.CircleToABB(obj, hit.transform.gameObject))
                        {
                            obj.GetComponent<Renderer>().material.color = Color.red;
                            hit.transform.gameObject.GetComponent<Renderer>().material.color = Color.green;
                        }
                        else if(collisionFunctions.CircleToOBB(obj, hit.transform.gameObject, hit.transform.localScale.x * 0.5f))
                        {
                            obj.GetComponent<Renderer>().material.color = Color.red;
                            hit.transform.gameObject.GetComponent<Renderer>().material.color = Color.green;
                        }
                        else
                        {
                            obj.GetComponent<Renderer>().material.color = Color.white;
                            hit.transform.gameObject.GetComponent<Renderer>().material.color = Color.white;
                        }
                    }
                    else if(obj.GetComponent<MeshFilter>().mesh.name.Contains("Sphere"))
                    {
                        if(collisionFunctions.CircleToCircle(hit.transform.gameObject, obj, hit.transform.localScale.x * 0.5f))
                        {
                           obj.GetComponent<Renderer>().material.color = Color.red;
                            hit.transform.gameObject.GetComponent<Renderer>().material.color = Color.green;
                        }
                    }
                    else
                    {
                        obj.GetComponent<Renderer>().material.color = Color.white;
                        hit.transform.gameObject.GetComponent<Renderer>().material.color = Color.white;
                    }
                }
            }

            if (Input.GetKey(KeyCode.Q))
            {
                hit.transform.Rotate(0f, 0f, rotSpeed * Time.deltaTime, Space.Self);
            }
            else if (Input.GetKey(KeyCode.E))
            {
                hit.transform.Rotate(0f, 0f, -rotSpeed * Time.deltaTime, Space.Self);
            }
        }
    }
}
