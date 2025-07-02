#!/bin/bash

echo "🏰 Memory Palace - Simple HTTP Start"
echo "===================================="
echo
echo "This will start the Memory Palace on HTTP port 80"
echo "You can add SSL later with ./quick-setup.sh"
echo

# Make sure we're using HTTP-only config
cp nginx/nginx-initial.conf nginx/nginx.conf

# Start services
docker-compose up -d

echo "✅ Services started!"
echo "🌐 Your Memory Palace is now available at:"
echo "   • http://blok-nijmegen.nl"
echo "   • http://your-server-ip"
echo
echo "📊 Check status: docker-compose ps"
echo "📝 View logs: docker-compose logs -f"
echo "🔒 Add SSL: ./quick-setup.sh"