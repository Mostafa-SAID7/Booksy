# API Reference

## Base URL
```
https://localhost:5001/api
```

## Authentication
All protected endpoints require JWT token in header:
```
Authorization: Bearer <token>
```

---

## Authentication Endpoints

### Register User
```
POST /authentication/register
Content-Type: application/json

{
  "email": "user@example.com",
  "password": "Password@123",
  "confirmPassword": "Password@123",
  "firstName": "John",
  "lastName": "Doe"
}

Response: 200 OK
{
  "id": "uuid",
  "email": "user@example.com",
  "firstName": "John",
  "lastName": "Doe",
  "createdAt": "2026-01-01T00:00:00Z"
}
```

### Login
```
POST /authentication/login
Content-Type: application/json

{
  "email": "user@example.com",
  "password": "Password@123"
}

Response: 200 OK
{
  "token": "eyJhbGc...",
  "expiresIn": 3600,
  "user": { ... }
}
```

---

## Books Endpoints

### Get All Books
```
GET /books?pageNumber=1&pageSize=10&searchTerm=fiction&sortBy=title

Response: 200 OK
{
  "success": true,
  "data": {
    "items": [ ... ],
    "pageNumber": 1,
    "pageSize": 10,
    "totalItems": 50,
    "totalPages": 5
  }
}
```

### Get Book by ID
```
GET /books/{id}

Response: 200 OK
{
  "id": "uuid",
  "title": "Book Title",
  "description": "...",
  "price": 19.99,
  "authorId": "uuid",
  "categoryId": "uuid",
  "tags": [ ... ],
  "rating": 4.5,
  "reviews": [ ... ]
}
```

### Create Book (Admin Only)
```
POST /books
Authorization: Bearer <token>
Content-Type: application/json

{
  "title": "New Book",
  "description": "Description",
  "price": 29.99,
  "stock": 100,
  "authorId": "uuid",
  "categoryId": "uuid"
}

Response: 201 Created
```

### Update Book (Admin Only)
```
PUT /books/{id}
Authorization: Bearer <token>
Content-Type: application/json

{ ... }

Response: 204 No Content
```

### Delete Book (Admin Only)
```
DELETE /books/{id}
Authorization: Bearer <token>

Response: 204 No Content
```

---

## Authors Endpoints

### Get All Authors
```
GET /authors?pageNumber=1&pageSize=10

Response: 200 OK
```

### Create Author (Admin Only)
```
POST /authors
Authorization: Bearer <token>
Content-Type: application/json

{
  "name": "Author Name",
  "bio": "Author biography"
}

Response: 201 Created
```

### Update Author (Admin Only)
```
PUT /authors/{id}
Authorization: Bearer <token>

Response: 204 No Content
```

### Delete Author (Admin Only)
```
DELETE /authors/{id}
Authorization: Bearer <token>

Response: 204 No Content
```

---

## Categories Endpoints

### Get All Categories
```
GET /categories

Response: 200 OK
```

### Create Category (Admin Only)
```
POST /categories
Authorization: Bearer <token>
Content-Type: application/json

{
  "name": "Fiction",
  "description": "Fictional books"
}

Response: 201 Created
```

---

## Cart Endpoints

### Get User Cart
```
GET /carts/{userId}
Authorization: Bearer <token>

Response: 200 OK
{
  "userId": "uuid",
  "items": [ ... ],
  "total": 99.99
}
```

### Add to Cart (Auth Required)
```
POST /carts/add
Authorization: Bearer <token>
Content-Type: application/json

{
  "userId": "uuid",
  "bookId": "uuid",
  "quantity": 1
}

Response: 204 No Content
```

### Remove from Cart (Auth Required)
```
POST /carts/remove
Authorization: Bearer <token>
Content-Type: application/json

{
  "userId": "uuid",
  "bookId": "uuid"
}

Response: 204 No Content
```

### Clear Cart (Auth Required)
```
POST /carts/clear
Authorization: Bearer <token>
Content-Type: application/json

{
  "userId": "uuid"
}

Response: 204 No Content
```

---

## Orders Endpoints

### Get User Orders
```
GET /orders/user/{userId}
Authorization: Bearer <token>

Response: 200 OK
```

### Create Order (Auth Required)
```
POST /orders
Authorization: Bearer <token>
Content-Type: application/json

{
  "userId": "uuid",
  "shippingAddress": "...",
  "paymentMethod": "credit-card"
}

Response: 201 Created
```

### Update Order Status (Admin Only)
```
PUT /orders/{id}/status
Authorization: Bearer <token>
Content-Type: application/json

{
  "status": "shipped"
}

Response: 204 No Content
```

### Cancel Order (Auth Required)
```
POST /orders/{id}/cancel
Authorization: Bearer <token>

Response: 204 No Content
```

---

## Reviews Endpoints

### Get Book Reviews
```
GET /reviews/book/{bookId}?pageNumber=1&pageSize=10

Response: 200 OK
```

### Create Review (Auth Required)
```
POST /reviews
Authorization: Bearer <token>
Content-Type: application/json

{
  "bookId": "uuid",
  "userId": "uuid",
  "rating": 5,
  "comment": "Great book!"
}

Response: 201 Created
```

### Update Review (Auth Required)
```
PUT /reviews/{id}
Authorization: Bearer <token>
Content-Type: application/json

{
  "rating": 4,
  "comment": "Updated comment"
}

Response: 204 No Content
```

### Delete Review (Admin Only)
```
DELETE /reviews/{id}
Authorization: Bearer <token>

Response: 204 No Content
```

---

## Error Responses

### 400 Bad Request
```json
{
  "success": false,
  "message": "Validation failed",
  "errors": [
    {
      "field": "email",
      "message": "Invalid email format"
    }
  ]
}
```

### 401 Unauthorized
```json
{
  "success": false,
  "message": "Authentication required"
}
```

### 403 Forbidden
```json
{
  "success": false,
  "message": "Admin role required"
}
```

### 404 Not Found
```json
{
  "success": false,
  "message": "Book not found"
}
```

### 409 Conflict
```json
{
  "success": false,
  "message": "Email already registered"
}
```

---

## Rate Limiting

Standard API rate limiting applies:
- **200 requests per minute** per IP
- Exceeding limit returns `429 Too Many Requests`

---

## Status Codes

| Code | Meaning |
|------|---------|
| 200 | OK |
| 201 | Created |
| 204 | No Content |
| 400 | Bad Request |
| 401 | Unauthorized |
| 403 | Forbidden |
| 404 | Not Found |
| 409 | Conflict |
| 429 | Too Many Requests |
| 500 | Internal Server Error |

---

## Pagination

All list endpoints support:
- `pageNumber` (default: 1)
- `pageSize` (default: 10, max: 100)
- `searchTerm` (optional)
- `sortBy` (optional)

Example: `/api/books?pageNumber=2&pageSize=20&sortBy=price:desc`
