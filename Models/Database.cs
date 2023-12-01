namespace DBMS.Models
{
    public class Database
    {
        private readonly SortedDictionary<int, Table> tables = new();
        private int nextId = 0;

        public int TableCount => tables.Count;
        public IReadOnlyDictionary<int, Table> Tables => tables;

        public Database() { }

        public Table AddTable(string name, Column[] columns)
        {
            Table table = new(nextId, name, columns);
            tables[nextId] = table;
            nextId++;
            return table;
        }

        public void RemoveTable(int id) => tables.Remove(id);

        public void Save(string path)
        {
            using FileStream fs = new(path, FileMode.Create);
            using BinaryWriter writer = new(fs);
            Write(writer);
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write7BitEncodedInt(TableCount);
            foreach (KeyValuePair<int, Table> entry in tables)
                entry.Value.Write(writer);
            writer.Write7BitEncodedInt(nextId);
        }

        public static Database Load(string path)
        {
            using FileStream fs = new(path, FileMode.Open);
            using BinaryReader reader = new(fs);
            Database database = new(reader);
            if (fs.Position != fs.Length)
                throw new FileFormatException("File has extra data");
            return database;
        }

        public Database(BinaryReader reader)
        {
            int tableCount = reader.Read7BitEncodedInt();
            for (int i = 0; i < tableCount; i++)
            {
                Table table = new(reader);
                tables[table.Id] = table;
            }
            nextId = reader.Read7BitEncodedInt();
        }
    }
}
