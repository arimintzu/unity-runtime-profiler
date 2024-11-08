using System.Collections;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using TMPro;

namespace BytesOne.Profiler
{
    public class SystemProfiler : MonoBehaviour
    {
        public bool enableDebug;

        // System information
        private string os;
        private string deviceModel;
        private string processorType;
        private int processorCount;
        private string graphicsDeviceName;
        private string graphicsDeviceVendor;
        private string graphicsDeviceVersion;

        // FPS and render time tracking
        private float deltaTime = 0.0f;
        private int frameCount = 0;
        private float fps;
        private float renderTime = 0f;

        // Logging interval
        public float logInterval = 1.0f;
        private float logTimer = 0.0f;

        // Average FPS tracking
        private float cumulativeFps = 0.0f;
        private int logCount = 0;

        // Stopwatch for measuring render time per frame
        private readonly Stopwatch renderTimeStopwatch = new Stopwatch();

        // Public properties for external access
        public string OS => os;
        public string DeviceModel => deviceModel;
        public string ProcessorType => processorType;
        public int ProcessorCount => processorCount;
        public string GraphicsDeviceName => graphicsDeviceName;
        public string GraphicsDeviceVendor => graphicsDeviceVendor;
        public string GraphicsDeviceVersion => graphicsDeviceVersion;
        public float FPS => fps;
        public float AverageFPS => logCount > 0 ? cumulativeFps / logCount : 0;
        public float RenderTime => renderTime / Mathf.Max(frameCount, 1);

        void Start()
        {
            os = SystemInfo.operatingSystem;
            deviceModel = SystemInfo.deviceModel;
            processorType = SystemInfo.processorType;
            processorCount = SystemInfo.processorCount;
            graphicsDeviceName = SystemInfo.graphicsDeviceName;
            graphicsDeviceVendor = SystemInfo.graphicsDeviceVendor;
            graphicsDeviceVersion = SystemInfo.graphicsDeviceVersion;

            LogSystemInfo();
            StartCoroutine(MeasureRenderTime());
        }

        void Update()
        {
            deltaTime += Time.deltaTime;
            frameCount++;
            logTimer += Time.deltaTime;

            if (logTimer >= logInterval)
            {
                fps = frameCount / deltaTime;
                cumulativeFps += fps;
                logCount++;

                deltaTime = 0f;
                renderTime = 0f;
                frameCount = 0;
                logTimer = 0f;
            }
        }

        void LateUpdate()
        {
            renderTimeStopwatch.Start();
        }

        private IEnumerator MeasureRenderTime()
        {
            while (true)
            {
                yield return new WaitForEndOfFrame();
                renderTimeStopwatch.Stop();
                renderTime += (float)renderTimeStopwatch.Elapsed.TotalMilliseconds;
                renderTimeStopwatch.Reset();
            }
        }

        private void LogSystemInfo()
        {
            if (enableDebug)
            {
                Debug.Log($"OS: {os}");
                Debug.Log($"Device: {deviceModel}");
                Debug.Log($"CPU: {processorType}");
                Debug.Log($"Core: {processorCount}");
                Debug.Log($"GPU: {graphicsDeviceName}");
                Debug.Log($"GPU Vendor: {graphicsDeviceVendor}");
                Debug.Log($"GPU Ver: {graphicsDeviceVersion}");
            }
        }
    }
}
