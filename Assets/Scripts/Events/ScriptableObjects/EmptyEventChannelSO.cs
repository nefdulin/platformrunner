using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PlatformRunner
{
    [CreateAssetMenu(menuName = "Events/Empty Channel", fileName = "New Empty Channel")]
    public class EmptyEventChannelSO : ScriptableObject
    {
        public UnityAction OnEventRaised;

        public void RaiseEvent() => OnEventRaised?.Invoke();
    }
}