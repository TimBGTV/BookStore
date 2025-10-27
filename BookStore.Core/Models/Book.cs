using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Models
{
    public class Book
    {
        public const int maxTitleLenght = 250;
        private Book(Guid id, string title, string description, decimal price) 
        {
            Id = id;
            Title = title;
            Description = description;
            Price = price;
        }

        // Так как это доменная модель, мы не должны извне взаимодействовать с полями, так что сеттеров тут нет.
        public Guid Id { get; }
        public string Title { get; } = string.Empty;
        public string Description { get; } = string.Empty;
        public decimal Price { get; }

        public static (Book Book, string Error) Create(Guid id, string title, string description, decimal price)
        {
            var error = string.Empty;

            // Валидация для заголовка книги. В дальнейшем можно добавить валидацию для поля price.
            if (string.IsNullOrWhiteSpace(title) || title.Length > maxTitleLenght)
                error = "Title can not be empty or longer than 250 symbols!";

            var book = new Book(id, title, description, price);

            return (book, error);
        }
    }
}
