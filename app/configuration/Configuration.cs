
using Newtonsoft.Json;

namespace Lms {

    // A helper class who is instantiated by a static class
    // DO NOT INSTANTIATE THIS CLASS
    // Use the static config given by Global.config
    public readonly struct _Config {
        // Defined configuration values here
        // must be public getters and init only
        // And must set default values only where appropriate
        public required string Version {get; init;} = "v0";
        public bool PrintConfiguration {get; init;} = false;
        public _Config() {}
    }

    public static class Global {
        public readonly static _Config config;

        static Global() {
            string configPath = Environment.GetEnvironmentVariable("LMS_CONFIG_PATH")?? "./config.json";
            var configJson = File.ReadAllText(configPath);

            config = JsonConvert.DeserializeObject<_Config>(configJson);
        }
    }
}