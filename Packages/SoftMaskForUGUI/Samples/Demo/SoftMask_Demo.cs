using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

namespace Coffee.UISoftMask.Demos
{
    public class SoftMask_Demo : MonoBehaviour
    {
        [SerializeField] RawImage[] softMaskBufferViewer;
        [SerializeField] SoftMask[] softMask;
        [SerializeField] Text text;
        [SerializeField] GameObject title;


        // Use this for initialization
        void OnEnable()
        {
            if (title)
                title.SetActive(true);

            if (text)
                text.text = string.Format("VRSettings: {0}\nloadedDeviceName: {1}\nstereoRenderingMode: {2}\nUVStartsAtTop: {3}",
                    XRSettings.enabled,
                    XRSettings.loadedDeviceName,
                    XRSettings.stereoRenderingMode,
                    SystemInfo.graphicsUVStartsAtTop);

            for (int i = 0; i < softMask.Length; i++)
            {
                softMaskBufferViewer[i].texture = softMask[i].softMaskBuffer;
                softMaskBufferViewer[i+1].texture = softMask[i].softMaskBuffer2;
            }
        }

        public void SetWorldSpase(bool flag)
        {
            if (flag)
            {
                GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
                GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
                transform.rotation = Quaternion.Euler(new Vector3(0, 6, 0));
            }
        }

        public void SetScreenSpase(bool flag)
        {
            if (flag)
            {
                GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
            }
        }

        public void SetOverlay(bool flag)
        {
            if (flag)
            {
                GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            }
        }
    }
}
