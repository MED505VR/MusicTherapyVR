using Normal.Realtime;
using UnityEngine;

namespace LoopScripts
{
    public class BeatTest : MonoBehaviour
    {
        [SerializeField] private BeatSync _beatSync;
        private GameObject leftHand, rightHand;
        private Color black = new Color(0, 0, 0, 1);
        private Color white = new Color(1, 1, 1, 1);
        private Color red = new Color(1, 0, 0, 1);
        private GameObject myObject;
        private RealtimeView myView;

        [SerializeField] private Color _color;
        private Color _previousColor;


        private MeshRenderer meshRenderer;
        public bool _play;
        private bool _prevPlay;
        private AudioSource audioPlayer;


        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            // Get a reference to the color sync component
            _beatSync = GetComponent<BeatSync>();
            leftHand = GameObject.Find("LeftHandAnchor");
            rightHand = GameObject.Find("RightHandAnchor");
            myObject = GameObject.Find("Sphere1");
            myView = myObject.GetComponent<RealtimeView>();
            audioPlayer = GetComponent<AudioSource>();
            _color = white;
            _play = false;
        }


        private void color()
        {
            if (_play == true)
                _color = red;
            else if (_play == false) _color = white;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == leftHand || other.gameObject == rightHand)
            {
                myView.RequestOwnership();
                _play = !_play;
                color();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            myView.ClearOwnership();
        }

        private void Update()
        {
            // If the color has changed (via the inspector), call SetColor on the color sync component.
            if (_color != _previousColor)
            {
                _beatSync.SetColor(_color);
                _previousColor = _color;
            }

            if (_play != _prevPlay)
            {
                _beatSync.Setplay(_play);
                _prevPlay = _play;
            }
        }
    }
}