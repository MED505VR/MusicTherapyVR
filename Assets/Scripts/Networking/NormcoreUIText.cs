using UnityEngine;
using UnityEngine.UI;

namespace Networking
{
    public class NormcoreUIText : MonoBehaviour
    {
        private Text _text;
        private InitializeNormcore _initializeNormcore;

        private void Start()
        {
            _text = GetComponent<Text>();
            _initializeNormcore = FindObjectOfType<InitializeNormcore>();

            _text.text = _initializeNormcore.currentRoomName;
        }
    }
}