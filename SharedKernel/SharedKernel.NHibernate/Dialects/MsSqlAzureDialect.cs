using NHibernate.Dialect;

namespace SharedKernel.NHibernate.Dialects
{
    public class MsSqlAzureDialect : MsSql2008Dialect
    {
        public override string PrimaryKeyString => "primary key CLUSTERED";
    }
}
