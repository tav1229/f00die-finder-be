using f00die_finder_be.Common;
using f00die_finder_be.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace f00die_finder_be.Data.Seed
{
    public class Seed
    {
        public static void SeedAll(DataContext context)
        {
            SeedCity(context);
            SeedDistrict(context);
            SeedWard(context);
            SeedPriceRangePerPerson(context);
            SeedServingType(context);
            SeedCuisineType(context);
            SeedAddionalService(context);
            SeedCustomerType(context);
            SeedUser(context);
            SeedReservations(context);
            SeedReviewComments(context);
        }

        public static void SeedPriceRangePerPerson(DataContext context)
        {
            if (context.PriceRangePerPersons.Any()) return;

            context.PriceRangePerPersons.Add(new PriceRangePerPerson { Name = "Dưới 200.000đ", PriceOrder = 1 });
            context.PriceRangePerPersons.Add(new PriceRangePerPerson { Name = "200.000đ - 400.000đ", PriceOrder = 2 });
            context.PriceRangePerPersons.Add(new PriceRangePerPerson { Name = "500.000đ - 1.000.000đ", PriceOrder = 3 });
            context.PriceRangePerPersons.Add(new PriceRangePerPerson { Name = "Trên 1.000.000đ", PriceOrder = 4 });
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

            context.CuisineTypes.Add(new CuisineType { Name = "Món lẩu", IconUrl = "https://pastaxi-manager.onepas.vn/Upload/DanhMucHienThi/Avatar/638441023317916801-icon-lau.png" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món nướng", IconUrl = "https://pastaxi-manager.onepas.vn/Upload/DanhMucHienThi/Avatar/638441027537096717-icon-nuong.png" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món chay", IconUrl = "https://pastaxi-manager.onepas.vn/Upload/DanhMucHienThi/Avatar/638011884827831221-mon-chay-pasgo.png" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món nhậu", IconUrl = "https://pastaxi-manager.onepas.vn/Upload/DanhMucHienThi/Avatar/638011851008641451-quan-nhau-pasgo.png" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món Việt Nam", IconUrl = "https://pastaxi-manager.onepas.vn/Upload/DanhMucHienThi/Avatar/638011878657732280-mon-viet-pasgo.png" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món ăn miền Bắc" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món ăn miền Trung" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món ăn miền Nam" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món Nhật Bản", IconUrl = "https://pastaxi-manager.onepas.vn/Upload/DanhMucHienThi/Avatar/638011867970619295-mon-nhat-ban-pasgo.png" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món Hàn Quốc", IconUrl = "https://pastaxi-manager.onepas.vn/Upload/DanhMucHienThi/Avatar/638442738231563544-mon-han.png" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món Thái Lan" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món Trung Quốc" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món Á", IconUrl = "https://pastaxi-manager.onepas.vn/Upload/DanhMucHienThi/Avatar/638239778553118285-mon-chau-a.png" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món Âu", IconUrl = "https://pastaxi-manager.onepas.vn/Upload/DanhMucHienThi/Avatar/638239806231357799-mon-chau-au.png" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món Ý" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món Mỹ" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món Pháp" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món Singapore" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món Nga" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món hải sản", IconUrl = "https://pastaxi-manager.onepas.vn/Upload/DanhMucHienThi/Avatar/638011847245865102-hai-san-pasgo.png" });

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
        //             CreateAt = DateTimeOffset.Now,
        //             UpdateAt = DateTimeOffset.Now,
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
            var allPriceRangePerPersons = context.PriceRangePerPersons.ToList();

            var restaurantImages = new List<string>()
            {
                "https://images.unsplash.com/photo-1513442542250-854d436a73f2?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MjB8fHJlc3RhdXJhbnQlMjBmb29kfGVufDB8fDB8fHww",
                "https://images.unsplash.com/photo-1515668166700-4c11137632fc?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTV8fHJlc3RhdXJhbnQlMjBmb29kfGVufDB8fDB8fHww",
        "https://images.unsplash.com/photo-1515668236457-83c3b8764839?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTl8fHJlc3RhdXJhbnQlMjBmb29kfGVufDB8fDB8fHww",
        "https://images.unsplash.com/photo-1564758565388-0d5da0cbb08c?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTZ8fHJlc3RhdXJhbnQlMjBmb29kfGVufDB8fDB8fHww",
        "https://plus.unsplash.com/premium_photo-1673580742890-4af144293960?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTd8fHJlc3RhdXJhbnQlMjBmb29kfGVufDB8fDB8fHww",
        "https://images.unsplash.com/photo-1457460866886-40ef8d4b42a0?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTR8fHJlc3RhdXJhbnQlMjBmb29kfGVufDB8fDB8fHww",
        "https://plus.unsplash.com/premium_photo-1661600643912-dc6dbb1db475?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTN8fHJlc3RhdXJhbnQlMjBmb29kfGVufDB8fDB8fHww",
        "https://images.unsplash.com/photo-1564759296729-771e78c26df7?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mjh8fHJlc3RhdXJhbnQlMjBmb29kfGVufDB8fDB8fHww",
        "https://plus.unsplash.com/premium_photo-1661777692723-ba8dd05065d9?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mjl8fHJlc3RhdXJhbnQlMjBmb29kfGVufDB8fDB8fHww",
        "https://images.unsplash.com/photo-1543992321-cefacfc2322e?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MjR8fHJlc3RhdXJhbnQlMjBmb29kfGVufDB8fDB8fHww",
        "https://images.unsplash.com/photo-1515669097368-22e68427d265?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MjJ8fHJlc3RhdXJhbnQlMjBmb29kfGVufDB8fDB8fHww",
        "https://images.unsplash.com/photo-1543826173-70651703c5a4?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MzF8fHJlc3RhdXJhbnQlMjBmb29kfGVufDB8fDB8fHww",
        "https://plus.unsplash.com/premium_photo-1661777712373-9a9ee6e01007?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mzd8fHJlc3RhdXJhbnQlMjBmb29kfGVufDB8fDB8fHww",
        "https://plus.unsplash.com/premium_photo-1661668648046-f2bc1091e4b8?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MzN8fHJlc3RhdXJhbnQlMjBmb29kfGVufDB8fDB8fHww",
        "https://images.unsplash.com/photo-1546456674-8aa8c81b9b8e?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MzJ8fHJlc3RhdXJhbnQlMjBmb29kfGVufDB8fDB8fHww",
        "https://images.unsplash.com/photo-1576829824883-bf9e6b522252?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NDR8fHJlc3RhdXJhbnQlMjBmb29kfGVufDB8fDB8fHww",
        "https://images.unsplash.com/photo-1626056087729-a1f5c1a7d90d?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mzh8fHJlc3RhdXJhbnQlMjBmb29kfGVufDB8fDB8fHww",
        "https://images.unsplash.com/photo-1504674900247-0877df9cc836?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NDJ8fHJlc3RhdXJhbnQlMjBmb29kfGVufDB8fDB8fHww",
        "https://images.unsplash.com/photo-1484723091739-30a097e8f929?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mzl8fHJlc3RhdXJhbnQlMjBmb29kfGVufDB8fDB8fHww",
        "https://plus.unsplash.com/premium_photo-1678897742200-85f052d33a71?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NDl8fHJlc3RhdXJhbnQlMjBmb29kfGVufDB8fDB8fHww",
        "https://images.unsplash.com/photo-1511690656952-34342bb7c2f2?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NDd8fHJlc3RhdXJhbnQlMjBmb29kfGVufDB8fDB8fHww",
        "https://plus.unsplash.com/premium_photo-1661600135596-dcb910b05340?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NDV8fHJlc3RhdXJhbnQlMjBmb29kfGVufDB8fDB8fHww",
        "https://images.unsplash.com/photo-1554502078-ef0fc409efce?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NDZ8fHJlc3RhdXJhbnQlMjBmb29kfGVufDB8fDB8fHww"
            };

            var menuImages = new List<string>()
            {
                "https://cdn.pastaxi-manager.onepas.vn/Content/Uploads/Prices/nguyenhuong/brilliant/8.jpg",
                "https://cdn.pastaxi-manager.onepas.vn/Content/Uploads/Prices/nguyenhuong/brilliant/7.jpg",
                "https://cdn.pastaxi-manager.onepas.vn/Content/Uploads/Prices/nguyenhuong/brilliant/6.jpg",
                "https://cdn.pastaxi-manager.onepas.vn/Content/Uploads/Prices/nguyenhuong/brilliant/5.jpg",
                "https://cdn.pastaxi-manager.onepas.vn/Content/Uploads/Prices/nguyenhuong/brilliant/4.jpg",
                "https://cdn.pastaxi-manager.onepas.vn/Content/Uploads/Prices/nguyenhuong/brilliant/3.jpg",
                "https://cdn.pastaxi-manager.onepas.vn/Content/Uploads/Prices/nguyenhuong/brilliant/2.jpg",
                "https://cdn.pastaxi-manager.onepas.vn/Content/Uploads/Prices/nguyenhuong/brilliant/1.jpg",
                "https://pasgo.vn/Upload/anh-chi-tiet/nha-hang-pho-bien-nguyen-tat-thanh-4-normal-466712229281.webp",
                "https://pasgo.vn/Upload/anh-chi-tiet/nha-hang-pho-bien-nguyen-tat-thanh-5-normal-466713529282.webp",
                "https://pasgo.vn/Upload/anh-chi-tiet/nha-hang-pho-bien-nguyen-tat-thanh-6-normal-466715429283.webp",
                "https://pasgo.vn/Upload/anh-chi-tiet/nha-hang-pho-bien-nguyen-tat-thanh-3-normal-466711829280.webp",
                "https://pasgo.vn/Upload/anh-chi-tiet/nha-hang-pho-bien-nguyen-tat-thanh-2-normal-466711629279.webp",
                "https://cdn.pastaxi-manager.onepas.vn/Content/Uploads/Prices/vuvu/MENU/congviencavoi/cong-vien-nha-hang-ca-voi-vo-nguyen-giap-9.jpg",
                "https://cdn.pastaxi-manager.onepas.vn/Content/Uploads/Prices/vuvu/MENU/congviencavoi/cong-vien-nha-hang-ca-voi-vo-nguyen-giap-8.jpg",
                "https://cdn.pastaxi-manager.onepas.vn/Content/Uploads/Prices/vuvu/MENU/congviencavoi/cong-vien-nha-hang-ca-voi-vo-nguyen-giap-5.jpg",
                "https://cdn.pastaxi-manager.onepas.vn/Content/Uploads/Prices/vuvu/MENU/congviencavoi/cong-vien-nha-hang-ca-voi-vo-nguyen-giap-6.jpg",
                "https://cdn.pastaxi-manager.onepas.vn/Content/Uploads/Prices/vuvu/MENU/congviencavoi/cong-vien-nha-hang-ca-voi-vo-nguyen-giap-7.jpg",
                "https://cdn.pastaxi-manager.onepas.vn/Content/Uploads/Prices/vuvu/MENU/congviencavoi/cong-vien-nha-hang-ca-voi-vo-nguyen-giap-4.jpg",
                "https://cdn.pastaxi-manager.onepas.vn/Content/Uploads/Prices/vuvu/MENU/congviencavoi/cong-vien-nha-hang-ca-voi-vo-nguyen-giap-3.jpg",
                "https://cdn.pastaxi-manager.onepas.vn/Content/Uploads/Prices/vuvu/MENU/congviencavoi/cong-vien-nha-hang-ca-voi-vo-nguyen-giap-2.jpg",
                "https://cdn.pastaxi-manager.onepas.vn/Content/Uploads/Prices/vuvu/MENU/congviencavoi/cong-vien-nha-hang-ca-voi-vo-nguyen-giap-1.jpg",
                "https://cdn.pastaxi-manager.onepas.vn/Content/Uploads/Prices/vuvu/MENU/congviencavoi/cong-vien-nha-hang-ca-voi-vo-nguyen-giap-10.jpg",
                "https://cdn.pastaxi-manager.onepas.vn/Content/Uploads/Prices/vuvu/MENU/congviencavoi/cong-vien-nha-hang-ca-voi-vo-nguyen-giap-18.jpg",
                "https://cdn.pastaxi-manager.onepas.vn/Content/Uploads/Prices/vuvu/MENU/congviencavoi/cong-vien-nha-hang-ca-voi-vo-nguyen-giap-17.jpg",
                "https://cdn.pastaxi-manager.onepas.vn/Content/Uploads/Prices/vuvu/MENU/congviencavoi/cong-vien-nha-hang-ca-voi-vo-nguyen-giap-16.jpg",
                "https://cdn.pastaxi-manager.onepas.vn/Content/Uploads/Prices/vuvu/MENU/congviencavoi/cong-vien-nha-hang-ca-voi-vo-nguyen-giap-15.jpg",
                "https://cdn.pastaxi-manager.onepas.vn/Content/Uploads/Prices/vuvu/MENU/congviencavoi/cong-vien-nha-hang-ca-voi-vo-nguyen-giap-14.jpg",
                "https://cdn.pastaxi-manager.onepas.vn/Content/Uploads/Prices/vuvu/MENU/congviencavoi/cong-vien-nha-hang-ca-voi-vo-nguyen-giap-13.jpg",
                "https://cdn.pastaxi-manager.onepas.vn/Content/Uploads/Prices/vuvu/MENU/congviencavoi/cong-vien-nha-hang-ca-voi-vo-nguyen-giap-12.jpg",
                "https://cdn.pastaxi-manager.onepas.vn/Content/Uploads/Prices/vuvu/MENU/congviencavoi/cong-vien-nha-hang-ca-voi-vo-nguyen-giap-11.jpg",
            };

            var notes = new List<string>()
            {
                "Giảm 10% cho hóa đơn từ 500k",
                "Giảm 20% cho hóa đơn từ 1 triệu",
                "Giảm 30% cho hóa đơn từ 2 triệu",
                "Giảm 40% cho hóa đơn từ 3 triệu",
                "Giảm 50% cho hóa đơn từ 4 triệu",
                "Giảm 60% cho hóa đơn từ 5 triệu"
            };

            var phoneNumbers = new List<string>()
            {
                "0987892321",
                "0932654322",
                "0987654223",
                "0327254324",
                "0912354325",
                "0787654326"
            };

            var specialDishes = new List<string>()
            {
                "Bún bò Huế",
                "Bún chả",
                "Bún đậu mắm tôm",
                "Bún riêu",
                "Bún thịt nướng",
                "Bún ốc",
                "Bún mắm",
                "Bún mọc",
                "Bún măng vịt"
            };
                var wards = context.WardOrCommunes.ToList();

            Random random = new Random();
            var passwordSalt = SecurityFunction.GenerateRandomString();
            var customer = new User()
            {
                CreatedDate = random.Next(1, 100) % 2 == 0 ? DateTimeOffset.Now.AddMinutes(random.Next(1, 100)) : DateTimeOffset.Now.AddDays(-random.Next(1, 100)),
                PasswordSalt = passwordSalt,
                HashedPassword = SecurityFunction.HashPassword("string", passwordSalt),
                FullName = "Lê Văn C",
                PhoneNumber = "03182491223",
                Email = "tranngoctin77@gmail.com",
                Role = Role.Customer,
                Status = UserStatus.Active
            };

            var admin = new User()
            {
                CreatedDate = random.Next(1, 100) % 2 == 0 ? DateTimeOffset.Now.AddMinutes(random.Next(1, 100)) : DateTimeOffset.Now.AddDays(-random.Next(1, 100)),
                PasswordSalt = passwordSalt,
                HashedPassword = SecurityFunction.HashPassword("string", passwordSalt),
                FullName = "Nguyễn Văn B",
                PhoneNumber = "08213798224",
                Email = "tranngoctin7777@gmail.com",
                Role = Role.Admin,
                Status = UserStatus.Active
            };

            var restaurantOwner = new User()
            {
                CreatedDate = random.Next(1, 100) % 2 == 0 ? DateTimeOffset.Now.AddMinutes(random.Next(1, 100)) : DateTimeOffset.Now.AddDays(-random.Next(1, 100)),
                PasswordSalt = passwordSalt,
                HashedPassword = SecurityFunction.HashPassword("string", passwordSalt),
                FullName = "Nguyễn Văn A",
                PhoneNumber = "0921873821",
                Email = "vuanhtran09@gmail.com",
                Role = Role.RestaurantOwner,
                Restaurant = new Restaurant
                {
                    CreatedDate = random.Next(1, 100) % 2 == 0 ? DateTimeOffset.Now.AddMinutes(random.Next(1, 100)) : DateTimeOffset.Now.AddDays(-random.Next(1, 100)),
                    Name = "Nhà hàng Biển Cả",
                    Phone = phoneNumbers[random.Next(phoneNumbers.Count)],
                    PriceRangePerPerson = allPriceRangePerPersons[random.Next(allPriceRangePerPersons.Count)],
                    Capacity = random.Next(10, 200),
                    SpecialDishes = specialDishes[random.Next(specialDishes.Count)],
                    Description =  @"<div class=""summary"">
    <h2>Tóm tắt</h2>
    <div class=""summary-content"">
<link href=""https://pastaxi-manager.onepas.vn/content/style-special.css"" rel=""""stylesheet"" type=""text/css""><div class=""row-app""><div class=""col-xs-app-12""><div class=""txt-title"">Phù hợp:</div><div class=""text-description"">Gặp mặt, bạn bè, gia đình, sinh nhật… </div><div class=""txt-title"">Món đặc sắc:</div><div class=""text-description"">Combo lẩu Hongkong số 2, Combo lẩu Hongkong số 3, Bánh bao kim sa, Há cảo tôm, Mỳ xíu,  Xíu mại, Há cảo rau, Hoành thánh chiên…</div><div class=""txt-title"">Không gian:</div><div class=""text-description""><p>- Hiện đại </p><p>- Sức chứa: 80 khách</p><p>- Không gian riêng (có vách ngăn): 10 khách</p></div><div class=""txt-title"">Chỗ để xe:</div><div class=""text-description""><p>- Xe ô tô: Trước cửa nhà hàng (Miễn phí)
</p><p>- Xe máy: Trước cửa nhà hàng (Miễn phí)
</p></div><div class=""txt-title"">Điểm đặc trưng:</div><div class=""text-description""><p>-	Nằm tại vị trí đắc địa trên con đường ven sông Hàn đẹp nhất TP. Đà Nẵng. </p><p>-	Bữa tiệc ẩm thực Hồng Kông đặc sắc và chất lượng. 
</p></div></div>    </div>
</div>                </div>",
                    Note = notes[random.Next(notes.Count)], 
                    Status = RestaurantStatus.Active,
                    ReservationCount = random.Next(0, 100),
                    Rating = 0,
                    Location = new Location()
                {
                    Address = random.Next(1, 300).ToString() + " " + "Đường " + "Nguyễn Văn " +  (char)('A' + new Random().Next(0, 26)),
                    WardOrCommuneId = wards[random.Next(wards.Count)].Id
                },
                    
                    Images = new List<RestaurantImage>()
            {
                new RestaurantImage() { ImageType = ImageType.Menu, URL = menuImages[random.Next(menuImages.Count)] },
                new RestaurantImage() { ImageType = ImageType.Menu, URL = menuImages[random.Next(menuImages.Count)] },
                new RestaurantImage() { ImageType = ImageType.Menu, URL = menuImages[random.Next(menuImages.Count)] },
                new RestaurantImage() { ImageType = ImageType.Menu, URL = menuImages[random.Next(menuImages.Count)] },
                new RestaurantImage() { ImageType = ImageType.Restaurant, URL = restaurantImages[random.Next(restaurantImages.Count)] },
                new RestaurantImage() { ImageType = ImageType.Restaurant, URL = restaurantImages[random.Next(restaurantImages.Count)] },
                new RestaurantImage() { ImageType = ImageType.Restaurant, URL = restaurantImages[random.Next(restaurantImages.Count)] },
                new RestaurantImage() { ImageType = ImageType.Restaurant, URL = restaurantImages[random.Next(restaurantImages.Count)] },
                new RestaurantImage() { ImageType = ImageType.Restaurant, URL = restaurantImages[random.Next(restaurantImages.Count)] },
                new RestaurantImage() { ImageType = ImageType.Restaurant, URL = restaurantImages[random.Next(restaurantImages.Count)] },
            },
                    RestaurantCuisineTypes = new List<RestaurantCuisineType>()
            {
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
                        new RestaurantAdditionalService() { AdditionalService = allAdditionalServices[random.Next(allAdditionalServices.Count)] },
                    },
                    RestaurantCustomerTypes = new List<RestaurantCustomerType>()
                    {
                        new RestaurantCustomerType() { CustomerType = allCustomerTypes[random.Next(allCustomerTypes.Count)] },
                    },
                    BusinessHours = new List<BusinessHour>()
            {
                new BusinessHour() { Id = new Guid(), DayOfWeek = DayOfWeek.Monday, OpenTime = new TimeSpan(random.Next(5, 10), 0, 0), CloseTime = new TimeSpan(random.Next(20, 24), 0, 0) },
                new BusinessHour() { Id = new Guid(), DayOfWeek  = DayOfWeek.Tuesday, OpenTime = new TimeSpan(random.Next(5, 10), 0, 0), CloseTime = new TimeSpan(random.Next(20, 24), 0, 0) },
                new BusinessHour() { Id = new Guid(), DayOfWeek  = DayOfWeek.Wednesday, OpenTime = new TimeSpan(random.Next(5, 10), 0, 0), CloseTime = new TimeSpan(random.Next(20, 24), 0, 0) },
                new BusinessHour() { Id = new Guid(), DayOfWeek  = DayOfWeek.Thursday, OpenTime = new TimeSpan(random.Next(5, 10), 0, 0), CloseTime = new TimeSpan(random.Next(20, 24), 0, 0) },
                new BusinessHour() { Id = new Guid(), DayOfWeek  = DayOfWeek.Friday, OpenTime = new TimeSpan(random.Next(5, 10), 0, 0), CloseTime = new TimeSpan(random.Next(20, 24), 0, 0) },
                new BusinessHour() { Id = new Guid(), DayOfWeek  = DayOfWeek.Saturday, OpenTime = new TimeSpan(random.Next(5, 10), 0, 0), CloseTime = new TimeSpan(random.Next(20, 24), 0, 0) },
                new BusinessHour() { Id = new Guid(), DayOfWeek  = DayOfWeek.Sunday, OpenTime = new TimeSpan(random.Next(5, 10), 0, 0), CloseTime = new TimeSpan(random.Next(20, 24), 0, 0) }
            }
                }

            };
            for (int i = 1; i < 50; i++)
            {
                var user = new User();
                user.CreatedDate = random.Next(1, 100) % 2 == 0 ? DateTimeOffset.Now.AddMinutes(random.Next(1, 100)) : DateTimeOffset.Now.AddDays(-random.Next(1, 100));
                user.PasswordSalt = passwordSalt;
                user.HashedPassword = SecurityFunction.HashPassword("string", passwordSalt);
                user.FullName = "Tên người dùng" + i.ToString();
                user.PhoneNumber = "09129812" + i.ToString();
                user.Email = "mail" + i.ToString() + "@mail.com";
                user.Role = Role.RestaurantOwner;
                user.Restaurant = new Restaurant
                {
                    CreatedDate = random.Next(1, 100) % 2 == 0 ? DateTimeOffset.Now.AddMinutes(random.Next(1, 100)) : DateTimeOffset.Now.AddDays(-random.Next(1, 100)),
                    Name = "Nhà hàng " + i,
                    Phone = phoneNumbers[random.Next(phoneNumbers.Count)],
                    PriceRangePerPerson = allPriceRangePerPersons[random.Next(allPriceRangePerPersons.Count)],
                    Capacity = random.Next(10, 200),
                    SpecialDishes = specialDishes[random.Next(specialDishes.Count)],
                    Description =  @"<div class=""summary"">
    <h2>Tóm tắt</h2>
    <div class=""summary-content"">
<link href=""https://pastaxi-manager.onepas.vn/content/style-special.css"" rel=""""stylesheet"" type=""text/css""><div class=""row-app""><div class=""col-xs-app-12""><div class=""txt-title"">Phù hợp:</div><div class=""text-description"">Gặp mặt, bạn bè, gia đình, sinh nhật… </div><div class=""txt-title"">Món đặc sắc:</div><div class=""text-description"">Combo lẩu Hongkong số 2, Combo lẩu Hongkong số 3, Bánh bao kim sa, Há cảo tôm, Mỳ xíu,  Xíu mại, Há cảo rau, Hoành thánh chiên…</div><div class=""txt-title"">Không gian:</div><div class=""text-description""><p>- Hiện đại </p><p>- Sức chứa: 80 khách</p><p>- Không gian riêng (có vách ngăn): 10 khách</p></div><div class=""txt-title"">Chỗ để xe:</div><div class=""text-description""><p>- Xe ô tô: Trước cửa nhà hàng (Miễn phí)
</p><p>- Xe máy: Trước cửa nhà hàng (Miễn phí)
</p></div><div class=""txt-title"">Điểm đặc trưng:</div><div class=""text-description""><p>-	Nằm tại vị trí đắc địa trên con đường ven sông Hàn đẹp nhất TP. Đà Nẵng. </p><p>-	Bữa tiệc ẩm thực Hồng Kông đặc sắc và chất lượng. 
</p></div></div>    </div>
</div>                </div>",
                    Note = notes[random.Next(notes.Count)], 
                    Status = RestaurantStatus.Active,
                    ReservationCount = random.Next(0, 100),
                    Rating = 0,
                    Location = new Location()
                {
                    Address = random.Next(1, 300).ToString() + " " + "Đường " + "Nguyễn Văn " +  (char)('A' + new Random().Next(0, 26)),
                    WardOrCommuneId = wards[random.Next(wards.Count)].Id

                },
                    
                    Images = new List<RestaurantImage>()
            {
                new RestaurantImage() { ImageType = ImageType.Menu, URL = menuImages[random.Next(menuImages.Count)] },
                new RestaurantImage() { ImageType = ImageType.Menu, URL = menuImages[random.Next(menuImages.Count)] },
                new RestaurantImage() { ImageType = ImageType.Menu, URL = menuImages[random.Next(menuImages.Count)] },
                new RestaurantImage() { ImageType = ImageType.Menu, URL = menuImages[random.Next(menuImages.Count)] },
                new RestaurantImage() { ImageType = ImageType.Restaurant, URL = restaurantImages[random.Next(restaurantImages.Count)] },
                new RestaurantImage() { ImageType = ImageType.Restaurant, URL = restaurantImages[random.Next(restaurantImages.Count)] },
                new RestaurantImage() { ImageType = ImageType.Restaurant, URL = restaurantImages[random.Next(restaurantImages.Count)] },
                new RestaurantImage() { ImageType = ImageType.Restaurant, URL = restaurantImages[random.Next(restaurantImages.Count)] },
                new RestaurantImage() { ImageType = ImageType.Restaurant, URL = restaurantImages[random.Next(restaurantImages.Count)] },
                new RestaurantImage() { ImageType = ImageType.Restaurant, URL = restaurantImages[random.Next(restaurantImages.Count)] },
            },
                    RestaurantCuisineTypes = new List<RestaurantCuisineType>()
            {
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
                        new RestaurantAdditionalService() { AdditionalService = allAdditionalServices[random.Next(allAdditionalServices.Count)] },
                    },
                    RestaurantCustomerTypes = new List<RestaurantCustomerType>()
                    {
                        new RestaurantCustomerType() { CustomerType = allCustomerTypes[random.Next(allCustomerTypes.Count)] },
                    },
                    BusinessHours = new List<BusinessHour>()
            {
                new BusinessHour() { Id = new Guid(), DayOfWeek = DayOfWeek.Monday, OpenTime = new TimeSpan(random.Next(5, 10), 0, 0), CloseTime = new TimeSpan(random.Next(20, 24), 0, 0) },
                new BusinessHour() { Id = new Guid(), DayOfWeek  = DayOfWeek.Tuesday, OpenTime = new TimeSpan(random.Next(5, 10), 0, 0), CloseTime = new TimeSpan(random.Next(20, 24), 0, 0) },
                new BusinessHour() { Id = new Guid(), DayOfWeek  = DayOfWeek.Wednesday, OpenTime = new TimeSpan(random.Next(5, 10), 0, 0), CloseTime = new TimeSpan(random.Next(20, 24), 0, 0) },
                new BusinessHour() { Id = new Guid(), DayOfWeek  = DayOfWeek.Thursday, OpenTime = new TimeSpan(random.Next(5, 10), 0, 0), CloseTime = new TimeSpan(random.Next(20, 24), 0, 0) },
                new BusinessHour() { Id = new Guid(), DayOfWeek  = DayOfWeek.Friday, OpenTime = new TimeSpan(random.Next(5, 10), 0, 0), CloseTime = new TimeSpan(random.Next(20, 24), 0, 0) },
                new BusinessHour() { Id = new Guid(), DayOfWeek  = DayOfWeek.Saturday, OpenTime = new TimeSpan(random.Next(5, 10), 0, 0), CloseTime = new TimeSpan(random.Next(20, 24), 0, 0) },
                new BusinessHour() { Id = new Guid(), DayOfWeek  = DayOfWeek.Sunday, OpenTime = new TimeSpan(random.Next(5, 10), 0, 0), CloseTime = new TimeSpan(random.Next(20, 24), 0, 0) }
            }
                };
                context.Users.Add(user);
            }

            context.Users.Add(customer);
            context.Users.Add(admin);
            context.Users.Add(restaurantOwner);

            context.SaveChanges();
        }

        public static void SeedReservations(DataContext context)
        {
            if (context.Reservations.Any())
            {
                return;
            }
            var random = new Random();

            var restaurant = context.Restaurants.Include(x => x.Owner).Where(x => x.Owner.Email == "vuanhtran09@gmail.com").FirstOrDefault();

            for (int i = 1; i <= 20; i++)
            {
                var user = new User
                {
                    FullName = $"Khách hàng {i}",
                    Email = $"email{i}@example.com",
                    PhoneNumber = $"098765432{i}",
                    Role = Role.Customer,
                    HashedPassword = $"hashedpassword{i}",
                    PasswordSalt = $"salt{i}",
                    Status = UserStatus.Active
                };

                context.Users.Add(user);

                var reservation = new Reservation
                {
                    ReservationTime = random.Next(1, 100) % 2 == 0 ? DateTimeOffset.Now.AddMinutes(random.Next(1, 100)) : DateTimeOffset.Now.AddDays(random.Next(1, 100)),
                    CustomerName = user.FullName,
                    CustomerPhone = user.PhoneNumber,
                    NumberOfAdults = random.Next(0, 10),
                    NumberOfChildren = random.Next(0, 10),
                    User = user,
                    Restaurant = restaurant,
                    ReservationStatus = random.Next(0, 2) == 0 ? ReservationStatus.Confirmed : ReservationStatus.Pending,
                    CreatedDate = random.Next(1, 100) % 2 == 0 ? DateTimeOffset.Now.AddMinutes(random.Next(1, 100)) : DateTimeOffset.Now.AddDays(-random.Next(1, 100))
                };

                context.Reservations.Add(reservation);
            }

            var user1 = context.Users.FirstOrDefault(x => x.Email == "tranngoctin77@gmail.com");

            var restaurants = context.Restaurants.ToList();

            for (int i = 1; i <= 20; i++)
            {
                var reservation = new Reservation
                {
                    ReservationTime = DateTimeOffset.Now.AddDays(i),
                    CustomerName = user1.FullName,
                    CustomerPhone = user1.PhoneNumber,
                    NumberOfAdults = random.Next(0, 10),
                    NumberOfChildren = random.Next(0, 10),
                    User = user1,
                    Restaurant = restaurants[random.Next(restaurants.Count)],
                    ReservationStatus = random.Next(0, 2) == 0 ? ReservationStatus.Confirmed : ReservationStatus.Pending,
                    CreatedDate = random.Next(1, 100) % 2 == 0 ? DateTimeOffset.Now.AddMinutes(random.Next(1, 100)) : DateTimeOffset.Now.AddDays(-random.Next(1, 100))
                };

                context.Reservations.Add(reservation);
            }

            context.SaveChanges();
        }

        public static void SeedReviewComments(DataContext context)
        {
            if (context.ReviewComments.Any())
            {
                return;
            }

            var listReviewContent = new List<string>()
            {
                "Món ăn ngon, giá cả hợp lý",
                "Nhân viên phục vụ tận tình",
                "Không gian quán đẹp, sạch sẽ",
                "Món ăn không ngon, giá cả đắt",
                "Nhân viên phục vụ không tốt",
                "Không gian quán không đẹp, không sạch sẽ",
                "Món ăn ngon, giá cả hợp lý",
                "Nhân viên phục vụ tận tình",
                "Không gian quán đẹp, sạch sẽ",
                "Món ăn không ngon, giá cả đắt"
            };

            var customers = context.Users.Where(u => u.Role == Role.Customer).ToList();

            var restaurants = context.Restaurants.ToList();

            var random = new Random();

            foreach (var customer in customers)
            {
                foreach (var restaurant in restaurants)
                {
                    for (int i = 1; i <= 1; i++)
                    {
                        var reviewComment = new ReviewComment
                        {
                            Content = listReviewContent[random.Next(listReviewContent.Count)],
                            Rating = (short)(random.Next(1, 6)),
                            User = customer,
                            Restaurant = restaurant,
                            CreatedDate = random.Next(1, 100) % 2 == 0 ? DateTimeOffset.Now.AddMinutes(random.Next(1, 100)) : DateTimeOffset.Now.AddDays(-random.Next(1, 100)
                                                       )
                        };

                        context.ReviewComments.Add(reviewComment);
                    }
                }
            }
            context.SaveChanges();

            foreach (var restaurant in restaurants)
            {
                var reviews = context.ReviewComments.Where(r => r.RestaurantId == restaurant.Id).ToList();
                if (reviews.Count > 0)
                {
                    restaurant.Rating = (short)Math.Round(reviews.Average(r => r.Rating));
                }

                context.Update(restaurant);

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