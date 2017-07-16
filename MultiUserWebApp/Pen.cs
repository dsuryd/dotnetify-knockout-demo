using System;
using System.Collections.Generic;
using System.Threading;
using winTimer = System.Timers;

namespace MultiUserWebApp
{
   /// <summary>
   /// This is a singleton to keep the records on all active users on the bunny pen and raises events
   /// when those users move their bunnies or are departing the pen (by closing their browser).
   /// </summary>
   public class Pen : IDisposable
   {
      public class Bunny
      {
         public long Id { get; set; }
         public float Tint { get; set; }
         public Location Whereabout { get; set; }
      }

      public class Location
      {
         public float X { get; set; }
         public float Y { get; set; }
         public float Angle { get; set; }
      }

      public class ChangedBunniesEventArgs : EventArgs { public List<Bunny> Bunnies { get; set; } }
      public class DepartedBunniesEventArgs : EventArgs { public List<long> BunnyIds { get; set; } }

      /// <summary>
      /// Occurs when the state of a bunny changed, e.g. position, tint.
      /// </summary>
      public event EventHandler<ChangedBunniesEventArgs> ChangedBunnies;

      /// <summary>
      /// Occurs when a bunny leaves then pen.
      /// </summary>
      public event EventHandler<DepartedBunniesEventArgs> DepartedBunnies;

      public static Pen Singleton
      {
         get
         {
            lock (_singletonSync)
            {
               if (_singleton == null)
                  _singleton = new Pen();
            }
            return _singleton;
         }
      }

      #region Private Fields

      private const int PEN_WIDTH = 500;
      private const int PEN_HEIGHT = 400;

      // This constant sets the milliseconds rate interval to raise events to control the performance.
      private const int UPDATE_RATE = 100;

      private static Pen _singleton;
      private static object _singletonSync = new object();

      private long _bunnySequence;
      private long _bunnyCount;
      private object _clientUpdateLock = new object();
      private object _serverUpdateLock = new object();
      private List<Bunny> _allBunnies = new List<Bunny>();
      private List<Bunny> _changedBunnies = new List<Bunny>();
      private List<long> _departedBunnyIds = new List<long>();
      private ChangedBunniesEventArgs _changedEventArgs = new ChangedBunniesEventArgs();
      private DepartedBunniesEventArgs _departedEventArgs = new DepartedBunniesEventArgs();
      private winTimer.Timer _updateTimer = new winTimer.Timer(UPDATE_RATE);
      private Random _random = new Random();

      #endregion

      protected Pen()
      {
         _changedEventArgs.Bunnies = new List<Bunny>();
         _departedEventArgs.BunnyIds = new List<long>();

         _updateTimer.Elapsed += UpdateTimer_Elapsed;
         _updateTimer.Start();
      }

      public void Dispose()
      {
         _updateTimer.Stop();
         _updateTimer.Elapsed -= UpdateTimer_Elapsed;
      }

      public Bunny Add(out List<Bunny> oOtherBunnies)
      {
         // Add a new bunny. The pen can be accessed by view models on multiple threads, so it needs to be thread-safe.
         Interlocked.Increment(ref _bunnyCount);
         var id = Interlocked.Increment(ref _bunnySequence);
         var bunny = new Bunny { Id = id, Whereabout = new Location { X = _random.Next(10, PEN_WIDTH - 10), Y = _random.Next(10, PEN_HEIGHT - 10) } };

         lock (_clientUpdateLock)
         {
            oOtherBunnies = new List<Bunny>(_allBunnies);
            _allBunnies.Add(bunny);
            _changedBunnies.Add(bunny);
         }

         return bunny;
      }

      public void Remove(Bunny iBunny)
      {
         lock (_clientUpdateLock)
         {
            _allBunnies.Remove(iBunny);
            _departedBunnyIds.Add(iBunny.Id);
         }
         Interlocked.Decrement(ref _bunnyCount);
      }

      public void MarkAsChanged(Bunny iBunny)
      {
         lock (_clientUpdateLock)
         {
            if (_changedBunnies.IndexOf(iBunny) < 0)
               _changedBunnies.Add(iBunny);
         }
      }

      /// <summary>
      /// This timer regulates the interval of when events are raised to keep performance under control
      /// in case there are many users rapidly moving the bunnies at the same time.
      /// </summary>
      private void UpdateTimer_Elapsed(object sender, winTimer.ElapsedEventArgs e)
      {
         lock (_clientUpdateLock)
         {
            lock (_serverUpdateLock)
            {
               if (_changedBunnies.Count > 0)
                  _changedEventArgs.Bunnies.AddRange(_changedBunnies);

               if (_departedBunnyIds.Count > 0)
                  _departedEventArgs.BunnyIds.AddRange(_departedBunnyIds);
            }

            _changedBunnies.Clear();
            _departedBunnyIds.Clear();
         }

         lock (_serverUpdateLock)
         {
            if (ChangedBunnies != null && _changedEventArgs.Bunnies.Count > 0)
               ChangedBunnies(this, _changedEventArgs);

            if (DepartedBunnies != null && _departedEventArgs.BunnyIds.Count > 0)
               DepartedBunnies(this, _departedEventArgs);

            _changedEventArgs.Bunnies.Clear();
            _departedEventArgs.BunnyIds.Clear();
         }

         // When there are no more users, dispose this singleton.
         if (Interlocked.Read(ref _bunnyCount) == 0)
         {
            lock (_singletonSync)
            {
               Dispose();
               _singleton = null;
            }
         }
      }
   }
}