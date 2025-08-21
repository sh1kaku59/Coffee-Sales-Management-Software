# Coffee Sales Management Software

## 1. Mô tả
Phần mềm quản lý bán hàng cho quán cà phê (POS + Backoffice) hỗ trợ:
- Quản lý sản phẩm (cà phê, đồ uống, topping, combo, định mức nguyên liệu).
- Quản lý kho (nhập, xuất, kiểm kê, cảnh báo tồn tối thiểu).
- Quản lý nhà cung cấp.
- Quản lý khách hàng & chương trình khách hàng thân thiết (tích điểm / hạng thành viên).
- Bán hàng tại quầy (POS) với nhiều phương thức thanh toán (tiền mặt, QR / ví, thẻ).
- Quản lý hóa đơn, chiết khấu, khuyến mãi (happy hour, combo, voucher).
- Phân quyền người dùng (Admin, Quản lý, Thu ngân, Thủ kho).
- Báo cáo doanh thu, lợi nhuận gộp, top sản phẩm, thống kê theo ca / ngày / tháng.
- Sao lưu & khôi phục dữ liệu.

Mục tiêu: Tối ưu quy trình vận hành quán, giảm sai sót nhập liệu, cung cấp báo cáo tức thời hỗ trợ ra quyết định.

## 2. Vai trò
(Điền rõ vai trò của bạn trong dự án – ví dụ:)
- Full-stack Developer / Software Engineer phụ trách thiết kế kiến trúc, triển khai các module cốt lõi (Domain, Application, Infrastructure) và giao diện nghiệp vụ.
- Thiết lập quy trình build, chuẩn hóa coding conventions & code review.
- Phân tích yêu cầu nghiệp vụ cùng stakeholder và chuyển hóa thành mô hình miền (Domain Model).

## 3. Phương pháp đã áp dụng
(Cập nhật lại đúng với thực tế dự án của bạn)
- Phân tích & mô hình hóa miền (Domain Modeling) dựa trên các thực thể: Product, Ingredient, StockEntry, Supplier, Customer, LoyaltyTransaction, Order, OrderLine, Invoice, Promotion, User, Role.
- Áp dụng nguyên tắc SOLID và Clean Architecture / Layered Architecture để tách biệt domain logic khỏi concerns hạ tầng.
- Sử dụng Repository Pattern + Unit of Work (nếu có).
- Sử dụng DTO + Mapping (AutoMapper hoặc mapping thủ công).
- Áp dụng chuẩn hóa exception handling & logging.
- Quy trình làm việc Agile / Iterative (Sprint ngắn, backlog, daily sync) (nếu có).
- Kiểm thử: Unit test cho domain services, integration test (nếu triển khai).
- Tối ưu truy vấn (index DB, tránh N+1, batching).
- Bảo mật cơ bản: xác thực (Authentication), phân quyền (Authorization role-based), hashing mật khẩu (ví dụ: BCrypt), kiểm soát đầu vào (Input validation).

## 4. Trách nhiệm
(Tùy chỉnh theo phần bạn thực sự đảm nhiệm)
- Thiết kế kiến trúc tổng thể (tầng Domain / Application / Infrastructure / Presentation).
- Xây dựng schema cơ sở dữ liệu & migrations.
- Cài đặt các module: Quản lý sản phẩm, kho, bán hàng, hóa đơn, báo cáo thống kê.
- Implement logic khuyến mãi (tính giá linh hoạt: theo phần trăm, giá cố định, combo).
- Thiết lập & tích hợp authentication / authorization.
- Tối ưu hiệu năng truy vấn và xử lý batch cập nhật tồn kho.
- Viết unit tests cho domain services cốt lõi (tính tồn kho, áp khuyến mãi).
- Viết tài liệu kỹ thuật cho team (onboarding, conventions).
- Hỗ trợ fix bug sản xuất & cải thiện logging/monitoring.

## 5. Công nghệ và kỹ thuật đã áp dụng
(Điền chính xác theo project; bên dưới là ví dụ thường gặp)
- Ngôn ngữ: C# (.NET 8 / .NET 7 / .NET 6)  
- Kiểu ứng dụng: (Ví dụ) Desktop (WPF / WinForms) / Web (ASP.NET Core MVC / Razor Pages / Blazor) / API (ASP.NET Core Web API).
- CSDL: SQL Server / PostgreSQL / SQLite (ghi rõ cái bạn dùng) + Migrations (Entity Framework Core).
- ORM: Entity Framework Core (Code First / Database First).
- Thư viện / Package: (Ví dụ) AutoMapper, FluentValidation, Serilog / NLog, BCrypt.Net, MediatR (nếu dùng CQRS), ClosedXML (xuất Excel), QuestPDF / iText (xuất hóa đơn PDF), Hangfire (tác vụ nền).
- Authentication & Authorization: ASP.NET Identity / JWT Bearer / Custom.
- Logging & Monitoring: Serilog + sinks (Console/File/Seq) (nếu dùng).
- Kiểm thử: xUnit / NUnit / MSTest + Moq / NSubstitute.
- Triển khai: (Ví dụ) Docker + docker-compose / Triển khai on-prem / Azure App Service.
- CI/CD: (Ví dụ) GitHub Actions (build, test, publish).
- Mẫu kiến trúc / patterns: Clean Architecture, Repository Pattern, Unit of Work, DTO, Layered Architecture, Strategy (áp dụng tính giá / khuyến mãi), Specification Pattern (lọc động), Factory (khởi tạo hóa đơn / khuyến mãi), Observer / Domain Events (cập nhật tồn kho sau bán).
- Bảo mật: Hash mật khẩu, Input validation, Role-based Access Control (RBAC).
- Tối ưu hiệu năng: Caching (MemoryCache / Redis), batching cập nhật tồn kho, chỉ định Include chọn lọc, lập chỉ mục (DB Indexes).
- Khả năng mở rộng: Phân tách logic thành services tách biệt, interface-driven design.
- Quốc tế hoá / bản địa hoá (nếu có): Resource files / CultureInfo (ví dụ cho định dạng tiền tệ).

## 6. Kiến trúc (Giả định mẫu – chỉnh lại theo code thực)
```
src/
  Domain/
    Entities/
    ValueObjects/
    Interfaces/
    Events/
    Services/
  Application/
    DTOs/
    Interfaces/
    Services/
    Validators/
    Mappings/
  Infrastructure/
    Persistence/
      Context/
      Migrations/
      Repositories/
    Security/
    Logging/
  Presentation/ (hoặc UI/)
    Controllers/ (nếu Web API / MVC)
    Views/ (nếu MVC)
    Pages/ (nếu Razor Pages)
    Components/ (nếu Blazor)
  Tests/
    DomainTests/
    ApplicationTests/
```
Luồng chính:
1. UI/Presentation nhận request từ người dùng.
2. Application xử lý use case (Service / Handler / Mediator).
3. Domain thực thi logic nghiệp vụ thuần (Entities, Value Objects, Domain Services).
4. Infrastructure cung cấp triển khai repository, context DB, logging.
5. Trả kết quả (DTO/ViewModel) về UI.

## 7. Các Use Case chính
- Thêm / cập nhật / ngưng kinh doanh sản phẩm.
- Tạo đơn hàng POS nhanh (scan hoặc chọn nhanh).
- Áp dụng khuyến mãi tự động theo thời gian hoặc mã voucher.
- Tạo hóa đơn & ghi nhận thanh toán (nhiều phương thức).
- Cập nhật tồn kho sau bán hàng (giảm định mức nguyên liệu).
- Nhập kho (PO -> nhận hàng -> cập nhật stock entries).
- Báo cáo: Doanh thu theo ngày / ca / nhóm sản phẩm; Top N sản phẩm; Tồn kho dưới ngưỡng.
- Quản lý khách hàng: Tích điểm, nâng hạng thành viên, lịch sử giao dịch.
- Quản lý người dùng & phân quyền truy cập chức năng.

## 8. Mô hình dữ liệu (Ví dụ rút gọn)
(Chỉnh cho đúng với DB thực tế)
- Product(ProductId, Name, CategoryId, Price, Unit, IsActive, ...).
- Ingredient(IngredientId, Name, Unit, CurrentStock, ReorderLevel).
- ProductIngredient(ProductId, IngredientId, QuantityPerUnit).
- Supplier(SupplierId, Name, ContactInfo, ...).
- StockEntry(StockEntryId, IngredientId, Quantity, UnitCost, Type(In/Out/Adjust), Timestamp).
- Customer(CustomerId, Name, Phone, LoyaltyPoints, Tier).
- Order(OrderId, CustomerId, CreatedAt, Status, Subtotal, DiscountTotal, GrandTotal, PaymentStatus).
- OrderLine(OrderLineId, OrderId, ProductId, Qty, UnitPrice, LineDiscount, LineTotal).
- Promotion(PromotionId, Code, Type(Percent/Fixed/Combo/TimeRange), Conditions, Value, StartAt, EndAt).
- Invoice(InvoiceId, OrderId, PaymentMethod, PaidAmount, ChangeAmount, IssuedAt, InvoiceNumber).
- User(UserId, Username, PasswordHash, RoleId, IsActive).
- Role(RoleId, Name, Permissions...).

## 9. Luồng nghiệp vụ minh họa: Bán hàng POS
1. Thu ngân tạo đơn tạm, chọn sản phẩm -> hệ thống tính tạm subtotal.
2. Áp khuyến mãi phù hợp (theo thời gian / nhập mã).
3. Tính tổng tiền phải trả, hiển thị phương thức thanh toán.
4. Nhập số tiền khách đưa / xác nhận giao dịch QR.
5. Ghi nhận hóa đơn, cập nhật tồn kho (trừ nguyên liệu theo định mức).
6. Cộng điểm khách hàng (nếu có Loyalty).
7. Ghi log & sẵn sàng in / xuất PDF.

## 10. Bảo mật & Phân quyền
- Role-based Authorization (Admin / Manager / Cashier / Inventory).
- Ẩn hoặc vô hiệu hóa chức năng không phù hợp quyền.
- Hash mật khẩu (BCrypt / PBKDF2).
- (Nếu Web/API) JWT Access Token + Refresh Token (nếu áp dụng).

## 11. Logging & Giám sát
- Logging mức độ: Information (request lifecycle), Warning (tồn kho thấp), Error (DB/Unhandled).
- Correlation Id cho mỗi request (nếu Web/API).
- (Tùy chọn) Audit log: ai thay đổi giá sản phẩm / tồn kho.

## 12. Kiểm thử
- Unit tests: domain services (tính tồn kho, áp khuyến mãi).
- Integration tests: repository + DB in-memory / test container.
- (Nếu có) UI/instrumentation tests.

## 13. Hướng dẫn cài đặt (Điền cụ thể)
Prerequisites:
- .NET SDK (phiên bản ...)
- SQL Server / PostgreSQL / SQLite
- (Tuỳ chọn) Docker

Các bước:
1. Clone repo.
2. Cấu hình chuỗi kết nối trong appsettings.(Development).json.
3. Chạy migrations: `dotnet ef database update` (nếu dùng EF Core).
4. Build & run: `dotnet run` (hoặc mở solution trong IDE).
5. Đăng nhập bằng tài khoản seed (Admin) (ghi rõ nếu có).

## 14. Roadmap (Ví dụ)
- [ ] Tích hợp máy in hóa đơn ESC/POS.
- [ ] Dashboard realtime (SignalR / WebSocket).
- [ ] Tự động đặt hàng nguyên liệu khi tồn kho dưới ngưỡng.
- [ ] Module đa chi nhánh / đồng bộ hóa.
- [ ] Triển khai container hóa & CI/CD hoàn chỉnh.

## 15. Đóng góp
1. Fork & tạo nhánh feature.
2. Commit theo convention (ví dụ: feat:, fix:, refactor:).
3. Mở Pull Request mô tả rõ thay đổi & ảnh chụp (nếu UI).

## 16. Giấy phép
(Thêm license nếu có – MIT / Apache-2.0 / Proprietary.)

## 17. Liên hệ
- Tác giả: (Tên bạn)
- Email / GitHub: (Thông tin)

---

Ghi chú: Một số nội dung ở trên là giả định mô hình chuẩn của hệ thống quản lý bán hàng cho quán cà phê. Hãy cập nhật lại cho khớp 100% mã nguồn thực tế (tên namespace, lớp, pattern thật sự dùng, v.v.).
