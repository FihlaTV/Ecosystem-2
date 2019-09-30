using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SFB;

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
        openImage();
    }

    public IEnumerator openImage()
    {
        var extensions = new[] {
                new ExtensionFilter("Image Files", "png", "jpg", "jpeg" )
            };
        var path = StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, false);

        WWW www = new WWW("file:///" + path[0]);
        while (!www.isDone)
            yield return null;
        imagePlaceholder.GetComponent<RawImage>().texture = www.texture;
    }

}
