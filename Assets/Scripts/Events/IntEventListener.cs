using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PlatformRunner
{
    public class IntEventListener : MonoBehaviour
    {
        public UnityEvent<int> OnEventRaised;

        [SerializeField]
        private IntEventChannelSO m_Channel;

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

        public void Respond(int value) => OnEventRaised?.Invoke(value);
    }

}