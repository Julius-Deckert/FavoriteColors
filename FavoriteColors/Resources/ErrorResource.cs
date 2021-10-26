using System.Collections.Generic;

namespace FavoriteColors.Resources
{
    public class ErrorResource
    {
        private List<string> Messages { get; }

        public ErrorResource(List<string> messages)
        {
            Messages = messages ?? new List<string>();
        }

        public ErrorResource(string message)
        {
            Messages = new List<string>();

            if (!string.IsNullOrWhiteSpace(message))
            {
                Messages.Add(message);
            }
        }
    }
}
