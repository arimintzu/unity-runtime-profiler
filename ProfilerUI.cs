using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;  // Import TextMeshPro namespace
using UnityEngine.UI;
namespace BytesOne.Profiler
{
    public class ProfilerUI : MonoBehaviour
    {
        public Transform profilerCanvas;
        public Button btnToggleProfiler; 

        [Title("System Profiler")]
        public TextMeshProUGUI osText;
        public TextMeshProUGUI deviceModelText;
        public TextMeshProUGUI processorTypeText;
        public TextMeshProUGUI processorCountText;
        public TextMeshProUGUI graphicsDeviceNameText;
        public TextMeshProUGUI graphicsDeviceVendorText;
        public TextMeshProUGUI graphicsDeviceVersionText;
        public TextMeshProUGUI fpsText;
        public TextMeshProUGUI averageFpsText;
        public TextMeshProUGUI renderTimeText;

        [Title("Memory Profiler")]
        public TextMeshProUGUI reservedMemoryText;
        public TextMeshProUGUI allocatedMemoryText;
        public TextMeshProUGUI managedHeapMemoryText;
        public TextMeshProUGUI managedUsedMemoryText;
        public TextMeshProUGUI graphicsDriverMemoryText;

        [Title("Rendering Profiler")]
        public TextMeshProUGUI setPassCallsText;
        public TextMeshProUGUI drawCallsText;
        public TextMeshProUGUI batchesText;
        public TextMeshProUGUI trianglesText;
        public TextMeshProUGUI verticesText;

        private SystemProfiler systemProfiler;
        private MemoryProfiler memoryProfiler;
        private RenderingProfiler renderingProfiler;

        void Start()
        {
            if (btnToggleProfiler)
            {
                btnToggleProfiler.onClick.AddListener(() =>
                {
                    if (profilerCanvas)
                    {
                        profilerCanvas.gameObject.SetActive(!profilerCanvas.gameObject.activeSelf);
                    }
                });
            }
            systemProfiler = FindObjectOfType<SystemProfiler>();
            memoryProfiler = FindObjectOfType<MemoryProfiler>();
            renderingProfiler = FindObjectOfType<RenderingProfiler>();

            if (systemProfiler == null)
            {
                Debug.LogError("SystemProfiler component not found in the scene.");
                //enabled = false;
            }

            if (memoryProfiler == null)
            {
                Debug.LogError("MemoryProfiler component not found in the scene.");
                //enabled = false;
            }

            if (renderingProfiler == null)
            {
                Debug.LogError("RenderingProfiler component not found in the scene.");
                //enabled = false;
            }

            if (systemProfiler != null)
            {
                osText.text = $"OS: {systemProfiler.OS}";
                deviceModelText.text = $"Device: {systemProfiler.DeviceModel}";
                processorTypeText.text = $"CPU: {systemProfiler.ProcessorType}";
                processorCountText.text = $"Cores: {systemProfiler.ProcessorCount}";
                graphicsDeviceNameText.text = $"GPU: {systemProfiler.GraphicsDeviceName}";
                graphicsDeviceVendorText.text = $"GPU Vendor: {systemProfiler.GraphicsDeviceVendor}";
                graphicsDeviceVersionText.text = $"GPU Version: {systemProfiler.GraphicsDeviceVersion}";
            }
        }

        void Update()
        {
            if (systemProfiler != null)
            {
                fpsText.text = $"FPS: {Mathf.RoundToInt(systemProfiler.FPS)}";
                averageFpsText.text = $"Avg FPS: {Mathf.RoundToInt(systemProfiler.AverageFPS)}";
                renderTimeText.text = $"Render: {systemProfiler.RenderTime:F2} ms";
            }

            if (memoryProfiler != null)
            {
                reservedMemoryText.text = $"Reserved: {memoryProfiler.FormatMemorySize(memoryProfiler.ReservedMemory)}";
                allocatedMemoryText.text = $"Allocated: {memoryProfiler.FormatMemorySize(memoryProfiler.AllocatedMemory)}";
                managedHeapMemoryText.text = $"Heap Size: {memoryProfiler.FormatMemorySize(memoryProfiler.ManagedHeapMemory)}";
                managedUsedMemoryText.text = $"Managed Used: {memoryProfiler.FormatMemorySize(memoryProfiler.ManagedUsedMemory)}";
                graphicsDriverMemoryText.text = $"GPU: {memoryProfiler.FormatMemorySize(memoryProfiler.GraphicsDriverMemory)}";
            }

            if (renderingProfiler != null)
            {
                setPassCallsText.text = $"Set Pass: {renderingProfiler.SetPassCalls}";
                drawCallsText.text = $"Draw Calls: {renderingProfiler.DrawCalls}";
                batchesText.text = $"Batches: {renderingProfiler.Batches}";
                trianglesText.text = $"Triangles: {renderingProfiler.Triangles}";
                verticesText.text = $"Vertices: {renderingProfiler.Vertices}";
            }
        }
    }
}
