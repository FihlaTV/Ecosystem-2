using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demarcation : MonoBehaviour
{
    public GameObject RectanglePrefab;

    private bool creatingArea;

    private Vector3 oldPosition;

    private struct Point
    {
        public float x;
        public float y;

        public Point(float xP , float yP)
        {
            x = xP;
            y = yP;
        }
    }

    private struct Rectangle
    {
        public Point leftDown;
        public Point RightUp;

        public Rectangle(Point LD, Point RP)
        {
            leftDown = LD;
            RightUp = RP;
        }

    }

    private List<Rectangle> listRectangles;

    private GameObject newRectangle;
    private Vector3 initialPosition;

    private void Start()
    {
        listRectangles = new List<Rectangle>();
    }

    void Update()
    {
        if(Input.GetButtonDown("Space"))
        {
            

            foreach (Rectangle rec in listRectangles)
            {

            }
        }

        if(creatingArea)
        {

            var goal = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            var scaleX = goal.x - oldPosition.x;
            var scaleY = goal.y - oldPosition.y;

            newRectangle.transform.localScale += new Vector3(scaleX,scaleY,1);
            newRectangle.transform.position += new Vector3(scaleX / 2, scaleY / 2, 0);

            oldPosition = goal;

            if (Input.GetMouseButtonUp(0))
            {
                listRectangles.Add(new Rectangle(new Point(initialPosition.x, initialPosition.y), new Point(goal.x, goal.y)));
                creatingArea = false;
            }
        }
        else if(Input.GetMouseButtonDown(0))
        {

            initialPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            initialPosition.z = -1;

            newRectangle = Instantiate(RectanglePrefab, initialPosition, Quaternion.identity);

            var offsetX = 1.0f;
            var offsetY = 1.0f;

            newRectangle.transform.position += new Vector3(offsetX, offsetY, 0);

            oldPosition = newRectangle.transform.position +  new Vector3(2,2,0);



            creatingArea = true;
        }






    }
}
