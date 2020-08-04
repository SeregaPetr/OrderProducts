using OrderProducts.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OrderProducts.DAL
{
    public class OrderProductDbInitializer : DropCreateDatabaseIfModelChanges<OrderProductContext>
    {
        protected override void Seed(OrderProductContext context)
        {
            context.PaymentTypes.AddRange(new List<PaymentType> {
                new PaymentType { NamePaymentType = "Наличными курьеру" },
                new PaymentType { NamePaymentType = "Карточкой при получении товара" }
            });
            context.SaveChanges();

            var menuCategories = new List<MenuCategory>
            {
                 new MenuCategory{NameCategory="ПИЦЦА",Comment="Вкусная пицца в лучших традициях итальянской кухни", Photo="picca-morskaya.jpg"},
                 new MenuCategory{NameCategory="БУРГЕРЫ",Comment="Вкусные бургеры в лучших традициях американской кухни", Photo="burger-dvoynoy.jpg"},
                 new MenuCategory{NameCategory="БЛЮДА НА МАНГАЛЕ",Comment="Нежное, сочное и ароматное мясо покорит не только Ваше сердце!",Photo="myasnoj-set.jpg"},
                 new MenuCategory{NameCategory="САЛАТЫ",Comment="Большой выбор салатов на любой вкус", Photo="teplyj-salat-s-moreproduktami.jpg"},
                 new MenuCategory{NameCategory="ГАРНИРЫ",Comment="Вкуснее не бывает", Photo="kartofel.jpg"},
                 new MenuCategory{NameCategory="ПЕРВЫЕ БЛЮДА",Comment="Наши ароматные и насыщенные супы,  согреют и насытят вас в холодные зимние дни.",Photo="miso-sup-s-ugremi.jpg"},
                 new MenuCategory{NameCategory="Соусы",Comment="Соусы под любое блюдо", Photo="sous-shashlichniy.jpg"},
            };
            menuCategories.ForEach(s => context.MenuCategories.Add(s));
            context.SaveChanges();

            var pizza = new List<Dish>
            {
                new Dish
                {
                    NameDish="4 сезона Mix",
                    CompositionDish="соус «Помодоро», сыр моцарелла, грибы, подкопченное куриное филе, ветчина, бекон, маслины",
                    Weight=500,
                    Price=125,
                    MenuCategoryId=1,
                    Photo="4-sezona-mix.jpg"
                },
                new Dish
                {
                    NameDish = "Коррида",
                    CompositionDish = "соус «Помодоро», сыр моцарелла, салями",
                    Weight = 400,
                    Price = 115,
                    MenuCategoryId = 1,
                    Photo="korrida.jpg"
                },
                new Dish
                {
                    NameDish="Пицца Гавайская",
                    CompositionDish="соус сливочный, сыр моцарелла, подкопченное куриное филе, ананас",
                    Weight=500,
                    Price=105,
                    MenuCategoryId=1,
                    Photo="gavaiskaya.jpg"
                },
                new Dish
                {
                    NameDish = "Пицца Сырный бум",
                    CompositionDish = "соус белый сливочный, сыр моцарелла, сыр дорблю, сыр фета, сыр пармезан",
                    Weight = 400,
                    Price = 145,
                    MenuCategoryId = 1,
                    Photo="sirnij-boom.jpg"
                },
                new Dish
                {
                    NameDish="Пицца 4 сезона",
                    CompositionDish="соус «Помодоро», сыр моцарелла, грибы, подкопченное куриное филе, ветчина, бекон, маслины",
                    Weight=500,
                    Price=135,
                    MenuCategoryId=1,
                    Photo="picca-4-sezona.jpg"
                },
                new Dish
                {
                    NameDish="Палермо",
                    CompositionDish="соус «Помодоро», сыр моцарелла, подкопченное куриное филе, грибы, помидоры, ветчина",
                    Weight=500,
                    Price=130,
                    MenuCategoryId=1,
                    Photo="picca-palermo.jpg"
                },
                new Dish
                {
                    NameDish = "Охотничья",
                    CompositionDish = "соус «Помодоро», сыр моцарелла, охотничьи колбаски, ветчина, огурец соленый, грибы, перец острый",
                    Weight = 400,
                    Price = 130,
                    MenuCategoryId = 1,
                    Photo="ohotnica-1.jpg"
                },
                new Dish
                {
                    NameDish="Сицилия",
                    CompositionDish="оус «Помодоро», сыр моцарелла, ветчина, салями, грибы, орегано, оливковое масло",
                    Weight=500,
                    Price=130,
                    MenuCategoryId=1,
                    Photo="siciliya.jpg"
                },
                new Dish
                {
                    NameDish = "Капри",
                    CompositionDish = "соус «Помодоро», сыр моцарелла, подкопченное куриное филе, грибы, помидоры, кукуруза",
                    Weight = 400,
                    Price = 115,
                    MenuCategoryId = 1,
                    Photo="kapri-1.jpg"
                },
                new Dish
                {
                    NameDish="Регина",
                    CompositionDish="соус «Помодоро», сыр моцарелла, ветчина, грибы, орегано, маслины, оливковое масло)",
                    Weight=500,
                    Price=115,
                    MenuCategoryId=1,
                    Photo="picca-regina.jpg"
                },
                new Dish
                {
                    NameDish = "Дель Поло",
                    CompositionDish = "соус «Помодоро», сыр моцарелла, телятина, подкопченное куриное филе, перец болгарский",
                    Weight = 400,
                    Price = 145,
                    MenuCategoryId = 1,
                    Photo="del-polo-1.jpg"
                },
                new Dish
                {
                    NameDish="Карбонара ",
                    CompositionDish="соус белый сливочный, бекон, сыр моцарелла, помидоры «Черри», укроп",
                    Weight=500,
                    Price=110,
                    MenuCategoryId=1,
                    Photo="picca-karbonara.jpg"
                },
                new Dish
                {
                    NameDish = "Коза ностра",
                    CompositionDish = "соус «Помодоро», двойной сыр моцарелла, ветчина, помидоры, маслины, перец острый",
                    Weight = 400,
                    Price = 130,
                    MenuCategoryId = 1,
                    Photo="la-cosa-nostra-1.jpg"
                },
                new Dish
                {
                    NameDish="Торро",
                    CompositionDish="соус «Помодоро», сыр моцарелла, телятина, помидоры, лук белый репчатый, перец болгарский, перец острый, орегано, оливковое масло",
                    Weight=500,
                    Price=130,
                    MenuCategoryId=1,
                    Photo="del-polo-2.jpg"
                },
                new Dish
                {
                    NameDish = "Морская",
                    CompositionDish = "соус «Помодоро», сыр моцарелла, креветки, перец болгарский, помидоры, маслины, орегано, оливковое масло, руккола, лимон",
                    Weight = 400,
                    Price = 150,
                    MenuCategoryId = 1,
                    Photo="picca-morskaya.jpg"
                }
            };
            pizza.ForEach(s => context.Dishes.Add(s));
            context.SaveChanges();

            var burgers = new List<Dish>
            {
                new Dish
                {
                    NameDish="Дабл-бургер с телятиной",
                    CompositionDish="2 котлеты из телятины, бекон, сыр, помидор, лук крымский, айсберг, огурец маринованный, соус цезарь/картофель фри/ соус барбекю",
                    Weight=500,
                    Price=180,
                    MenuCategoryId=2,
                    Photo="burger-dvoynoy.jpg"
                },
                new Dish
                {
                    NameDish = "Бургер с телятиной ",
                    CompositionDish = "котлета из телятины, бекон, сыр, помидор, лук крымский, огурец маринованный, айсберг, соус цезарь/картофель фри/ соус барбекю",
                    Weight = 350,
                    Price = 150,
                    MenuCategoryId = 2,
                    Photo="burger-govyadina.jpg"
                },
                new Dish
                {
                    NameDish="Бургер с курицей",
                    CompositionDish="котлета куриная, сыр, помидор, айсберг, лук крымский, соус цезарь/картофель фри/соус барбекю",
                    Weight=300,
                    Price=130,
                    MenuCategoryId=2,
                    Photo="burger-kyrica.jpg"
                }
            };
            burgers.ForEach(s => context.Dishes.Add(s));
            context.SaveChanges();

            var mangal = new List<Dish>
            {
                new Dish
                {
                    NameDish="Шашлык из свинины",
                    CompositionDish="ошеек",
                    Weight = 250,
                    Price=135,
                    MenuCategoryId=3,
                    Photo="shashlyk-iz-svininy.jpg"
                },
                new Dish
                {
                    NameDish="Мясной сет",
                    CompositionDish="крылышки куриные — 250г, свиные ребрышки — 250г, шашлык из свинины (ошеек) — 250 г, люля-кебаб из телятины — 250 г, соус к шашлыку — 50 г",
                    Weight = 1000,
                    Price=450,
                    MenuCategoryId=3,
                    Photo="myasnoj-set.jpg"
                },
                new Dish
                {
                    NameDish="Лаваш",
                    CompositionDish="1 шт.",
                    Weight = 200,
                    Price=30,
                    MenuCategoryId=3,
                    Photo="hleb.jpg"
                },
                new Dish
                {
                    NameDish="Шашлык из куриного филе",
                    CompositionDish="куриного филе",
                    Weight = 250,
                    Price=95,
                    MenuCategoryId=3,
                    Photo="shashlyk-kurinyj.jpg"
                },
                new Dish
                {
                    NameDish="Грибы на мангале в соевом соусе",
                    CompositionDish="грибы с луклом",
                    Weight = 200,
                    Price=65,
                    MenuCategoryId=3,
                    Photo="gribi-mangal.jpg"
                },
                new Dish
                {
                    NameDish="Крылышки",
                    CompositionDish="куриные крылишки",
                    Weight = 350,
                    Price=90,
                    MenuCategoryId=3,
                    Photo="krylyshki-kurinye.jpg"
                },
                new Dish
                {
                    NameDish="Шашлык из телятины",
                    CompositionDish="телятина",
                    Weight = 250,
                    Price=135,
                    MenuCategoryId=3,
                    Photo="shashlyk-iz-telyatiny.jpg"
                },
                new Dish
                {
                    NameDish="Овощи на мангале",
                    CompositionDish="помидор, кабачок, болгарский перец",
                    Weight = 250,
                    Price=115,
                    MenuCategoryId=3,
                    Photo="ovoshhi-2.jpg"
                },
                new Dish
                {
                    NameDish="Свиные ребрышки",
                    CompositionDish=" ребрышки",
                    Weight = 250,
                    Price=115,
                    MenuCategoryId=3,
                    Photo="svinye-rebryshki.jpg"
                },
            };
            mangal.ForEach(s => context.Dishes.Add(s));
            context.SaveChanges();

            var salads = new List<Dish>
            {
                new Dish
                {
                    NameDish="Цезарь",
                    CompositionDish="лист салата, куриное филе, крутоны, пармезан, яйцо перепелиное, соус «Цезарь», помидоры «Черри»)",
                    Weight = 270,
                    Price=95,
                    MenuCategoryId=4,
                    Photo="cezar-1.jpg"
                },
                new Dish
                {
                    NameDish="Теплый салат с морепродуктами",
                    CompositionDish="креветка, мидии, кальмар, салат айсберг, руккола, перец болгарский, соус устричный, соус унаги, лимон, помидор черри, кунжут",
                    Weight = 210,
                    Price=155,
                    MenuCategoryId=4,
                    Photo="teplyj-salat-s-moreproduktami.jpg"
                },
                new Dish
                {
                    NameDish="Греческий",
                    CompositionDish="помидор, огурец свежий, перец болгарский, маслины, сыр фета, лимон, лук крымский, оливковое масло",
                    Weight = 270,
                    Price=80,
                    MenuCategoryId=4,
                    Photo="grecheskij.jpg"
                },
                new Dish
                {
                    NameDish="Чиполлино",
                    CompositionDish="лист салата, куриное филе, помидор, перец болгарский, яйцо куриное, кукуруза, огурец свежий, соус майонез собственного производства ",
                    Weight = 250,
                    Price=95,
                    MenuCategoryId=4,
                    Photo="chipollino.jpg"
                },
                new Dish
                {
                    NameDish="Милан",
                    CompositionDish="лист салата, помидор, огурец, ветчина, яйцо куриное, кукуруза, зеленый лук, соус майонез собственного производства",
                    Weight = 260,
                    Price=105,
                    MenuCategoryId=4,
                    Photo="milan.jpg"
                },
                new Dish
                {
                    NameDish="Салат летний ",
                    CompositionDish="помидор, огурец, болгарский перец, растительное масло/сметана/ майонез на выбор",
                    Weight = 260,
                    Price=65,
                    MenuCategoryId=4,
                    Photo="letnij.jpg"
                },
                new Dish
                {
                    NameDish="Теплый салат с телятиной",
                    CompositionDish="телятина, цукини, лук крымский, перец болгарский, шампиньоны, перец чили, огурец, лист салата, соус сладкий чили, унаги, кунжут, соевый соус",
                    Weight = 250,
                    Price=125,
                    MenuCategoryId=4,
                    Photo="s-telyatinoj-2.jpg"
                }
            };
            salads.ForEach(s => context.Dishes.Add(s));
            context.SaveChanges();

            var garnish = new List<Dish>
            {
                new Dish
                {
                    NameDish="Картофель фри",
                    CompositionDish="",
                    Weight = 150,
                    Price=40,
                    MenuCategoryId=5,
                    Photo="french-fries.jpg"
                },
                new Dish
                {
                    NameDish="Картофель по-креольски",
                    CompositionDish="",
                    Weight = 150,
                    Price=40,
                    MenuCategoryId=5,
                    Photo="kartofel.jpg"
                },
                new Dish
                {
                    NameDish="Запеченный картофель с беконом",
                    CompositionDish="",
                    Weight = 250,
                    Price=50,
                    MenuCategoryId=5,
                    Photo="zapechennyj-kartofel-s-bekonom.jpg"
                },
                new Dish
                {
                    NameDish="Стейк свиной",
                    CompositionDish="",
                    Weight = 250,
                    Price=150,
                    MenuCategoryId=5,
                    Photo="svinoy-steik.jpg"
                }
            };
            garnish.ForEach(s => context.Dishes.Add(s));
            context.SaveChanges();

            var firstMeal = new List<Dish>
            {
                new Dish
                {
                    NameDish="Бульон",
                    CompositionDish="куриное филе, перепелиное яйцо, фетучини, укроп",
                    Weight = 300,
                    Price=60,
                    MenuCategoryId=6, 
                    Photo="buljon.jpg"
                },
                new Dish
                {
                    NameDish="Окрошка",
                    CompositionDish="на курином бульйоне, куриное филе, яйца, огурцы, картофель, укроп, зеленый лук, сметана, майонез (",
                    Weight = 300,
                    Price=60,
                    MenuCategoryId=6,
                    Photo="okroshka.jpg"
                },
                new Dish
                {
                    NameDish="Мисо суп с угрем",
                    CompositionDish="мисо паста, угорь, хондаши, яйцо, лук зеленый, кунжут",
                    Weight = 300,
                    Price=140,
                    MenuCategoryId=6,
                    Photo="miso-sup-s-ugremi.jpg"
                },
                new Dish
                {
                    NameDish="Суп с тигровой креветкой",
                    CompositionDish="креветки тигровые, лапша удон, паста том ям, хондаши, лук зеленый, кунжут",
                    Weight = 300,
                    Price=150,
                    MenuCategoryId=6,
                    Photo="supstigrovoy-krevetkoyi-1.jpg"
                },
                new Dish
                {
                    NameDish="Мисо суп с лососем",
                    CompositionDish="мисо паста, лосось филе, хондаши, лук зеленый, кунжу",
                    Weight = 300,
                    Price=105,
                    MenuCategoryId=6,
                    Photo="miso-sup-s-tofu-i-lososem.jpg"
                },
                new Dish
                {
                    NameDish="Суп с морепродуктами",
                    CompositionDish="креветки, мидии, кальмар, лапша удон, грибы шиитаке, хондаши, лук зеленый, кунжут",
                    Weight = 300,
                    Price=140,
                    MenuCategoryId=6,
                    Photo="sup-s-moreproduktamii.jpg"
                }
            };
            firstMeal.ForEach(s => context.Dishes.Add(s));
            context.SaveChanges();

            var sauces = new List<Dish>
            {
                new Dish
                {
                    NameDish="Соус белый чесночный",
                    CompositionDish="",
                    Weight = 50,
                    Price=20,
                    MenuCategoryId=7,
                    Photo="chesno4nij.jpg"
                },
                new Dish
                {
                    NameDish="Соус к шашлыку",
                    CompositionDish="",
                    Weight = 50,
                    Price=20,
                    MenuCategoryId=7,
                    Photo="sous-shashlichniy.jpg"
                },
                new Dish
                {
                    NameDish="Соус барбекю",
                    CompositionDish="",
                    Weight = 50,
                    Price=20,
                    MenuCategoryId=7,
                    Photo="sous-barbekyu.jpg"
                },
                new Dish
                {
                    NameDish="Соус сырный",
                    CompositionDish="",
                    Weight = 50,
                    Price=20,
                    MenuCategoryId=7,
                    Photo="sirniy-sous.jpg"
                }
            };
            sauces.ForEach(s => context.Dishes.Add(s));
            context.SaveChanges();

          
        }
    }
}