using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class InputUtils : MonoBehaviour
    {
        public static void GetSmoothRawAxis(string name, ref float axis, float sensitivity, float gravity)
        {
            var r = Input.GetAxisRaw(name);
            var s = sensitivity;
            var g = gravity;
            var t = Time.unscaledDeltaTime;

            if (r != 0)
            {
                axis = Mathf.Clamp(axis + r * s * t, -1f, 1f);
            }
            else
            {
                axis = Mathf.Clamp01(Mathf.Abs(axis) - g * t) * Mathf.Sign(axis);
            }
        }
    }
}
