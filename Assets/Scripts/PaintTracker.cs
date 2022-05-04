using System;
using UnityEngine;
using Es.InkPainter;

namespace PlatformRunner
{
    public class PaintTracker : MonoBehaviour
    {
        public Action<float> PaintPercentageUpdated;

        private InkCanvas canvas;
        private Texture2D texture;
        private RenderTexture paintTexture;

        private int textureLength = 0;
        private float percentage = 0.0f; 
        void Start()
        {
            canvas = GetComponent<InkCanvas>();

            canvas.OnInitializedAfter += SetupTextures;
            canvas.OnPaintEnd += OnPaintEnd;
        }

        private void Update()
        {
            //if (Input.GetMouseButtonDown(0))
            //{
            //    timer = 0.0f;
            //    painting = true;
            //}

            //if (Input.GetMouseButton(0))
            //{
            //    if (painting == true)
            //    {
            //        if (timer >= UpdateInterval)
            //        {
            //            RenderTexture.active = paintTexture;
            //            texture.ReadPixels(new Rect(0, 0, texture.width, texture.height), 0, 0);
            //            texture.Apply();
            //            Debug.Log("We read the data");

            //            Color[] colorBuffer = texture.GetPixels();

            //            int redCounter = 0;
            //            foreach (Color color in colorBuffer)
            //            {
            //                if (color == Color.red)
            //                {
            //                    redCounter++;
            //                }
            //            }

            //            Debug.Log("Red amount" + redCounter);
            //            timer = 0.0f;
            //        }

            //        timer += Time.deltaTime;
            //    }
            //}

            //if (Input.GetMouseButtonUp(0))
            //{
            //    timer = 0.0f;
            //    painting = false;
            //}

        }
        void SetupTextures(InkCanvas paintedCanvas)
        {
            paintTexture = canvas.GetPaintMainTexture("PaintWall");
            texture = new Texture2D(paintTexture.width, paintTexture.height);
            textureLength = texture.width * texture.height;
        }
        void OnPaintEnd(InkCanvas paintedCanvas)
        {
            RenderTexture.active = paintTexture;
            texture.ReadPixels(new Rect(0, 0, texture.width, texture.height), 0, 0);
            texture.Apply();

            Color[] colorBuffer = texture.GetPixels();

            int redCounter = 0;
            foreach (Color color in colorBuffer)
            {
                if (color == Color.red)
                {
                    redCounter++;
                }
            }

            float newPercentage = 100 * ((float) redCounter / (float) textureLength);
            if (newPercentage != percentage)
            {
                percentage = newPercentage;

                PaintPercentageUpdated?.Invoke(percentage);
            }
        }
    }

}