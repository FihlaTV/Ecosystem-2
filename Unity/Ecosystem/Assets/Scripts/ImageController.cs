using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SFB;
using System.IO;

public class ImageController : MonoBehaviour
{

    public GameObject imagePlaceholder;
    public GameObject canvas;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick()
    {
        var extensions = new[] {
                new ExtensionFilter("Image Files", "png", "jpg", "jpeg" )
            };
        var filePath = StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, false);

        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath[0]))
        {
            fileData = File.ReadAllBytes(filePath[0]);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }

        var size = new Vector2(tex.width, tex.height);

        imagePlaceholder.GetComponent<RectTransform>().sizeDelta = size;
        canvas.GetComponent<RectTransform>().sizeDelta = size;

        var xBorders = new Vector2(-tex.width/2, tex.width / 2);
        var yBorders = new Vector2(-tex.height / 2, tex.height / 2);



        CameraMovement.setXBoundaries(xBorders);
        CameraMovement.setYBoundaries(yBorders);


        imagePlaceholder.GetComponent<RawImage>().texture = tex;

    }

  //  //public IEnumerator openImage()
  //  {
        

   //     WWW www = new WWW("file:///" + path[0]);
   //     while (!www.isDone)
   //         yield return null;
  //      imagePlaceholder.GetComponent<RawImage>().texture = www.texture;
   // }

}
