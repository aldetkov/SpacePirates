using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Содержит информацию о статичном положении камеры, её зоне видимости
/// </summary>
public static class ViewPortSizeInfo
{

    public static Vector3 leftBot;
    public static Vector3 rightTop;

    static ViewPortSizeInfo() {
        Vector3 cameraToObject = Vector3.zero - Camera.main.transform.position;
        float distance = -Vector3.Project(cameraToObject, Camera.main.transform.forward).y;
        leftBot = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        rightTop = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, distance));
    }
}