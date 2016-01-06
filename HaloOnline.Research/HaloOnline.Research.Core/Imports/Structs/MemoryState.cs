namespace HaloOnline.Research.Core.Imports.Structs
{
    /// <summary>
    /// TODO: description
    /// </summary>
    public enum MemoryState : uint
    {
        Commit = 1 << 12,
        Reserve = 1 << 13,
        Free = 1 << 16
    }
}
