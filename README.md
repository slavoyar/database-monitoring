# Project startup

To start project you should run the following command
```
docker-compose -f src/docker-compose.yml up -d
```

After contaiers startup go to `http://localhost/dashboard` and login to the application with credentials:
```
login: admin
password: Qwe123!@#
```

After you logged in you will see observable servers. All of them should be show with down status.

To start getting info from them you should run 
```
docker-compose -f src/docker-compose-logs.yml up -d
```
