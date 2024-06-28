using System.Reflection;

namespace BigBrother.Utilities
{
	internal static class AttributesUtilities
	{
		public static IEnumerable<Type> GetAnnotatedClasses<TAttribute>() where TAttribute : Attribute
		{
			var assembly = Assembly.GetExecutingAssembly();
			return assembly.GetTypes().Where(type => type.GetCustomAttribute<TAttribute>() != null);
		}

		public static IEnumerable<Type> GetAnnotatedClasses<TAttribute>(Func<TAttribute, bool> predicate) where TAttribute : Attribute
		{
			return GetAnnotatedClasses<TAttribute>().Where(type => predicate.Invoke(type.GetCustomAttribute<TAttribute>()!));
		}
	}
}
