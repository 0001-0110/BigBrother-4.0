﻿namespace UtilityMinistry.Extensions
{
    public static class CollectionExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T item in enumerable)
                action.Invoke(item);
        }

        public static void Add<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            foreach (T item in items)
                collection.Add(item);
        }

        public static T? GetRandom<T>(this IEnumerable<T> enumerable)
        {
            // If the collection is empty, return default value
            return !enumerable.Any() ? default : enumerable.ElementAt(new Random().Next(enumerable.Count()));
        }
    }
}
