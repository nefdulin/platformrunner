using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PlatformRunner
{
    [CreateAssetMenu(menuName = "Events/Int Channel", fileName = "New Int Channel")]
    public class IntEventChannelSO : ScriptableObject
    {
        public UnityAction<int> OnEventRaised;

        public void RaiseEvent(int value) => OnEventRaised?.Invoke(value);
    } 
}
