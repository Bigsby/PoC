docker build -t dock-net .
docker run -d -p 8080:80 --name myapp dock-net
