#!/bin/bash

echo "🏰 Memory Palace - Production Deployment with HTTPS"
echo "=================================================="
echo

# Check if domain points to this server
echo "⚠️  Before proceeding, ensure:"
echo "   • Domain blok-nijmegen.nl points to this server's IP"
echo "   • Ports 80 and 443 are open"
echo "   • Server is publicly accessible"
echo

read -p "Continue with SSL setup? (y/N) " -n 1 -r
echo
if [[ ! $REPLY =~ ^[Yy]$ ]]; then
    echo "Cancelled."
    exit 1
fi

echo "🚀 Starting HTTPS setup..."

# Run the SSL initialization
./init-letsencrypt.sh

# Start all services
echo "🔄 Starting all services..."
docker-compose up -d

echo
echo "🎉 Deployment complete!"
echo "🌐 Your Memory Palace is now available at:"
echo "   • https://blok-nijmegen.nl"
echo "   • https://www.blok-nijmegen.nl"
echo
echo "📊 To check status:"
echo "   docker-compose ps"
echo "   docker-compose logs -f"