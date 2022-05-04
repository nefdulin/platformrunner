using UnityEngine;
using UnityEngine.Events;

namespace PlatformRunner
{
    [CreateAssetMenu(menuName = "Events/Float Channel", fileName = "New Float Channel")]
    public class FloatEventChannelSO : ScriptableObject
    {
        public UnityAction<float> OnEventRaised;

        public void RaiseEvent(float value) => OnEventRaised?.Invoke(value);
    }

}