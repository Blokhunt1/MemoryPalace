#!/bin/bash

echo "🔍 Check Domain Conflicts on VPS"
echo "================================"

echo "1. Check current hostname..."
hostname
echo "Hostname: $(hostname)"
echo "FQDN: $(hostname -f)"

echo
echo "2. Check /etc/hosts file..."
cat /etc/hosts

echo
echo "3. Check what's currently running on port 80..."
sudo netstat -tlnp | grep :80

echo
echo "4. Check if Apache/Nginx is installed and running..."
systemctl is-active apache2 2>/dev/null || echo "Apache2: not running"
systemctl is-active nginx 2>/dev/null || echo "System Nginx: not running"
systemctl is-active httpd 2>/dev/null || echo "HTTPD: not running"

echo
echo "5. Check what domains resolve to this IP..."
echo "Your external IP:"
curl -s ifconfig.me
echo

echo "6. Test domain resolution..."
echo "blok-nijmegen.nl resolves to:"
dig +short blok-nijmegen.nl

echo "www.blok-nijmegen.nl resolves to:"
dig +short www.blok-nijmegen.nl

echo
echo "7. Check if VPS provider has a default web server..."
curl -I http://$(curl -s ifconfig.me) 2>/dev/null | head -5

echo
echo "8. Check for existing nginx/apache configs..."
ls -la /etc/nginx/sites-enabled/ 2>/dev/null || echo "No system nginx sites"
ls -la /etc/apache2/sites-enabled/ 2>/dev/null || echo "No apache sites"

echo
echo "💡 Common VPS issues:"
echo "   - VPS provider runs Apache/Nginx by default"
echo "   - Default 'Welcome' page on port 80"
echo "   - Multiple domains pointing to same IP"
echo "   - Need to stop/disable default web server"