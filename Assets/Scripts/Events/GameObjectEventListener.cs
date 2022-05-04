using UnityEngine;
using UnityEngine.Events;

namespace PlatformRunner
{
    public class GameObjectEventListener : MonoBehaviour
    {
        public UnityEvent<GameObject> OnEventRaised;

        [SerializeField]
        private GameObjectEventChannelSO m_Channel;

        private void OnEnable()
        {
            if (m_Channel != null)
                m_Channel.OnEventRaised += Response;
        }

        private void OnDisable()
        {
            if (m_Channel != null)
                m_Channel.OnEventRaised -= Response;
        }

        public void Response(GameObject value) => OnEventRaised?.Invoke(value);
    } 
}
