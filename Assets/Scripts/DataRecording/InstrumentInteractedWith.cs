using UnityEngine;

namespace DataRecording
{
    public class InstrumentInteractedWith : MonoBehaviour
    {
        public bool interactedWith;
        private GameObject _leftHand, _rightHand;
        private void Awake()
        {
            _leftHand = GameObject.Find("LeftHandAnchor");
            _rightHand = GameObject.Find("RightHandAnchor");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == _leftHand || _rightHand)
            {
                interactedWith = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == _leftHand || _rightHand)
            {
                interactedWith = false;
            }
        }
    }
}
