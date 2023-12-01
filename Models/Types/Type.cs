namespace DBMS.Models.Types
{
    public abstract class Type
    {
        public abstract Values.Value Parse(string s);
        public abstract void Write(BinaryWriter writer);

        public static Type Read(BinaryReader reader)
        {
            return reader.ReadByte() switch
            {
                Integer.CODE => new Integer(),
                Real.CODE => new Real(),
                Char.CODE => new Char(),
                String.CODE => new String(),
                Color.CODE => new Color(),
                ColorInvl.CODE => new ColorInvl(reader),
                _ => throw new NotImplementedException("Unknown type code")
            };
        }

        public abstract Values.Value ReadValue(BinaryReader reader);

        public static bool operator ==(Type left, Type right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Type left, Type right)
        {
            return !left.Equals(right);
        }
    }
}
