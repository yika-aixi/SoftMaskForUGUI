#if UNITY_2018_3_OR_NEWER
using UnityEditor.Experimental.SceneManagement;
#endif
using UnityEditor.SceneManagement;


namespace Coffee.UISoftMask
{
    public static class EditorUtils
    {
        public static void MarkPrefabDirty ()
        {
            #if UNITY_2018_3_OR_NEWER
                var prefabStage = PrefabStageUtility.GetCurrentPrefabStage ();
                if (prefabStage != null)
                {
                    EditorSceneManager.MarkSceneDirty (prefabStage.scene);
                }
            #endif
        }
    }
}
