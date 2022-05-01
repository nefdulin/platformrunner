using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformRunner
{
    public class PaintWall : MonoBehaviour
    {
        private Texture baseTexture;
        private RenderTexture mainRenderTexture; 

        // Start is called before the first frame update
        void Start()
        {
            baseTexture = GetComponent<Renderer>().material.mainTexture;
            mainRenderTexture = new RenderTexture(baseTexture.width, baseTexture.height, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
            mainRenderTexture.filterMode = baseTexture.filterMode;
            Graphics.Blit(baseTexture, mainRenderTexture);
        }

        // Update is called once per frame
        void Update()
        {

        }
    } 
}
