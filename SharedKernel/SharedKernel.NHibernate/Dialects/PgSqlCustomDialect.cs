using System;
using System.Collections.Generic;
using NHibernate.Dialect;
using NHibernate.Id;
using NHibernate.Type;

namespace SharedKernel.NHibernate.Dialects
{
    public class PgSqlCustomDialect : PostgreSQLDialect
    {
        public override Type NativeIdentifierGeneratorClass => typeof(SequenceByConvention);
    }

    public class SequenceByConvention : SequenceGenerator
    {
        public override void Configure(IType type, IDictionary<string, string> parms, Dialect dialect)
        {
            parms["sequence"] = GetSequenceNameFromTableName(parms["target_table"]);
            base.Configure(type, parms, dialect);
        }

        private static string GetSequenceNameFromTableName(string tableName)
        {
            return tableName.Substring(0, Math.Min(26, tableName.Length)) + "_seq";
        }
    }
}