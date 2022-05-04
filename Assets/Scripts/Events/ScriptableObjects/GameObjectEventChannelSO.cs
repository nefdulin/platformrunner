using UnityEngine;
using UnityEngine.Events;

namespace PlatformRunner
{
    [CreateAssetMenu(menuName ="Events/GameObject Channel", fileName = "New GameObject Channel")]
    public class GameObjectEventChannelSO : ScriptableObject
    {
        public UnityAction<GameObject> OnEventRaised;

        public void RaiseEvent(GameObject value) => OnEventRaised?.Invoke(value);
    }
}