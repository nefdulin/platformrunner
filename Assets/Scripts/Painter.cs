using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Es.InkPainter;

namespace PlatformRunner
{
    public class Painter : MonoBehaviour
    {
        public Brush Brush;

        private PlayerInputActions m_InputActions;

        private Camera m_Camera;

        private void Awake()
        {
            m_InputActions = new PlayerInputActions();
            m_InputActions.Enable();

            m_Camera = GetComponent<Camera>();
        }

        private void Update()
        {
            GameStatus status = GameManager.Instance.Status;

            if (status == GameStatus.PAINTING)
            {
                if (m_InputActions.Player.Paint.IsPressed())
                {
					if (Input.GetMouseButton(0))
					{
						Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);
						RaycastHit hitInfo;
						if (Physics.Raycast(ray, out hitInfo))
						{
							InkCanvas paintObject = hitInfo.transform.GetComponent<InkCanvas>();
                            paintObject?.Paint(Brush, hitInfo);
                        }
					}
				}
            }
        }
    }

}