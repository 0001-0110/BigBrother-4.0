using System.Reflection;

namespace UtilityMinistry.Extensions
{
    public static class TypeExtensions
    {
        public static bool HasInterface(this Type type, Type @interface)
        {
            if (!@interface.IsInterface)
                throw new ArgumentException($"{@interface} is not an interface");

            // TODO This is not a pretty code
            static bool filter(Type typeObj, object? criteriaObj) => typeObj.ToString() == criteriaObj.ToString();
            return type.FindInterfaces(filter, @interface).Length > 0;
        }

        public static IEnumerable<Type> GetImplementations(this Type @interface, Assembly? assembly = null)
        {
            if (!@interface.IsInterface)
                throw new ArgumentException("Can't get implementations of non interface types");

            return (assembly ?? Assembly.GetCallingAssembly()).GetTypes().Where(type => type.HasInterface(@interface) && !type.IsAbstract);
        }

        public static IEnumerable<FieldInfo> GetFields(this Type type, Type propertyType)
        {
            return type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(field => field.FieldType.IsAssignableTo(propertyType));
        }
    }
}
