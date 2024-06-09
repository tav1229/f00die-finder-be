using f00die_finder_be.Common;
using f00die_finder_be.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

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

            context.CuisineTypes.Add(new CuisineType { Name = "Lẩu", IconUrl = "https://pastaxi-manager.onepas.vn/Upload/DanhMucHienThi/Avatar/638441023317916801-icon-lau.png" });
            context.CuisineTypes.Add(new CuisineType { Name = "Nướng", IconUrl = "https://pastaxi-manager.onepas.vn/Upload/DanhMucHienThi/Avatar/638441027537096717-icon-nuong.png" });
            context.CuisineTypes.Add(new CuisineType { Name = "Lẩu-Nướng" });
            context.CuisineTypes.Add(new CuisineType { Name = "Quán chay", IconUrl = "https://pastaxi-manager.onepas.vn/Upload/DanhMucHienThi/Avatar/638011884827831221-mon-chay-pasgo.png" });
            context.CuisineTypes.Add(new CuisineType { Name = "Quán nhậu", IconUrl = "https://pastaxi-manager.onepas.vn/Upload/DanhMucHienThi/Avatar/638011851008641451-quan-nhau-pasgo.png" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món Việt", IconUrl = "https://pastaxi-manager.onepas.vn/Upload/DanhMucHienThi/Avatar/638011878657732280-mon-viet-pasgo.png" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món ăn miền Bắc" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món ăn miền Trung" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món ăn miền Nam" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món Nhật", IconUrl = "https://pastaxi-manager.onepas.vn/Upload/DanhMucHienThi/Avatar/638011867970619295-mon-nhat-ban-pasgo.png" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món Hàn", IconUrl = "https://pastaxi-manager.onepas.vn/Upload/DanhMucHienThi/Avatar/638442738231563544-mon-han.png" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món Thái" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món Trung" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món Á", IconUrl = "https://pastaxi-manager.onepas.vn/Upload/DanhMucHienThi/Avatar/638239778553118285-mon-chau-a.png" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món Âu", IconUrl = "https://pastaxi-manager.onepas.vn/Upload/DanhMucHienThi/Avatar/638239806231357799-mon-chau-au.png" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món Ý" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món Mỹ" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món Pháp" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món Singapore" });
            context.CuisineTypes.Add(new CuisineType { Name = "Món Nga" });
            context.CuisineTypes.Add(new CuisineType { Name = "Đặc sản" });
            context.CuisineTypes.Add(new CuisineType { Name = "Hải sản", IconUrl = "https://pastaxi-manager.onepas.vn/Upload/DanhMucHienThi/Avatar/638011847245865102-hai-san-pasgo.png" });

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
                "https://storage.pasgo.com.vn/PasGoGianHang/2261f583-857f-4f9b-b305-47e9ab151517.webp?Width=280&Type=webp",
                "https://storage.pasgo.com.vn/PasGoGianHang/9c98749c-123a-4f09-b010-3177af74e264.webp?Width=280&Type=webp",
                "https://storage.pasgo.com.vn/PasGoGianHang/1cf4cccb-c75f-4c9f-9013-1fe44048ed6a.webp?Width=280&Type=webp",
                "https://storage.pasgo.com.vn/PasGoGianHang/88dcc2a2-1568-4ac8-b72e-0c582c1ebf24.webp?Width=280&Type=webp",
                "https://storage.pasgo.com.vn/PasGoGianHang/88dcc2a2-1568-4ac8-b72e-0c582c1ebf24.webp?Width=280&Type=webp",
                "https://storage.pasgo.com.vn/PasGoGianHang/b3f709a3-80a8-41fa-a701-44badaaf443e.webp?Width=280&Type=webp",
                "https://storage.pasgo.com.vn/PasGoGianHang/a171ccc7-32d5-4207-8689-259d9d926c28.webp?Width=280&Type=webp",
                "https://storage.pasgo.com.vn/PasGoGianHang/4c3500b1-9adf-4f08-9bd1-0e59a9c6654c.webp?Width=280&Type=webp",
                "https://storage.pasgo.com.vn/PasGoGianHang/5593ef8f-6781-471a-b24d-037a0907f224.webp?Width=280&Type=webp",
                "https://storage.pasgo.com.vn/PasGoGianHang/25879c45-c27d-446d-b6eb-e9d82d9a86ae.webp",
                "https://storage.pasgo.com.vn/PasGoGianHang/ead3550d-ef8b-45c3-8055-81e70047569c.webp",
                "https://storage.pasgo.com.vn/PasGoGianHang/1ef4d0c0-9523-44a6-887d-5528838be6fc.webp",
                "https://storage.pasgo.com.vn/PasGoGianHang/5593ef8f-6781-471a-b24d-037a0907f224.webp",
                "https://pastaxi-manager.onepas.vn/Upload/DoiTuong/Banner/638384481343377175-7f44618c-fdc9-48f9-842a-003cfe2f3ed3.png",
                "https://pastaxi-manager.onepas.vn/Upload/DoiTuong/Banner/638384481343367166-758a3891-44d7-4fb9-b151-66cc6ed7ba58.png",
                "https://pastaxi-manager.onepas.vn/Upload/DoiTuong/Banner/638384481343367166-0b198136-edb1-4692-92c5-90620392bfbf.png",
                "https://storage.pasgo.com.vn/PasGoGianHang/50aed043-17c2-43e5-8fae-b8eee422f458.webp",
                "https://storage.pasgo.com.vn/PasGoGianHang/ead3550d-ef8b-45c3-8055-81e70047569c.webp",
                "https://storage.pasgo.com.vn/PasGoGianHang/d99700e6-ed68-46cb-bc73-8c5868927932.webp",
                "https://storage.pasgo.com.vn/PasGoGianHang/a4ef2a45-52bf-415c-aa04-a9c20c268f54.webp",
                "https://storage.pasgo.com.vn/PasGoGianHang/833a5bad-9ec6-4e78-98d9-cb4caa0f2a93.webp",
                "https://storage.pasgo.com.vn/PasGoGianHang/5e2f84df-c16d-4fa8-85e3-f258e8229f78.webp",
                "https://storage.pasgo.com.vn/PasGoGianHang/25879c45-c27d-446d-b6eb-e9d82d9a86ae.webp",
                "https://storage.pasgo.com.vn/PasGoGianHang/e8983735-2ebe-480e-8f62-f51b1168386e.webp"
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
            for (int i = 1; i < 40; i++)
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
                    ReservationTime = DateTimeOffset.Now.AddDays(i),
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