/************************************************************************************
Copyright : Copyright (c) Facebook Technologies, LLC and its affiliates. All rights reserved.

Your use of this SDK or tool is subject to the Oculus SDK License Agreement, available at
https://developer.oculus.com/licenses/oculussdk/

Unless required by applicable law or agreed to in writing, the Utilities SDK distributed
under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF
ANY KIND, either express or implied. See the License for the specific language governing
permissions and limitations under the License.
************************************************************************************/

using System;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions;


namespace UnityEngine.EventSystems
{
    /// <summary>
    /// Extension of Unity's PointerEventData to support ray based pointing and also touchpad swiping
    /// </summary>
    public class OVRPointerEventData : PointerEventData
    {
        public OVRPointerEventData(EventSystem eventSystem)
            : base(eventSystem)
        {
        }

        public Ray worldSpaceRay;
        public Vector2 swipeStart;

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("<b>Position</b>: " + position);
            sb.AppendLine("<b>delta</b>: " + delta);
            sb.AppendLine("<b>eligibleForClick</b>: " + eligibleForClick);
            sb.AppendLine("<b>pointerEnter</b>: " + pointerEnter);
            sb.AppendLine("<b>pointerPress</b>: " + pointerPress);
            sb.AppendLine("<b>lastPointerPress</b>: " + lastPress);
            sb.AppendLine("<b>pointerDrag</b>: " + pointerDrag);
            sb.AppendLine("<b>worldSpaceRay</b>: " + worldSpaceRay);
            sb.AppendLine("<b>swipeStart</b>: " + swipeStart);
            sb.AppendLine("<b>Use Drag Threshold</b>: " + useDragThreshold);
            return sb.ToString();
        }
    }


    /// <summary>
    /// Static helpers for OVRPointerEventData.
    /// </summary>
    public static class PointerEventDataExtension
    {
        public static bool IsVRPointer(this PointerEventData pointerEventData)
        {
            return pointerEventData is OVRPointerEventData;
        }

        public static Ray GetRay(this PointerEventData pointerEventData)
        {
            var vrPointerEventData = pointerEventData as OVRPointerEventData;
            Assert.IsNotNull(vrPointerEventData);

            return vrPointerEventData.worldSpaceRay;
        }

        public static Vector2 GetSwipeStart(this PointerEventData pointerEventData)
        {
            var vrPointerEventData = pointerEventData as OVRPointerEventData;
            Assert.IsNotNull(vrPointerEventData);

            return vrPointerEventData.swipeStart;
        }

        public static void SetSwipeStart(this PointerEventData pointerEventData, Vector2 start)
        {
            var vrPointerEventData = pointerEventData as OVRPointerEventData;
            Assert.IsNotNull(vrPointerEventData);

            vrPointerEventData.swipeStart = start;
        }
    }
}