namespace DBMS.Models
{
    public class TableDifference : Table
    {
        public readonly Table LeftTable, RightTable;

        private TableDifference(int id, string name, Column[] columns, Table leftTable, Table rightTable)
            : base(id, name, columns)
        {
            LeftTable = leftTable;
            RightTable = rightTable;
        }

        public void Calculate()
        {
            base.ClearRows();
            foreach (Row row in LeftTable.Rows.Values)
                if (!RightTable.ContainsRow(row))
                    base.AddRow(row.Cells);
        }

        public static TableDifference Create(Table leftTable, Table rightTable)
        {
            if (leftTable.Columns.Count != rightTable.Columns.Count)
                throw new ArgumentException("Table difference: tables have different column counts");
            for (int i = 0; i < leftTable.Columns.Count; i++)
                if (leftTable.Columns[i].Type != rightTable.Columns[i].Type)
                    throw new ArgumentException("Table difference: tables have different column types");
            Column[] columns = new Column[leftTable.Columns.Count];
            for (int i = 0; i < leftTable.Columns.Count; i++)
                columns[i] = new(
                    leftTable.Columns[i].Name == rightTable.Columns[i].Name ?
                        leftTable.Columns[i].Name :
                        $"\"{leftTable.Columns[i].Name}\" / \"{rightTable.Columns[i].Name}\"",
                    leftTable.Columns[i].Type);
            TableDifference difference = new(
                leftTable.Id | (rightTable.Id << 16) | (1 << 31),
                $"Difference \"{leftTable.Name}\" - \"{rightTable.Name}\"",
                columns,
                leftTable, rightTable);
            return difference;
        }

        public override Row AddRow(Values.Value[] cells) => throw new NotImplementedException("Table difference is read-only");
        public override void RemoveRow(int id) => throw new NotImplementedException("Table difference is read-only");
        public override void ClearRows() => throw new NotImplementedException("Table difference is read-only");
    }
}
