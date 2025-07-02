#!/bin/bash

# Init script for Let's Encrypt with nginx
# This script will set up SSL certificates for blok-nijmegen.nl

domains=(blok-nijmegen.nl www.blok-nijmegen.nl)
rsa_key_size=4096
data_path="./certbot"
email="it@blok-nijmegen.nl" # Adding a valid email is strongly recommended
staging=0 # Set to 1 if you're testing your setup to avoid hitting request limits

if [ -d "$data_path" ]; then
  read -p "Existing data found for $domains. Continue and replace existing certificate? (y/N) " decision
  if [ "$decision" != "Y" ] && [ "$decision" != "y" ]; then
    exit
  fi
fi

if [ ! -e "$data_path/conf/options-ssl-nginx.conf" ] || [ ! -e "$data_path/conf/ssl-dhparams.pem" ]; then
  echo "### Downloading recommended TLS parameters ..."
  mkdir -p "$data_path/conf"
  curl -s https://raw.githubusercontent.com/certbot/certbot/master/certbot-nginx/certbot_nginx/_internal/tls_configs/options-ssl-nginx.conf > "$data_path/conf/options-ssl-nginx.conf"
  curl -s https://raw.githubusercontent.com/certbot/certbot/master/certbot/certbot/ssl-dhparams.pem > "$data_path/conf/ssl-dhparams.pem"
  echo
fi

echo "### Creating dummy certificate for $domains ..."
path="/etc/letsencrypt/live/$domains"
mkdir -p "$data_path/conf/live/$domains"
docker-compose -f docker-compose-http-only.yml run --rm certbot \
  sh -c "mkdir -p /etc/letsencrypt/live/$domains && \
  openssl req -x509 -nodes -newkey rsa:$rsa_key_size -days 1 \
    -keyout '/etc/letsencrypt/live/$domains/privkey.pem' \
    -out '/etc/letsencrypt/live/$domains/fullchain.pem' \
    -subj '/CN=localhost'"
echo

echo "### Starting services with HTTP-only configuration ..."
docker-compose -f docker-compose-http-only.yml up -d
echo

echo "### Deleting dummy certificate for $domains ..."
docker-compose -f docker-compose-http-only.yml run --rm certbot \
  sh -c "rm -Rf /etc/letsencrypt/live/$domains && \
  rm -Rf /etc/letsencrypt/archive/$domains && \
  rm -Rf /etc/letsencrypt/renewal/$domains.conf"
echo

echo "### Requesting Let's Encrypt certificate for $domains ..."
# Join $domains to -d args
domain_args=""
for domain in "${domains[@]}"; do
  domain_args="$domain_args -d $domain"
done

# Select appropriate email arg
case "$email" in
  "") email_arg="--register-unsafely-without-email" ;;
  *) email_arg="--email $email" ;;
esac

# Enable staging mode if needed
if [ $staging != "0" ]; then staging_arg="--staging"; fi

docker-compose -f docker-compose-http-only.yml run --rm --entrypoint "\
  certbot certonly --webroot -w /var/www/certbot \
    $staging_arg \
    $email_arg \
    $domain_args \
    --rsa-key-size $rsa_key_size \
    --agree-tos \
    --force-renewal" certbot
echo

echo "### Stopping HTTP-only services ..."
docker-compose -f docker-compose-http-only.yml down
echo

echo "### Starting full services with SSL configuration ..."
docker-compose up -d

echo "### Setup complete! ✅"
echo "Your site is now available at:"
echo "  - https://blok-nijmegen.nl"
echo "  - https://www.blok-nijmegen.nl"
echo "  - HTTP requests will automatically redirect to HTTPS"