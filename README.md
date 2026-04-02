<p align="center">
  <img src="https://github.com/ilgiso91/Isopass/blob/master/mavi.png" width="200" alt="ISO Pass Logo">
</p>

# ISO Pass
Simple C# console password manager with AES encryption.

# Isopass 🔐  
A simple and secure **C# console-based password manager** that encrypts all stored passwords using **AES encryption**.  
Isopass is designed as a lightweight, beginner-friendly, and portfolio-ready project.

---

## 🚀 Features

- 🔑 **Master Password Protection**  
  - First run: You set a master password  
  - Next runs: Master password is validated  
  Stored as SHA-256 hash (`master.hash`)

- 🔐 **AES Encryption**  
  - All passwords are encrypted before being saved  
  - Stored in `passwords.json`

- 📁 **Password Management**  
  - Add new password  
  - View all passwords (masked with `******`)  
  - Update existing password  
  - Delete password by ID  
  - Search by site or username  

- 🖥️ **Clean Console UI**  
  - Banner  
  - Clear menu  
  - User-friendly messages  

---

## 📦 How It Works

### 1️⃣ First Run  
- Program asks for a **master password**  
- Hash is saved to `master.hash`  
- This password becomes your permanent key  

### 2️⃣ Adding Passwords  
You can store:
- Site  
- Username  
- Password (encrypted)

### 3️⃣ Viewing Passwords  
- Passwords are shown **masked**  
- Example: `********`

### 4️⃣ Searching  
Search by:
- Site  
- Username  

### 5️⃣ Updating & Deleting  
Each entry has an **ID**, making operations easy.

---

## 🛠️ Technologies Used

| Component | Description |
|----------|-------------|
| **C#** | Main programming language |
| **.NET Console App** | Application type |
| **AES Encryption** | Protects stored passwords |
| **SHA-256** | Secures master password |
| **JSON Storage** | Lightweight local database |

---

## 📂 Project Structure



---

## ▶️ Running the App

1. Clone the repository  
2. Open the solution in Visual Studio  
3. Run the project  
4. Set your master password  
5. Start managing your passwords securely  

---

## 📸 Screenshots (Optional)

You can add screenshots here later.

---

## 📌 Notes

This project is for **learning and portfolio purposes**.  
For production use, additional security measures are recommended.

---

## 👤 Author

**iso (ilgiso91)**  
C# Developer • Learning • Building • Improving  
