using System;
using System.Text;

namespace HaloOnline.Research.Core.Runtime
{
    // TODO: replace with tagtool components

    public struct TagClass
    {
        public uint Value;

        public TagClass(uint value)
        {
            Value = value;
        }

        public new string ToString()
        {
            if (Value == 0xFFFFFFF) return null;
            byte[] classBytes = BitConverter.GetBytes(Value);
            Array.Reverse(classBytes);
            return Encoding.ASCII.GetString(classBytes);
        }
    }
}
