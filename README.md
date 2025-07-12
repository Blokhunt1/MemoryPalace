# 🏰 Memory Palace App

A stunning .NET 8 Blazor Server application for creating and managing memory palaces using image sets. Features a World of Warcraft-inspired floating navigation design with full-screen immersive experience.

![Memory Palace App](https://img.shields.io/badge/.NET-8.0-blue) ![Blazor](https://img.shields.io/badge/Blazor-Server-purple) ![SQLite](https://img.shields.io/badge/Database-SQLite-green) ![Docker](https://img.shields.io/badge/Docker-Ready-blue)

## ✨ Features

### 🎮 **WoW-Style Interface**
- **Floating Navigation Bar**: Glass-morphism top navigation with backdrop blur
- **Full-Screen Background**: Dynamic slideshow from memory palace images  
- **Dark Fantasy Theme**: Rich gradients, gold accents, and cinematic styling
- **Epic Typography**: Poppins for UI, Cinzel for headings

### 🏗️ **Memory Palace Creation**
- **Bulk Upload**: Upload zip files containing images to create memory palaces
- **Automatic Organization**: Images automatically converted into sequential loci
- **Smart Processing**: Supports JPG, PNG, GIF, BMP, WebP formats
- **Instant Walkthrough**: Navigate directly to walkthrough after creation
test
### 🚶 **Advanced Walkthrough System**
- **Two-Column Layout**: Image display + information panels
- **Toggle Modes**: Switch between Image Information and Actual Information
- **Information Types**:
  - **Image Information**: Location, Image Key, Loci Category, Image Value
  - **Actual Information**: Category, Info Key, Info Value
- **Progress Tracking**: Visual progress bar and navigation controls

### 📊 **Palace Management**
- **Palace Overview**: List all memory palaces with statistics
- **Quick Actions**: Walk Through, Edit Table, Delete
- **Complete Table View**: Bulk edit all loci data at once
- **Responsive Design**: Optimized for desktop and mobile

## 🛠️ Tech Stack

- **.NET 8**: Latest framework with performance improvements
- **Blazor Server**: Real-time interactive web UI
- **Entity Framework Core**: Code-first database approach
- **SQLite**: Lightweight, serverless database
- **Bootstrap 5**: Responsive component library
- **Docker**: Containerization support

## 🚀 Getting Started

### Prerequisites
- .NET 8 SDK or Docker

### 🏃 Quick Start

#### Option 1: .NET CLI
```bash
# Clone the repository
git clone <your-repo-url>
cd MemoryPalaceApp

# Restore dependencies
dotnet restore

# Run the application
dotnet run
```

#### Option 2: Docker (Simple)
```bash
# Build and run with Docker (data will be lost on container restart)
docker build -t memory-palace-app .
docker run -p 8080:8080 memory-palace-app
```

#### Option 3: Docker with Persistent Data (Recommended)
```bash
# Use docker-compose for persistent data
docker-compose up -d

# Or manually with volumes
docker build -t memory-palace-app .
docker run -d \
  -p 8080:8080 \
  -v memory_palace_data:/app/data \
  -v memory_palace_uploads:/app/wwwroot/uploads \
  --name memory-palace \
  memory-palace-app
```

#### Option 4: Production with HTTPS (blok-nijmegen.nl) 🔒
```bash
# One command - automatic SSL certificate generation and HTTPS setup
docker-compose up -d

# That's it! Your site will be available at:
# https://blok-nijmegen.nl
# https://www.blok-nijmegen.nl
```

**How it works:**
1. `certbot-init` gets SSL certificates using standalone mode (port 80)
2. `nginx` starts with HTTPS configuration after certificates exist
3. `certbot` renews certificates automatically every 24 hours

**Prerequisites for HTTPS setup:**
- Domain `blok-nijmegen.nl` must point to your server's IP
- Ports 80 and 443 must be open and available
- Server must be publicly accessible from the internet

### 📱 Usage

1. **Create Palace**: Upload a zip file with images on the `/upload` page
2. **Name & Categorize**: Provide a name and subject for your palace
3. **Walkthrough**: Navigate through images and add memory associations
4. **Manage**: View all palaces, edit information, or delete as needed

## 🎨 Design Philosophy

### **World of Warcraft Inspired**
- Floating navigation bar with glass-morphism effects
- Rich dark theme with gold accent colors (#ffd700)
- Immersive full-screen experience
- Smooth animations and hover effects

### **Typography Hierarchy**
- **Poppins**: Modern sans-serif for UI elements and body text
- **Cinzel**: Classical serif for headings and fantasy elements
- **Strategic Usage**: Balances readability with thematic consistency

## 📁 Project Structure

```
MemoryPalaceApp/
├── 📁 Data/                    # Entity Framework context
├── 📁 Models/                  # Data models (MemoryPalace, Loci)
├── 📁 Services/                # Business logic (Palace & Image services)
├── 📁 Pages/                   # Blazor pages
│   ├── Index.razor            # WoW-style home with slideshow
│   ├── Upload.razor           # Palace creation
│   ├── Palaces.razor          # Palace management
│   ├── Walkthrough.razor      # Two-column walkthrough
│   └── EditPalace.razor       # Table editing
├── 📁 Shared/                  # Layout components
│   ├── FloatingNavbar.razor   # WoW-style navigation
│   └── MainLayout.razor       # Main layout
├── 📁 wwwroot/                # Static assets
│   ├── 📁 css/                # Styling
│   ├── 📁 js/                 # JavaScript
│   └── 📁 uploads/            # User uploaded images
└── 📄 Program.cs              # Application entry point
```

## 🗄️ Database Schema

### Memory Palace
- **Name** (required): Palace identifier
- **Subject**: Topic or category
- **CreatedAt**: Timestamp
- **PointsOfLoci**: Related loci collection

### Loci
- **Order**: Sequence position
- **Location**: Physical/virtual location description
- **ImagePath**: Stored image reference
- **Category**: Information category
- **WrittenKey/Value**: Factual information
- **ImageKey/Value**: Memory mnemonics

## 🔄 Walkthrough Modes

### **Image Information Mode**
Focus on visual memory techniques:
- **Location**: Describe the scene
- **Image Key**: Visual memory cue
- **Loci**: Category or type
- **Image Value**: Memory association

### **Actual Information Mode**
Focus on factual data:
- **Category**: Information type
- **Info Key**: Key term/concept  
- **Info Value**: Detailed information

## 🎯 Key Features Breakdown

### **🎮 Gaming-Inspired UI**
- Floating navbar with rounded corners and shadow
- Backdrop blur effects throughout
- Smooth hover animations and transitions
- Progress bars and visual feedback

### **📱 Responsive Design**
- Mobile-first approach
- Flexible layouts that adapt to screen size
- Touch-friendly navigation
- Optimized image loading

### **⚡ Performance Optimized**
- Server-side rendering with Blazor
- Efficient image processing
- Minimal database queries
- Fast navigation between palaces

## 🐳 Docker Support

The application includes full Docker support with:
- Multi-stage build process
- Optimized image size
- Production-ready configuration
- Easy deployment
- **NGINX reverse proxy with automatic SSL certificates**
- **One-command HTTPS setup for production**

### **📀 Data Persistence**

**⚠️ Important:** By default, Docker containers are ephemeral - all data is lost when the container stops.

#### **What Needs Persistence:**
- **SQLite Database** (`/app/data/memorypalace.db`) - All memory palaces and loci
- **Uploaded Images** (`/app/wwwroot/uploads/`) - User-uploaded zip file contents
- **Application Logs** (`/app/logs/`) - Runtime logs (optional)

#### **Recommended Setup:**
```bash
# Use docker-compose (easiest)
docker-compose up -d

# Or manual volumes
docker run -d \
  -p 8080:8080 \
  -v memory_palace_data:/app/data \
  -v memory_palace_uploads:/app/wwwroot/uploads \
  -v memory_palace_logs:/app/logs \
  --name memory-palace \
  memory-palace-app
```

#### **Volume Management:**
```bash
# List volumes
docker volume ls

# Backup data
docker run --rm -v memory_palace_data:/data -v $(pwd):/backup ubuntu tar czf /backup/memory-palace-backup.tar.gz /data

# Restore data
docker run --rm -v memory_palace_data:/data -v $(pwd):/backup ubuntu tar xzf /backup/memory-palace-backup.tar.gz -C /
```

## 🔒 HTTPS Production Setup

### **Automatic SSL with Let's Encrypt**

The application includes a complete NGINX reverse proxy setup with automatic SSL certificate generation using Let's Encrypt and Certbot.

#### **🚀 One-Command Deployment:**
```bash
# Complete HTTPS setup with automatic SSL certificates
docker-compose up -d
```

#### **📋 What This Setup Includes:**
- **NGINX Reverse Proxy**: Routes traffic from blok-nijmegen.nl to the app
- **Automatic SSL Certificates**: Let's Encrypt certificates for HTTPS
- **Auto-Renewal**: Certificates automatically renew every 24 hours
- **HTTP to HTTPS Redirect**: All HTTP traffic automatically redirects to HTTPS
- **Blazor SignalR Support**: Proper WebSocket proxying for real-time features
- **Large File Upload Support**: 100MB max file size for memory palace zip files

#### **🏗️ Architecture:**
```
Internet → NGINX (80/443) → Memory Palace App (8080)
             ↓
        Let's Encrypt SSL (certbot-init → nginx)
```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit changes (`git commit -m 'Add amazing feature'`)
4. Push to branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🙏 Acknowledgments

- Inspired by World of Warcraft's epic interface design
- Memory Palace technique based on ancient Greek method of loci
- Built with modern .NET 8 and Blazor technologies

---

**Created with ❤️ for enhanced learning and memory retention**
