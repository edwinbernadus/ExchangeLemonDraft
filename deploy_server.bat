c:
cd\
cd temp
cd publish
del *.* /s /q
cd ..
rmdir publish /s /q
mkdir publish

cd C:\exchangelemondraft
git pull

powershell ./build.ps1 -Target Deploy
cd C:\exchangelemondraft\ExchangeLemon\ExchangeLemonCore
dotnet publish -o c:\temp\publish -c release

iisreset
c:
cd\
cd inetpub
cd wwwroot
del *.* /s /q
curl -u o.0rfE5FBqWbUy8egPL3aihXj1V5LPwysW: -X POST https://api.pushbullet.com/v2/pushes --header "Content-Type: application/json" --data-binary "{\"type\": \"note\", \"title\": \"Build\", \"body\": \"Server Deploy\"}"