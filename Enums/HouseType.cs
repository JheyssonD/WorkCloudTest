namespace WorkCloudTest.Enums
{
    /// <summary>
    /// Enum tipo de Casas
    /// </summary>
    public class HouseType : Enumeration
    {
        /// <summary>
        /// Gryffindor House
        /// </summary>
        public static readonly HouseType Gryffindor = new HouseType(0, "Gryffindor");

        /// <summary>
        /// Hufflepuff House
        /// </summary>
        public static readonly HouseType Hufflepuff = new HouseType(1, "Hufflepuff");

        /// <summary>
        /// Ravenclaw House
        /// </summary>
        public static readonly HouseType Ravenclaw = new HouseType(2, "Ravenclaw");

        /// <summary>
        /// Slytherin House
        /// </summary>
        public static readonly HouseType Slytherin = new HouseType(3, "Slytherin");

        public HouseType() : base()
        {
        }

        private HouseType(int value, string name)
            : base(value, name)
        {
        }
    }
}
