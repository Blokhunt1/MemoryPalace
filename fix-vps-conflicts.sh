#!/bin/bash

echo "🔧 Fix VPS Domain Conflicts"
echo "==========================="

echo "This script will stop conflicting web servers and free up port 80"
echo

read -p "Stop Apache if running? (y/N): " choice
if [[ $choice =~ ^[Yy]$ ]]; then
    sudo systemctl stop apache2 2>/dev/null || echo "Apache2 not found"
    sudo systemctl disable apache2 2>/dev/null || echo "Apache2 not found"
    sudo systemctl stop httpd 2>/dev/null || echo "HTTPD not found"
    sudo systemctl disable httpd 2>/dev/null || echo "HTTPD not found"
    echo "✅ Apache services stopped"
fi

echo
read -p "Stop system Nginx if running? (y/N): " choice
if [[ $choice =~ ^[Yy]$ ]]; then
    sudo systemctl stop nginx 2>/dev/null || echo "System Nginx not found"
    sudo systemctl disable nginx 2>/dev/null || echo "System Nginx not found"
    echo "✅ System Nginx stopped"
fi

echo
echo "Checking what's still on port 80..."
sudo netstat -tlnp | grep :80

echo
echo "Starting our Docker nginx..."
docker-compose up -d nginx

echo
echo "Testing if port 80 is now accessible..."
sleep 5
curl -I http://localhost || echo "❌ Still can't access port 80"

echo
echo "✅ VPS conflicts should be resolved"
echo "Now try: ./init-letsencrypt.sh"