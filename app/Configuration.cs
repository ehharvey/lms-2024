
namespace Lms {

    // A helper class who is instantiated by a static class
    // DO NOT INSTANTIATE THIS CLASS
    // Use the static config given by Global.config
    public readonly struct _Config {
        // Defined configuration values here
        // must be public getters and init only
        // And must set default values only where appropriate

        // PrintConfiguration is read by Program.cs and, when true,
        // Causes the program to print its own configuration on startup
        public bool PrintConfiguration {get; init;} = false;

        // EnabledControllers define which controllers are enabled
        // and usable
        public readonly string[] EnabledControllers {get; init;} = [];

        // Add more config parameters here. For example, add
        // DB credentials
        // URLs (to external dependencies like DBs, APIs, etc.)
        // Feature flags
        // Alternate implementations switches
        
        private _Config(string _) {} // A readonly struct needs a constructor,
                                     // but we use reflection to generate our classes
                                     // So this constructor isn't used

        public static System.Reflection.PropertyInfo GetProperty(string configPropertyName) {
            return typeof(_Config).GetProperty(configPropertyName) ?? 
                throw new ArgumentException("Invalid configPropertyName");
        }

        public static Type GetPropertyType(System.Reflection.PropertyInfo property) {
            return Type.GetType($"System.{property.PropertyType.Name}", true) ?? 
                throw new Exception($"Error getting type for property: {property.Name}");
        }

        public static T ParseConfigValue<T>(string configPropertyName, string configPropertyValueString) {
            var property = GetProperty(configPropertyName);

            var propertyType = GetPropertyType(property);

            var convertResult = Convert.ChangeType(configPropertyValueString, propertyType);
            return (T)convertResult;
        }


        public static _Config CreateConfig(IEnumerable<Tuple<string,string>> configProperties) {
            var result = Activator.CreateInstance(typeof(_Config))?? 
                throw new Exception("Unable to instantiate new _Config.");
            foreach (var propertyNameAndValue in configProperties)
            {
                var prop = GetProperty(propertyNameAndValue.Item1);
                prop.SetValue(result, ParseConfigValue<object>(propertyNameAndValue.Item1, propertyNameAndValue.Item2));
            }
            return (_Config)result;
        }

        public static _Config CreateConfig(string directoryPath = "./config/") {
            directoryPath = Environment.GetEnvironmentVariable("LMS_CONFIG_DIRECTORY")?? directoryPath;
            var directoryContents = Directory.GetFiles(directoryPath)?? throw new Exception($"Error iterating files at {Path.GetFullPath(directoryPath)}");
            var directoryFiles = directoryContents.Where(File.Exists); // Filters based on file

            var configProperties = directoryFiles.Select(
                (dF) => Tuple.Create(Path.GetFileName(dF), File.ReadAllText(dF))
            );

            return CreateConfig(configProperties);
        }
    }

    public static class Global {
        public readonly static _Config config;

        static Global() {
            config = _Config.CreateConfig();
        }
    }
}