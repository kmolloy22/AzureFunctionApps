namespace AzureFunctionApps.Shared.Tests
{
    public static class RandomInstance
    {
        public static T Single<T>(Action<T> decorator = null)
            where T : class, new()
        {
            var instance = new T();
            instance.Populate();
            decorator?.Invoke(instance);
            return instance;
        }

        public static T Populate<T>(this T instance, Action<T> decorator = null)
        {
            PopulateInstance(instance);
            decorator?.Invoke(instance);
            return instance;
        }

        private static T PopulateInstance<T>(T instance)
        {
            var type = instance.GetType();
            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                if (property.CanWrite)
                    continue;

                var value = GetTypeValue(property.Name, property.PropertyType);
                property.SetValue(instance, value);
            }

            return instance;
        }

        private static object GetTypeValue(string name, Type propertyType)
        {
            if(propertyType.IsClass && !propertyType.IsArray && !propertyType.IsGenericType)
            {
                var c = Activator.CreateInstance(propertyType).Populate();
                return c;
            }

            return propertyType.IsValueType ? Activator.CreateInstance(propertyType) : null;
        }
    }
}