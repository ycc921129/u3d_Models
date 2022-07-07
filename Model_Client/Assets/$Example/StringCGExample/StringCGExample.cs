using System.Text;
using FutureCore;
using UnityEngine;
using UnityEngine.Profiling;

public class StringCGExample : MonoBehaviour
{
    StringBuilder m_StringBuilder = new StringBuilder(100);
    string m_StringBuildertxt = string.Empty;

    private void Start()
    {
        m_StringBuildertxt = m_StringBuilder.GetGarbageFreeString(); 
        
    }

    private void Update()
    {
        int i = Random.Range(0, 100);
        float f = Random.Range(0.01f, 200.01f);
        float d = Random.Range(0.01f, 200.01f);
        string s = "yusong: " + i.ToString();

        Profiler.BeginSample("string.format");
        string s1 = string.Format("{0}{1}{2}{3}", i.ToString(), f.ToString(), d.ToString(), s);
        Profiler.EndSample();

        Profiler.BeginSample("+=");
        string s2 = i.ToString() + f.ToString() + d.ToString() + s;
        Profiler.EndSample();

        Profiler.BeginSample("StringBuilder");
        string s3 = new StringBuilder().Append(i).Append(f).Append(d).Append(s).ToString();
        Profiler.EndSample();

        Profiler.BeginSample("StrExt.Format");
        string s4 = StrExt.Format("{0}{1:0.00}{2:0.00}{3}", i, f, d, s);
        Profiler.EndSample();

        Profiler.BeginSample("EmptyGC");
        m_StringBuilder.GarbageFreeClear();
        m_StringBuilder.ConcatFormat("{0}{1:0.00}{2:0.00}{3}", i, f, d, s);
        string s5 = m_StringBuildertxt;
        Profiler.EndSample();

        Debug.LogFormat("s1 : {0}", s1);
        Debug.LogFormat("s2 : {0}", s2);
        Debug.LogFormat("s3 : {0}", s3);
        Debug.LogFormat("s4 : {0}", s4);
        Debug.LogFormat("s5 : {0}", s5);
    }
}