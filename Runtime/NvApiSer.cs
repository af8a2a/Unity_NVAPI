using System.Runtime.InteropServices;

namespace NVAPI
{
    public static class NvApiSer
    {
        private const string DllName = "UnityPlugin";

        /// <summary>
        /// Check if the NVAPI driver supports the Shader Execution Reordering opcode.
        /// </summary>
        [DllImport(DllName)]
        public static extern bool NvAPI_IsShaderExecutionReorderingAPISupported();

        /// <summary>
        /// Check if the GPU hardware supports thread reordering (requires Ada architecture / RTX 40-series+).
        /// </summary>
        [DllImport(DllName)]
        public static extern bool NvAPI_IsShaderExecutionReorderingSupportedByGPU();

        /// <summary>
        /// Set the UAV slot used for NVAPI shader extensions.
        /// Must be called before dispatching ray tracing shaders that use SER.
        /// </summary>
        /// <param name="uavSlot">The UAV register slot number (e.g. 7 for register u7).</param>
        [DllImport(DllName)]
        public static extern bool NvAPI_SetNvShaderExtnSlot(uint uavSlot);
    }
}
