namespace StoreAutomatization.Data;

using StoreAutomatization.Models;
using StoreAutomatization.Repositories;
public class MockData
{
    public static void Initialize(IStoreRepository repository)
    {
        // ------------------ КАТЕГОРІЇ (Categories) ------------------
        repository.AddCategory(new Category { id = 1, name = "Фрукти та ягоди" });
        repository.AddCategory(new Category { id = 2, name = "Овочі та зелень" });
        repository.AddCategory(new Category { id = 3, name = "Гриби" });
        repository.AddCategory(new Category { id = 4, name = "Готова кулінарія" });
        repository.AddCategory(new Category { id = 5, name = "М'ясо" });
        repository.AddCategory(new Category { id = 6, name = "Птиця" });
        repository.AddCategory(new Category { id = 7, name = "М'ясні напівфабрикати" });
        repository.AddCategory(new Category { id = 8, name = "Ковбасні вироби та копченості" });
        repository.AddCategory(new Category { id = 9, name = "Риба та морепродукти" });
        repository.AddCategory(new Category { id = 10, name = "Молоко та вершки" });
        repository.AddCategory(new Category { id = 11, name = "Кисломолочні продукти" });
        repository.AddCategory(new Category { id = 12, name = "Масло та маргарин" });
        repository.AddCategory(new Category { id = 13, name = "Сири та сирні продукти" });
        repository.AddCategory(new Category { id = 14, name = "Яйця" });
        repository.AddCategory(new Category { id = 15, name = "Хліб" });
        repository.AddCategory(new Category { id = 16, name = "Здобна випічка" });
        repository.AddCategory(new Category { id = 17, name = "Кондитерські вироби" });
        repository.AddCategory(new Category { id = 18, name = "Солодощі та снеки" });
        repository.AddCategory(new Category { id = 19, name = "Крупи та бобові" });
        repository.AddCategory(new Category { id = 20, name = "Макаронні вироби" });
        repository.AddCategory(new Category { id = 21, name = "Борошно та інгредієнти для випічки" });
        repository.AddCategory(new Category { id = 22, name = "Рослинна олія, соуси та спеції" });
        repository.AddCategory(new Category { id = 23, name = "Консервація" });
        repository.AddCategory(new Category { id = 24, name = "Сухі сніданки" });
        repository.AddCategory(new Category { id = 25, name = "Підсолоджувачі" });
        repository.AddCategory(new Category { id = 26, name = "Кава" });
        repository.AddCategory(new Category { id = 27, name = "Чай" });
        repository.AddCategory(new Category { id = 28, name = "Какао та гарячий шоколад" });
        repository.AddCategory(new Category { id = 29, name = "Вода" });
        repository.AddCategory(new Category { id = 30, name = "Безалкогольні напої" });
        repository.AddCategory(new Category { id = 31, name = "Алкогольні напої" });
        repository.AddCategory(new Category { id = 32, name = "Напівфабрикати" });
        repository.AddCategory(new Category { id = 33, name = "Заморожені продукти" });
        repository.AddCategory(new Category { id = 34, name = "Морозиво та лід" });

        // ------------------ ТОВАРИ ТА ІНФОРМАЦІЯ (Products and ProductInfo) ------------------
        var product1 = new Product { id = 1, category_id = 1, name = "Яблука Голден", count = 150, price = 38.90m };
        product1.SetInfo("100% яблука", 52, "Від +2°C до +4°C");
        repository.AddProduct(product1);

        var product2 = new Product { id = 2, category_id = 1, name = "Банани Еквадор", count = 200, price = 64.50m };
        product2.SetInfo("100% банани", 89, "Від +12°C до +14°C");
        repository.AddProduct(product2);

        var product3 = new Product { id = 3, category_id = 1, name = "Лохина (упаковка)", count = 40, price = 149.00m };
        product3.SetInfo("Ягоди лохини свіжі", 39, "Від 0°C до +4°C");
        repository.AddProduct(product3);

        var product4 = new Product { id = 4, category_id = 2, name = "Помідори Черрі", count = 80, price = 89.90m };
        product4.SetInfo("Помідори", 18, "Від +10°C до +15°C");
        repository.AddProduct(product4);

        var product5 = new Product { id = 5, category_id = 2, name = "Огірки Естафета", count = 120, price = 95.00m };
        product5.SetInfo("Огірки", 15, "Від +8°C до +10°C");
        repository.AddProduct(product5);

        var product6 = new Product { id = 6, category_id = 3, name = "Печериці свіжі", count = 50, price = 84.00m };
        product6.SetInfo("Гриби печериці", 22, "Від +2°C до +4°C");
        repository.AddProduct(product6);

        var product7 = new Product { id = 7, category_id = 5, name = "Свинина (вирізка)", count = 25, price = 249.00m };
        product7.SetInfo("Свинина", 12, "Від +2°C до +2°C");
        repository.AddProduct(product7);

        var product8 = new Product { id = 8, category_id = 6, name = "Філе куряче Наша Ряба", count = 60, price = 179.90m };
        product8.SetInfo("М'ясо курчати-бройлера", 113, "Від 0°C до +2°C");
        repository.AddProduct(product8);

        var product9 = new Product { id = 9, category_id = 8, name = "Ковбаса Лікарська", count = 35, price = 165.00m };
        product9.SetInfo("Свинина, яловичина, яйця, молоко, спеції", 257, "Від 0°C до +6°C");
        repository.AddProduct(product9);

        var product10 = new Product { id = 10, category_id = 9, name = "Стейк лосося охолоджений", count = 15, price = 420.00m };
        product10.SetInfo("Сьомга атлантична", 142, "Від 0°C до +2°C");
        repository.AddProduct(product10);

        var product11 = new Product { id = 11, category_id = 10, name = "Молоко Яготинське 2.6%", count = 100, price = 46.50m };
        product11.SetInfo("Молоко коров'яче незбиране, молоко знежирене", 53, "Від +2°C до +6°C");
        repository.AddProduct(product11);

        var product12 = new Product { id = 12, category_id = 11, name = "Кефір Галичина 1%", count = 45, price = 42.90m };
        product12.SetInfo("Молоко коров'яче, закваска на кефірних грибках", 36, "Від +2°C до +6°C");
        repository.AddProduct(product12);

        var product13 = new Product { id = 13, category_id = 12, name = "Масло Селянське 73%", count = 80, price = 88.50m };
        product13.SetInfo("Вершки з коров'ячого молока", 661, "Від -5°C до +5°C");
        repository.AddProduct(product13);

        var product14 = new Product { id = 14, category_id = 13, name = "Сир Твердий Комо Канторі", count = 30, price = 115.00m };
        product14.SetInfo("Молоко коров'яче, сіль, закваска, фермент", 345, "Від -4°C до +6°C");
        repository.AddProduct(product14);

        var product15 = new Product { id = 15, category_id = 14, name = "Яйця курячі Ясенсвіт 10 шт", count = 200, price = 72.00m };
        product15.SetInfo("Яйця курячі харчові", 157, "Від 0°C до +20°C");
        repository.AddProduct(product15);

        var product16 = new Product { id = 16, category_id = 15, name = "Хліб Бородінський нарізаний", count = 40, price = 34.50m };
        product16.SetInfo("Борошно житнє, вода, солод, сіль, коріандр", 208, "При температурі не нижче +6°C");
        repository.AddProduct(product16);

        var product17 = new Product { id = 17, category_id = 15, name = "Батон Київський", count = 55, price = 28.90m };
        product17.SetInfo("Борошно пшеничне, вода, дріжджі, цукор, сіль", 262, "При температурі не нижче +6°C");
        repository.AddProduct(product17);

        var product18 = new Product { id = 18, category_id = 17, name = "Торт Празький ROSHEN", count = 10, price = 485.00m };
        product18.SetInfo("Цукор, масло, згущене молоко, какао, яйця", 412, "Від +2°C до +6°C");
        repository.AddProduct(product18);

        var product19 = new Product { id = 19, category_id = 19, name = "Гречка Сквірянка 1 кг", count = 120, price = 36.90m };
        product19.SetInfo("Крупа гречана ядриця", 335, "У сухому прохолодному місці");
        repository.AddProduct(product19);

        var product20 = new Product { id = 20, category_id = 20, name = "Спагетті Barilla n.5", count = 90, price = 68.00m };
        product20.SetInfo("Борошно з твердих сортів пшениці, вода", 359, "У сухому місці");
        repository.AddProduct(product20);

        var product21 = new Product { id = 21, category_id = 22, name = "Олія соняшникова Олейна 850 мл", count = 150, price = 69.50m };
        product21.SetInfo("Олія соняшникова рафінована", 899, "У захищеному від світла місці");
        repository.AddProduct(product21);

        var product22 = new Product { id = 22, category_id = 26, name = "Кава в зернах Lavazza Oro 1 кг", count = 40, price = 780.00m };
        product22.SetInfo("100% Арабіка", 2, "У сухому прохолодному місці");
        repository.AddProduct(product22);

        var product23 = new Product { id = 23, category_id = 27, name = "Чай чорний Lipton Yellow Label", count = 85, price = 74.00m };
        product23.SetInfo("Чай чорний байховий", 1, "У сухому місці без сторонніх запахів");
        repository.AddProduct(product23);

        var product24 = new Product { id = 24, category_id = 29, name = "Вода Моршинська слабогазована 1.5л", count = 300, price = 24.50m };
        product24.SetInfo("Вода мінеральна природна", 0, "Від +5°C до +25°C");
        repository.AddProduct(product24);

        var product25 = new Product { id = 25, category_id = 30, name = "Coca-Cola 2л", count = 180, price = 56.90m };
        product25.SetInfo("Вода, цукор, діоксид вуглецю, барвник, кофеїн", 42, "Від 0°C до +20°C");
        repository.AddProduct(product25);

        var product26 = new Product { id = 26, category_id = 33, name = "Пельмені Три Ведмеді Фірмові 800г", count = 65, price = 159.00m };
        product26.SetInfo("Борошно, м'ясо (свинина, яловичина), цибуля, сіль", 245, "При температурі -18°C");
        repository.AddProduct(product26);

        var product27 = new Product { id = 27, category_id = 34, name = "Морозиво Хрещатик Пломбір", count = 110, price = 38.00m };
        product27.SetInfo("Молоко, вершкове масло, цукор, ванілін", 227, "При температурі -18°C");
        repository.AddProduct(product27);

        // ------------------ ЧЕКИ ТА АЙТЕМИ (Receipts and ReceiptItems) ------------------
        var receipt1 = new Receipt { id = 1, time = DateTime.Now.AddDays(-2).AddHours(10), customer_name = "Олександр Петренко", shop_name = "Сільpo" };
        receipt1.AddItem(product1.id, 2, product1.price); // Яблука (2 кг)
        receipt1.AddItem(product4.id, 1, product4.price); // Помідори Черрі (1 кг)
        receipt1.AddItem(product8.id, 1, product8.price); // Куряче філе (1 кг)
        receipt1.Pay();
        repository.AddReceipt(receipt1);

        var receipt2 = new Receipt { id = 2, time = DateTime.Now.AddDays(-1).AddHours(15), customer_name = "Ірина Ковальчук", shop_name = "АТБ" };
        receipt2.AddItem(product11.id, 2, product11.price); // Молоко (2 шт)
        receipt2.AddItem(product13.id, 1, product13.price); // Масло (1 шт)
        receipt2.AddItem(product17.id, 1, product17.price); // Батон (1 шт)
        receipt2.AddItem(product9.id, 1, product9.price);  // Ковбаса Лікарська (1 шт)
        receipt2.Pay();
        repository.AddReceipt(receipt2);

        var receipt3 = new Receipt { id = 3, time = DateTime.Now.AddHours(-3), customer_name = "Дмитро Мельник", shop_name = "Novus" };
        receipt3.AddItem(product24.id, 6, product24.price); // Моршинська (6 шт)
        receipt3.AddItem(product25.id, 2, product25.price); // Coca-Cola (2 шт)
        receipt3.AddItem(product27.id, 3, product27.price); // Морозиво (3 шт)
        receipt3.Pay();
        repository.AddReceipt(receipt3);

        var receipt4 = new Receipt { id = 4, time = DateTime.Now.AddDays(-5).AddHours(18), customer_name = "Ольга Бойко", shop_name = "Ашан" };
        receipt4.AddItem(product15.id, 1, product15.price); // Яйця (1 уп)
        receipt4.AddItem(product19.id, 2, product19.price); // Гречка (2 кг)
        receipt4.AddItem(product20.id, 3, product20.price); // Спагетті (3 уп)
        receipt4.AddItem(product21.id, 1, product21.price); // Олія (1 шт)
        receipt4.AddItem(product14.id, 1, product14.price); // Сир Комо (1 шт)
        receipt4.Pay();
        repository.AddReceipt(receipt4);

        var receipt5 = new Receipt { id = 5, time = DateTime.Now.AddDays(-1).AddHours(9), customer_name = "Максим Шевченко", shop_name = "Сільpo" };
        receipt5.AddItem(product22.id, 1, product22.price); // Кава Lavazza (1 уп)
        receipt5.AddItem(product18.id, 1, product18.price); // Торт Празький (1 шт)
        receipt5.AddItem(product23.id, 1, product23.price); // Чай Lipton (1 уп)
        receipt5.Pay();
        repository.AddReceipt(receipt5);

        var receipt6 = new Receipt { id = 6, time = DateTime.Now.AddHours(-1), customer_name = "Анна Ткаченко", shop_name = "МегаМаркет" };
        receipt6.AddItem(product26.id, 2, product26.price); // Пельмені (2 уп)
        receipt6.AddItem(product12.id, 1, product12.price); // Кефір (1 шт)
        receipt6.Pay();
        repository.AddReceipt(receipt6);

        var receipt7 = new Receipt { id = 7, time = DateTime.Now.AddDays(-10), customer_name = "Віктор Романенко", shop_name = "АТБ" };
        receipt7.AddItem(product10.id, 2, product10.price); // Стейк лосося (2 шт)
        receipt7.AddItem(product3.id, 1, product3.price);  // Лохина (1 уп)
        receipt7.AddItem(product5.id, 1, product5.price);  // Огірки (1 шт)
        receipt7.AddItem(product6.id, 1, product6.price);  // Печериці (1 шт)
        receipt7.Pay();
        repository.AddReceipt(receipt7);
    }
}