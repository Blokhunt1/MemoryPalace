#!/bin/bash

echo "🔍 Testing Port 80 Access for Let's Encrypt"
echo "==========================================="

echo "1. Check if nginx is running..."
docker-compose ps nginx

echo
echo "2. Check if port 80 is bound..."
docker-compose exec nginx netstat -tlnp | grep :80 || echo "Port 80 not bound"

echo
echo "3. Test local access to acme-challenge..."
docker-compose exec nginx ls -la /var/www/certbot/.well-known/ || echo "acme-challenge directory missing"

echo
echo "4. Create test file for acme-challenge..."
docker-compose exec nginx mkdir -p /var/www/certbot/.well-known/acme-challenge/
docker-compose exec nginx sh -c 'echo "test123" > /var/www/certbot/.well-known/acme-challenge/test'

echo
echo "5. Test access from inside container..."
docker-compose exec nginx wget -qO- http://localhost/.well-known/acme-challenge/test || echo "❌ Local access failed"

echo
echo "6. Check firewall/iptables status..."
sudo iptables -L INPUT -n | grep 80 || echo "No specific port 80 rules found"

echo
echo "7. Check if any service is blocking port 80..."
sudo netstat -tlnp | grep :80

echo
echo "8. Test external access (this might fail due to firewall)..."
curl -I http://blok-nijmegen.nl/.well-known/acme-challenge/test || echo "❌ External access failed - firewall issue"

echo
echo "💡 If external access fails, you need to:"
echo "   - Open port 80 in your firewall"
echo "   - Check cloud provider security groups"
echo "   - Ensure no other service is using port 80"