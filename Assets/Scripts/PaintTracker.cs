using System;
using UnityEngine;
using Es.InkPainter;

namespace PlatformRunner
{
    public class PaintTracker : MonoBehaviour
    {
        [SerializeField]
        private FloatEventChannelSO m_OnPaintPercentageUpdate;

        private InkCanvas m_Canvas;
        private Texture2D m_Texture;
        private RenderTexture m_PaintTexture;

        private int m_TextureLength = 0;
        private float m_Percentage = 0.0f; 

        void Start()
        {
            m_Canvas = GetComponent<InkCanvas>();

            m_Canvas.OnInitializedAfter += SetupTextures;
            m_Canvas.OnPaintEnd += OnPaintEnd;
        }

        void SetupTextures(InkCanvas paintedCanvas)
        {
            m_PaintTexture = m_Canvas.GetPaintMainTexture("PaintWall");
            m_Texture = new Texture2D(m_PaintTexture.width, m_PaintTexture.height);
            m_TextureLength = m_Texture.width * m_Texture.height;
        }

        void OnPaintEnd(InkCanvas paintedCanvas)
        {
            RenderTexture.active = m_PaintTexture;
            m_Texture.ReadPixels(new Rect(0, 0, m_Texture.width, m_Texture.height), 0, 0);
            m_Texture.Apply();

            Color[] colorBuffer = m_Texture.GetPixels();

            int redCounter = 0;
            foreach (Color color in colorBuffer)
            {
                if (color == Color.red)
                {
                    redCounter++;
                }
            }

            float newPercentage = 100 * ((float) redCounter / (float) m_TextureLength);
            if (newPercentage != m_Percentage)
            {
                m_Percentage = newPercentage;

                m_OnPaintPercentageUpdate.RaiseEvent(m_Percentage);
            }
        }
    }

}