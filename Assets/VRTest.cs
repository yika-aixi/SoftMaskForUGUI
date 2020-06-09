#if UNITY_EDITOR
using UnityEditor;

namespace Coffee.UISoftMask.Demos
{
    class VRMenu
    {
        private const string kMenuVirtualRealitySupported = "VR/Virtual Reality Supported";
        private const string kMenuMultiPass = "VR/Multi Pass";
        private const string kMenuSinglePass = "VR/Single Pass";
        private const string kMenuSinglePassInstancing = "VR/Single Pass Instancing";

        [MenuItem(kMenuVirtualRealitySupported, false, 0)]
        private static void VirtualRealitySupported()
        {
            PlayerSettings.virtualRealitySupported = !PlayerSettings.virtualRealitySupported;
        }

        [MenuItem(kMenuVirtualRealitySupported, true)]
        private static bool _VirtualRealitySupported()
        {
            Menu.SetChecked(kMenuVirtualRealitySupported, PlayerSettings.virtualRealitySupported);
            return true;
        }

        [MenuItem(kMenuMultiPass, false, 100)]
        private static void MultiPass()
        {
            PlayerSettings.stereoRenderingPath = StereoRenderingPath.MultiPass;
        }

        [MenuItem(kMenuMultiPass, true)]
        private static bool _MultiPass()
        {
            Menu.SetChecked(kMenuMultiPass, PlayerSettings.stereoRenderingPath == StereoRenderingPath.MultiPass);
            return PlayerSettings.virtualRealitySupported;
        }

        [MenuItem(kMenuSinglePass, false, 101)]
        private static void SinglePass()
        {
            PlayerSettings.stereoRenderingPath = StereoRenderingPath.SinglePass;
        }

        [MenuItem(kMenuSinglePass, true)]
        private static bool _SinglePass()
        {
            Menu.SetChecked(kMenuSinglePass, PlayerSettings.stereoRenderingPath == StereoRenderingPath.SinglePass);
            return PlayerSettings.virtualRealitySupported;
        }

        [MenuItem(kMenuSinglePassInstancing, false, 102)]
        private static void SinglePassInstancing()
        {
            PlayerSettings.stereoRenderingPath = StereoRenderingPath.Instancing;
        }

        [MenuItem(kMenuSinglePassInstancing, true)]
        private static bool _SinglePassInstancing()
        {
            Menu.SetChecked(kMenuSinglePassInstancing, PlayerSettings.stereoRenderingPath == StereoRenderingPath.Instancing);
            return PlayerSettings.virtualRealitySupported;
        }
    }
}
#endif
