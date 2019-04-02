using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Valve.VR.InteractionSystem.Sample
{
    public class DisableLock : MonoBehaviour
    {
        public LockToPoint[] Products;
        public Text text;
        public bool IsLockDisabled;
        public void DisableLockToPoint()
        {
            foreach (LockToPoint product in Products)
            {
                product.enabled = IsLockDisabled;
            }
            IsLockDisabled = !IsLockDisabled;
            Debug.Log("IsLockDisabled:" + IsLockDisabled.ToString());
            text.text = "IsLockDisabled:" + IsLockDisabled.ToString();
        }
    }
}
