using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagementSystem
{
    // ==========================================
    // Клас Program (Вхідна точка)
     ==========================================
    class Program
    {
        static void Main(string[] args)
        {
            // ПІБ: Борушевський Роман
            // Група: ПД-25

            Console.WriteLine("Library Management System Started...");

            // Приклад використання (для перевірки)
            LibraryManager manager = new LibraryManager();

            manager.AddItem(new Book("The C# Programming Language", 2010, "Anders Hejlsberg"));
            manager.AddItem(new Magazine("MSDN Magazine", 2019, 11));

            foreach (var item in manager.GetAllItems())
            {
                Console.WriteLine(item.GetDisplayInfo());
            }

            Console.ReadLine();
        }
    }

    // ==========================================
    // 1. Інтерфейс ILibraryItem
    // ==========================================
    public interface ILibraryItem
    {
        int Id { get; }
        string Title { get; }
        int Year { get; }
        string GetDisplayInfo();
    }

    // ==========================================
    // 2. Абстрактний клас LibraryItemBase
    // ==========================================
    public abstract class LibraryItemBase : ILibraryItem
    {
        private static int _nextId = 1;

        // Властивість Id тільки для читання (без сетера), ініціалізується в конструкторі
        public int Id { get; }
        public string Title { get; set; }
        public int Year { get; set; }

        protected LibraryItemBase(string title, int year)
        {
            Id = _nextId++; // Автоматичний інкремент
            Title = title;
            Year = year;
        }

        public abstract string GetItemType();

        public virtual string GetDisplayInfo()
        {
            // Формат: "[Type] ID: [Id], Title: [Title], Year: [Year]"
            // Приклад: Book ID: 4, Title: "C#", Year: 2025
            return $"{GetItemType()} ID: {Id}, Title: \"{Title}\", Year: {Year}";
        }
    }

    // ==========================================
    // 3. Клас Book
    // ==========================================
    public class Book : LibraryItemBase
    {
        public string Author { get; set; }

        public Book(string title, int year, string author) : base(title, year)
        {
            Author = author;
        }

        public override string GetItemType()
        {
            return "Book";
        }

        public override string GetDisplayInfo()
        {
            // Додаємо інформацію про автора до базового виводу
            return $"{base.GetDisplayInfo()}, Author: {Author}";
        }
    }

    // ==========================================
    // 4. Клас Magazine
    // ==========================================
    public class Magazine : LibraryItemBase
    {
        public int IssueNumber { get; set; }

        public Magazine(string title, int year, int issueNumber) : base(title, year)
        {
            IssueNumber = issueNumber;
        }

        public override string GetItemType()
        {
            return "Magazine";
        }

        public override string GetDisplayInfo()
        {
            // Додаємо інформацію про номер випуску до базового виводу
            return $"{base.GetDisplayInfo()}, Issue Number: {IssueNumber}";
        }
    }

    // ==========================================
    // 5. Generic клас LibraryCatalog<T>
    // ==========================================
    public class LibraryCatalog<T> where T : ILibraryItem
    {
        private List<T> _items = new List<T>();

        public void AddItem(T item)
        {
            _items.Add(item);
        }

        public List<T> GetAllItems()
        {
            // Повертаємо копію списку або сам список (за завданням достатньо повернути список)
            return new List<T>(_items);
        }

        public T GetItemById(int id)
        {
            // Повертає елемент або default(T) (null для reference types), якщо не знайдено
            return _items.FirstOrDefault(item => item.Id == id);
        }
    }

    // ==========================================
    // 6. Клас LibraryManager
    // ==========================================
    public class LibraryManager
    {
        private LibraryCatalog<Book> _bookCatalog;
        private LibraryCatalog<Magazine> _magazineCatalog;

        public LibraryManager()
        {
            _bookCatalog = new LibraryCatalog<Book>();
            _magazineCatalog = new LibraryCatalog<Magazine>();
        }

        public void AddItem(ILibraryItem item)
        {
            if (item is Book book)
            {
                _bookCatalog.AddItem(book);
            }
            else if (item is Magazine magazine)
            {
                _magazineCatalog.AddItem(magazine);
            }
            // Ігноруємо об'єкти, які не є ані Book, ані Magazine
        }

        public List<ILibraryItem> GetAllItems()
        {
            List<ILibraryItem> allItems = new List<ILibraryItem>();

            // Додаємо книги
            foreach (var book in _bookCatalog.GetAllItems())
            {
                allItems.Add(book);
            }

            // Додаємо журнали
            foreach (var magazine in _magazineCatalog.GetAllItems())
            {
                allItems.Add(magazine);
            }

            return allItems;
        }

        public ILibraryItem? GetItemById(int id)
        {
            // Спочатку шукаємо в каталозі книг
            var book = _bookCatalog.GetItemById(id);
            if (book != null) return book;

            // Якщо не знайшли, шукаємо в каталозі журналів
            var magazine = _magazineCatalog.GetItemById(id);
            if (magazine != null) return magazine;

            // Якщо ніде не знайшли
            return null;
        }
    }
}