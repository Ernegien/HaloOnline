namespace HaloOnline.Research.Core.Imports.Structs
{
    /// <summary>
    /// TODO: description
    /// </summary>
    public enum MemoryType : uint
    {
        Private = 1 << 17,
        Mapped= 1 << 18,
        Image = 1 << 24
    }
}
