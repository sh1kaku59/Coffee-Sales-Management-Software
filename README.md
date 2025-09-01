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
- Full-stack Developer / Software Engineer phụ trách thiết kế kiến trúc, triển khai các module cốt lõi (Domain, Application, Infrastructure) và giao diện nghiệp vụ.
- Thiết lập quy trình build, chuẩn hóa coding conventions & code review.
- Phân tích yêu cầu nghiệp vụ cùng stakeholder và chuyển hóa thành mô hình miền (Domain Model).

## 3. Phương pháp đã áp dụng
- Phân tích & mô hình hóa miền (Domain Modeling) dựa trên các thực thể: Product, Ingredient, StockEntry, Supplier, Customer, LoyaltyTransaction, Order, OrderLine, Invoice, Promotion, User, Role.
- Áp dụng nguyên tắc SOLID và Clean Architecture / Layered Architecture để tách biệt domain logic khỏi concerns hạ tầng.
- Quy trình làm việc Agile / Iterative (Sprint ngắn, backlog, daily sync)

## 4. Trách nhiệm
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
- Ngôn ngữ: C# (.NET 6 )  
- Kiểu ứng dụng: Desktop (WinForms).
- CSDL: SQL Server + Migrations (Entity Framework Core).
- ORM: Entity Framework Core (Code First).
- CI/CD: (Ví dụ) GitHub Actions (build, test, publish).
- Mẫu kiến trúc / patterns: Clean Architecture, Repository Pattern, Unit of Work, DTO, Layered Architecture, Strategy (áp dụng tính giá / khuyến mãi), Specification Pattern (lọc động), Factory (khởi tạo hóa đơn / khuyến mãi), Observer / Domain Events (cập nhật tồn kho sau bán).
- Khả năng mở rộng: Phân tách logic thành services tách biệt, interface-driven design.
- Quốc tế hoá: Resource files.

## 6. Kiến trúc 
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

## 8. Mô hình dữ liệu
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

## 10. Bảo mật & Phân quyền
- Role-based Authorization (Admin / Staff).
- Ẩn hoặc vô hiệu hóa chức năng không phù hợp quyền.



