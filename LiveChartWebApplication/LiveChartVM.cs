using System;
using System.Timers;
using DotNetify;

namespace LiveChartWebApplication
{
   /// <summary>
   /// This examples demonstrates real time push notification that updates a chart on the browser every second.
   /// </summary>
   public class LiveChartVM : BaseVM
   {
      private Timer _timer = new Timer(1000);
      private Random _random = new Random();

      public double[] Data
      {
         get { return Get<double[]>(); }
         set { Set(value); }
      }

      /// <summary>
      /// Constructor.
      /// </summary>
      public LiveChartVM()
      {
         // Create initial data for the chart.
         Data = new double[20];
         for (int i = 0; i < 20; i++)
            Data[i] = _random.Next(1, 100);

         // Run a timer every second to update the chart.
         _timer.Elapsed += Timer_Elapsed;
         _timer.Start();
      }

      public override void Dispose()
      {
         _timer.Stop();
         _timer.Elapsed -= Timer_Elapsed;

         // Call base.Dispose to raise Disposed event.
         base.Dispose();
      }

      private void Timer_Elapsed(object sender, ElapsedEventArgs e)
      {
         Data = new double[] { _random.Next(1, 100) };

         // This is a base method to cause changed properties from all active view models to be pushed to the browser.
         PushUpdates();
      }
   }
}
