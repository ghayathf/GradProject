using System.Data.Common;

namespace TheNeqatcomApp.Core.Common
{
    public interface IDBContext
    {
        DbConnection Connection { get; }
    }
}
