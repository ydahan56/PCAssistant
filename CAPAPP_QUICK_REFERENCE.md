# Chrome Capture Issue - Quick Reference

## The Issue
Capapp times out when trying to capture Chrome windows.

## Why It Happens
1. Chrome uses Chromium rendering engine
2. Chromium doesn't expose Direct3D surfaces in the way Windows.Graphics.Capture requires
3. `CreateForWindow()` returns `IntPtr.Zero` silently (doesn't throw exception)
4. Code tried to use the null pointer anyway
5. Never received any frames
6. Waited 3 seconds → timeout

## The Fix (What Was Done)
Added two null checks:

### Check #1: In `CreateItemForWindow()` (line 257-261)
```csharp
if (itemPtr == IntPtr.Zero)
{
	return null;  // ← Detect incompatible window immediately
}
```

### Check #2: In `CaptureWindowWGC()` (line 145-149)
```csharp
if (item == null)
{
	SendResult($"Window 0x{hwnd:X8} is not eligible for capture...", false);
	return null;  // ← Report error and exit early
}
```

## Result
**Before:** 3-second timeout with generic message  
**After:** Immediate failure with clear diagnostic message

## Who Can Be Captured?
✅ Notepad, Explorer, VS Code, Word, Excel  
✅ Video games, Discord  
❌ **Chrome, Edge, Brave, Vivaldi, Firefox, Opera**  

## Why Chrome Can't Be Captured (Technical)
```
Chrome Rendering:
  Browser Window Display
	 ↓ (only composite bitmap)
  GPU Process (separate)
	 ↓
  ANGLE Layer (abstraction)
	 ↓
  DirectX API

Windows.Graphics.Capture Requirement:
  Direct3D 11 GPU Surface
	 ↓
  DXGI Surface
	 ↓
  GPU Memory

❌ Chrome can't provide this = incompatible
```

## Code Changes Summary
| File | Line | Change |
|------|------|--------|
| `DllMain.cs` | 257-261 | Add null check for CreateForWindow result |
| `DllMain.cs` | 145-149 | Add null check before using capture item |
| `DllMain.cs` | 350-358 | Update SendResult method signature |
| `DllMain.cs` | 78-122 | Update Execute method error reporting |

## Files Created (Documentation)
- `CAPAPP_CHROME_DIAGNOSIS.md` - Full technical analysis
- `WINDOWS_GRAPHICS_CAPTURE_ANALYSIS.md` - Windows Graphics Capture deep dive
- `CAPAPP_FIX_SUMMARY.md` - Before/after comparison

## Testing
```
❌ Chrome → "Window is not eligible for capture"
✅ Notepad → Image captured successfully
✅ Excel → Image captured successfully
❌ Minimized app → "Timed out waiting for frame"
```

## Build
✅ Successful - No compilation errors

---

**TL;DR:** Chrome windows can't be captured with Windows.Graphics.Capture due to architectural incompatibility. The fix detects this early and reports clearly instead of hanging.
