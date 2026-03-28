# Unity NVAPI Plugin

Unity Native Plugin wrapping NVIDIA NVAPI, providing Shader Execution Reordering (SER) interfaces for D3D12 ray tracing.

## Prerequisites

- Windows 10/11 (x64)
- Visual Studio 2022+ with C++ desktop workload
- CMake 3.20+
- NVIDIA RTX GPU (Ada architecture / RTX 40-series+ recommended for SER)

## Clone

NVAPI SDK is included as a git submodule. Clone with `--recursive`:

```bash
git clone --recursive https://github.com/af8a2a/Unity_NVAPI.git
```

If you already cloned without `--recursive`:

```bash
git submodule update --init --recursive
```

## Build

```bash
cmake -B build -G "Visual Studio 17 2022" -A x64
cmake --build build --config Release
```

Output DLL: `build/bin/Release/UnityPlugin.dll`

## Usage in Unity

### 1. Deploy the Plugin

Copy `UnityPlugin.dll` to your Unity project's `Assets/Plugins/x86_64/` folder.

### 2. C# Interop

```csharp
using System.Runtime.InteropServices;

public static class NVAPI_SER
{
    [DllImport("UnityPlugin")]
    public static extern bool NvAPI_IsShaderExecutionReorderingAPISupported();

    [DllImport("UnityPlugin")]
    public static extern bool NvAPI_IsShaderExecutionReorderingSupportedByGPU();

    [DllImport("UnityPlugin")]
    public static extern bool NvAPI_SetNvShaderExtnSlot(uint uavSlot);
}
```

### 3. HLSL Shader Integration

Copy the following NVAPI shader headers from `External/NVAPI/` to your Unity shader include path:
- `nvHLSLExtns.h`
- `nvHLSLExtnsInternal.h`
- `nvShaderExtnEnums.h`

In your ray tracing shaders:

```hlsl
#define NV_SHADER_EXTN_SLOT u7
#include "nvHLSLExtns.h"

// In ray generation shader:
NvHitObject hitObj = NvTraceRayHitObject(...);
NvReorderThread(hitObj);
NvInvokeHitObject(..., hitObj);
```

## Exported API

| Function | Description |
|---|---|
| `NvAPI_IsShaderExecutionReorderingAPISupported()` | Check if the NVAPI driver supports the SER opcode |
| `NvAPI_IsShaderExecutionReorderingSupportedByGPU()` | Check if the GPU hardware supports thread reordering |
| `NvAPI_SetNvShaderExtnSlot(uint uavSlot)` | Set the UAV slot used for NVAPI shader extensions |

## License

This project is MIT licensed. NVAPI SDK (included as submodule) is also [MIT licensed](https://github.com/NVIDIA/nvapi/blob/main/License.txt).
