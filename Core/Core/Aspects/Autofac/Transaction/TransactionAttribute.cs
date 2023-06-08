using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Core.Aspects.Autofac.Transaction
{
    public class TransactionAttribute : MethodInterception
    {
        public override void Intercept(IInvocation invocation)
        {
            using(TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    // burda yapılmaya çalışılanlar.
                    invocation.Proceed();
                    transactionScope.Complete();
                }
                catch (Exception e)
                {
                    transactionScope.Dispose();
                    throw;
                    // buraya geldiğinde iptal edilecek
                }
            }

        }
    }
}
