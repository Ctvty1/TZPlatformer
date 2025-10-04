using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform cam;
    public float move;
    public bool lockY = false;
    public bool z;

    public bool notCam;

    private void Start()
    {
        if (notCam == false)
            cam = FindAnyObjectByType<Camera>().transform;
    }

    public void Update()
    {
        if (z == false)
        {
            if (lockY)
            {
                transform.position = new Vector2(cam.position.x * move, transform.position.y);
            }
            else
            {
                transform.position = new Vector2(cam.position.x * move, cam.position.y * move);
            }
        }
        else if (z == true)
        {
            if (lockY)
            {
                transform.position = new Vector3(cam.position.x * move, transform.position.y, -10);
            }
            else
            {
                transform.position = new Vector3(cam.position.x * move, cam.position.y * move, -10);
            }
        }
    }
}