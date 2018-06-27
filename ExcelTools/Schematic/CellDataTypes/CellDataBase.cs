namespace ExcelTools.Schematic.CellDataTypes
{
    public abstract class CellDataBase<TValue> : CellData
    {
        public TValue Value { get; }

        protected CellDataBase(TValue value)
        {
            Value = value;
        }

        public override string ValueStr => Value == null ? null : Value.ToString();
    }
}
