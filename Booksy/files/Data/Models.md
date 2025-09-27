 ├─ Models/
 │    ├─ Entities/
 │    │    ├─ Common/
 │    │    │    ├─ BaseEntity.cs
 │    │    │    ├─ IAuditableEntity.cs
 │    │    │    └─ ISoftDeletable.cs
 │    │    ├─ Books/
 │    │    │    ├─ Book.cs
 │    │    │    ├─ Author.cs
 │    │    │    ├─ Category.cs
 │    │    │    ├─ Publisher.cs
 │    │    │    ├─ BookAuthor.cs
 │    │    │    ├─ BookCategory.cs
 │    │    │    └─ Review.cs
 │    │    ├─ Orders/
 │    │    │    ├─ Order.cs
 │    │    │    ├─ OrderItem.cs
 │    │    │    ├─ Payment.cs
 │    │    │    └─ Shipment.cs
 │    │    ├─ Users/
 │    │    │    ├─ User.cs
 │    │    │    ├─ UserProfile.cs
 │    │    │    ├─ Address.cs
 │    │    │    ├─ Cart.cs
 │    │    │    ├─ CartItem.cs
 │    │    │    ├─ Wishlist.cs
 │    │    │    └─ WishlistItem.cs
 │    │    └─ Promotions/
 │    │         ├─ Promotion.cs
 │    │         ├─ Coupon.cs
 │    │         └─ Discount.cs
 │    │
 │    ├─ ValueObjects/
 │    │    ├─ Money.cs
 │    │    ├─ Email.cs
 │    │    ├─ Phone.cs
 │    │    └─ ISBN.cs
 │    │
 │    ├─ Enums/
 │    │    ├─ OrderStatus.cs
 │    │    ├─ PaymentStatus.cs
 │    │    ├─ ShipmentStatus.cs
 │    │    ├─ UserRole.cs
 │    │    ├─ BookCondition.cs
 │    │    └─ PromotionType.cs
 │    │
 │    ├─ Events/
 │    │    ├─ BookCreatedEvent.cs
 │    │    ├─ OrderCreatedEvent.cs
 │    │    ├─ OrderStatusChangedEvent.cs
 │    │    ├─ UserRegisteredEvent.cs
 │    │    └─ PaymentProcessedEvent.cs
 │    │
 │    ├─ DTOs/
 │    │    ├─ Requests/
 │    │    │    ├─ Auth/
 │    │    │    │    ├─ LoginRequest.cs
 │    │    │    │    ├─ RegisterRequest.cs
 │    │    │    │    ├─ ChangePasswordRequest.cs
 │    │    │    │    └─ ResetPasswordRequest.cs
 │    │    │    ├─ Books/
 │    │    │    │    ├─ BookCreateRequest.cs
 │    │    │    │    ├─ BookUpdateRequest.cs
 │    │    │    │    └─ BookFilterRequest.cs
 │    │    │    ├─ Orders/
 │    │    │    │    ├─ OrderCreateRequest.cs
 │    │    │    │    ├─ OrderUpdateRequest.cs
 │    │    │    │    └─ CheckoutRequest.cs
 │    │    │    └─ Cart/
 │    │    │         ├─ CartItemRequest.cs
 │    │    │         └─ CartUpdateRequest.cs
 │    │    └─ Responses/
 │    │         ├─ Auth/
 │    │         │    ├─ AuthResponse.cs
 │    │         │    └─ UserProfileResponse.cs
 │    │         ├─ Books/
 │    │         │    ├─ BookResponse.cs
 │    │         │    ├─ BookDetailResponse.cs
 │    │         │    └─ BookSearchResponse.cs
 │    │         ├─ Orders/
 │    │         │    ├─ OrderResponse.cs
 │    │         │    ├─ OrderSummaryResponse.cs
 │    │         │    └─ OrderItemResponse.cs
 │    │         ├─ Cart/
 │    │         │    ├─ CartResponse.cs
 │    │         │    └─ CartItemResponse.cs
 │    │         └─ Admin/
 │    │              ├─ DashboardStatsResponse.cs
 │    │              ├─ SalesReportResponse.cs
 │    │              └─ UserManagementResponse.cs