using System.Collections.Generic;
using DotNetify;

namespace MultiUserWebApp
{
   /// <summary>
   /// This view model provides data for the HTML view and represents a particular user view of the bunny pen.
   /// It talks to the singleton 'Pen' instance which keeps records on how many active users in the pen and
   /// raises events when other users are moving their bunnies or departing.
   /// </summary>
   public class BunnyPenVM : BaseVM
   {
      public Pen.Bunny MyBunny
      {
         get { return _myBunny; }
      }

      public float MyBunnyTint
      {
         get { return _myBunny.Tint; }
         set
         {
            _myBunny.Tint = value;
            Pen.Singleton.MarkAsChanged(_myBunny);
         }
      }

      public List<Pen.Bunny> OtherBunnies
      {
         get { return _otherBunnies; }
      }

      public List<long> DepartedBunnyIds
      {
         get { return _departedBunnyIds; }
      }

      public Pen.Location MoveTo
      {
         get { return null; }
         set
         {
            _myBunny.Whereabout = value;
            Pen.Singleton.MarkAsChanged(_myBunny);
         }
      }

      private Pen.Bunny _myBunny;
      private List<Pen.Bunny> _otherBunnies;
      private List<long> _departedBunnyIds = new List<long>();

      public BunnyPenVM()
      {
         _myBunny = Pen.Singleton.Add(out _otherBunnies);

         Pen.Singleton.ChangedBunnies += Pen_ChangedBunnies;
         Pen.Singleton.DepartedBunnies += Pen_DepartedBunnies;
      }

      public override void Dispose()
      {
         Pen.Singleton.ChangedBunnies -= Pen_ChangedBunnies;
         Pen.Singleton.DepartedBunnies -= Pen_DepartedBunnies;
         Pen.Singleton.Remove(_myBunny);
         base.Dispose();
      }

      private void Pen_ChangedBunnies(object sender, Pen.ChangedBunniesEventArgs e)
      {
         _otherBunnies.Clear();
         _otherBunnies.AddRange(e.Bunnies);
         _otherBunnies.Remove(_myBunny);
         if (_otherBunnies.Count > 0)
         {
            Changed(() => OtherBunnies);
            PushUpdates();
         }
      }

      private void Pen_DepartedBunnies(object sender, Pen.DepartedBunniesEventArgs e)
      {
         _departedBunnyIds.Clear();
         _departedBunnyIds.AddRange(e.BunnyIds);
         if (_departedBunnyIds.Count > 0)
         {
            Changed(() => DepartedBunnyIds);
            PushUpdates();
         }
      }
   }
}