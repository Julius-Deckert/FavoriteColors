using System;
using FavoriteColors.Domain.Models;

namespace FavoriteColors.Domain.Services.Communication
{
    public class SavePersonResponse : BaseResponse
    {
        public Person Person { get; private set; }

        private SavePersonResponse(bool success, string message, Person person) : base(success, message)
        {
            Person = person;
        }

        /// <summary>
        ///     Creates a success response.
        /// </summary>
        /// <param name="category">Saved category.</param>
        /// <returns>Response.</returns>
        public SavePersonResponse(Person person) : this(true, string.Empty, person){ }

        /// <summary>
        ///     Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public SavePersonResponse(string message) : this(false, message, null){ }
    }
}
