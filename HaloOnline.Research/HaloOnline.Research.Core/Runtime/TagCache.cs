using System;
using System.Diagnostics;
using HaloOnline.Research.Core.Utilities;

namespace HaloOnline.Research.Core.Runtime
{
    // TODO: replace with tagtool components

    public class TagCache
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private GameProcess Game { get; }

        public uint TagIndexTable => new DefaultDictionary<GameVersion, uint>(Game.Version)
        {
            [GameVersion.Alpha] = ProcessAddress.FromImageAddress(0x22AAFFC),
            [GameVersion.Latest] = ProcessAddress.FromImageAddress(0x2F48008)
        };

        public uint TagAddressTable => new DefaultDictionary<GameVersion, uint>(Game.Version)
        {
            [GameVersion.Alpha] = ProcessAddress.FromImageAddress(0x22AAFF8),
            [GameVersion.Latest] = ProcessAddress.FromImageAddress(0x2F8C5C8)
        };

        public TagCache(GameProcess game)
        {
            if (game == null)
                throw new ArgumentNullException();

            Game = game;
        }

        public uint GetTagAddress(uint tagDatum)
        {
            ushort datumSalt = (ushort)(tagDatum >> 16); // unused
            ushort datumIndex = (ushort)(tagDatum & ushort.MaxValue);

            if (Game.Version == GameVersion.Alpha)
            {
                uint tagIndex = Game.Memory.ReadUInt32(Game.Memory.ReadUInt32(TagIndexTable) + datumIndex * sizeof(uint));

                return tagIndex == uint.MaxValue
                    ? 0
                    : Game.Memory.ReadUInt32(Game.Memory.ReadUInt32(TagAddressTable) + tagIndex * sizeof(uint));
            }

            // latest allocated static buffers instead
            if (Game.Version == GameVersion.Latest)
            {
                uint tagIndex = Game.Memory.ReadUInt32(TagIndexTable + datumIndex * sizeof(uint));
                return tagIndex == uint.MaxValue
                    ? 0
                    : Game.Memory.ReadUInt32(TagAddressTable + tagIndex * sizeof(uint));
            }

            return 0;
        }

        public uint GetTagMainStructAddress(uint tagDatum)
        {
            uint address = GetTagAddress(tagDatum);
            return address + Game.Memory.ReadUInt32(address + 0x10);
        }

        public uint GetTagClass(uint tagDatum)
        {
            uint address = GetTagAddress(tagDatum);
            return address + Game.Memory.ReadUInt32(address + 0x14);
        }

        public Tag GetTag(uint tagDatum)
        {
            return new Tag(Game.Memory, GetTagAddress(tagDatum));
        }
    }
}
