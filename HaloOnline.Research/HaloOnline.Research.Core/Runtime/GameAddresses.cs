using System;
using System.Diagnostics;
using HaloOnline.Research.Core.Utilities;

namespace HaloOnline.Research.Core.Runtime
{
    /// <summary>
    /// TODO: description
    /// </summary>
    public class GameAddresses
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private GameProcess Game { get; }

        /// <summary>
        /// The object array address.
        /// </summary>
        public uint Object
        {
            get
            {
                var tlsOffset = new DefaultDictionary<GameVersion, uint>(Game.Version)
                {
                    [GameVersion.Alpha] = 0x448,
                    [GameVersion.Latest] = 0xC
                };
                return Game.Memory.ReadUInt32(Game.TlsAddress + tlsOffset);
            }
        }

        /// <summary>
        /// The director globals address.
        /// </summary>
        public uint DirectorGlobals
        {
            get
            {
                var tlsOffset = new DefaultDictionary<GameVersion, uint>(Game.Version)
                {
                    [GameVersion.Alpha] = 0x60,
                    [GameVersion.Latest] = 0xC0
                };
                return Game.Memory.ReadUInt32(Game.TlsAddress + tlsOffset);
            }
        }

        /// <summary>
        /// The players data array address.
        /// </summary>
        public uint Players
        {
            get
            {
                var tlsOffset = new DefaultDictionary<GameVersion, uint>(Game.Version)
                {
                    [GameVersion.Alpha] = 0x40,
                    [GameVersion.Latest] = 8
                };
                return Game.Memory.ReadUInt32(Game.TlsAddress + tlsOffset);
            }
        }

        /// <summary>
        /// The players globals address.
        /// </summary>
        public uint PlayersGlobals
        {
            get
            {
                var tlsOffset = new DefaultDictionary<GameVersion, uint>(Game.Version)
                {
                    [GameVersion.Alpha] = 0x44,
                    [GameVersion.Latest] = 0x30
                };
                return Game.Memory.ReadUInt32(Game.TlsAddress + tlsOffset);
            }
        }

        /// <summary>
        /// The player mapping globals address.
        /// </summary>
        public uint PlayerMappingGlobals
        {
            get
            {
                var tlsOffset = new DefaultDictionary<GameVersion, uint>(Game.Version)
                {
                    [GameVersion.Alpha] = 0x5C,
                    [GameVersion.Latest] = 0x84
                };
                return Game.Memory.ReadUInt32(Game.TlsAddress + tlsOffset);
            }
        }

        /// <summary>
        /// The player control globals address.
        /// </summary>
        public uint PlayerControlGlobals
        {
            get
            {
                var tlsOffset = new DefaultDictionary<GameVersion, uint>(Game.Version)
                {
                    [GameVersion.Alpha] = 0xC4,
                    [GameVersion.Latest] = 0x4C
                };
                return Game.Memory.ReadUInt32(Game.TlsAddress + tlsOffset);
            }
        }


        /// <summary>
        /// The deterministic player control globals address.
        /// </summary>
        public uint PlayerControlGlobalsDeterministic
        {
            get
            {
                var tlsOffset = new DefaultDictionary<GameVersion, uint>(Game.Version)
                {
                    [GameVersion.Alpha] = 0xC8,
                    [GameVersion.Latest] = 0x224
                };
                return Game.Memory.ReadUInt32(Game.TlsAddress + tlsOffset);
            }
        }

        /// <summary>
        /// The game time globals address.
        /// </summary>
        public uint GameTimeGlobals
        {
            get
            {
                var tlsOffset = new DefaultDictionary<GameVersion, uint>(Game.Version)
                {
                    [GameVersion.Alpha] = 0x50,
                    [GameVersion.Latest] = 0x64
                };
                return Game.Memory.ReadUInt32(Game.TlsAddress + tlsOffset);
            }
        }

        /// <summary>
        /// The game globals address.
        /// </summary>
        public uint GameGlobals
        {
            get
            {
                var tlsOffset = new DefaultDictionary<GameVersion, uint>(Game.Version)
                {
                    [GameVersion.Alpha] = 0x3C,
                    [GameVersion.Latest] = 0x24
                };
                return Game.Memory.ReadUInt32(Game.TlsAddress + tlsOffset);
            }
        }

        /// <summary>
        /// TODO: description
        /// </summary>
        /// <param name="game"></param>
        public GameAddresses(GameProcess game)
        {
            if (game == null)
                throw new ArgumentNullException();

            Game = game;
        }
    }
}
