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
        public MeshRenderer colorSate;
        public void DisableLockToPoint()
        {
            foreach (LockToPoint product in Products)
            {
                product.enabled = IsLockDisabled;
            }

            if (Products[0].enabled)
            {
                colorSate.material.color = Color.green;
            }
            else
            {
                colorSate.material.color = Color.white;
            }
            Debug.Log("IsLockDisabled:" + IsLockDisabled.ToString());
            text.text = "IsLockDisabled:" + IsLockDisabled.ToString();
            IsLockDisabled = !IsLockDisabled;   
        }
    }
}
