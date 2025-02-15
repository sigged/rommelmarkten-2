using Rommelmarkten.Api.Domain.Common;
using System.Security.Cryptography;

namespace Rommelmarkten.Api.Domain.ValueObjects
{
    public class Blob : ValueObject
    {
        public Blob()
        {
        }

        public Blob(string name, string type, byte[] content)
        {
            Name = name;
            Type = type;
            Content = content;
        }

        public string Name { get; set; } = string.Empty;

        private byte[] content = [];
        public byte[] Content
        {
            get => content;
            private set
            {
                content = value;
                if (content != null)
                    ContentHash = BitConverter.ToString(MD5.Create().ComputeHash(content)).Replace("-", "");
                else
                    ContentHash = null;
            }
        }

        public string? ContentHash { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public override string ToString()
        {
            return Name;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Type;
            yield return ContentHash ?? string.Empty;
        }

    }
}
