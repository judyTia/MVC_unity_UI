using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class swipePaper :  EventTrigger
{

    private float dragSpeed = 0.01f;
    public override void OnEndDrag(PointerEventData eventData)
    {

        Vector3 dragVectorDirection = (eventData.position - eventData.pressPosition).normalized;

        DraggedDirection myDirection = GetDragDirection(dragVectorDirection);
        if(myDirection == DraggedDirection.Left)
        {
            controller.Instance.Randomword();
        }
        else if(myDirection == DraggedDirection.Right)
        {
            controller.Instance.ReverseOpenPaper();
        }
    }


    public override void OnDrag(PointerEventData eventData)
    {
        Vector3 delta = eventData.position - eventData.pressPosition;
        print("delta" + delta);
        Vector3 pos = transform.position;
        print("pos.x" + pos.x);
        pos.x += delta.x * dragSpeed;
        transform.position = pos;
       
    }

    private enum DraggedDirection
    {
        Up,
        Down,
        Right,
        Left
    }
    private DraggedDirection GetDragDirection(Vector3 dragVector)
    {
        float positiveX = Mathf.Abs(dragVector.x);
        float positiveY = Mathf.Abs(dragVector.y);
        DraggedDirection draggedDir;
        if (positiveX > positiveY)
        {
            draggedDir = (dragVector.x > 0) ? DraggedDirection.Right : DraggedDirection.Left;
        }
        else
        {
            draggedDir = (dragVector.y > 0) ? DraggedDirection.Up : DraggedDirection.Down;
        }
        Debug.Log(draggedDir);
        return draggedDir;
    }
}
