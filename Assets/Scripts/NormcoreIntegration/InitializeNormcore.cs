using UnityEngine;

namespace NormcoreIntegration
{
    public class InitializeNormcore : MonoBehaviour
    {
        private Normal.Realtime.Realtime realtime;

        private void Awake()
        {
            realtime = GetComponent<Normal.Realtime.Realtime>();
        }

        private void Start()
        {
            if (Application.isEditor)
                realtime.Connect("Dev Room", null);

            else realtime.Connect("Test Room", null);
        }
    }
}
