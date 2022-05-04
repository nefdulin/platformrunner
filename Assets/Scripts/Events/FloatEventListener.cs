using UnityEngine;
using UnityEngine.Events;


namespace PlatformRunner
{
    public class FloatEventListener : MonoBehaviour
    {
        public UnityEvent<float> OnEventRaised;

        [SerializeField]
        private FloatEventChannelSO m_Channel;

        private void OnEnable()
        {
            if (m_Channel != null)
                m_Channel.OnEventRaised += Respond;
        }

        private void OnDisable()
        {
            if (m_Channel != null)
                m_Channel.OnEventRaised -= Respond;
        }

        public void Respond(float value) => OnEventRaised?.Invoke(value);
    }
}