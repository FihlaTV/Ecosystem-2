using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SFB;

public class Demarcation : MonoBehaviour
{
    public GameObject RectanglePrefab;

    private bool creatingArea;

    private float width;
    private float height;
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
        public Point rightUp;

        public Rectangle(Point LD, Point RP)
        {
            leftDown = LD;
            rightUp = RP;
        }

        public string toString()
        {
            return "{" + "minX:" + leftDown.x + " , minY:" + leftDown.y + " , maxX:" +  rightUp.x + " , maxY:" + rightUp.y + "}";
        }

    }

    private List<Rectangle> listRectangles;

    private GameObject newRectangle;
    private Vector3 initialPosition;

    private void OnEnable()
    {
        width = CameraMovement.getXBoundaries().y;
        height = CameraMovement.getYBoundaries().y;
    }


    private void Start()
    {
        listRectangles = new List<Rectangle>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {

            var path = StandaloneFileBrowser.OpenFilePanel("Open File", "", "", false);

            var iter = 1;

            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(path[0]))
            {
             
                foreach (Rectangle rec in listRectangles)
                {
                    file.WriteLine(rec.toString());
                    iter++;
                }
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
                listRectangles.Add(new Rectangle(new Point(initialPosition.x + width, initialPosition.y + height), new Point(goal.x + width, goal.y + height)));
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
