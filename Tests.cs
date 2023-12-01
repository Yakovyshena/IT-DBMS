using Xunit;

namespace DBMS
{
    public class Tests
    {
        private static void CheckColor(Models.Values.Color color, byte r, byte g, byte b)
        {
            Assert.Equal(r, color.R);
            Assert.Equal(g, color.G);
            Assert.Equal(b, color.B);
        }

        private static void CheckColor(Models.Values.ColorInvl color, byte r, byte g, byte b)
        {
            Assert.Equal(r, color.R);
            Assert.Equal(g, color.G);
            Assert.Equal(b, color.B);
        }

        [Fact]
        public void ParsingAndValidating()
        {
            Assert.Equal('A', new Models.Types.Char().Parse("A").Val);
            Assert.Throws<FormatException>(() => new Models.Types.Char().Parse("AB"));

            Assert.Equal(123, new Models.Types.Integer().Parse(" 123  ").Val);
            Assert.Throws<FormatException>(() => new Models.Types.Integer().Parse("A123"));

            Assert.Equal(new decimal(123), new Models.Types.Real().Parse(" 123  ").Val);
            Assert.Equal(new decimal(123.45), new Models.Types.Real().Parse(" 123.45  ").Val);
            Assert.Throws<FormatException>(() => new Models.Types.Real().Parse("A123"));

            CheckColor(new Models.Types.Color().Parse(" 123 45 6 "), 123, 45, 6);
            CheckColor(new Models.Types.Color().Parse("efregr=123,g45 6"), 123, 45, 6);
            Assert.Throws<FormatException>(() => new Models.Types.Color().Parse("123 45"));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Models.Types.Color().Parse("123 456 7"));
            Assert.Throws<FormatException>(() => new Models.Types.Color().Parse("123 45 6 7"));

            Assert.Throws<ArgumentException>(() => new Models.Types.ColorInvl(0, 255, 10, 9, 0, 255));
            CheckColor(new Models.Types.ColorInvl(100, 200, 40, 50, 6, 6).Parse(" 123 45 6 "), 123, 45, 6);
            CheckColor(new Models.Types.ColorInvl(100, 200, 40, 50, 6, 6).Parse("efregr=123,g45 6"), 123, 45, 6);
            Assert.Throws<FormatException>(() => new Models.Types.ColorInvl(0, 255, 0, 255, 0, 255).Parse("123 45"));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Models.Types.ColorInvl(0, 255, 0, 255, 0, 255).Parse("123 456 7"));
            Assert.Throws<FormatException>(() => new Models.Types.ColorInvl(0, 255, 0, 255, 0, 255).Parse("123 45 6 7"));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Models.Types.ColorInvl(100, 200, 100, 200, 100, 200).Parse("99 150 150"));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Models.Types.ColorInvl(100, 200, 100, 200, 100, 200).Parse("150 99 150"));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Models.Types.ColorInvl(100, 200, 100, 200, 100, 200).Parse("150 150 99"));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Models.Types.ColorInvl(100, 200, 100, 200, 100, 200).Parse("201 150 150"));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Models.Types.ColorInvl(100, 200, 100, 200, 100, 200).Parse("150 201 150"));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Models.Types.ColorInvl(100, 200, 100, 200, 100, 200).Parse("150 150 201"));
        }

        [Fact]
        public void LoadAndSave()
        {
            Models.Database original = new();

            Models.Column[] columns1 = new Models.Column[]
            {
                new Models.Column("column1", new Models.Types.Integer()),
                new Models.Column("column2", new Models.Types.Real()),
                new Models.Column("column3", new Models.Types.Char())
            };
            Models.Table table1 = original.AddTable("table1", columns1);
            table1.AddRow(new Models.Values.Value[] { columns1[0].Type.Parse("123"), columns1[1].Type.Parse("123.45"), columns1[2].Type.Parse("A") });
            table1.AddRow(new Models.Values.Value[] { columns1[0].Type.Parse("678"), columns1[1].Type.Parse("902.23"), columns1[2].Type.Parse("B") });

            Models.Column[] columns2 = new Models.Column[]
            {
                new Models.Column("column4", new Models.Types.String()),
                new Models.Column("column5", new Models.Types.Color()),
                new Models.Column("column6", new Models.Types.ColorInvl(10, 20, 30, 40, 50, 60))
            };
            Models.Table table2 = original.AddTable("table2", columns2);
            table2.AddRow(new Models.Values.Value[] { columns2[0].Type.Parse("egrge"), columns2[1].Type.Parse("1 2 3"), columns2[2].Type.Parse("10 30 50") });
            table2.AddRow(new Models.Values.Value[] { columns2[0].Type.Parse("erwtve"), columns2[1].Type.Parse("4 5 6"), columns2[2].Type.Parse("20 40 60") });

            original.Save("test.dbf");

            Models.Database loaded = Models.Database.Load("test.dbf");

            Assert.Equal(original.TableCount, loaded.TableCount);

            for (int i = 0; i < original.TableCount; i++)
            {
                Assert.Equal(original.Tables[i].Id, loaded.Tables[i].Id);
                Assert.Equal(original.Tables[i].Name, loaded.Tables[i].Name);

                Assert.Equal(original.Tables[i].Columns.Count, loaded.Tables[i].Columns.Count);
                for (int j = 0; j < original.Tables[i].Columns.Count; j++)
                {
                    Assert.Equal(original.Tables[i].Columns[j].Name, loaded.Tables[i].Columns[j].Name);
                    Assert.Equal(original.Tables[i].Columns[j].Type, loaded.Tables[i].Columns[j].Type);
                }

                Assert.Equal(original.Tables[i].Rows.Count, loaded.Tables[i].Rows.Count);
                Assert.Equal(original.Tables[i].Rows[i].Id, loaded.Tables[i].Rows[i].Id);
                Assert.Equal(original.Tables[i].Rows[i].Cells, loaded.Tables[i].Rows[i].Cells);
            }
        }

        private static void CheckTableDifference(Models.TableDifference tableDifference, Models.Values.Value[][] rows)
        {
            tableDifference.Calculate();
            Assert.Equal(rows.Length, tableDifference.Rows.Count);
            for (int i = 0; i < rows.Length; i++)
                Assert.Equal(rows[i], tableDifference.Rows[i].Cells);
        }

        [Fact]
        public void TableDifference()
        {
            Models.Column[] columns = new Models.Column[]
            {
                new Models.Column("column0", new Models.Types.Integer()),
                new Models.Column("column1", new Models.Types.Real()),
                new Models.Column("column2", new Models.Types.Char()),
                new Models.Column("column3", new Models.Types.String()),
                new Models.Column("column4", new Models.Types.Color()),
                new Models.Column("column5", new Models.Types.ColorInvl(10, 20, 30, 40, 50, 60))
            };
            Models.Values.Value[] row = new Models.Values.Value[]
            {
                columns[0].Type.Parse("123"),
                columns[1].Type.Parse("123.45"),
                columns[2].Type.Parse("A"),
                columns[3].Type.Parse("ery5y4"),
                columns[4].Type.Parse("1 2 3"),
                columns[5].Type.Parse("10 30 50")
            };
            Models.Values.Value[] row0 = new Models.Values.Value[]
            {
                columns[0].Type.Parse("1234"),
                columns[1].Type.Parse("123.45"),
                columns[2].Type.Parse("A"),
                columns[3].Type.Parse("ery5y4"),
                columns[4].Type.Parse("1 2 3"),
                columns[5].Type.Parse("10 30 50")
            };
            Models.Values.Value[] row1 = new Models.Values.Value[]
            {
                columns[0].Type.Parse("123"),
                columns[1].Type.Parse("123.456"),
                columns[2].Type.Parse("A"),
                columns[3].Type.Parse("ery5y4"),
                columns[4].Type.Parse("1 2 3"),
                columns[5].Type.Parse("10 30 50")
            };
            Models.Values.Value[] row2 = new Models.Values.Value[]
            {
                columns[0].Type.Parse("123"),
                columns[1].Type.Parse("123.45"),
                columns[2].Type.Parse("B"),
                columns[3].Type.Parse("ery5y4"),
                columns[4].Type.Parse("1 2 3"),
                columns[5].Type.Parse("10 30 50")
            };
            Models.Values.Value[] row3 = new Models.Values.Value[]
            {
                columns[0].Type.Parse("123"),
                columns[1].Type.Parse("123.45"),
                columns[2].Type.Parse("A"),
                columns[3].Type.Parse("ery5y44"),
                columns[4].Type.Parse("1 2 3"),
                columns[5].Type.Parse("10 30 50")
            };
            Models.Values.Value[] row4 = new Models.Values.Value[]
            {
                columns[0].Type.Parse("123"),
                columns[1].Type.Parse("123.45"),
                columns[2].Type.Parse("A"),
                columns[3].Type.Parse("ery5y4"),
                columns[4].Type.Parse("4 5 6"),
                columns[5].Type.Parse("10 30 50")
            };
            Models.Values.Value[] row5 = new Models.Values.Value[]
            {
                columns[0].Type.Parse("123"),
                columns[1].Type.Parse("123.45"),
                columns[2].Type.Parse("A"),
                columns[3].Type.Parse("ery5y4"),
                columns[4].Type.Parse("1 2 3"),
                columns[5].Type.Parse("11 31 51")
            };

            Models.Table table1 = new(0, "table1", columns);
            table1.AddRow(row);

            columns[0] = new("column00", new Models.Types.Integer());

            Models.Table table2 = new(0, "table2", columns);
            table2.AddRow(row0);
            table2.AddRow(row1);
            table2.AddRow(row2);
            table2.AddRow(row3);
            table2.AddRow(row4);
            table2.AddRow(row5);

            Models.Table table3 = new(0, "table2", columns);
            table3.AddRow(row);

            Models.Table table4 = new(0, "table2", columns);

            columns[0] = new("column00", new Models.Types.Real());

            Models.Table table5 = new(0, "table2", columns);

            CheckTableDifference(Models.TableDifference.Create(table1, table2), new Models.Values.Value[][] { row });
            CheckTableDifference(Models.TableDifference.Create(table1, table3), Array.Empty<Models.Values.Value[]>());
            CheckTableDifference(Models.TableDifference.Create(table1, table4), new Models.Values.Value[][] { row });
            Assert.Throws<ArgumentException>(() => Models.TableDifference.Create(table1, table5));
            CheckTableDifference(Models.TableDifference.Create(table2, table1), new Models.Values.Value[][] { row0, row1, row2, row3, row4, row5 });
        }
    }
}
