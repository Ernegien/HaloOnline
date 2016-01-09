using System;
using System.IO;
using HaloOnline.Research.Core.Imports;

namespace HaloOnline.Research.Core.Utilities
{
    /// <summary>
    /// Provides memory access to a remote process.
    /// </summary>
    public unsafe class ProcessStream : Stream
    {
        #region Properties

        public IntPtr ProcessHandle { get; }

        public BinaryReader Reader { get; }

        public BinaryWriter Writer { get; }

        public override long Position { get; set; }

        public override long Length
        {
            get { throw new NotSupportedException(); }
        }

        public override bool CanRead => true;

        public override bool CanSeek => true;

        public override bool CanWrite => true;

        #endregion

        #region Constructor / Destructor

        public ProcessStream(IntPtr processHandle)
        {
            ProcessHandle = processHandle;
            Reader = new BinaryReader(this);
            Writer = new BinaryWriter(this);
        }

        #endregion

        #region Methods

        public override void Flush()
        {
            // do nothing
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    Position = offset;
                    break;
                case SeekOrigin.Current:
                    Position += offset;
                    break;
                default:
                    throw new InvalidOperationException();
            }
            return Position;
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (offset + count > buffer.Length)
                throw new IndexOutOfRangeException();

            int bytesRead;
            fixed (byte* pBuffer = buffer)
            {
                Kernel32.ReadProcessMemory(ProcessHandle, (UIntPtr)Position, pBuffer + offset, count, out bytesRead);
            }
            Position += bytesRead;
            return bytesRead;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (offset + count > buffer.Length)
                throw new IndexOutOfRangeException();

            fixed (byte* pBuffer = buffer)
            {
                int bytesWritten;
                Kernel32.WriteProcessMemory(ProcessHandle, (UIntPtr)Position, pBuffer + offset, count, out bytesWritten);
                Position += bytesWritten;
            }
        }

        #endregion

        #region Supplemental Methods

        public bool ReadBoolean()
        {
            return Reader.ReadBoolean();
        }

        public bool ReadBoolean(long address)
        {
            Position = address;
            return Reader.ReadBoolean();
        }

        public char ReadChar()
        {
            return Reader.ReadChar();
        }

        public char ReadChar(long address)
        {
            Position = address;
            return Reader.ReadChar();
        }

        public char[] ReadChars(int count)
        {
            return Reader.ReadChars(count);
        }

        public char[] ReadChars(long address, int count)
        {
            Position = address;
            return Reader.ReadChars(count);
        }

        public byte ReadByte(long address)
        {
            Position = address;
            return Reader.ReadByte();
        }

        public byte[] ReadBytes(int count)
        {
            return Reader.ReadBytes(count);
        }

        public byte[] ReadBytes(long address, int count)
        {
            Position = address;
            return Reader.ReadBytes(count);
        }

        public decimal ReadDecimal()
        {
            return Reader.ReadDecimal();
        }

        public decimal ReadDecimal(long address)
        {
            Position = address;
            return Reader.ReadDecimal();
        }

        public double ReadDouble()
        {
            return Reader.ReadDouble();
        }

        public double ReadDouble(long address)
        {
            Position = address;
            return Reader.ReadDouble();
        }

        public short ReadInt16()
        {
            return Reader.ReadInt16();
        }

        public short ReadInt16(long address)
        {
            Position = address;
            return Reader.ReadInt16();
        }

        public int ReadInt32()
        {
            return Reader.ReadInt32();
        }

        public int ReadInt32(long address)
        {
            Position = address;
            return Reader.ReadInt32();
        }

        public Int64 ReadInt64()
        {
            return Reader.ReadInt64();
        }

        public Int64 ReadInt64(long address)
        {
            Position = address;
            return Reader.ReadInt64();
        }

        public sbyte ReadSByte()
        {
            return Reader.ReadSByte();
        }

        public sbyte ReadSByte(long address)
        {
            Position = address;
            return Reader.ReadSByte();
        }

        public float ReadSingle(long address)
        {
            Position = address;
            return Reader.ReadSingle();
        }

        public float ReadSingle()
        {
            return Reader.ReadSingle();
        }

        public ushort ReadUInt16()
        {
            return Reader.ReadUInt16();
        }

        public ushort ReadUInt16(long address)
        {
            Position = address;
            return Reader.ReadUInt16();
        }

        public uint ReadUInt32()
        {
            return Reader.ReadUInt32();
        }

        public uint ReadUInt32(long address)
        {
            Position = address;
            return Reader.ReadUInt32();
        }

        public void WriteBoolean(long address, bool value)
        {
            Position = address;
            Writer.Write(value);
        }

        public void WriteBoolean(bool value)
        {
            Writer.Write(value);
        }

        public void WriteByte(long address, byte value)
        {
            Position = address;
            Writer.Write(value);
        }

        public void WriteChar(char value)
        {
            Writer.Write(value);
        }

        public void WriteChar(long address, char value)
        {
            Position = address;
            Writer.Write(value);
        }

        public void WriteUInt16(long address, ushort value)
        {
            Position = address;
            Writer.Write(value);
        }

        public void WriteUInt16(ushort value)
        {
            Writer.Write(value);
        }

        public void WriteUInt32(long address, uint value)
        {
            Position = address;
            Writer.Write(value);
        }

        public void WriteUInt32(uint value)
        {
            Writer.Write(value);
        }

        public void WriteUInt64(long address, ulong value)
        {
            Position = address;
            Writer.Write(value);
        }

        public void WriteUInt64(ulong value)
        {
            Writer.Write(value);
        }

        public void WriteInt64(long value)
        {
            Writer.Write(value);
        }

        public void WriteInt64(long address, long value)
        {
            Position = address;
            Writer.Write(value);
        }

        public void WriteInt32(long address, int value)
        {
            Position = address;
            Writer.Write(value);
        }

        public void WriteInt32(int value)
        {
            Writer.Write(value);
        }

        public void WriteSingle(long address, float value)
        {
            Position = address;
            Writer.Write(value);
        }

        public void WriteSingle(float value)
        {
            Writer.Write(value);
        }

        public void WriteDouble(long address, double value)
        {
            Position = address;
            Writer.Write(value);
        }

        public void WriteDouble(double value)
        {
            Writer.Write(value);
        }

        public void WriteNops(long address, int count)
        {
            Position = address;
            byte[] nops = new byte[count];
            for (int i = 0; i < nops.Length; i++)
            {
                nops[i] = 0x90;
            }
            Write(nops, 0, nops.Length);
        }

        public void Write(long address, params object[] values)
        {
            Position = address;
            foreach (object obj in values)
            {
                switch (Convert.GetTypeCode(obj))
                {
                    case TypeCode.Boolean:
                    case TypeCode.Byte:
                    case TypeCode.Char: WriteByte(Convert.ToByte(obj)); break;
                    case TypeCode.Int16:
                    case TypeCode.UInt16: WriteUInt16(Convert.ToUInt16(obj)); break;
                    case TypeCode.Int32:
                    case TypeCode.UInt32: WriteUInt32(Convert.ToUInt32(obj)); break;
                    case TypeCode.Int64:
                    case TypeCode.UInt64: WriteUInt64(Convert.ToUInt64(obj)); break;
                    case TypeCode.Single: WriteSingle(Convert.ToSingle(obj)); break;
                    case TypeCode.Double: WriteDouble(Convert.ToDouble(obj)); break;
                    case TypeCode.Object:
                        // TODO: revisit this
                        byte[] bytes = obj as byte[];
                        if (bytes != null) Write(bytes, 0, bytes.Length);
                        else throw new InvalidOperationException("Invalid datatype.");
                        break;
                    default: throw new InvalidOperationException("Invalid datatype.");
                }
            }
        }

        #endregion
    }
}