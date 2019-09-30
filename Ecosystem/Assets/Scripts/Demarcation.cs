using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demarcation : MonoBehaviour
{
    public GameObject RectanglePrefab;

    private bool creatingArea;

    private Vector3 oldPosition;

    private GameObject newRectangle;

    void Update()
    {
        if(creatingArea)
        {

            var goal = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            var scaleX = goal.x - oldPosition.x;
            var scaleY = goal.y - oldPosition.y;

            newRectangle.transform.localScale = new Vector3(scaleX,scaleY,1);
            newRectangle.transform.position += new Vector3(scaleX / 2, scaleY / 2, 0);

            oldPosition = goal;

            if (Input.GetMouseButtonUp(0))
            {
                creatingArea = false;
            }
        }
        else if(Input.GetMouseButtonDown(0))
        {

            Vector3 initialPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            initialPosition.z = -1;

            newRectangle = Instantiate(RectanglePrefab, initialPosition, Quaternion.identity);

            var offsetX = 1.0f;
            var offsetY = 1.0f;

            newRectangle.transform.position += new Vector3(offsetX, offsetY, 0);

            oldPosition = newRectangle.transform.position;

            creatingArea = true;
        }






    }
}
