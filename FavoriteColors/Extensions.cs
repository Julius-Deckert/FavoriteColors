using System;
using FavoriteColors.Domain.Models;
using FavoriteColors.Dtos;

namespace FavoriteColors
{
    public static class Extensions
    {
        public static PersonDto AsDto(this Person person)
        {
            return new PersonDto
            {
                Id = person.Id,
                LastName = person.LastName,
                Name = person.Name,
                ZipCode = person.ZipCode,
                City = person.City,
                Color = person.Color
            };
        }
    }
}
