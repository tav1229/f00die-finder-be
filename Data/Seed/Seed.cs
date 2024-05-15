using f00die_finder_be.Common;
using f00die_finder_be.Entities;
using System.Text.Json;

namespace f00die_finder_be.Data.Seed
{
    public class Seed
    {
        public static void SeedAll(DataContext context)
        {
            SeedServingType(context);
            SeedCuisineType(context);
            SeedAddionalService(context);
            SeedUser(context);
            SeedCity(context);
            SeedDistrict(context);
            SeedWard(context);
            SeedLocation(context);
            SeedCustomerType(context);
        }

        public static void SeedCustomerType(DataContext context)
        {
            if (context.CustomerTypes.Any()) return;

            context.CustomerTypes.Add(new CustomerType { Name = "Văn phòng" });
            context.CustomerTypes.Add(new CustomerType { Name = "Cặp đôi" });
            context.CustomerTypes.Add(new CustomerType { Name = "Bạn bè" });
            context.CustomerTypes.Add(new CustomerType { Name = "Gia đình" });
            context.CustomerTypes.Add(new CustomerType { Name = "Tiệc/Sự kiện" });

            context.SaveChanges();
        }

        public static void SeedServingType(DataContext context)
        {
            if (context.ServingTypes.Any()) return;

            context.ServingTypes.Add(new ServingType { Name = "Buffet" });
            context.ServingTypes.Add(new ServingType { Name = "Alacarte" });

            context.SaveChanges();
        }

        public static void SeedCuisineType(DataContext context)
        {
            if (context.CuisineTypes.Any()) return;

            context.CuisineTypes.Add(new CuisineType { Name = "Lẩu" });
            context.CuisineTypes.Add(new CuisineType { Name = "Nướng" });
            context.CuisineTypes.Add(new CuisineType { Name = "Lẩu-Nướng" });
            context.CuisineTypes.Add(new CuisineType { Name = "Quán chay" });
            context.CuisineTypes.Add(new CuisineType { Name = "Quán nhậu" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món việt" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món ăn miền Bắc" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món ăn miền Trung" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món ăn miền Nam" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món Nhật" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món Hàn" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món Thái" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món Trung" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món Á" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món Âu" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món Ý" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món Mỹ" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món Pháp" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món Singapore" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món Nga" });
            context.CuisineTypes.Add(new CuisineType { Name = "Đặc sản" });
            context.CuisineTypes.Add(new CuisineType { Name = "Hải sản" });

            context.SaveChanges();
        }

        public static void SeedAddionalService(DataContext context)
        {
            if (context.AdditionalServices.Any()) return;

            context.AdditionalServices.Add(new AdditionalService() { Name = "Chỗ để xe" });
            context.AdditionalServices.Add(new AdditionalService() { Name = "Phòng riêng" });
            context.AdditionalServices.Add(new AdditionalService() { Name = "Có xuất hóa đơn" });
            context.AdditionalServices.Add(new AdditionalService() { Name = "Karaoke" });
            context.AdditionalServices.Add(new AdditionalService() { Name = "Màn chiếu" });
            context.AdditionalServices.Add(new AdditionalService() { Name = "Wifi" });
            context.AdditionalServices.Add(new AdditionalService() { Name = "Thanh toán thẻ" });
            context.AdditionalServices.Add(new AdditionalService() { Name = "Trang trí sự kiện" });
            context.AdditionalServices.Add(new AdditionalService() { Name = "Bàn ngoài trời" });
            context.AdditionalServices.Add(new AdditionalService() { Name = "Khu vui chơi trẻ em" });
            context.SaveChanges();
        }


        // public static void SeedRestaurants(DataContext context) {
        //     if(context.Restaurants.Any()) return;

        //     var restaurantList = new List<Restaurant>();

        //     for (int i = 0; i < 300; i++)
        //     {
        //         Random random = new Random();
        //         PriceRange[] priceRanges = (PriceRange[])Enum.GetValues(typeof(PriceRange));
        //         PriceRange randomPriceRange = priceRanges[random.Next(priceRanges.Length)];

        //         var restaurant = new Restaurant
        //         {
        //             Id = Guid.NewGuid(),
        //             Name = "Restaurant " + i,
        //             Phone = "123-456-7890",
        //             PriceRange = randomPriceRange,
        //             Capacity = 50,
        //             SpecialDishes = "Special dishes for Restaurant " + i,
        //             Introduction = "Introduction for Restaurant " + i,
        //             Note = "Note for Restaurant " + i,
        //             CreateAt = DateTime.Now,
        //             UpdateAt = DateTime.Now,
        //             UserId = new Guid("ee475df2-87c9-4cca-f211-08db6510c972"),
        //         };
        //         restaurantList.Add(restaurant);
        //         // context.Add(restaurant);
        //     }
        //     context.AddRange(restaurantList);
        //     context.SaveChanges();
        // }

        public static void SeedUser(DataContext context)
        {
            if (context.Users.Any())
            {
                return;
            }
            var allCuisineTypes = context.CuisineTypes.ToList();
            var allServingTypes = context.ServingTypes.ToList();
            var allAdditionalServices = context.AdditionalServices.ToList();
            var allCustomerTypes = context.CustomerTypes.ToList();

            Random random = new Random();
            PriceRangePerPerson[] priceRanges = (PriceRangePerPerson[])Enum.GetValues(typeof(PriceRangePerPerson));

            for (int i = 10; i < 15; i++)
            {
                var passwordSalt = SecurityFunction.GenerateRandomString();
                var hashedPassword = SecurityFunction.HashPassword("seed", passwordSalt);

                var user = new User();
                user.Username = "user" + i.ToString();
                user.HashedPassword = hashedPassword;
                user.PasswordSalt = passwordSalt;
                user.FullName = "user " + i.ToString();
                user.Phone = "09876543" + i.ToString();
                user.Email = "user" + i.ToString() + "@gmail.com";
                user.Role = Role.RestaurantOwner;

                user.Restaurant = new Restaurant
                {
                    Name = "Restaurant " + i,
                    Phone = "123-456-7890",
                    PriceRangePerPerson = priceRanges[random.Next(priceRanges.Length)],
                    Capacity = 50,
                    SpecialDishes = "Special dishes for Restaurant " + i,
                    Description = "Description for Restaurant " + i,
                    Note = "Note for Restaurant " + i,
                    Images = new List<RestaurantImage>()
            {
                new RestaurantImage() { ImageType = ImageType.Restaurant, URL = "http://product.hstatic.net/1000275435/product/19649878852941771838_optimized_c11ad126d57340ea9debbba2dc3cd99b_large_7dbef784c9734a1e94eb9f85be955251_master.jpg" },
                new RestaurantImage() { ImageType = ImageType.Restaurant, URL = "http://product.hstatic.net/1000275435/product/19649878852941771838_optimized_c11ad126d57340ea9debbba2dc3cd99b_large_7dbef784c9734a1e94eb9f85be955251_master.jpg" },
                new RestaurantImage() { ImageType = ImageType.Restaurant, URL = "http://product.hstatic.net/1000275435/product/19649878852941771838_optimized_c11ad126d57340ea9debbba2dc3cd99b_large_7dbef784c9734a1e94eb9f85be955251_master.jpg" },
                new RestaurantImage() { ImageType = ImageType.Menu, URL = "http://file.hstatic.net/1000275435/file/85837c3c4612984cc103_23f1291ce0f1456f8bbce7641a90c3a4_grande.jpg" },
                new RestaurantImage() { ImageType = ImageType.Menu, URL = "http://file.hstatic.net/1000275435/file/778377404d6e9330ca7f_795187b94acf4d7390cf0cf2b22d16f2_grande.jpg" },
                new RestaurantImage() { ImageType = ImageType.Menu, URL = "http://file.hstatic.net/1000275435/file/5021dc98e6b638e861a7_c615283c5ea64eba891084c1ee08fc9b_grande.jpg" },
                new RestaurantImage() { ImageType = ImageType.Menu, URL = "http://file.hstatic.net/1000275435/file/751adea5e48b3ad5639a_4fbedd0e3d9e4c4e996f51868eeaedb6_grande.jpg" },
                new RestaurantImage() { ImageType = ImageType.Menu, URL = "http://file.hstatic.net/1000275435/file/1220f69eccb012ee4ba1_5106efe71a3f40349f9247fe8b8df5c1_grande.jpg" }
            },
                    RestaurantCuisineTypes = new List<RestaurantCuisineType>()
            {
                new RestaurantCuisineType() { CuisineType = allCuisineTypes[random.Next(allCuisineTypes.Count)] },
                new RestaurantCuisineType() { CuisineType = allCuisineTypes[random.Next(allCuisineTypes.Count)] },
                new RestaurantCuisineType() { CuisineType = allCuisineTypes[random.Next(allCuisineTypes.Count)] }
            },
                    RestaurantServingTypes = new List<RestaurantServingType>()
                    {
                        new RestaurantServingType() { ServingType = allServingTypes[random.Next(allServingTypes.Count)] },
                    },
                    RestaurantAdditionalServices = new List<RestaurantAdditionalService>()
                    {
                        new RestaurantAdditionalService() { AdditionalService = allAdditionalServices[random.Next(allAdditionalServices.Count)] },
                        new RestaurantAdditionalService() { AdditionalService = allAdditionalServices[random.Next(allAdditionalServices.Count)] },
                        new RestaurantAdditionalService() { AdditionalService = allAdditionalServices[random.Next(allAdditionalServices.Count)] }
                    },
                    RestaurantCustomerTypes = new List<RestaurantCustomerType>()
                    {
                        new RestaurantCustomerType() { CustomerType = allCustomerTypes[random.Next(allCustomerTypes.Count)] },
                        new RestaurantCustomerType() { CustomerType = allCustomerTypes[random.Next(allCustomerTypes.Count)] }
                    },
                    BusinessHours = new List<BusinessHour>()
            {
                new BusinessHour() { Id = new Guid(), DayOfWeek = DayOfWeek.Monday, OpenTime = new TimeSpan(7, 0, 0), CloseTime = new TimeSpan(23, 0, 0) },
                new BusinessHour() { Id = new Guid(), DayOfWeek  = DayOfWeek.Tuesday, OpenTime = new TimeSpan(7, 0, 0), CloseTime = new TimeSpan(23, 0, 0) },
                new BusinessHour() { Id = new Guid(), DayOfWeek  = DayOfWeek.Wednesday, OpenTime = new TimeSpan(7, 0, 0), CloseTime = new TimeSpan(23, 0, 0) },
                new BusinessHour() { Id = new Guid(), DayOfWeek  = DayOfWeek.Thursday, OpenTime = new TimeSpan(7, 0, 0), CloseTime = new TimeSpan(23, 0, 0) },
                new BusinessHour() { Id = new Guid(), DayOfWeek  = DayOfWeek.Friday, OpenTime = new TimeSpan(7, 0, 0), CloseTime = new TimeSpan(23, 0, 0) },
                new BusinessHour() { Id = new Guid(), DayOfWeek  = DayOfWeek.Saturday, OpenTime = new TimeSpan(7, 0, 0), CloseTime = new TimeSpan(23, 0, 0) },
                new BusinessHour() { Id = new Guid(), DayOfWeek  = DayOfWeek.Sunday, OpenTime = new TimeSpan(6, 0, 0), CloseTime = new TimeSpan(23, 0, 0) }
            }
                };
                context.Users.Add(user);
            }

            context.SaveChanges();
        }

        public static void SeedCity(DataContext context)
        {
            if (context.ProvinceOrCities.Any()) return;

            var locationText = System.IO.File.ReadAllText("Data\\Seed\\Vietnam-location\\cities.json");
            var cities = JsonSerializer.Deserialize<List<CitySeedDto>>(locationText);
            var cityEntities = new List<ProvinceOrCity>();

            if (cities == null)
            {
                return;
            }
            foreach (var city in cities)
            {
                cityEntities.Add(new ProvinceOrCity()
                {
                    Code = short.Parse(city.code),
                    Name = city.name
                });
            }
            context.AddRange(cityEntities);
            context.SaveChanges();
        }

        public static void SeedDistrict(DataContext context)
        {
            if (context.Districts.Any()) return;

            var districtText = System.IO.File.ReadAllText("Data\\Seed\\Vietnam-location\\districts.json");
            var districts = JsonSerializer.Deserialize<List<DistrictSeedDto>>(districtText);
            var districtEntities = new List<District>();

            if (districts == null)
            {
                return;
            }
            foreach (var district in districts)
            {
                districtEntities.Add(new District()
                {
                    Code = short.Parse(district.code),
                    Name = district.name,
                    ProvinceOrCityId = context.ProvinceOrCities.Where(p => p.Code == Int32.Parse(district.parent_code)).Select(p => p.Id).FirstOrDefault()

                });
            }
            context.AddRange(districtEntities);
            context.SaveChanges();
        }

        public static void SeedWard(DataContext context)
        {
            if (context.WardOrCommunes.Any()) return;

            var wardText = System.IO.File.ReadAllText("Data\\Seed\\Vietnam-location\\wards.json");
            var wards = JsonSerializer.Deserialize<List<WardSeedDto>>(wardText);
            var wardEntities = new List<WardOrCommune>();

            if (wards == null)
            {
                return;
            }
            foreach (var ward in wards)
            {
                wardEntities.Add(new WardOrCommune()
                {
                    Code = short.Parse(ward.code),
                    Name = ward.name,
                    //DistrictID = Int32.Parse(ward.parent_code)
                    DistrictId = context.Districts.Where(d => d.Code == Int32.Parse(ward.parent_code)).Select(d => d.Id).FirstOrDefault()
                });
            }
            context.AddRange(wardEntities);
            context.SaveChanges();
        }

        public static void SeedLocation(DataContext context)
        {
            if (context.Locations.Any()) return;

            List<Guid> restaurantIds = context.Restaurants.Select(r => r.Id).ToList();
            var locationList = new List<Location>();
            for (int i = 0; i < 5; i++)
            {
                var location = new Location
                {
                    Id = Guid.NewGuid(),
                    Address = "Address for Location " + i,
                    WardOrCommuneId = context.WardOrCommunes.Where(w => w.Code == 1).Select(w => w.Id).FirstOrDefault(),
                    RestaurantId = restaurantIds[i]
                };

                locationList.Add(location);
            }
            context.AddRange(locationList);
            context.SaveChanges();
        }

        

    }

    public class CitySeedDto
    {
        public string? name { get; set; }

        public string? code { get; set; }

        public string? slug { get; set; }

        public string? type { get; set; }

        public string? name_with_type { get; set; }
    }

    public class DistrictSeedDto
    {
        public string? name { get; set; }

        public string? type { get; set; }

        public string? slug { get; set; }

        public string? name_with_type { get; set; }

        public string? path { get; set; }

        public string? path_with_type { get; set; }

        public string? code { get; set; }

        public string? parent_code { get; set; }
    }

    public class WardSeedDto
    {
        public string? name { get; set; }

        public string? type { get; set; }

        public string? slug { get; set; }

        public string? name_with_type { get; set; }

        public string? path { get; set; }

        public string? path_with_type { get; set; }

        public string? code { get; set; }

        public string? parent_code { get; set; }
    }
}