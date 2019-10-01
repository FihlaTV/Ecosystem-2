using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SFB;
using System.IO;

public class ImageController : MonoBehaviour
{

    public GameObject imagePlaceholder;

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

        imagePlaceholder.GetComponent<RectTransform>().sizeDelta = new Vector2 (tex.width, tex.height);
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
