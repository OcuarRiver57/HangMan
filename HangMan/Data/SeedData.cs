using HangMan.Data;
using HangMan.Models;

namespace HangMan.Data
{
    public class SeedData
    {
        private List<Category> Categories
        {
            get
            {
                return
                    new List<Category>
                    {
                        new() { Name = "Furniture", Description = "Common household furniture items such as chairs, tables, and sofas." },
                        new() { Name = "Fast Food", Description = "Popular fast food items from restaurants and chains." },
                        new() { Name = "Dishware", Description = "Items used for serving and eating food, such as plates and bowls." },
                        new() { Name = "Animals", Description = "Names of animals from around the world." },
                        new() { Name = "Fruits", Description = "Common and exotic fruits." },
                        new() { Name = "Vegetables", Description = "Edible vegetables used in cooking." },
                        new() { Name = "Countries", Description = "Names of countries around the globe." },
                        new() { Name = "Cities", Description = "Well-known cities from different countries." },
                        new() { Name = "Sports", Description = "Popular sports and athletic activities." },
                        new() { Name = "Instruments", Description = "Musical instruments of all types." },
                        new() { Name = "Clothing", Description = "Articles of clothing and apparel." },
                        new() { Name = "Vehicles", Description = "Types of vehicles used for transportation." },
                        new() { Name = "Professions", Description = "Different jobs and occupations." },
                        new() { Name = "Movies", Description = "Titles of popular movies." },
                        new() { Name = "Technology", Description = "Common technology-related terms and devices." },
                        new() { Name = "Weather", Description = "Weather-related terms and natural phenomena." },
                        new() { Name = "Body Parts", Description = "Parts of the human body." },
                        new() { Name = "School Supplies", Description = "Items commonly used in school settings." },
                        new() { Name = "Kitchen Appliances", Description = "Appliances found in a typical kitchen." },
                        new() { Name = "Tools", Description = "Hand tools and power tools." }
                    };
            }
        }

        private List<Word> Words
        {
            get
            {
                List<(string Spelling, string Category)> ws = new()
                {
                    new() { Spelling = "Chair", Category = "Furniture" },
                    new() { Spelling = "Table", Category = "Furniture" },
                    new() { Spelling = "Couch", Category = "Furniture" },
                    new() { Spelling = "Ottoman", Category = "Furniture" },
                    new() { Spelling = "Wardrobe", Category = "Furniture" },
                    new() { Spelling = "Bench", Category = "Furniture" },
                    new() { Spelling = "Cabinet", Category = "Furniture" },
                    new() { Spelling = "Desk", Category = "Furniture" },
                    new() { Spelling = "Dresser", Category = "Furniture" },
                    new() { Spelling = "Recliner", Category = "Furniture" },
                    new() { Spelling = "Nightstand", Category = "Furniture" },
                    new() { Spelling = "Stool", Category = "Furniture" },
                    new() { Spelling = "Armoire", Category = "Furniture" },
                    new() { Spelling = "Bookshelf", Category = "Furniture" },
                    new() { Spelling = "Loveseat", Category = "Furniture" },
                    new() { Spelling = "Hutch", Category = "Furniture" },
                    new() { Spelling = "Futon", Category = "Furniture" },
                    new() { Spelling = "Cupboard", Category = "Furniture" },

                    new() { Spelling = "Burger", Category = "Fast Food" },
                    new() { Spelling = "Fries", Category = "Fast Food" },
                    new() { Spelling = "Taco", Category = "Fast Food" },
                    new() { Spelling = "Sandwich", Category = "Fast Food" },
                    new() { Spelling = "Milkshake", Category = "Fast Food" },
                    new() { Spelling = "Burrito", Category = "Fast Food" },
                    new() { Spelling = "OnionRings", Category = "Fast Food" },
                    new() { Spelling = "Pizza", Category = "Fast Food" },
                    new() { Spelling = "Hotdog", Category = "Fast Food" },
                    new() { Spelling = "Wrap", Category = "Fast Food" },
                    new() { Spelling = "Pretzel", Category = "Fast Food" },
                    new() { Spelling = "Quesadilla", Category = "Fast Food" },
                    new() { Spelling = "Popcorn", Category = "Fast Food" },
                    new() { Spelling = "Nuggets", Category = "Fast Food" },
                    new() { Spelling = "Bagel", Category = "Fast Food" },
                    new() { Spelling = "Sliders", Category = "Fast Food" },
                    new() { Spelling = "Churro", Category = "Fast Food" },
                    new() { Spelling = "CornDog", Category = "Fast Food" },

                    new() { Spelling = "Plate", Category = "Dishware" },
                    new() { Spelling = "Bowl", Category = "Dishware" },
                    new() { Spelling = "Fork", Category = "Dishware" },
                    new() { Spelling = "Platter", Category = "Dishware" },
                    new() { Spelling = "Teacup", Category = "Dishware" },
                    new() { Spelling = "Saucer", Category = "Dishware" },
                    new() { Spelling = "Pitcher", Category = "Dishware" },
                    new() { Spelling = "Spoon", Category = "Dishware" },
                    new() { Spelling = "Knife", Category = "Dishware" },
                    new() { Spelling = "Goblet", Category = "Dishware" },
                    new() { Spelling = "Tray", Category = "Dishware" },
                    new() { Spelling = "Ladle", Category = "Dishware" },
                    new() { Spelling = "Tureen", Category = "Dishware" },
                    new() { Spelling = "Cup", Category = "Dishware" },
                    new() { Spelling = "Decanter", Category = "Dishware" },
                    new() { Spelling = "Colander", Category = "Dishware" },
                    new() { Spelling = "Mug", Category = "Dishware" },
                    new() { Spelling = "Saucepan", Category = "Dishware" },

                    new() { Spelling = "Elephant", Category = "Animals" },
                    new() { Spelling = "Giraffe", Category = "Animals" },
                    new() { Spelling = "Kangaroo", Category = "Animals" },
                    new() { Spelling = "Alligator", Category = "Animals" },
                    new() { Spelling = "Dolphin", Category = "Animals" },
                    new() { Spelling = "Cheetah", Category = "Animals" },
                    new() { Spelling = "Rabbit", Category = "Animals" },
                    new() { Spelling = "Zebra", Category = "Animals" },
                    new() { Spelling = "Penguin", Category = "Animals" },
                    new() { Spelling = "Moose", Category = "Animals" },
                    new() { Spelling = "Otter", Category = "Animals" },
                    new() { Spelling = "Panther", Category = "Animals" },
                    new() { Spelling = "Koala", Category = "Animals" },
                    new() { Spelling = "Lion", Category = "Animals" },
                    new() { Spelling = "Hyena", Category = "Animals" },
                    new() { Spelling = "Lemur", Category = "Animals" },
                    new() { Spelling = "Bison", Category = "Animals" },
                    new() { Spelling = "Weasel", Category = "Animals" },

                    new() { Spelling = "Apple", Category = "Fruits" },
                    new() { Spelling = "Banana", Category = "Fruits" },
                    new() { Spelling = "Cherry", Category = "Fruits" },
                    new() { Spelling = "Strawberry", Category = "Fruits" },
                    new() { Spelling = "Blueberry", Category = "Fruits" },
                    new() { Spelling = "Watermelon", Category = "Fruits" },
                    new() { Spelling = "Peach", Category = "Fruits" },
                    new() { Spelling = "Mango", Category = "Fruits" },
                    new() { Spelling = "Orange", Category = "Fruits" },
                    new() { Spelling = "Papaya", Category = "Fruits" },
                    new() { Spelling = "Kiwi", Category = "Fruits" },
                    new() { Spelling = "Apricot", Category = "Fruits" },
                    new() { Spelling = "Plum", Category = "Fruits" },
                    new() { Spelling = "Pineapple", Category = "Fruits" },
                    new() { Spelling = "Guava", Category = "Fruits" },
                    new() { Spelling = "Lychee", Category = "Fruits" },
                    new() { Spelling = "Fig", Category = "Fruits" },
                    new() { Spelling = "Coconut", Category = "Fruits" },

                    new() { Spelling = "Carrot", Category = "Vegetables" },
                    new() { Spelling = "Broccoli", Category = "Vegetables" },
                    new() { Spelling = "Spinach", Category = "Vegetables" },
                    new() { Spelling = "Onion", Category = "Vegetables" },
                    new() { Spelling = "Potato", Category = "Vegetables" },
                    new() { Spelling = "Cabbage", Category = "Vegetables" },
                    new() { Spelling = "Radish", Category = "Vegetables" },
                    new() { Spelling = "Pepper", Category = "Vegetables" },
                    new() { Spelling = "Cucumber", Category = "Vegetables" },
                    new() { Spelling = "Turnip", Category = "Vegetables" },
                    new() { Spelling = "Zucchini", Category = "Vegetables" },
                    new() { Spelling = "Garlic", Category = "Vegetables" },
                    new() { Spelling = "Celery", Category = "Vegetables" },
                    new() { Spelling = "Lettuce", Category = "Vegetables" },
                    new() { Spelling = "Parsnip", Category = "Vegetables" },
                    new() { Spelling = "Okra", Category = "Vegetables" },
                    new() { Spelling = "Beet", Category = "Vegetables" },
                    new() { Spelling = "Squash", Category = "Vegetables" },

                    new() { Spelling = "Canada", Category = "Countries" },
                    new() { Spelling = "Brazil", Category = "Countries" },
                    new() { Spelling = "Japan", Category = "Countries" },
                    new() { Spelling = "Argentina", Category = "Countries" },
                    new() { Spelling = "Thailand", Category = "Countries" },
                    new() { Spelling = "Norway", Category = "Countries" },
                    new() { Spelling = "Egypt", Category = "Countries" },
                    new() { Spelling = "Germany", Category = "Countries" },
                    new() { Spelling = "Mexico", Category = "Countries" },
                    new() { Spelling = "Sweden", Category = "Countries" },
                    new() { Spelling = "Chile", Category = "Countries" },
                    new() { Spelling = "Greece", Category = "Countries" },
                    new() { Spelling = "Vietnam", Category = "Countries" },
                    new() { Spelling = "Italy", Category = "Countries" },
                    new() { Spelling = "Peru", Category = "Countries" },
                    new() { Spelling = "Poland", Category = "Countries" },
                    new() { Spelling = "Kenya", Category = "Countries" },
                    new() { Spelling = "Iceland", Category = "Countries" },

                    new() { Spelling = "London", Category = "Cities" },
                    new() { Spelling = "Paris", Category = "Cities" },
                    new() { Spelling = "Tokyo", Category = "Cities" },
                    new() { Spelling = "Madrid", Category = "Cities" },
                    new() { Spelling = "Toronto", Category = "Cities" },
                    new() { Spelling = "Beijing", Category = "Cities" },
                    new() { Spelling = "Dubai", Category = "Cities" },
                    new() { Spelling = "Sydney", Category = "Cities" },
                    new() { Spelling = "Chicago", Category = "Cities" },
                    new() { Spelling = "Seoul", Category = "Cities" },
                    new() { Spelling = "Mumbai", Category = "Cities" },
                    new() { Spelling = "Lisbon", Category = "Cities" },
                    new() { Spelling = "Cairo", Category = "Cities" },
                    new() { Spelling = "Berlin", Category = "Cities" },
                    new() { Spelling = "Athens", Category = "Cities" },
                    new() { Spelling = "Prague", Category = "Cities" },
                    new() { Spelling = "Havana", Category = "Cities" },
                    new() { Spelling = "Oslo", Category = "Cities" },

                    new() { Spelling = "Soccer", Category = "Sports" },
                    new() { Spelling = "Tennis", Category = "Sports" },
                    new() { Spelling = "Baseball", Category = "Sports" },
                    new() { Spelling = "Volleyball", Category = "Sports" },
                    new() { Spelling = "Swimming", Category = "Sports" },
                    new() { Spelling = "Boxing", Category = "Sports" },
                    new() { Spelling = "Cycling", Category = "Sports" },
                    new() { Spelling = "Hockey", Category = "Sports" },
                    new() { Spelling = "Cricket", Category = "Sports" },
                    new() { Spelling = "Skating", Category = "Sports" },
                    new() { Spelling = "Archery", Category = "Sports" },
                    new() { Spelling = "Surfing", Category = "Sports" },
                    new() { Spelling = "Rowing", Category = "Sports" },
                    new() { Spelling = "Rugby", Category = "Sports" },
                    new() { Spelling = "Karate", Category = "Sports" },
                    new() { Spelling = "Fencing", Category = "Sports" },
                    new() { Spelling = "Bowling", Category = "Sports" },
                    new() { Spelling = "Diving", Category = "Sports" },

                    new() { Spelling = "Guitar", Category = "Instruments" },
                    new() { Spelling = "Piano", Category = "Instruments" },
                    new() { Spelling = "Violin", Category = "Instruments" },
                    new() { Spelling = "Cello", Category = "Instruments" },
                    new() { Spelling = "Clarinet", Category = "Instruments" },
                    new() { Spelling = "Harp", Category = "Instruments" },
                    new() { Spelling = "Saxophone", Category = "Instruments" },
                    new() { Spelling = "Drums", Category = "Instruments" },
                    new() { Spelling = "Trumpet", Category = "Instruments" },
                    new() { Spelling = "Trombone", Category = "Instruments" },
                    new() { Spelling = "Oboe", Category = "Instruments" },
                    new() { Spelling = "Banjo", Category = "Instruments" },
                    new() { Spelling = "Mandolin", Category = "Instruments" },
                    new() { Spelling = "Flute", Category = "Instruments" },
                    new() { Spelling = "Tambourine", Category = "Instruments" },
                    new() { Spelling = "Accordion", Category = "Instruments" },
                    new() { Spelling = "Ukulele", Category = "Instruments" },
                    new() { Spelling = "Bassoon", Category = "Instruments" },

                    new() { Spelling = "Jacket", Category = "Clothing" },
                    new() { Spelling = "Sweater", Category = "Clothing" },
                    new() { Spelling = "Sandals", Category = "Clothing" },
                    new() { Spelling = "Coat", Category = "Clothing" },
                    new() { Spelling = "Boots", Category = "Clothing" },
                    new() { Spelling = "Gloves", Category = "Clothing" },
                    new() { Spelling = "Trousers", Category = "Clothing" },
                    new() { Spelling = "Jeans", Category = "Clothing" },
                    new() { Spelling = "Scarf", Category = "Clothing" },
                    new() { Spelling = "Blouse", Category = "Clothing" },
                    new() { Spelling = "Skirt", Category = "Clothing" },
                    new() { Spelling = "Pajamas", Category = "Clothing" },
                    new() { Spelling = "Vest", Category = "Clothing" },
                    new() { Spelling = "Hat", Category = "Clothing" },
                    new() { Spelling = "Hoodie", Category = "Clothing" },
                    new() { Spelling = "Cardigan", Category = "Clothing" },
                    new() { Spelling = "Belt", Category = "Clothing" },
                    new() { Spelling = "Tie", Category = "Clothing" },

                    new() { Spelling = "Bicycle", Category = "Vehicles" },
                    new() { Spelling = "Airplane", Category = "Vehicles" },
                    new() { Spelling = "Submarine", Category = "Vehicles" },
                    new() { Spelling = "Helicopter", Category = "Vehicles" },
                    new() { Spelling = "Yacht", Category = "Vehicles" },
                    new() { Spelling = "Tractor", Category = "Vehicles" },
                    new() { Spelling = "Van", Category = "Vehicles" },
                    new() { Spelling = "Motorcycle", Category = "Vehicles" },
                    new() { Spelling = "Scooter", Category = "Vehicles" },
                    new() { Spelling = "Ferry", Category = "Vehicles" },
                    new() { Spelling = "Ambulance", Category = "Vehicles" },
                    new() { Spelling = "Limousine", Category = "Vehicles" },
                    new() { Spelling = "Tank", Category = "Vehicles" },
                    new() { Spelling = "Truck", Category = "Vehicles" },
                    new() { Spelling = "Bulldozer", Category = "Vehicles" },
                    new() { Spelling = "Glider", Category = "Vehicles" },
                    new() { Spelling = "Rickshaw", Category = "Vehicles" },
                    new() { Spelling = "Snowmobile", Category = "Vehicles" },

                    new() { Spelling = "Teacher", Category = "Professions" },
                    new() { Spelling = "Doctor", Category = "Professions" },
                    new() { Spelling = "Engineer", Category = "Professions" },
                    new() { Spelling = "Dentist", Category = "Professions" },
                    new() { Spelling = "Architect", Category = "Professions" },
                    new() { Spelling = "Chef", Category = "Professions" },
                    new() { Spelling = "Firefighter", Category = "Professions" },
                    new() { Spelling = "Lawyer", Category = "Professions" },
                    new() { Spelling = "Pilot", Category = "Professions" },
                    new() { Spelling = "Plumber", Category = "Professions" },
                };

                List<Word> words = new();

                foreach (var w in ws)
                {
                    foreach(Category c in this.Categories)
                    {
                        if(w.Category == c.Name)
                        {
                            Word newWord = new(w.Spelling, c);
                            words.Add(newWord);
                        }
                    }
                }

                return words;
            }
        }

        public void Seed(AppDbContext context)
        {
            if (!context.Categories.Any() && !context.Words.Any())
            {
                context.Categories.AddRange(Categories);
                context.Words.AddRange(Words);
                context.SaveChanges();
            }
        }
    }
}
