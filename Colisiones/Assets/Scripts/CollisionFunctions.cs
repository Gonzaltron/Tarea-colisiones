using UnityEngine;

public class CollisionFunctions : MonoBehaviour
{
    public bool PointToAABB(GameObject cube)
    {
        Vector3 cubeDownRight = new Vector3(cube.transform.position.x + 0.5f, cube.transform.position.y - 0.5f);
        Vector3 cubeUpLeft = new Vector3(cube.transform.position.x - 0.5f, cube.transform.position.y + 0.5f);
        Vector3 cubeDownLeft = new Vector3(cube.transform.position.x - 0.5f, cube.transform.position.y - 0.5f);
        if (transform.position.x <= cubeUpLeft.x && transform.position.x >= cubeDownRight.x && transform.position.y <= cubeUpLeft.y && transform.position.y >= cubeDownLeft.y) { return true; }
        else { return false; }
    }

    public bool PointToCircle(GameObject sphere, float sphereTolerance)
    {
        
        float sphereDistance = Mathf.Sqrt((transform.position.x - sphere.transform.position.x) * (transform.position.x - sphere.transform.position.x) + (transform.position.y - sphere.transform.position.y) * (transform.position.y - sphere.transform.position.y));
        if (sphereDistance < sphereTolerance) { return true; }
        else { return false; }
    }    

    public bool PointToOBB(GameObject rCube)
    {
        Vector3 delta = transform.position - rCube.transform.position;
        Vector3 up = rCube.transform.up.normalized;
        Vector3 side = rCube.transform.right.normalized;
        float pUp = Vector3.Dot(delta, up);
        float pRight = Vector3.Dot(delta, side);
        if (Mathf.Abs(pUp) <= rCube.transform.localScale.y * 0.5f && Mathf.Abs(pRight) <= rCube.transform.localScale.x * 0.5f) { return true; }
        else { return false; }
    }

    public bool ABBToABB(GameObject cube, GameObject cube1)
    {
        Vector2 cubeUpRight = new Vector3(cube.transform.position.x + 0.5f, cube.transform.position.y + 0.5f);
        Vector2 cubeDownRight = new Vector3(cube.transform.position.x + 0.5f, cube.transform.position.y - 0.5f);
        Vector2 cubeUpLeft = new Vector3(cube.transform.position.x - 0.5f, cube.transform.position.y + 0.5f);
        Vector2 cubeDownLeft = new Vector3(cube1.transform.position.x - 0.5f, cube.transform.position.y - 0.5f);
        Vector2 cube1UpRight = new Vector3(cube1.transform.position.x + 0.5f, cube1.transform.position.y + 0.5f);
        Vector2 cube1DownRight = new Vector3(cube1.transform.position.x + 0.5f, cube1.transform.position.y - 0.5f);
        Vector2 cube1UpLeft = new Vector3(cube1.transform.position.x - 0.5f, cube1.transform.position.y + 0.5f);
        Vector2 cube1DownLeft = new Vector3(cube1.transform.position.x - 0.5f, cube1.transform.position.y - 0.5f);
        if(cubeUpRight.x < cube1UpRight.x && cubeUpRight.x > cube1UpLeft.x && cubeUpRight.y < cube1UpRight.y && cubeUpRight.y > cube1DownRight.y) { return true; }
        else if(cubeDownRight.x < cube1UpRight.x && cubeDownRight.x > cube1UpLeft.x && cubeDownRight.y < cube1UpRight.y && cubeDownRight.y > cube1DownRight.y) { return true; }
        else if(cubeUpLeft.x < cube1UpRight.x && cubeUpLeft.x > cube1UpLeft.x && cubeUpLeft.y < cube1UpRight.y && cubeUpLeft.y > cube1DownRight.y) { return true; }
        else if(cubeDownLeft.x < cube1UpRight.x && cubeDownLeft.x > cube1UpLeft.x && cubeDownLeft.y < cube1UpRight.y && cubeDownLeft.y > cube1DownRight.y) { return true; }
        else { return false; }
    }

    public bool CircleToABB(GameObject cube, GameObject sphere)
    {
        if(cube.transform.position.x - 0.5f < sphere.transform.position.x && cube.transform.position.x + 0.5f > sphere.transform.position.x && cube.transform.position.y - 0.5f < sphere.transform.position.y && cube.transform.position.y + 0.5f > sphere.transform.position.y) { return true; }
        else { return false; }
    }

    public bool CircleToCircle(GameObject sphere, GameObject sphere1, float sphereTolerance)
    {
        float distanceX = sphere.transform.position.x - sphere1.transform.position.x;
        float distanceY = sphere.transform.position.y - sphere1.transform.position.y;
        float distance = Mathf.Sqrt(distanceX * distanceX + distanceY * distanceY);
        if(distance < sphereTolerance * 2) { return true; }
        else { return false; }
    }

    public bool CircleToOBB(GameObject sphere, GameObject rCube, float sphereTolerance)
    {
        Vector3 delta = sphere.transform.position - rCube.transform.position;
        Vector3 up = rCube.transform.up.normalized;
        Vector3 side = rCube.transform.right.normalized;
        float pUp = Vector3.Dot(delta, up);
        float pRight = Vector3.Dot(delta, side);
        float closestX = Mathf.Clamp(pRight, -rCube.transform.localScale.x * 0.5f, rCube.transform.localScale.x * 0.5f);
        float closestY = Mathf.Clamp(pUp, -rCube.transform.localScale.y * 0.5f, rCube.transform.localScale.y * 0.5f);
        float distanceX = pRight - closestX;
        float distanceY = pUp - closestY;
        float distance = Mathf.Sqrt(distanceX * distanceX + distanceY * distanceY);
        if (distance < sphereTolerance) { return true; }
        else { return false; }
    }

    public bool OBBToOBB(GameObject rCube, GameObject rCube1)
    {
        Vector3 delta = rCube.transform.position - rCube1.transform.position;
        Vector3 up = rCube.transform.up.normalized;
        Vector3 side = rCube.transform.right.normalized;
        Vector3 up1 = rCube1.transform.up.normalized;
        Vector3 side1 = rCube1.transform.right.normalized;
        float pUp = Vector3.Dot(delta, up);
        float pRight = Vector3.Dot(delta, side);
        float pUp1 = Vector3.Dot(delta, up1);
        float pRight1 = Vector3.Dot(delta, side1);
        if (Mathf.Abs(pUp) <= rCube.transform.localScale.y * 0.5f && Mathf.Abs(pRight) <= rCube.transform.localScale.x * 0.5f && Mathf.Abs(pUp1) <= rCube1.transform.localScale.y * 0.5f && Mathf.Abs(pRight1) <= rCube1.transform.localScale.x * 0.5f) { return true; }
        else { return false; }
    }

    public bool ABBToOBB(GameObject rCube, GameObject cube)
    {
        Vector3 delta = rCube.transform.position - cube.transform.position;
        Vector3 up = rCube.transform.up.normalized;
        Vector3 side = rCube.transform.right.normalized;
        float pUp = Vector3.Dot(delta, up);
        float pRight = Vector3.Dot(delta, side);
        if (Mathf.Abs(pUp) <= rCube.transform.localScale.y * 0.5f + 0.5f && Mathf.Abs(pRight) <= rCube.transform.localScale.x * 0.5f + 0.5f) { return true; }
        else { return false; }
    }
}
