using System.Security.AccessControl;
using HaloOnline.Research.Core.Utilities;

namespace HaloOnline.Research.Core.Runtime
{
    public class GameAddresses
    {
        private GameProcess Game { get; }

        public uint Objects => new DefaultDictionary<GameVersion, uint>(Game.Version)
        {
            [GameVersion.Alpha] = 0x448,
            [GameVersion.Latest] = 0xC
        };

        public uint DirectorGlobals => new DefaultDictionary<GameVersion, uint>(Game.Version)
        {
            [GameVersion.Alpha] = 0x60,
            [GameVersion.Latest] = 0xC0
        };

        public GameAddresses(GameProcess game)
        {
            Game = game;
        }
    }
}
