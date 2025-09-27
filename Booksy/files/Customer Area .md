Areas/
 ?? Customer/
     ?? Controllers/
     ?    ?? BooksController.cs           <-- Browse books, details
     ?    ?? CartsController.cs           <-- Cart management
     ?    ?? CheckoutsController.cs       <-- Checkout & payments
     ?    ?? OrdersController.cs          <-- Customer order history
     ?    ?? ValuesController.cs          <-- Filter/search books
     ?    ?? StatisticsController.cs      <-- Top books, traffic, recommendations
     ?    ?? PromotionsController.cs      <-- View active promotions
     ?    ?? ProfilesController.cs        <-- Customer profile management
     ?? Models/
     ?    ?? DTOs/
     ?    ?    ?? CartItemRequest.cs
     ?    ?    ?? CheckoutRequest.cs
     ?    ?    ?? BookFilterRequest.cs
     ?    ?    ?? OrderResponse.cs
     ?    ?? ViewModels/
     ?? Services/                         <-- Optional: business logic services
     ?? CustomerAreaSettings.cs
