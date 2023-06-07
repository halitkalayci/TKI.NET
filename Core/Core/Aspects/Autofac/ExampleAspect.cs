using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac
{
    public class ExampleAspect : MethodInterception
    {
        protected override void OnBefore(IInvocation invocation)
        {
            Debug.WriteLine("OnBefore çalıştı.");
        }

        protected override void OnAfter(IInvocation invocation)
        {
            Debug.WriteLine("OnAfter çalıştı..");
        }
    }
}
