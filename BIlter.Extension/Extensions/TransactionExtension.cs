using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIlter.Extension.Extensions
{
    /// <summary>
    /// Revit database taransaction extensions
    /// </summary>
    public static class TransactionExtension
    {
        public static void NewTransaction(this Document document, string name, Action action)
        {
            using (Transaction ts = new Transaction(document, name))
            {
                ts.Start();
                action?.Invoke();
                ts.Commit();
            }
        }
        public static TransactionStatus NewTransactionGroup(this Document document, string name, Func<bool> func)
        {
            TransactionStatus status = TransactionStatus.Uninitialized;
            using (TransactionGroup ts = new TransactionGroup(document, name))
            {
                ts.Start();
                bool result = func.Invoke();
                status = result ? ts.Assimilate() : ts.RollBack();
            }
            return status;
        }
    }
}