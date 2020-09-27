using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MaterialTester
{
    [MenuItem("CONTEXT/Graphic/Test Material For Rendering")]
    static void TestMaterialForRendering(MenuCommand command)
    {
        var graphic = command.context as Graphic;
        if (!graphic) return;

        var first = graphic.materialForRendering;
        Debug.LogFormat("First: {0}, {1}", first, first.GetInstanceID());

        var second = graphic.materialForRendering;
        Debug.LogFormat("First: {0}, {1}", second, second.GetInstanceID());

        Debug.Assert(first == second);
    }
}
