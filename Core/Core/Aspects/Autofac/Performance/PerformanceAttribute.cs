using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Performance
{
    public class PerformanceAttribute : MethodInterception
    {
        private int _interval;
        private Stopwatch _stopwatch;

        public PerformanceAttribute(int interval=10)
        {
            _interval = interval;
            // Core IoC
            _stopwatch = new Stopwatch();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            _stopwatch.Start();
        }
        protected override void OnAfter(IInvocation invocation)
        {
            if(_stopwatch.Elapsed.TotalSeconds > _interval)
            {
                Debug.WriteLine($"Performans Problemi -> {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name} methodu toplam {_stopwatch.Elapsed.TotalSeconds} saniye sürdü.");
            }
            _stopwatch.Stop();
        }
    }
}
