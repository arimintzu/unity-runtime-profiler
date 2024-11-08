using UnityEngine;
using Profiling = UnityEngine.Profiling;

namespace BytesOne.Profiler
{
    public class MemoryProfiler : MonoBehaviour
    {
#if UNITY_2020_2_OR_NEWER
        private float deltaTime = 0.25f;
#endif
        public float updateTime = 0.25f;

        // Public properties to access memory stats
        public long ReservedMemory { get; private set; }
        public long AllocatedMemory { get; private set; }
        public long ManagedHeapMemory { get; private set; }
        public long ManagedUsedMemory { get; private set; }
        public long GraphicsDriverMemory { get; private set; }

        void Update()
        {
            deltaTime += Time.deltaTime;

            if (deltaTime > updateTime)
            {
                UpdateMemoryStats();
                deltaTime = 0;
            }
        }

        private void UpdateMemoryStats()
        {
            ReservedMemory = Profiling.Profiler.GetTotalReservedMemoryLong();
            AllocatedMemory = Profiling.Profiler.GetTotalAllocatedMemoryLong();
            ManagedHeapMemory = Profiling.Profiler.GetMonoHeapSizeLong();
            ManagedUsedMemory = Profiling.Profiler.GetMonoUsedSizeLong();
            GraphicsDriverMemory = Profiling.Profiler.GetAllocatedMemoryForGraphicsDriver();
        }

        public string FormatMemorySize(long bytes)
        {
            return (bytes / (1024.0f * 1024.0f)).ToString("F2") + " MB";
        }
    }
}
