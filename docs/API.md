# API Reference

**Base URL**: `https://localhost:5001/api`

**Authentication**: Include JWT token in header:
```
Authorization: Bearer <token>
```

---

## Quick Response Format

All responses follow this structure:
```json
{
  "success": true/false,
  "data": { ... },
  "errors": [ ... ]
}
```

---

## Authentication Endpoints

### Register
```
POST /authentication/register
{ "email", "password", "confirmPassword", "firstName", "lastName" }
→ 200: User details with ID
```

### Login
```
POST /authentication/login
{ "email", "password" }
→ 200: { "token", "expiresIn", "user" }
```

---

## Books Endpoints

### List Books
```
GET /books?pageNumber=1&pageSize=10&searchTerm=fiction
→ 200: { items, pageNumber, pageSize, totalItems, totalPages }
```

### Get Book
```
GET /books/{id}
→ 200: Book details with author, category, reviews, rating
```

### Create Book (Admin)
```
POST /books
[Authorize(Roles = "Admin")]
{ "title", "description", "price", "stock", "authorId", "categoryId" }
→ 201: Created book
```

### Update Book (Admin)
```
PUT /books/{id}
[Authorize(Roles = "Admin")]
→ 204: No content
```

### Delete Book (Admin)
```
DELETE /books/{id}
[Authorize(Roles = "Admin")]
→ 204: No content
```

---

## Cart Endpoints (Auth Required)

### Get Cart
```
GET /carts/{userId}
→ 200: { userId, items, total }
```

### Add to Cart
```
POST /carts/add
{ "userId", "bookId", "quantity" }
→ 204: No content
```

### Remove from Cart
```
POST /carts/remove
{ "userId", "bookId" }
→ 204: No content
```

### Clear Cart
```
POST /carts/clear
{ "userId" }
→ 204: No content
```

---

## Orders Endpoints

### Get User Orders (Auth Required)
```
GET /orders/user/{userId}
→ 200: Array of orders with items
```

### Create Order (Auth Required)
```
POST /orders
{ "userId", "shippingAddress", "paymentMethod" }
→ 201: Created order
```

### Cancel Order (Auth Required)
```
POST /orders/{id}/cancel
→ 204: No content
```

### Update Status (Admin)
```
PUT /orders/{id}/status
[Authorize(Roles = "Admin")]
{ "status" }
→ 204: No content
```

---

## Reviews Endpoints

### Get Book Reviews
```
GET /reviews/book/{bookId}?pageNumber=1&pageSize=10
→ 200: Paginated reviews
```

### Create Review (Auth Required)
```
POST /reviews
{ "bookId", "userId", "rating", "comment" }
→ 201: Created review
```

### Update Review (Auth Required)
```
PUT /reviews/{id}
{ "rating", "comment" }
→ 204: No content
```

### Delete Review (Admin)
```
DELETE /reviews/{id}
[Authorize(Roles = "Admin")]
→ 204: No content
```

---

## Other Endpoints

### Categories
```
GET /categories                    → 200: All categories
POST /categories [Admin]           → 201: Create
PUT /categories/{id} [Admin]       → 204: Update
DELETE /categories/{id} [Admin]    → 204: Delete
```

### Authors
```
GET /authors?pageNumber=1&pageSize=10  → 200: Paginated
POST /authors [Admin]                  → 201: Create
PUT /authors/{id} [Admin]              → 204: Update
DELETE /authors/{id} [Admin]           → 204: Delete
```

---

## Error Responses

| Status | Meaning | Example |
|--------|---------|---------|
| 400 | Bad Request | Validation errors |
| 401 | Unauthorized | Missing/invalid token |
| 403 | Forbidden | Admin role required, ownership violation |
| 404 | Not Found | Resource not found |
| 409 | Conflict | Email already registered |
| 429 | Too Many Requests | Rate limit exceeded |
| 500 | Server Error | Unhandled exception |

---

## Rate Limiting

- **Global**: 100 requests/minute per IP
- **Auth endpoints**: 5 requests/minute per IP
- **Response**: 429 Too Many Requests

---

## Pagination

All list endpoints support:
- `pageNumber` (default: 1)
- `pageSize` (default: 10, max: 100)
- `searchTerm` (optional)
- `sortBy` (optional, format: `field:asc|desc`)
