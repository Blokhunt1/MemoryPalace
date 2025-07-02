#!/bin/bash

echo "🏰 Simple NGINX + SSL Setup for Memory Palace"
echo "============================================="

# Step 1: Start with HTTP-only
echo "Step 1: Starting with HTTP-only configuration..."
cp nginx/nginx-initial.conf nginx/nginx.conf
docker-compose up -d

echo "Waiting for services to start..."
sleep 10

# Step 2: Get SSL certificates
echo "Step 2: Getting SSL certificates..."
docker-compose run --rm certbot certonly --webroot \
  -w /var/www/certbot \
  -d blok-nijmegen.nl \
  -d www.blok-nijmegen.nl \
  --email it@blok-nijmegen.nl \
  --agree-tos \
  --non-interactive

# Step 3: Switch to SSL configuration
echo "Step 3: Switching to SSL configuration..."
cp nginx/nginx-ssl.conf nginx/nginx.conf

# Step 4: Restart nginx with SSL
echo "Step 4: Restarting NGINX with SSL..."
docker-compose restart nginx

echo "✅ Setup complete!"
echo "🌐 Your site should now be available at:"
echo "   • https://blok-nijmegen.nl"
echo "   • https://www.blok-nijmegen.nl"