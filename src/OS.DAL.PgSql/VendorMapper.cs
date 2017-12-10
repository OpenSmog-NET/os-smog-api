using OS.DAL.PgSql.Model;

namespace OS.DAL.PgSql
{
    public class VendorMapper : IEntityMapper<Domain.Vendor, Model.Vendor>
    {
        public Vendor MapToModel(Domain.Vendor @object)
        {
            return new Vendor()
            {
            };
        }

        public Domain.Vendor MapFromModel(Vendor entity)
        {
            throw new System.NotImplementedException();
        }
    }
}