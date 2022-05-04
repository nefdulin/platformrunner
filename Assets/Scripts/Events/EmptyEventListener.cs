using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PlatformRunner
{
    public class EmptyEventListener : MonoBehaviour
    {
        public UnityEvent OnEventRaised;

        [SerializeField]
        private EmptyEventChannelSO m_Channel;

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

        public void Respond()
        {
            if (OnEventRaised != null)
                OnEventRaised?.Invoke();
        }
    }

}