namespace HaloOnline.Research.Core.Runtime
{
    /// <summary>
    /// Halo Online game versions. I'm only concerned with researching the first and last releases.
    /// </summary>
    public enum GameVersion : uint
    {
        /// <summary>
        /// 1.106708 cert_ms23
        /// </summary>
        Alpha = 0x550C2DC5,

        /// <summary>
        /// 12.1.700255 cert_ms30_oct19
        /// </summary>
        Latest = 0x565493D6
    }
}
