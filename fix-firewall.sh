#!/bin/bash

echo "🔥 Fix Firewall for Let's Encrypt"
echo "================================="

echo "This script will open ports 80 and 443 for Let's Encrypt and HTTPS"
echo "Choose your system:"
echo

echo "1. Ubuntu/Debian with UFW:"
echo "   sudo ufw allow 80"
echo "   sudo ufw allow 443"
echo "   sudo ufw reload"
echo

echo "2. CentOS/RHEL with firewalld:"
echo "   sudo firewall-cmd --permanent --add-port=80/tcp"
echo "   sudo firewall-cmd --permanent --add-port=443/tcp"
echo "   sudo firewall-cmd --reload"
echo

echo "3. Generic iptables:"
echo "   sudo iptables -A INPUT -p tcp --dport 80 -j ACCEPT"
echo "   sudo iptables -A INPUT -p tcp --dport 443 -j ACCEPT"
echo "   sudo iptables-save"
echo

echo "4. Check if Apache/other web server is running on port 80:"
echo "   sudo systemctl stop apache2"
echo "   sudo systemctl disable apache2"
echo

echo "5. For cloud providers (AWS/GCP/Azure):"
echo "   - Check Security Groups/Firewall Rules in your cloud console"
echo "   - Allow inbound traffic on ports 80 and 443 from anywhere (0.0.0.0/0)"
echo

read -p "Run UFW commands automatically? (y/N): " choice
if [[ $choice =~ ^[Yy]$ ]]; then
    sudo ufw allow 80
    sudo ufw allow 443
    sudo ufw reload
    echo "✅ UFW rules added"
fi