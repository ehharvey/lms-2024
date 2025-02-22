
using System.Collections;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Lms.Views {
    public class SimpleStdout : IView
    {
        ISerializer serializer;

        public SimpleStdout() {
            serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
        }

        // Stringify a single object, suitable for stdout
        public string Stringify(object o)
        {
            return serializer.Serialize(o);
        }
    }
}