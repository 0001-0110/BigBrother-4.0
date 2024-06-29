using System.Reflection;

namespace BigBrother.Extensions
{
	internal static class TypeExtensions
	{
		public static bool HasInterface(this Type type, Type @interface)
		{
			if (!@interface.IsInterface)
				throw new ArgumentException($"{@interface} is not an interface");
			
			// TODO This is not a pretty code
			TypeFilter filter = (typeObj, criteriaObj) => typeObj.ToString() == criteriaObj.ToString();
			return type.FindInterfaces(filter, @interface).Length > 0;
		}
	}
}
