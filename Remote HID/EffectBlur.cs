
using System.Runtime.InteropServices;

namespace Remote_HID
{
    internal class EffectBlur
    {

        public void EnableBlur(nint formHandle)
        {
            int accentState = 4;
            var accent = new AccentPolicy
            {
                AccentState = accentState,
                GradientColor = (0 << 24) | (0x33 << 16) | (0x33 << 8) | 0x66,
                AnimationId =1
            };
            int sizeOfPolicy = Marshal.SizeOf(accent);
            IntPtr accentPtr = Marshal.AllocHGlobal(sizeOfPolicy);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowCompositionAttributeData
            {
                Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
                SizeOfData = sizeOfPolicy,
                Data = accentPtr
            };

            SetWindowCompositionAttribute(formHandle, ref data);

            Marshal.FreeHGlobal(accentPtr);
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct AccentPolicy
        {
            public int AccentState;
            public int Flags;
            public int GradientColor;
            public int AnimationId;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct WindowCompositionAttributeData
        {
            public WindowCompositionAttribute Attribute;
            public IntPtr Data;
            public int SizeOfData;
        }

        private enum WindowCompositionAttribute
        {
            WCA_ACCENT_POLICY = 19
        }

        [DllImport("user32.dll")]
        private static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
    }
}
