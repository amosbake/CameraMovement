# 摄像机响应用户操作移动

[素材包](http://devassets.com/assets/desert-environment/)

[镜头控制插件](https://www.youtube.com/redirect?q=https%3A%2F%2Fwww.assetstore.unity3d.com%2F%23%21%2Fcontent%2F43321&event=video_description&v=cfjLQrMGEb4&redir_token=Lm3IbJizFst7-PTsXoATNaMUf-58MTUxNDIwNjgxNEAxNTE0MTIwNDE0)
## 目标

在RTS游戏和一些需要显示地图的RPG游戏中,常常出现用鼠标和键盘的方向键操作 玩家镜头的移动,从而达到自由观看游戏场景的效果
本篇的目标就是实现这一效果

1. wasd 分别对应镜头的 上,左,下,右 移动
2. 鼠标移动到屏幕边缘时, 镜头向对应方向卷动
3. 滚动鼠标滚轮时, 视角拉近,拉远

## API
1. 获取键盘按键: `Input.GetKey("w")`
2. 获取鼠标位置: `Input.mousePosition` 
3. 获取屏幕大小: `Screen.width/Screen.height`
4. 限制值的范围: `Mathf.Clamp(value,min,max)`

## 实际应用:
1. 将场景的摄像机调节成 x增大 镜头左移, z增大 镜头上移, y增大 镜头拉远. 并对摄像机添加脚本`CameraController`
2. 创建镜头滚动的配置参数: PanSpeed(镜头移动速度), ScrollSpeed(镜头深度变化速度)等
3. 在Update()方法中 添加对应键盘,鼠标事件的监听,并对保存的镜头position临时变量赋值
4. 确认position的边界条件,再设置到镜头的Transform中

## CameraController

```C#

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

```
