using System;
using System.Collections.Generic;
using System.Linq;

// Enum для статусів замовлення. Робить код читабельним та типобезпечним.
public enum OrderStatus
{
    New,
    InProgress,
    Ready,
    Paid
}

// === Абстрактний базовий клас для всіх позицій меню ===
// Демонструє АБСТРАКЦІЮ.
public abstract class MenuItem
{
    // ІНКАПСУЛЯЦІЯ: дані захищені, доступ через властивості.
    public string Name { get; set; }
    public decimal Price { get; set; }

    protected MenuItem(string name, decimal price)
    {
        Name = name;
        Price = price;
    }

    // Абстрактний метод, який змушує всі дочірні класи реалізувати свою логіку відображення.
    public abstract void Display();
}

// === Клас для страв, що наслідує MenuItem ===
// Демонструє НАСЛІДУВАННЯ.
public class Dish : MenuItem
{
    public string Category { get; set; }

    public Dish(string name, decimal price, string category) : base(name, price)
    {
        Category = category;
    }

    // ПОЛІМОРФІЗМ: перевизначення методу базового класу.
    public override void Display()
    {
        Console.WriteLine($"{Name} ({Category}) - {Price:C}");
    }
}

// === Клас для напоїв, що наслідує MenuItem ===
public class Drink : MenuItem
{
    public int VolumeMl { get; set; }
    public bool IsAlcoholic { get; set; }

    public Drink(string name, decimal price, int volumeMl, bool isAlcoholic) : base(name, price)
    {
        VolumeMl = volumeMl;
        IsAlcoholic = isAlcoholic;
    }

    public override void Display()
    {
        string alcoholInfo = IsAlcoholic ? "алкогольний" : "безалкогольний";
        Console.WriteLine($"{Name} ({VolumeMl} мл, {alcoholInfo}) - {Price:C}");
    }
}

// === Клас Замовлення ===
public class Order
{
    private static int _nextId = 101; // Статичне поле для генерації унікальних ID

    // ІНКАПСУЛЯЦІЯ
    public int Id { get; private set; }
    public int TableNumber { get; set; }
    public OrderStatus Status { get; set; }

    // КОМПОЗИЦІЯ: Замовлення складається з позицій меню. Список є приватним.
    private readonly List<MenuItem> _items;

    // Властивість тільки для читання, яка повертає копію списку, щоб уникнути змін ззовні.
    public IReadOnlyList<MenuItem> Items => _items.AsReadOnly();

    // Властивість, що автоматично розраховується.
    public decimal TotalPrice => _items.Sum(item => item.Price);

    public Order(int tableNumber)
    {
        Id = _nextId++;
        TableNumber = tableNumber;
        Status = OrderStatus.New;
        _items = new List<MenuItem>(); // Ініціалізація списку
    }

    // Метод для додавання позицій (змінює внутрішній стан)
    public void AddItem(MenuItem item)
    {
        _items.Add(item);
        Console.WriteLine($"Додано позицію: {item.Name}");
    }

    // Метод для видалення позицій
    public void RemoveItem(MenuItem item)
    {
        if (_items.Remove(item))
        {
            Console.WriteLine($"Видалено позицію: {item.Name}");
        }
    }

    public void DisplayDetails()
    {
        Console.WriteLine($"ID: {Id} | Стіл: {TableNumber} | Статус: {Status} | Сума: {TotalPrice:C}");
        Console.WriteLine("  Позиції в замовленні:");
        foreach (var item in _items)
        {
            Console.WriteLine($"  - {item.Name} ({item.Price:C})");
        }
    }
}

// === Клас Ресторан ===
public class Restaurant
{
    // АГРЕГАЦІЯ: Ресторан має список замовлень та меню, але вони є самостійними сутностями.
    private readonly List<MenuItem> _menu;
    private readonly List<Order> _orders;

    public Restaurant()
    {
        _menu = new List<MenuItem>();
        _orders = new List<Order>();
    }

    public void AddMenuItem(MenuItem item)
    {
        _menu.Add(item);
    }

    public void DisplayMenu()
    {
        Console.WriteLine("--- МЕНЮ РЕСТОРАНУ ---");
        foreach (var item in _menu)
        {
            item.Display();
        }
        Console.WriteLine("-----------------------");
    }

    public Order CreateOrder(int tableNumber)
    {
        var order = new Order(tableNumber);
        _orders.Add(order);
        Console.WriteLine($"\nСтворено нове замовлення №{order.Id} для столика №{tableNumber}");
        return order;
    }

    public void DisplayAllOrders()
    {
        Console.WriteLine("\n--- УСІ АКТИВНІ ЗАМОВЛЕННЯ ---");
        if (!_orders.Any())
        {
            Console.WriteLine("Активних замовлень немає.");
            return;
        }

        foreach (var order in _orders)
        {
            Console.WriteLine($"ID: {order.Id} | Стіл: {order.TableNumber} | Статус: {order.Status} | Сума: {order.TotalPrice:C}");
        }
        Console.WriteLine("---------------------------");
    }

    public Order FindOrderById(int id)
    {
        return _orders.FirstOrDefault(o => o.Id == id);
    }

    public List<MenuItem> FindMenuItems(string query)
    {
        // Пошук за назвою або категорією (для страв)
        return _menu.Where(item =>
            item.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
            (item is Dish dish && dish.Category.Contains(query, StringComparison.OrdinalIgnoreCase))
        ).ToList();
    }
}


// === Точка входу в програму ===
public class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        // 1. Налаштування ресторану та меню
        var restaurant = new Restaurant();

        // UPCAST (висхідне перетворення): об'єкти Dish та Drink неявно перетворюються на тип MenuItem
        MenuItem borscht = new Dish("Борщ", 120, "Перші страви");
        MenuItem steak = new Dish("Стейк", 350, "Основні страви");
        MenuItem coffee = new Drink("Кава", 60, 200, false);
        MenuItem juice = new Drink("Сік апельсиновий", 70, 250, false);
        MenuItem wine = new Drink("Вино червоне", 150, 150, true);

        restaurant.AddMenuItem(borscht);
        restaurant.AddMenuItem(steak);
        restaurant.AddMenuItem(coffee);
        restaurant.AddMenuItem(juice);
        restaurant.AddMenuItem(wine);

        // 2. Демонстрація роботи з меню
        restaurant.DisplayMenu();

        // 3. Створення та наповнення першого замовлення
        var orderForTable5 = restaurant.CreateOrder(5);
        orderForTable5.AddItem(borscht);
        orderForTable5.AddItem(juice);
        Console.WriteLine($"Поточна сума замовлення №{orderForTable5.Id}: {orderForTable5.TotalPrice:C}");

        // 4. Демонстрація зміни статусу замовлення
        Console.WriteLine($"\nСтатус замовлення №{orderForTable5.Id}: {orderForTable5.Status}");
        orderForTable5.Status = OrderStatus.InProgress;
        Console.WriteLine($"> Змінено статус на: {orderForTable5.Status}");
        orderForTable5.Status = OrderStatus.Ready;
        Console.WriteLine($"> Змінено статус на: {orderForTable5.Status}");
        orderForTable5.Status = OrderStatus.Paid;
        Console.WriteLine($"> Змінено статус на: {orderForTable5.Status}");

        // 5. Створення другого замовлення
        var orderForTable2 = restaurant.CreateOrder(2);
        orderForTable2.AddItem(steak);
        orderForTable2.AddItem(wine);
        orderForTable2.AddItem(coffee);

        // 6. Перегляд усіх замовлень
        restaurant.DisplayAllOrders();

        // 7. Пошук замовлення за ID
        Console.WriteLine("\n--- ПОШУК ЗАМОВЛЕННЯ ЗА ID ---");
        int idToFind = 102;
        var foundOrder = restaurant.FindOrderById(idToFind);
        if (foundOrder != null)
        {
            Console.WriteLine($"Знайдено замовлення з ID {idToFind}:");
            foundOrder.DisplayDetails();
        }
        else
        {
            Console.WriteLine($"Замовлення з ID {idToFind} не знайдено.");
        }

        // 8. Пошук позицій у меню
        Console.WriteLine("\n--- ПОШУК В МЕНЮ ЗА ЗАПИТОМ 'страви' ---");
        var foundItems = restaurant.FindMenuItems("страви");
        if (foundItems.Any())
        {
            foreach (var item in foundItems)
            {
                item.Display();
            }
        }
        else
        {
            Console.WriteLine("Нічого не знайдено.");
        }

        // 9. Демонстрація приведення типів (Upcast/Downcast)
        Console.WriteLine("\n--- ДЕМОНСТРАЦІЯ DOWNCAST ---");
        Console.WriteLine($"Перевірка позицій у замовленні №{orderForTable2.Id}:");
        // Ми маємо список типу MenuItem, але хочемо отримати доступ до властивостей Drink.
        foreach (var item in orderForTable2.Items)
        {
            // DOWNCAST (низхідне перетворення): перевіряємо, чи є item насправді об'єктом Drink.
            if (item is Drink drink)
            {
                // Якщо так, ми можемо безпечно використовувати властивості класу Drink
                Console.WriteLine($"- Позиція '{drink.Name}' є напоєм об'ємом {drink.VolumeMl} мл.");
            }
            else
            {
                Console.WriteLine($"- Позиція '{item.Name}' не є напоєм.");
            }
        }
    }
}