# Library Management System API Documentation

## Overview

The Library Management System API provides endpoints for managing books, patrons, and borrowing records. This documentation covers the available endpoints, request structures, responses, and possible error cases.

## Authentication

- The API requires authentication using JWT tokens.
- To authenticate, use the `/api/account/login` endpoint to obtain a token.
- Include the token in the `Authorization` header as `Bearer <token>`.

---

## **Authentication**

### **Login**

- **Endpoint:** `PUT /api/account/login`
- **Description:** Authenticates a user and returns a JWT token.
- **Request Body:**

```json
{
  "UserName": "string",
  "Password": "string"
}
```
### Request Parameters

| Parameter         | Type   | Required | Description |
|------------------|--------|----------|-------------|
| `UserName`         | string | ✅ Yes  | The name of the user.|
| `Password`        | string | ✅ Yes  | The Password of the account. |

- **Response:**

```json
{
  "Token": "string"
}
```

- **Possible Errors:**
  - `404 Not Found`:
    - `Invalid username or password. Please check your credentials and try again.`

---

## **Book Management**
### **Get All Books**

- **Endpoint:** `GET /api/books`
- **Description:** Retrieve a list of all books.
- **Response:**

```json
{
  "Id": "Guid",
  "Title": "string",
  "Author": "string",
  "Genre": "string",
  "CopiesAvailable": "int",
}
```
---
### **Get Book by ID**

- **Endpoint:** `GET /api/books/{id}`
- **Description:** Retrieves details of a specific book by its ID.
- **Path Parameters:**
  - `id` (Guid)
#### Request Parameters

| Parameter         | Type   | Required | Description |
|------------------|--------|----------|-------------|
| `id`         | Guid | ✅ Yes  | – The unique identifier of the book.|
- **Response:**

```json
{
  "Id": "Guid",
  "Title": "string",
  "Author": "string",
  "PublicationYear": "int",
  "ISBN": "string",
  "Publisher": "string",
  "Genre": "string",
  "CopiesAvailable": "int",
  "TotalCopies": "int"
}
```

- **Possible Errors:**
  - `404 Not Found`:
    - `The requested book does not exist in the library.`

---
### **Add Book**

- **Endpoint:** `Post /api/books`
- **Description:** Add a new book to the library.
- **Request Body:**
```json
{
  "title": "string",
  "author": "string",
  "publicationYear": "int",
  "isbn": "string",
  "publisher": "string",
  "genre": "string",
  "totalCopies": "int"
}
```
#### Request Parameters

| Parameter         | Type   | Required | Description |
|------------------|--------|----------|-------------|
| `title`         | string | ✅ Yes  | The title of the book. Must be unique. |
| `author`        | string | ✅ Yes  | The author of the book. |
| `publicationYear` | int   | ✅ Yes  | The year the book was published. Must be greater than 0. |
| `isbn`          | string | ✅ Yes  | The ISBN of the book. Must be 13 characters long and unique. |
| `publisher`     | string | ✅ Yes  | The publisher of the book. |
| `genre`        | string | ✅ Yes  | The genre of the book. |
| `totalCopies`   | int    | ✅ Yes  | The total number of copies available. Must be greater than 0. |

- **Possible Errors:**
  - `400 Bad Request`: 
    - `A book with this title already exists. Please choose a different title.`
    - `The ISBN must be exactly 13 characters long.`
    - `A book with this ISBN already exists. Please use a unique ISBN.`

---
### **Update Book**
- **Endpoint:** `Put /api/books/{id}`
- **Description:** update a book from the library.
- **Path Parameters:**
  - `id` (Guid)
- **Request Body:**
```json
{
  "title": "string",
  "author": "string",
  "publicationYear": "int",
  "isbn": "string",
  "publisher": "string",
  "genre": "string",
  "totalCopies": "int"
}
```
#### Request Parameters

| Parameter         | Type   | Required | Description |
|------------------|--------|----------|-------------|
| `id`         | Guid | ✅ Yes  | The unique identifier of the book. |
| `title`         | string | ✅ Yes  | The title of the book. Must be unique. |
| `author`        | string | ✅ Yes  | The author of the book. |
| `publicationYear` | int   | ✅ Yes  | The year the book was published. Must be greater than 0. |
| `isbn`          | string | ✅ Yes  | The ISBN of the book. Must be 13 characters long and unique. |
| `publisher`     | string | ✅ Yes  | The publisher of the book. |
| `genre`        | string | ✅ Yes  | The genre of the book. |
| `totalCopies`   | int    | ✅ Yes  | The total number of copies available. Must be greater than 0. |

- **Possible Errors:**
  - `400 Bad Request`: 
    - `A book with this title already exists. Please choose a different title.`
    - `The ISBN must be exactly 13 characters long.`
    - `A book with this ISBN already exists. Please use a unique ISBN.`
    - `The total number of copies cannot be less than the number of borrowed copies.`
  - `404 Not Found`:
    - `The requested book does not exist in the library.`

---
### **Delete Book**

- **Endpoint:** `DELETE /api/books/{id}`
- **Description:** Delete book from the library.
- **Path Parameters:**
  - `id` (Guid)
#### Request Parameters

| Parameter         | Type   | Required | Description |
|------------------|--------|----------|-------------|
| `id`         | Guid | ✅ Yes  | The unique identifier of the book. |
- **Possible Errors:**
  - `404 Not Found`:
    - `The requested book does not exist in the library.`
  - `400 Bad Request`
    - `There are borrowed copies of this book`
---

## **Patron Management**
### **Get All Books**

- **Endpoint:** `GET /api/patrons`
- **Description:** Retrieve a list of all patrons.
- **Response:**

```json
{
  "Id": "Guid",
  "Name": "string",
  "Email": "string",
  "PhoneNumber": "string",
}
```
---
### **Get Patron by ID**

- **Endpoint:** `GET /api/patrons/{id}`
- **Description:** Retrieves details of a specific patron by its ID.
- **Path Parameters:**
  - `id` (Guid)
#### Request Parameters

| Parameter         | Type   | Required | Description |
|------------------|--------|----------|-------------|
| `id`         | Guid | ✅ Yes  | – The unique identifier of the patron.|
- **Response:**

```json
{
  "Id": "Guid",
  "Name": "string",
  "Email": "string",
  "PhoneNumber": "string",
  "Address": {
    "Street" : "string",
    "City" : "string",
    "State" : "string",
    "PostalCode" : "string",
    "Country" : "string"
  },
  "MembershipDate": "DateTime"
}
```

- **Possible Errors:**
  - `404 Not Found`:
    - `The specified patron does not exist in the system.`

---
### **Add Patron**

- **Endpoint:** `Post /api/books`
- **Description:** Add a new book to the library.
- **Request Body:**
```json
{
  "Name": "string",
  "Email": "string",
  "PhoneNumber": "string",
  "Address": {
    "Street" : "string",
    "City" : "string",
    "State" : "string",
    "PostalCode" : "string",
    "Country" : "string"
  },
}
```
#### Request Parameters

| Parameter         | Type   | Required | Description |
|------------------|--------|----------|-------------|
| `Name`         | string | ✅ Yes  | The name of the patron. |
| `Email`        | string | ✅ Yes  | The email of the patron. |
| `PhoneNumber` | string   | ✅ Yes  | The phone number of the patron. |
| `Address`          | Json | ❎ No  | The Address of the patron. |

- **Possible Errors:**
  - `400 Bad Request`: 
    - `The provided email address is not valid. Please enter a correct email.`
    - `Some required fields are missing. Please check your input and try again.` , (`if the Address Added he should fill Street , City , Country`)

---
### **Update Patron**
- **Endpoint:** `Put /api/patrons/{id}`
- **Description:** update a patron from the system.
- **Path Parameters:**
  - `id` (Guid)
- **Request Body:**
```json
{
  "Name": "string",
  "Email": "string",
  "PhoneNumber": "string",
  "Address": {
    "Street" : "string",
    "City" : "string",
    "State" : "string",
    "PostalCode" : "string",
    "Country" : "string"
  },
}
```
#### Request Parameters

| Parameter         | Type   | Required | Description |
|------------------|--------|----------|-------------|
| `id`         | Guid | ✅ Yes  | The unique identifier of the patron. |
| `Name`         | string | ✅ Yes  | The name of the patron. |
| `Email`        | string | ✅ Yes  | The email of the patron. |
| `PhoneNumber` | string   | ✅ Yes  | The phone number of the patron. |
| `Address`          | Json | ❎ No  | The Address of the patron. |

- **Possible Errors:**
  - `400 Bad Request`: 
    - `The provided email address is not valid. Please enter a correct email.`
    - `Some required fields are missing. Please check your input and try again.` , (`if the Address Added he should fill Street , City , Country`)
  - `404 Not Found`:
    - `The requested patron does not exist in the system.`

---
### **Delete Patron**

- **Endpoint:** `DELETE /api/patrons/{id}`
- **Description:** Delete book from the system.
- **Path Parameters:**
  - `id` (Guid)
#### Request Parameters

| Parameter         | Type   | Required | Description |
|------------------|--------|----------|-------------|
| `id`         | Guid | ✅ Yes  | The unique identifier of the patron. |
- **Possible Errors:**
  - `404 Not Found`:
    - `The requested patron does not exist in the library.`
  - `400 Bad Request`
    - `This patron has books that have not been returned yet.`
---

## **Borrowing Books Management**

### **Borrow a Book**

- **Endpoint:** `POST /api/borrow/{bookId}/patron/{patronId}`

- **Description:** Allows a patron to borrow a book.

- **Path Parameters:**

  - `bookId` (Guid)
  - `patronId` (Guid)
#### Request Parameters

| Parameter         | Type   | Required | Description |
|------------------|--------|----------|-------------|
| `bookId`         | Guid | ✅ Yes  | The unique identifier of the book. |
| `patronId`        | Guid | ✅ Yes  | The unique identifier of the patron. |

- **Possible Errors:**

  - `404 Not Found`: 
    - `The specified patron does not exist in the system.`
    - `The requested book does not exist in the library.`
  - `400 Bad Request`: 
    - `This patron has already borrowed this book.`
    - `No copies of this book are currently available for borrowing.`
---
### **return a Book**

- **Endpoint:** `PUT /api/return/{bookId}/patron/{patronId}`

- **Description:** Allows a patron to return a book.

- **Path Parameters:**

  - `bookId` (Guid)
  - `patronId` (Guid)
#### Request Parameters

| Parameter         | Type   | Required | Description |
|------------------|--------|----------|-------------|
| `bookId`         | Guid | ✅ Yes  | The unique identifier of the book. |
| `patronId`        | Guid | ✅ Yes  | The unique identifier of the patron. |

- **Possible Errors:**

  - `404 Not Found`: 
    - `The specified patron does not exist in the system.`
    - `The requested book does not exist in the library.`
  - `400 Bad Request`: 
    - `This book was not borrowed by the specified patron.`
---

## **Notes**

- All apis (Not Get Apis) could Give `500 Internal Server Error` : `An unexpected error occurred. Please try again later.` if the Transaction is not completed.
- All apis (Not Login Api) will Give `401 unauthorized` if you didn't include the token in the `Authorization` header as `Bearer <token>`
- `superAdmin`:
  - `name`: `admin`
  - `password`: `admin`
