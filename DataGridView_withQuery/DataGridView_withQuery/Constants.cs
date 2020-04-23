namespace DataGridView_withQuery
{
    public static class Constants
    {
        public const string msgWarning = "Warning";
        public const string msgAttention = "Attention";
        public const string msgError = "Error";

        //value types
        public static readonly string[] types_Bool = { "Boolean" };
        public static readonly string[] types_Numeric = { "Int", "Double", "Byte", "Single", "Decimal" };
        public static readonly string[] types_DateTime = { "Date" };
        public static readonly string[] types_String = { "String", "Varchar" };


        // value types of the grid columns
        public const string ValueType_Bool = "Yes/No";
        public const string ValueType_Date = "Date";
        public const string ValueType_Numeric = "Numeric";
        public const string ValueType_String = "String";
        public static readonly string[] List_of_ValueTypes = { ValueType_Bool, ValueType_Date, ValueType_Numeric, ValueType_String };

        //conditions for search
        public const string operator_Eq = "=";
        public const string operator_NotEq = "<>";
        public const string operator_GEq = ">=";
        public const string operator_LEq = "<=";
        public const string operator_Like = "Is Like";
        public const string operator_Between = "Is between";
        public const string operator_Edit_Distance = "Within edit distance";
        public static readonly string[] List_of_Operators = { operator_Eq, operator_NotEq, operator_GEq, operator_LEq, operator_Like, operator_Between, operator_Edit_Distance };

        //search conjunctions
        public const string ConjAnd = "AND";
        public const string ConjOr = "OR";
        public const string ConjNot = "NOT";
        public const string ConjDeafult = "IF";
        public static readonly string[] List_of_Conj = { ConjAnd, ConjOr, ConjNot, ConjDeafult };

        //boolean values
        public const string ValueBool_True = "Yes";
        public const string ValueBool_False = "No";
    }
}
