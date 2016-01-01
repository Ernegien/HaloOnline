using System;
using HaloOnline.Research.Core.Utilities;

namespace HaloOnline.Research.Core.Runtime
{
    // TODO: replace with tagtool components

    public class TagCache
    {        
        public readonly DefaultDictionary<GameVersion, ProcessAddress> TagIndexTable;       
        public readonly DefaultDictionary<GameVersion, ProcessAddress> TagAddressTable;

        private GameProcess Game { get; }

        public TagCache(GameProcess game)
        {
            if (game == null)
                throw new ArgumentNullException();

            Game = game;

            TagIndexTable = new DefaultDictionary<GameVersion, ProcessAddress>(game.Version)
            {
                [GameVersion.Alpha] = new ProcessAddress(0x22AAFFC),
                [GameVersion.Latest] = new ProcessAddress(0x2F48008)
            };

            TagAddressTable = new DefaultDictionary<GameVersion, ProcessAddress>(game.Version)
            {
                [GameVersion.Alpha] = new ProcessAddress(0x22AAFF8),
                [GameVersion.Latest] = new ProcessAddress(0x2F8C5C8)
            };
        }

        public uint GetTagAddress(uint tagDatum)
        {
            ushort datumSalt = (ushort)(tagDatum >> 16); // unused
            ushort datumIndex = (ushort)(tagDatum & ushort.MaxValue);

            if (Game.Version == GameVersion.Alpha)
            {
                uint tagIndex = Game.MemoryStream.ReadUInt32(Game.MemoryStream.ReadUInt32((ProcessAddress)TagIndexTable) + datumIndex * sizeof(uint));

                return tagIndex == uint.MaxValue
                    ? 0
                    : Game.MemoryStream.ReadUInt32(Game.MemoryStream.ReadUInt32((ProcessAddress)TagAddressTable) + tagIndex * sizeof(uint));
            }

            // latest allocated static buffers instead
            if (Game.Version == GameVersion.Latest)
            {
                uint tagIndex = Game.MemoryStream.ReadUInt32((ProcessAddress)TagIndexTable + datumIndex * sizeof(uint));
                return tagIndex == uint.MaxValue
                    ? 0
                    : Game.MemoryStream.ReadUInt32((ProcessAddress)TagAddressTable + tagIndex * sizeof(uint));
            }

            return 0;
        }

        public uint GetTagMainStructAddress(uint tagDatum)
        {
            uint address = GetTagAddress(tagDatum);
            return address + Game.MemoryStream.ReadUInt32(address + 0x10);
        }

        public uint GetTagClass(uint tagDatum)
        {
            uint address = GetTagAddress(tagDatum);
            return address + Game.MemoryStream.ReadUInt32(address + 0x14);
        }

        public Tag GetTag(uint tagDatum)
        {
            return new Tag(Game.MemoryStream, GetTagAddress(tagDatum));
        }
    }
}
