using Unity.Profiling;
using UnityEngine;

namespace BytesOne.Profiler
{
    public class RenderingProfiler : MonoBehaviour
    {
#if UNITY_2020_2_OR_NEWER
        private float deltaTime = 0.25f;
#endif
        public float updateTime = 0.25f;
        public bool enableDebug;

        private ProfilerRecorder setPassRecorder;
        private ProfilerRecorder drawCallRecorder;
        private ProfilerRecorder batchesRecorder;
        private ProfilerRecorder trisRecorder;
        private ProfilerRecorder vertRecorder;

        public long SetPassCalls => setPassRecorder.IsRunning ? setPassRecorder.LastValue : 0;
        public long DrawCalls => drawCallRecorder.IsRunning ? drawCallRecorder.LastValue : 0;
        public long Batches => batchesRecorder.IsRunning ? batchesRecorder.LastValue : 0;
        public long Triangles => trisRecorder.IsRunning ? trisRecorder.LastValue : 0;
        public long Vertices => vertRecorder.IsRunning ? vertRecorder.LastValue : 0;

        void OnEnable()
        {
            setPassRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "SetPass Calls Count");
            drawCallRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Draw Calls Count");
            batchesRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Batches Count");
            trisRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Triangles Count");
            vertRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Vertices Count");
        }

        void OnDisable()
        {
            setPassRecorder.Dispose();
            drawCallRecorder.Dispose();
            batchesRecorder.Dispose();
            trisRecorder.Dispose();
            vertRecorder.Dispose();
        }

        void Update()
        {
            deltaTime += Time.deltaTime;

            if (deltaTime > updateTime)
            {
                if (enableDebug)
                {
                    Debug.Log("Set Pass Calls: " + SetPassCalls);
                    Debug.Log("Draw Calls: " + DrawCalls);
                    Debug.Log("Batches: " + Batches);
                    Debug.Log("Triangles: " + Triangles);
                    Debug.Log("Vertices: " + Vertices);
                }
                deltaTime = 0;
            }
        }
    }
}
