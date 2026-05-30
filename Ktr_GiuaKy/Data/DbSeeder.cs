using System;
using System.Linq;
using Ktr_GiuaKy.Data;
using Ktr_GiuaKy.Models;

namespace Ktr_GiuaKy.Data
{
    public static class DbSeeder
    {
        public static void Seed(ComputerStoreContext context)
        {
            context.Database.EnsureCreated();

            // 1. Seed Brands individually if they don't exist (10 major brands from user filter bar)
            var brandsToSeed = new[]
            {
                new { Name = "Dell", Country = "USA" },
                new { Name = "Asus", Country = "Taiwan" },
                new { Name = "HP", Country = "USA" },
                new { Name = "Apple", Country = "USA" },
                new { Name = "MSI", Country = "Taiwan" },
                new { Name = "Lenovo", Country = "China" },
                new { Name = "Acer", Country = "Taiwan" },
                new { Name = "Gigabyte", Country = "Taiwan" },
                new { Name = "Microsoft", Country = "USA" },
                new { Name = "LG", Country = "South Korea" }
            };

            foreach (var b in brandsToSeed)
            {
                if (!context.Brands.Any(x => x.Name == b.Name))
                {
                    context.Brands.Add(new Brand { Name = b.Name, Country = b.Country, Status = "active" });
                }
            }
            context.SaveChanges();

            // 2. Seed Categories individually if they don't exist
            var categoriesToSeed = new[]
            {
                new { Name = "Laptops", Desc = "Portable personal computers" },
                new { Name = "Desktops", Desc = "Stationary personal computers" },
                new { Name = "Gaming PCs", Desc = "High-performance computers for gaming" },
                new { Name = "Workstations", Desc = "Professional-grade computers" }
            };

            foreach (var c in categoriesToSeed)
            {
                if (!context.Categories.Any(x => x.Name == c.Name))
                {
                    context.Categories.Add(new Category { Name = c.Name, Description = c.Desc, Status = "active" });
                }
            }
            context.SaveChanges();

            // Fetch seeded Brands and Categories
            var dell = context.Brands.FirstOrDefault(b => b.Name == "Dell");
            var asus = context.Brands.FirstOrDefault(b => b.Name == "Asus");
            var apple = context.Brands.FirstOrDefault(b => b.Name == "Apple");
            var hp = context.Brands.FirstOrDefault(b => b.Name == "HP");
            var lenovo = context.Brands.FirstOrDefault(b => b.Name == "Lenovo");
            var acer = context.Brands.FirstOrDefault(b => b.Name == "Acer");
            var msi = context.Brands.FirstOrDefault(b => b.Name == "MSI");
            var gigabyte = context.Brands.FirstOrDefault(b => b.Name == "Gigabyte");
            var microsoft = context.Brands.FirstOrDefault(b => b.Name == "Microsoft");
            var lg = context.Brands.FirstOrDefault(b => b.Name == "LG");

            var laptops = context.Categories.FirstOrDefault(c => c.Name == "Laptops") ?? context.Categories.First();
            var gaming = context.Categories.FirstOrDefault(c => c.Name == "Gaming PCs") ?? laptops;
            var desktops = context.Categories.FirstOrDefault(c => c.Name == "Desktops") ?? laptops;
            var workstations = context.Categories.FirstOrDefault(c => c.Name == "Workstations") ?? laptops;

            // Helper function to seed safely check-by-check
            void AddProductIfNotExist(Product product)
            {
                if (!context.Products.Any(p => p.Sku == product.Sku || p.Name == product.Name))
                {
                    context.Products.Add(product);
                }
            }

            // ==================== SEEDING PRODUCTS FOR EACH BRAND ====================

            // --- DELL (At least 3 products) ---
            if (dell != null)
            {
                AddProductIfNotExist(new Product
                {
                    Sku = "DELL-XPS-13-NEW",
                    Name = "Dell XPS 13 9315 Elite",
                    Slug = "dell-xps-13-9315-elite",
                    BrandId = dell.Id,
                    CategoryId = laptops.Id,
                    Price = 24990000m,
                    CostPrice = 21000000m,
                    Quantity = 15,
                    Status = "active",
                    ShortDescription = "Ultra-thin premium laptop with 12th Gen Intel Core i5.",
                    FullDescription = "CPU: Intel Core i5-1230U | RAM: 8GB LPDDR5 | SSD: 512GB NVMe | Screen: 13.4\" FHD+ InfinityEdge | GPU: Intel Iris Xe Graphics | OS: Windows 11 Home | Weight: 1.17 kg",
                    Featured = true,
                    MainImage = "https://images.unsplash.com/photo-1593642632823-8f785ba67e45?q=80&w=600&auto=format&fit=crop"
                });

                AddProductIfNotExist(new Product
                {
                    Sku = "DELL-XPS-15",
                    Name = "Dell XPS 15 9530 Premium",
                    Slug = "dell-xps-15-9530-premium",
                    BrandId = dell.Id,
                    CategoryId = laptops.Id,
                    Price = 49990000m,
                    CostPrice = 45000000m,
                    Quantity = 5,
                    Status = "active",
                    ShortDescription = "Perfect laptop for creators with 13th Gen Intel Core i7 and RTX 4050.",
                    FullDescription = "CPU: Intel Core i7-13700H | RAM: 16GB DDR5 | SSD: 1TB NVMe PCIe 4.0 | Screen: 15.6\" FHD+ InfinityEdge | GPU: NVIDIA RTX 4050 6GB | OS: Windows 11 Home",
                    Featured = true,
                    MainImage = "https://images.unsplash.com/photo-1588872657578-7efd1f1555ed?q=80&w=600&auto=format&fit=crop"
                });

                AddProductIfNotExist(new Product
                {
                    Sku = "DELL-INS-16",
                    Name = "Dell Inspiron 16 5630",
                    Slug = "dell-inspiron-16-5630",
                    BrandId = dell.Id,
                    CategoryId = laptops.Id,
                    Price = 19490000m,
                    CostPrice = 17000000m,
                    Quantity = 12,
                    Status = "active",
                    ShortDescription = "Sleek and reliable everyday laptop with 13th Gen Intel i5.",
                    FullDescription = "CPU: Intel Core i5-1340P | RAM: 16GB LPDDR5 | SSD: 512GB NVMe PCIe | Screen: 16\" FHD+ ComfortView | GPU: Intel Iris Xe | OS: Windows 11 Home",
                    Featured = false,
                    MainImage = "https://images.unsplash.com/photo-1593642702821-c8da6771f0c6?q=80&w=600&auto=format&fit=crop"
                });
            }

            // --- ASUS (At least 3 products) ---
            if (asus != null)
            {
                AddProductIfNotExist(new Product
                {
                    Sku = "ASUS-ROG-G16-NEW",
                    Name = "ASUS ROG Strix G16 Gaming",
                    Slug = "asus-rog-strix-g16-gaming",
                    BrandId = asus.Id,
                    CategoryId = gaming.Id,
                    Price = 34490000m,
                    CostPrice = 30000000m,
                    Quantity = 5,
                    Status = "active",
                    ShortDescription = "High-end ROG gaming laptop with RTX 4060 and liquid metal cooling.",
                    FullDescription = "CPU: Intel Core i7-13650HX | RAM: 16GB DDR5 4800MHz | SSD: 512GB PCIe 4.0 NVMe | Screen: 16\" WUXGA 165Hz G-Sync | GPU: NVIDIA RTX 4060 8GB GDDR6 | OS: Windows 11 Home",
                    Featured = true,
                    MainImage = "https://images.unsplash.com/photo-1603302576837-37561b2e2302?q=80&w=600&auto=format&fit=crop"
                });

                AddProductIfNotExist(new Product
                {
                    Sku = "ASUS-ZB-14D",
                    Name = "ASUS Zenbook Pro 14 Duo OLED",
                    Slug = "asus-zenbook-pro-14-duo-oled",
                    BrandId = asus.Id,
                    CategoryId = laptops.Id,
                    Price = 42990000m,
                    CostPrice = 38000000m,
                    Quantity = 6,
                    Status = "active",
                    ShortDescription = "Revolutionary dual-screen Laptop with ScreenPad Plus for professional creators.",
                    FullDescription = "CPU: Intel Core i9-13900H | RAM: 32GB LPDDR5 | SSD: 1TB NVMe PCIe 4.0 | Screen: 14.5\" 2.8K 120Hz OLED Touch + 12.7\" ScreenPad Plus | GPU: NVIDIA RTX 4050 6GB | OS: Windows 11 Pro",
                    Featured = true,
                    MainImage = "https://images.unsplash.com/photo-1541807084-5c52b6b3adef?q=80&w=600&auto=format&fit=crop"
                });

                AddProductIfNotExist(new Product
                {
                    Sku = "ASUS-VV-15X",
                    Name = "ASUS Vivobook 15X OLED",
                    Slug = "asus-vivobook-15x-oled",
                    BrandId = asus.Id,
                    CategoryId = laptops.Id,
                    Price = 16990000m,
                    CostPrice = 14500000m,
                    Quantity = 14,
                    Status = "active",
                    ShortDescription = "Stunning 15-inch laptop with punchy OLED screen and Ryzen 5.",
                    FullDescription = "CPU: AMD Ryzen 5 5600H | RAM: 8GB DDR4 | SSD: 512GB NVMe | Screen: 15.6\" FHD OLED | GPU: AMD Radeon Graphics | OS: Windows 11 Home",
                    Featured = false,
                    MainImage = "https://images.unsplash.com/photo-1588872657578-7efd1f1555ed?q=80&w=600&auto=format&fit=crop"
                });
            }

            // --- APPLE (At least 3 products) ---
            if (apple != null)
            {
                AddProductIfNotExist(new Product
                {
                    Sku = "AAPL-MBP-14-NEW",
                    Name = "MacBook Pro 14\" M3 Max",
                    Slug = "macbook-pro-14-m3-max",
                    BrandId = apple.Id,
                    CategoryId = laptops.Id,
                    Price = 39990000m,
                    CostPrice = 35000000m,
                    Quantity = 8,
                    Status = "active",
                    ShortDescription = "Powerful MacBook Pro with groundbreaking Apple M3 chip.",
                    FullDescription = "CPU: Apple M3 8-core | RAM: 8GB Unified Memory | SSD: 512GB Superfast SSD | Screen: 14.2\" Liquid Retina XDR 120Hz | GPU: 10-core GPU | OS: macOS Sonoma | Battery: Up to 22 hours",
                    Featured = true,
                    MainImage = "https://images.unsplash.com/photo-1517336714731-489689fd1ca8?q=80&w=600&auto=format&fit=crop"
                });

                AddProductIfNotExist(new Product
                {
                    Sku = "AAPL-MBA-13",
                    Name = "MacBook Air 13\" M2",
                    Slug = "macbook-air-13-m2",
                    BrandId = apple.Id,
                    CategoryId = laptops.Id,
                    Price = 26490000m,
                    CostPrice = 22000000m,
                    Quantity = 20,
                    Status = "active",
                    ShortDescription = "Strikingly thin design with incredible speed and fanless silent performance.",
                    FullDescription = "CPU: Apple M2 8-core | RAM: 8GB Unified Memory | SSD: 256GB SSD | Screen: 13.6\" Liquid Retina TrueTone | GPU: 8-core GPU | OS: macOS | Weight: 1.24 kg",
                    Featured = false,
                    MainImage = "https://images.unsplash.com/photo-1611186871348-b1ce696e52c9?q=80&w=600&auto=format&fit=crop"
                });

                AddProductIfNotExist(new Product
                {
                    Sku = "AAPL-IMC-24",
                    Name = "Apple iMac 24\" M3",
                    Slug = "apple-imac-24-m3",
                    BrandId = apple.Id,
                    CategoryId = desktops.Id,
                    Price = 36990000m,
                    CostPrice = 33000000m,
                    Quantity = 3,
                    Status = "active",
                    ShortDescription = "Stunning all-in-one desktop computer with Apple M3 chip.",
                    FullDescription = "CPU: Apple M3 8-core | RAM: 8GB Unified Memory | SSD: 256GB SSD | Screen: 24\" 4.5K Retina Display | GPU: 10-core GPU | OS: macOS Sonoma",
                    Featured = true,
                    MainImage = "https://images.unsplash.com/photo-1542751371-adc38448a05e?q=80&w=600&auto=format&fit=crop"
                });
            }

            // --- HP (At least 3 products) ---
            if (hp != null)
            {
                AddProductIfNotExist(new Product
                {
                    Sku = "HP-SPEC-360",
                    Name = "HP Spectre x360 Convertible 14",
                    Slug = "hp-spectre-x360-convertible-14",
                    BrandId = hp.Id,
                    CategoryId = laptops.Id,
                    Price = 38490000m,
                    CostPrice = 33000000m,
                    Quantity = 4,
                    Status = "active",
                    ShortDescription = "Luxurious 2-in-1 convertible laptop with a breathtaking 3K2K OLED touch display.",
                    FullDescription = "CPU: Intel Core i7-1355U | RAM: 16GB LPDDR5 | SSD: 1TB NVMe SSD | Screen: 13.5\" 3K2K OLED Touch 360-degree flip | GPU: Intel Iris Xe | OS: Windows 11 Home | Accessories: HP Rechargeable MPP 2.0 Tilt Pen",
                    Featured = true,
                    MainImage = "https://images.unsplash.com/photo-1589561084283-930aa7b1ce50?q=80&w=600&auto=format&fit=crop"
                });

                AddProductIfNotExist(new Product
                {
                    Sku = "HP-OMEN-45L",
                    Name = "HP Omen 45L Gaming Desktop",
                    Slug = "hp-omen-45l-gaming-desktop",
                    BrandId = hp.Id,
                    CategoryId = desktops.Id,
                    Price = 64990000m,
                    CostPrice = 58000000m,
                    Quantity = 2,
                    Status = "active",
                    ShortDescription = "Extreme liquid-cooled high performance gaming rig with Omen Cryo Chamber.",
                    FullDescription = "CPU: Intel Core i9-13900K | RAM: 32GB HyperX DDR5 RGB | SSD: 2TB WD Black NVMe | GPU: NVIDIA RTX 4080 16GB GDDR6X | Cooling: 360mm Liquid Cryo Cooler | Power: 1000W 80 Plus Gold",
                    Featured = true,
                    MainImage = "https://images.unsplash.com/photo-1587202372775-e229f172b9d7?q=80&w=600&auto=format&fit=crop"
                });

                AddProductIfNotExist(new Product
                {
                    Sku = "HP-PAV-14",
                    Name = "HP Pavilion 14 Student Notebook",
                    Slug = "hp-pavilion-14-student-notebook",
                    BrandId = hp.Id,
                    CategoryId = laptops.Id,
                    Price = 15290000m,
                    CostPrice = 13500000m,
                    Quantity = 10,
                    Status = "active",
                    ShortDescription = "Compact and lightweight student/office laptop.",
                    FullDescription = "CPU: Intel Core i5-1235U | RAM: 8GB DDR4 | SSD: 512GB PCIe NVMe | Screen: 14\" FHD IPS | GPU: Intel Iris Xe | OS: Windows 11 Home",
                    Featured = false,
                    MainImage = "https://images.unsplash.com/photo-1589561084283-930aa7b1ce50?q=80&w=600&auto=format&fit=crop"
                });
            }

            // --- LENOVO (At least 3 products) ---
            if (lenovo != null)
            {
                AddProductIfNotExist(new Product
                {
                    Sku = "LENO-LEG-5P",
                    Name = "Lenovo Legion 5 Pro 16ARH7",
                    Slug = "lenovo-legion-5-pro-16arh7",
                    BrandId = lenovo.Id,
                    CategoryId = gaming.Id,
                    Price = 29990000m,
                    CostPrice = 26000000m,
                    Quantity = 10,
                    Status = "active",
                    ShortDescription = "Outstanding gaming powerhouse with 16\" QHD display and AMD Ryzen 7.",
                    FullDescription = "CPU: AMD Ryzen 7 6800H | RAM: 16GB DDR5 | SSD: 512GB PCIe 4.0 SSD | Screen: 16\" WQXGA (2560x1600) IPS 165Hz | GPU: NVIDIA RTX 3060 6GB | OS: Windows 11 Home",
                    Featured = false,
                    MainImage = "https://images.unsplash.com/photo-1588872657578-7efd1f1555ed?q=80&w=600&auto=format&fit=crop"
                });

                AddProductIfNotExist(new Product
                {
                    Sku = "LENO-X1-C11",
                    Name = "Lenovo ThinkPad X1 Carbon Gen 11",
                    Slug = "lenovo-thinkpad-x1-carbon-gen-11",
                    BrandId = lenovo.Id,
                    CategoryId = workstations.Id,
                    Price = 48990000m,
                    CostPrice = 43000000m,
                    Quantity = 5,
                    Status = "active",
                    ShortDescription = "Ultimate business class ultra-light carbon fiber workstation.",
                    FullDescription = "CPU: Intel Core i7-1360P vPro | RAM: 32GB LPDDR5 | SSD: 1TB NVMe PCIe Gen 4 | Screen: 14\" WUXGA IPS Low Power Anti-Glare | GPU: Intel Iris Xe Graphics | OS: Windows 11 Pro | Weight: 1.12 kg",
                    Featured = true,
                    MainImage = "https://images.unsplash.com/photo-1593642702821-c8da6771f0c6?q=80&w=600&auto=format&fit=crop"
                });

                AddProductIfNotExist(new Product
                {
                    Sku = "LENO-IP-3",
                    Name = "Lenovo IdeaPad Slim 3 14IAH8",
                    Slug = "lenovo-ideapad-slim-3-14iah8",
                    BrandId = lenovo.Id,
                    CategoryId = laptops.Id,
                    Price = 12490000m,
                    CostPrice = 11000000m,
                    Quantity = 15,
                    Status = "active",
                    ShortDescription = "Affordable and responsive daily driver laptop.",
                    FullDescription = "CPU: Intel Core i5-12450H | RAM: 8GB LPDDR5 | SSD: 512GB NVMe PCIe | Screen: 14\" FHD IPS | GPU: Intel UHD Graphics | OS: Windows 11 Home",
                    Featured = false,
                    MainImage = "https://images.unsplash.com/photo-1588872657578-7efd1f1555ed?q=80&w=600&auto=format&fit=crop"
                });
            }

            // --- ACER (At least 3 products) ---
            if (acer != null)
            {
                AddProductIfNotExist(new Product
                {
                    Sku = "ACER-NT-5",
                    Name = "Acer Nitro 5 Tiger AN515",
                    Slug = "acer-nitro-5-tiger-an515",
                    BrandId = acer.Id,
                    CategoryId = gaming.Id,
                    Price = 21990000m,
                    CostPrice = 19000000m,
                    Quantity = 10,
                    Status = "active",
                    ShortDescription = "Extremely popular gaming laptop with 12th Gen Core i5 and RTX 3050.",
                    FullDescription = "CPU: Intel Core i5-12500H | RAM: 8GB DDR4 | SSD: 512GB PCIe 4.0 | Screen: 15.6\" FHD IPS 144Hz | GPU: NVIDIA RTX 3050 4GB | OS: Windows 11 Home",
                    Featured = true,
                    MainImage = "https://images.unsplash.com/photo-1603302576837-37561b2e2302?q=80&w=600&auto=format&fit=crop"
                });

                AddProductIfNotExist(new Product
                {
                    Sku = "ACER-SF-GO",
                    Name = "Acer Swift Go 14 OLED Evo",
                    Slug = "acer-swift-go-14-oled-evo",
                    BrandId = acer.Id,
                    CategoryId = laptops.Id,
                    Price = 22990000m,
                    CostPrice = 20000000m,
                    Quantity = 6,
                    Status = "active",
                    ShortDescription = "Slim lightweight laptop featuring a stunning 90Hz OLED display.",
                    FullDescription = "CPU: Intel Core i5-13500H | RAM: 16GB LPDDR5 | SSD: 512GB PCIe Gen4 | Screen: 14\" 2.8K 90Hz OLED | GPU: Intel Iris Xe | OS: Windows 11 Home",
                    Featured = true,
                    MainImage = "https://images.unsplash.com/photo-1593642702821-c8da6771f0c6?q=80&w=600&auto=format&fit=crop"
                });

                AddProductIfNotExist(new Product
                {
                    Sku = "ACER-AS-3",
                    Name = "Acer Aspire 3 A315-59",
                    Slug = "acer-aspire-3-a315-59",
                    BrandId = acer.Id,
                    CategoryId = laptops.Id,
                    Price = 10490000m,
                    CostPrice = 9000000m,
                    Quantity = 18,
                    Status = "active",
                    ShortDescription = "Budget-friendly daily computing companion.",
                    FullDescription = "CPU: Intel Core i3-1215U | RAM: 8GB DDR4 | SSD: 512GB NVMe PCIe | Screen: 15.6\" FHD | GPU: Intel UHD Graphics | OS: Windows 11 Home",
                    Featured = false,
                    MainImage = "https://images.unsplash.com/photo-1588872657578-7efd1f1555ed?q=80&w=600&auto=format&fit=crop"
                });
            }

            // --- MSI (At least 3 products) ---
            if (msi != null)
            {
                AddProductIfNotExist(new Product
                {
                    Sku = "MSI-RD-78",
                    Name = "MSI Raider GE78 HX 13VI",
                    Slug = "msi-raider-ge78-hx-13vi",
                    BrandId = msi.Id,
                    CategoryId = gaming.Id,
                    Price = 89990000m,
                    CostPrice = 80000000m,
                    Quantity = 3,
                    Status = "active",
                    ShortDescription = "Absolute high-performance gaming flagship laptop with MSI Mystic Light bar.",
                    FullDescription = "CPU: Intel Core i9-13980HX | RAM: 64GB DDR5 (32GBx2) | SSD: 2TB PCIe 4.0 NVMe | Screen: 17\" QHD+ (2560x1600) 240Hz IPS-level | GPU: NVIDIA RTX 4090 16GB GDDR6 | Keyboard: SteelSeries Per-Key RGB",
                    Featured = true,
                    MainImage = "https://images.unsplash.com/photo-1607604276583-eef5d076aa5f?q=80&w=600&auto=format&fit=crop"
                });

                AddProductIfNotExist(new Product
                {
                    Sku = "MSI-CYG-15",
                    Name = "MSI Cyborg 15 A12VE",
                    Slug = "msi-cyborg-15-a12ve",
                    BrandId = msi.Id,
                    CategoryId = gaming.Id,
                    Price = 18990000m,
                    CostPrice = 16000000m,
                    Quantity = 0,
                    Status = "active",
                    ShortDescription = "Futuristic translucent design cyberpunk-inspired budget gaming laptop.",
                    FullDescription = "CPU: Intel Core i5-12450H | RAM: 8GB DDR5 4800MHz | SSD: 512GB PCIe 4.0 SSD | Screen: 15.6\" FHD (1920x1080) 144Hz | GPU: NVIDIA RTX 4050 6GB GDDR6 | OS: Windows 11 Home",
                    Featured = false,
                    MainImage = "https://images.unsplash.com/photo-1542751371-adc38448a05e?q=80&w=600&auto=format&fit=crop"
                });

                AddProductIfNotExist(new Product
                {
                    Sku = "MSI-MD-14",
                    Name = "MSI Modern 14 Ultra Slim",
                    Slug = "msi-modern-14-ultra-slim",
                    BrandId = msi.Id,
                    CategoryId = laptops.Id,
                    Price = 11990000m,
                    CostPrice = 10000000m,
                    Quantity = 8,
                    Status = "active",
                    ShortDescription = "Elegant ultra-light laptop for daily productivity.",
                    FullDescription = "CPU: Intel Core i3-1215U | RAM: 8GB DDR4 | SSD: 512GB PCIe SSD | Screen: 14\" FHD IPS-Level | GPU: Intel UHD Graphics | OS: Windows 11 Home | Weight: 1.4 kg",
                    Featured = false,
                    MainImage = "https://images.unsplash.com/photo-1593642702821-c8da6771f0c6?q=80&w=600&auto=format&fit=crop"
                });
            }

            // --- GIGABYTE (At least 3 products) ---
            if (gigabyte != null)
            {
                AddProductIfNotExist(new Product
                {
                    Sku = "GIGA-G5-GE",
                    Name = "Gigabyte G5 GE Esports Edition",
                    Slug = "gigabyte-g5-ge-esports-edition",
                    BrandId = gigabyte.Id,
                    CategoryId = gaming.Id,
                    Price = 17990000m,
                    CostPrice = 15500000m,
                    Quantity = 10,
                    Status = "active",
                    ShortDescription = "Powerful entry-level gaming laptop with 144Hz screen.",
                    FullDescription = "CPU: Intel Core i5-12500H | RAM: 8GB DDR4 | SSD: 512GB M.2 PCIe | Screen: 15.6\" FHD 144Hz | GPU: NVIDIA RTX 3050 4GB | OS: Windows 11 Home",
                    Featured = false,
                    MainImage = "https://images.unsplash.com/photo-1542751371-adc38448a05e?q=80&w=600&auto=format&fit=crop"
                });

                AddProductIfNotExist(new Product
                {
                    Sku = "GIGA-AOR-15",
                    Name = "Gigabyte AORUS 15 Elite",
                    Slug = "gigabyte-aorus-15-elite",
                    BrandId = gigabyte.Id,
                    CategoryId = gaming.Id,
                    Price = 36990000m,
                    CostPrice = 32000000m,
                    Quantity = 4,
                    Status = "active",
                    ShortDescription = "High-end professional gaming laptop with RTX 4060.",
                    FullDescription = "CPU: Intel Core i7-13700H | RAM: 16GB DDR5 | SSD: 1TB PCIe 4.0 | Screen: 15.6\" QHD 165Hz | GPU: NVIDIA RTX 4060 8GB | OS: Windows 11 Home",
                    Featured = true,
                    MainImage = "https://images.unsplash.com/photo-1607604276583-eef5d076aa5f?q=80&w=600&auto=format&fit=crop"
                });

                AddProductIfNotExist(new Product
                {
                    Sku = "GIGA-AERO-14",
                    Name = "Gigabyte Aero 14 OLED Creator",
                    Slug = "gigabyte-aero-14-oled-creator",
                    BrandId = gigabyte.Id,
                    CategoryId = laptops.Id,
                    Price = 41990000m,
                    CostPrice = 38000000m,
                    Quantity = 3,
                    Status = "active",
                    ShortDescription = "Gorgeous creators laptop with 2.8K OLED screen.",
                    FullDescription = "CPU: Intel Core i7-13700H | RAM: 16GB LPDDR5 | SSD: 1TB PCIe Gen4 | Screen: 14\" 2.8K QHD+ 90Hz OLED | GPU: NVIDIA RTX 4050 6GB | OS: Windows 11 Home",
                    Featured = true,
                    MainImage = "https://images.unsplash.com/photo-1541807084-5c52b6b3adef?q=80&w=600&auto=format&fit=crop"
                });
            }

            // --- MICROSOFT (At least 3 products) ---
            if (microsoft != null)
            {
                AddProductIfNotExist(new Product
                {
                    Sku = "MSFT-SF-PRO9",
                    Name = "Microsoft Surface Pro 9",
                    Slug = "microsoft-surface-pro-9",
                    BrandId = microsoft.Id,
                    CategoryId = laptops.Id,
                    Price = 24990000m,
                    CostPrice = 22000000m,
                    Quantity = 7,
                    Status = "active",
                    ShortDescription = "Versatile 2-in-1 touchscreen tablet with laptop power.",
                    FullDescription = "CPU: Intel Core i5-1235U | RAM: 8GB LPDDR5 | SSD: 256GB SSD | Screen: 13\" PixelSense Flow Touch 120Hz | GPU: Intel Iris Xe | OS: Windows 11 Home",
                    Featured = true,
                    MainImage = "https://images.unsplash.com/photo-1593642702821-c8da6771f0c6?q=80&w=600&auto=format&fit=crop"
                });

                AddProductIfNotExist(new Product
                {
                    Sku = "MSFT-SF-LAP5",
                    Name = "Microsoft Surface Laptop 5 Touch",
                    Slug = "microsoft-surface-laptop-5-touch",
                    BrandId = microsoft.Id,
                    CategoryId = laptops.Id,
                    Price = 28990000m,
                    CostPrice = 25000000m,
                    Quantity = 6,
                    Status = "active",
                    ShortDescription = "Sleek, elegant, and ultra-portable touch screen notebook.",
                    FullDescription = "CPU: Intel Core i5-1235U | RAM: 8GB LPDDR5 | SSD: 512GB SSD | Screen: 13.5\" PixelSense Touch | GPU: Intel Iris Xe | OS: Windows 11 Home",
                    Featured = false,
                    MainImage = "https://images.unsplash.com/photo-1589561084283-930aa7b1ce50?q=80&w=600&auto=format&fit=crop"
                });

                AddProductIfNotExist(new Product
                {
                    Sku = "MSFT-SF-STUDIO2",
                    Name = "Microsoft Surface Laptop Studio 2",
                    Slug = "microsoft-surface-laptop-studio-2",
                    BrandId = microsoft.Id,
                    CategoryId = workstations.Id,
                    Price = 56990000m,
                    CostPrice = 51000000m,
                    Quantity = 2,
                    Status = "active",
                    ShortDescription = "Ultimate creative workstation laptop with revolutionary hinge design.",
                    FullDescription = "CPU: Intel Core i7-13700H | RAM: 16GB DDR5 | SSD: 512GB Gen4 SSD | Screen: 14.4\" PixelSense Flow 120Hz Touch | GPU: NVIDIA RTX 4050 6GB | OS: Windows 11 Home",
                    Featured = true,
                    MainImage = "https://images.unsplash.com/photo-1593642632823-8f785ba67e45?q=80&w=600&auto=format&fit=crop"
                });
            }

            // --- LG (At least 3 products) ---
            if (lg != null)
            {
                AddProductIfNotExist(new Product
                {
                    Sku = "LG-GM-14",
                    Name = "LG Gram 14 Ultra Lightweight",
                    Slug = "lg-gram-14-ultra-lightweight",
                    BrandId = lg.Id,
                    CategoryId = laptops.Id,
                    Price = 25990000m,
                    CostPrice = 23000000m,
                    Quantity = 10,
                    Status = "active",
                    ShortDescription = "Incredibly lightweight business laptop weighting under 1kg.",
                    FullDescription = "CPU: Intel Core i5-1340P | RAM: 16GB LPDDR5 | SSD: 512GB PCIe Gen4 | Screen: 14\" WUXGA IPS Anti-Glare | GPU: Intel Iris Xe | OS: Windows 11 Home | Weight: 999 g",
                    Featured = false,
                    MainImage = "https://images.unsplash.com/photo-1593642702821-c8da6771f0c6?q=80&w=600&auto=format&fit=crop"
                });

                AddProductIfNotExist(new Product
                {
                    Sku = "LG-GM-16",
                    Name = "LG Gram 16 Slim Edition",
                    Slug = "lg-gram-16-slim-edition",
                    BrandId = lg.Id,
                    CategoryId = laptops.Id,
                    Price = 29990000m,
                    CostPrice = 26500000m,
                    Quantity = 8,
                    Status = "active",
                    ShortDescription = "Ultra-lightweight 16-inch laptop with a massive battery life.",
                    FullDescription = "CPU: Intel Core i5-1340P | RAM: 16GB LPDDR5 | SSD: 512GB PCIe Gen4 | Screen: 16\" WQXGA IPS | GPU: Intel Iris Xe | OS: Windows 11 Home | Weight: 1.19 kg",
                    Featured = true,
                    MainImage = "https://images.unsplash.com/photo-1588872657578-7efd1f1555ed?q=80&w=600&auto=format&fit=crop"
                });

                AddProductIfNotExist(new Product
                {
                    Sku = "LG-GM-STYLE",
                    Name = "LG Gram Style 14 OLED",
                    Slug = "lg-gram-style-14-oled",
                    BrandId = lg.Id,
                    CategoryId = laptops.Id,
                    Price = 38990000m,
                    CostPrice = 34000000m,
                    Quantity = 4,
                    Status = "active",
                    ShortDescription = "Extremely beautiful pearlescent laptop with a seamless haptic touchpad.",
                    FullDescription = "CPU: Intel Core i7-1360P | RAM: 16GB LPDDR5 | SSD: 512GB PCIe Gen4 | Screen: 14\" 2.8K 90Hz OLED | GPU: Intel Iris Xe | OS: Windows 11 Home | Weight: 999 g",
                    Featured = true,
                    MainImage = "https://images.unsplash.com/photo-1593642632823-8f785ba67e45?q=80&w=600&auto=format&fit=crop"
                });
            }

            context.SaveChanges();
        }
    }
}
