
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float PanSpeed = 20f;
    public float ScrollSpeed = 20f;
    public float PanBoarderThiness = 100f;

    public Vector2 PanLimit;
	public float MinY = 20f;
	public float MaxY = 100f;

    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey("w") || MouseAtScreenTopEdge())
        {
            pos.z += PanSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s") || MouseAtScreenBottomEdge())
        {
            pos.z -= PanSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a") || MouseAtScreenLeftEdge())
        {
            pos.x -= PanSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d") || MouseAtScreenRightEdge())
        {
            pos.x += PanSpeed * Time.deltaTime;
        }
        float scroll = Input.GetAxis("Mouse ScrollWheel");
		pos.y +=  scroll * ScrollSpeed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, -PanLimit.x, PanLimit.x);
        pos.z = Mathf.Clamp(pos.z, -PanLimit.y, PanLimit.y);
        pos.y =  Mathf.Clamp(pos.y,MinY,MaxY);

        transform.position = pos;
    }

    bool MouseAtScreenTopEdge()
    {
        return Input.mousePosition.y >= Screen.height - PanBoarderThiness && Input.mousePosition.y <= Screen.height;
    }

    bool MouseAtScreenBottomEdge()
    {
        return Input.mousePosition.y <= PanBoarderThiness && Input.mousePosition.y >= 0;
    }

    bool MouseAtScreenLeftEdge()
    {
        return Input.mousePosition.x <= PanBoarderThiness && Input.mousePosition.x >= 0;
    }

    bool MouseAtScreenRightEdge()
    {
        return Input.mousePosition.x >= Screen.width - PanBoarderThiness && Input.mousePosition.x <= Screen.width;
    }

}
