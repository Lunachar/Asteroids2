using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Asteroids2.Utilities
{
    public class OnlyOneEventSystemIsEnabled : MonoBehaviour
    {
        private void Awake()
        {
            EventSystem[] _eventSystems = FindObjectsOfType<EventSystem>();

            if (_eventSystems.Length > 1)
            {
                for (int i = 1; i < _eventSystems.Length; i++)
                {
                    Destroy(_eventSystems[i].gameObject);
                }
            }

            if (_eventSystems.Length > 0)
            {
                DontDestroyOnLoad(_eventSystems[0].gameObject);
                _eventSystems[0].enabled = true;
            }
        }
    }
}