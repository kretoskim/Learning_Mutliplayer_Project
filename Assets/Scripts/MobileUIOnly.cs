using UnityEngine;

public class MobileUIOnly : MonoBehaviour
{
    void Start()
    {
        #if !UNITY_ANDROID && !UNITY_IOS
            gameObject.SetActive(false);
        #endif
    }

}
