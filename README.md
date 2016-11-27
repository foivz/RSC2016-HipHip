# RSC2016-HipHip
## Programiranje Alexe 

Korišten AVS-Client koji omogućava snimanje glasovnih naredbi koje enkodira u Base64 te šalje na Node.js server na AVS-Server koji ih potom dekodira i prosljeđuje Alexi, koja otkriva intent i obavlja neku radnju

Izvori: 

https://github.com/miguelmota
https://github.com/alexa/alexa-avs-sample-app/wiki/Windows

Nakon Autentificiranja te konfiguracije sigurnosnih postavki Echo-a, dopušteno pristupanje Alexi od strane IFTTT-a te napravljena integracija (IF Alexa trigger + intent THEN IFTTT Maker HTTP Request (Put, Get, Post, etc.))