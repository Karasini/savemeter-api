using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaveMeter.Services.Finances.Domain.Aggregates.Category;

namespace SaveMeter.Services.Finances.Infrastructure.Mongo
{
    class CategoryReferenceSeed
    {
        public static List<CategoryReference> GenerateSeeds()
        {
            var categories = CategoriesSeed.Categories;

            return new List<CategoryReference>()
            {
                CreateRestaurantReference("pyszne"),
                CreateRestaurantReference("pyszne.pl"),
                CreateRestaurantReference("sushi"),
                CreateRestaurantReference("McDonalds"),
                CreateRestaurantReference("Beer"),
                CreateRestaurantReference("Food"),
                CreateRestaurantReference("Browar"),
                CreateRestaurantReference("CAFE"),
                CreateRestaurantReference("TRATTORIA"),
                CreateRestaurantReference("Pizzeria"),
                CreateRestaurantReference("CUKIERNIA"),
                CreateRestaurantReference("RESTAURACJA"),
                CreateShopReference("ZABKA"),
                CreateShopReference("kiosk"),
                CreateShopReference("Domino"),
                CreateShopReference("Lewiatan"),
                CreateShopReference("intermarche"),
                CreateShopReference("piekarnia"),
                CreateShopReference("Market"),
                CreateShopReference("Sklep"),
                CreateShopReference("Allegro"),
                CreateShopReference("BIEDRONKA"),
                CreateShopReference("Lidl"),
                CreateShopReference("empik"),
                CreateHealthReference("Apteka"),
                CreateHealthReference("PHARM"),
                CreateBeautyReference("Rossmann"),
                CreateBusReference("Uber"),
                CreateClothesReference("Half price"),
                CreateFuelReference("Orlen"),
                CreateFuelReference("Bp"),
                CreateFuelReference("lotos"),
                CreateFuelReference("Stacja paliw"),
                CreateEntertainmentReference("NETFLIX.COM"),
                CreateHairdresserReference("fryzjernia"),
            };

            Category GetCategory(string name)
            {
                return categories.First(x => x.Name == name);
            }

            CategoryReference CreateRestaurantReference(string key)
            {
                return CategoryReference.Create(GetCategory(CategoriesSeed.Restaurant).Id, key);
            }

            CategoryReference CreateShopReference(string key)
            {
                return CategoryReference.Create(GetCategory(CategoriesSeed.Shop).Id, key);
            }

            CategoryReference CreateHealthReference(string key)
            {
                return CategoryReference.Create(GetCategory(CategoriesSeed.Health).Id, key);
            }

            CategoryReference CreateBeautyReference(string key)
            {
                return CategoryReference.Create(GetCategory(CategoriesSeed.Beauty).Id, key);
            }

            CategoryReference CreateBusReference(string key)
            {
                return CategoryReference.Create(GetCategory(CategoriesSeed.Bus).Id, key);
            }

            CategoryReference CreateClothesReference(string key)
            {
                return CategoryReference.Create(GetCategory(CategoriesSeed.Clothes).Id, key);
            }

            CategoryReference CreateFuelReference(string key)
            {
                return CategoryReference.Create(GetCategory(CategoriesSeed.Fuel).Id, key);
            }

            CategoryReference CreateEntertainmentReference(string key)
            {
                return CategoryReference.Create(GetCategory(CategoriesSeed.Entertainment).Id, key);
            }

            CategoryReference CreateHairdresserReference(string key)
            {
                return CategoryReference.Create(GetCategory(CategoriesSeed.Hairdresser).Id, key);
            }

            CategoryReference CreateApartmentReference(string key)
            {
                return CategoryReference.Create(GetCategory(CategoriesSeed.Apartment).Id, key);
            }
        }
    }
}
