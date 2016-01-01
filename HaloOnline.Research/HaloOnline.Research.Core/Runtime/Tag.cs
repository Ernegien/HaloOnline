using System.Collections.Generic;
using System.IO;

namespace HaloOnline.Research.Core.Runtime
{
    // TODO: replace with tagtool components

    public class Tag
    {
        public const uint BaseHeaderSize = 0x24;
        public const uint PointerBase = 0x40000000;

        public uint Offset;

        // main header info
        public uint Checksum;
        public uint Size;
        public ushort DependencyCount;
        public ushort DataPointerCount;
        public ushort ResourcePointerCount;
        public uint MainStructOffset;    //root tag data offset (relative to tag base before tag header)
        public TagClass Class;
        public TagClass ParentClass;
        public TagClass GrandparentClass;
        public uint ClassId;

        // supplimental header info
        public List<uint> DependencyIndices = new List<uint>();
        public List<uint> DataPointerOffsets = new List<uint>();
        public List<uint> ResourcePointerOffsets = new List<uint>();

        // meta offset in tags.dat
        public uint MetaOffset => Offset + HeaderSize;

        public uint MetaSize => Size - HeaderSize;

        public uint HeaderSize
        {
            get
            {
                // unpadded size (provides most flexibility for editors that don't honor tag header padding)
                return (uint)(BaseHeaderSize + DependencyCount * 4 + DataPointerCount * 4 + ResourcePointerCount * 4);

                // padded to nearest 16 bytes
                //return
                //    (uint)
                //        ((BaseHeaderSize + DependencyCount*sizeof (uint) + DataPointerCount*sizeof (uint) +
                //          ResourcePointerCount*sizeof (uint)) + 0xF) & 0xFFFFFFF0;
            }
        }

        public Tag(Stream stream) : this(stream, stream.Position) { }

        public Tag(Stream stream, long position)
        {
            var reader = new BinaryReader(stream);
            stream.Position = position;

            Offset = (uint)position;

            Checksum = reader.ReadUInt32();
            Size = reader.ReadUInt32();
            DependencyCount = reader.ReadUInt16();
            DataPointerCount = reader.ReadUInt16();
            ResourcePointerCount = reader.ReadUInt16();
            stream.Position += sizeof(ushort);  // padding
            MainStructOffset = reader.ReadUInt32();
            Class = new TagClass(reader.ReadUInt32());
            ParentClass = new TagClass(reader.ReadUInt32());
            GrandparentClass = new TagClass(reader.ReadUInt32());
            ClassId = reader.ReadUInt32();

            for (int i = 0; i < DependencyCount; i++)
            {
                DependencyIndices.Add(reader.ReadUInt32());
            }

            for (int i = 0; i < DataPointerCount; i++)
            {
                DataPointerOffsets.Add(reader.ReadUInt32());
            }

            for (int i = 0; i < ResourcePointerCount; i++)
            {
                ResourcePointerOffsets.Add(reader.ReadUInt32());
            }
        }
    }
}
