namespace HotChocolate.Types.Filters
{
    public enum FilterKind : byte
    {
        /// <summary>
        /// Ignored Filters
        /// </summary>
        Ignored = 0x0,

        /// <summary>
        /// All String Filters
        /// </summary>
        String = 0x1,

        /// <summary>
        /// All Comparable Filters
        /// </summary>
        Comparable = 0x2,

        /// <summary>
        /// All Boolean Filters
        /// </summary>
        Boolean = 0x4,

        /// <summary>
        /// All Array Filters
        /// </summary>
        Array = 0x8,

        /// <summary>
        /// All Object Filters
        /// </summary>
        Object = 0x10,

        /// <summary>
        /// All Object Filters
        /// </summary>
        DateTime = 0x11,

        /// <summary>
        /// All Object Filters
        /// </summary>
        Geometry = 0x12,

        /// <summary>
        /// All Object Filters
        /// </summary>
        Skip = 0x13
    }
}
