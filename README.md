Pokemon-Web-API

If u got stuck with dotnet ef migrations add XXX, u would try to add flags: 
'-s' with mainDirectory(Pokemon-Web-API) & '-p' targetDirectory with DbContext(Entities)
But if your migration assembly project is main project, u should simply run dotnet ef migrations add XXX -s MainProject(Pokemon-Web-API)

Swagger:
-
For using Swagger u should to registrate at first, if u want to use PUT, POST, PATCH - use 'Administrator' role in the list of roles.
After registration go to login and get JWT, after that click button 'Authorize', and paste your JWT as here: 'Bearer TOKEN', where 'TOKEN' is your JWT, coppied during login.

SSL Certs problems:
-
At first, good tutorial about openssl - https://www.digitalocean.com/community/tutorials/openssl-essentials-working-with-ssl-certificates-private-keys-and-csrs#generating-csrs
If u got NET::ERR_CERT_AUTHORITY_INVALID - https://superuser.com/questions/1083766/how-do-i-deal-with-neterr-cert-authority-invalid-in-chrome

The file contained one certificate, which was not imported: localhost: Not a Certification Authority:
-
> openssl req -newkey rsa:2048 -nodes -keyout server.key -x509 -days 365 -out server.crt 
If doesn't help, u can try https://stackoverflow.com/questions/7580508/getting-chrome-to-accept-self-signed-localhost-certificate
To trust cert(Ubuntu/Debian) - https://unix.stackexchange.com/questions/90450/adding-a-self-signed-certificate-to-the-trusted-list

FINAL:
-
DONT install CERTS INTO CHROME BY GUI, use only certutil!!!!!!! 
certutil -d sql:$HOME/.pki/nssdb -L - this comand lists all available certificates

To delete cert: certutil -d sql:$HOME/.pki/nssdb -D -n <certificate nickname>

certutil -d sql:$HOME/.pki/nssdb -A -t "C,," -n "your nickname" -i /path/to/your/certiicate.crt 
Command above used for adding cert with nickname to chrome database

https://serverfault.com/questions/845766/generating-a-self-signed-cert-with-openssl-that-works-in-chrome-58 - command for creating .key and .cert
Command above differ from previous by adding [SAN] section, in order to avoid NET::ERR_CERT_COMMON_NAME_INVALID.





