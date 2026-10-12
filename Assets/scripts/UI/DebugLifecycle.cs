using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class DebugLifecycle : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("<color=green>DialogueCanvas AWAKE - obje oluşturuldu</color>");
    }

    void OnEnable()
    {
        Debug.Log("<color=cyan>DialogueCanvas ENABLE</color>");
    }

    void OnDisable()
    {
        var trace = new StackTrace(true);
        Debug.Log("<color=orange>DialogueCanvas DISABLE</color>\n" + trace.ToString());
    }

    void OnDestroy()
    {
        var trace = new StackTrace(true);
        Debug.Log("<color=red>DialogueCanvas DESTROY</color>\n" + trace.ToString());
    }
}