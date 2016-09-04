using System;
using System.Reflection;
using System.Linq;

namespace DataAccess
{
   public static class SimpleMapper
   {
      public static TDomain Map<TEntity, TDomain>(TEntity entity)
      {
         var bindingFlags = BindingFlags.Instance | BindingFlags.Public;
         var domainModel = Activator.CreateInstance<TDomain>();
         var domainProps = typeof(TDomain).GetProperties(bindingFlags).Where(i => i.SetMethod != null);

         foreach (var prop in typeof(TEntity).GetProperties(bindingFlags).Where(i => i.GetGetMethod() != null))
         {
            var domainProp = domainProps.FirstOrDefault(i => i.Name == prop.Name && i.PropertyType == prop.PropertyType);
            if (domainProp != null)
               domainProp.SetValue(domainModel, prop.GetValue(entity));
         }
         return domainModel;
      }
   }
}
