using System;
using DotNetify;
using System.Linq;
using System.Reactive.Linq;

namespace LiveChartWebApplication
{
   /// <summary>
   /// This examples demonstrates real time push notification that updates a chart on the browser every second.
   /// </summary>
   public class LiveChartVM : BaseVM
   {
      private IDisposable subscription;

      public LiveChartVM()
      {
         var _random = new Random();

         // Create initial data for the chart.
         var initialData = Enumerable.Range(0, 20).Select(_ => _random.Next(1, 100)).ToArray();
         var dataProperty = AddProperty("Data", initialData);

         // Run a timer every second to update the chart.
         subscription = Observable.Interval(TimeSpan.FromSeconds(1)).Subscribe(_ =>
         {
            var newData = new int[] { _random.Next(1, 100) };
            dataProperty.OnNext(newData);

            // This is a base method to cause changed properties from all active view models to be pushed to the browser.
            PushUpdates();
         });
      }

      public override void Dispose()
      {
         subscription?.Dispose();
         base.Dispose();
      }
   }
}
