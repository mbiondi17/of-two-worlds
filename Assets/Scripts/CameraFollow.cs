using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public PlayerController target;
    public Vector2 focusAreaSize;
    FocusArea focusarea;

    public float verticalOffset;

    void Start()
    {
        focusarea = new FocusArea(target.GetComponent<BoxCollider2D>().bounds, focusAreaSize);
    }
    void LateUpdate()
    {
        focusarea.Update(target.GetComponent<BoxCollider2D>().bounds);
        Vector2 focusPosition = focusarea.center + Vector2.up * verticalOffset;
        transform.position = (Vector3)focusPosition + Vector3.forward * -10;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, .5f);
        Gizmos.DrawCube(focusarea.center, focusAreaSize);
    }
    struct FocusArea
    {
        public Vector2 center;
        public Vector2 velocity;
        float left, right;
        float top, bottom;

        public FocusArea(Bounds targetbounds, Vector2 size)
        {
            left = targetbounds.center.x - size.x / 2;
            right = targetbounds.center.x + size.x / 2;
            top = targetbounds.min.y + size.y;
            bottom = targetbounds.min.y;

            velocity = Vector2.zero;
            center = new Vector2((left + right) / 2, (top + bottom) / 2);
        }


        public void Update(Bounds targetbounds)
        {
            float shiftX = 0;
            if (targetbounds.min.x < left)
            {
                shiftX = targetbounds.min.x - left;
            }
            else if (targetbounds.max.x > right)
            {
                shiftX = targetbounds.max.x - right;
            }
            left += shiftX;
            right += shiftX;

            float shiftY = 0;
            if (targetbounds.min.y < bottom)
            {
                shiftY = targetbounds.min.y - bottom;
            }
            else if (targetbounds.max.y > top)
            {
                shiftY = targetbounds.max.y - top;
            }
            top += shiftY;
            bottom += shiftY;

            center = new Vector2((left + right) / 2, (top + bottom) / 2);
            velocity = new Vector2(shiftX, shiftY);
        }
    }
}