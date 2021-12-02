# Datalagring-gruppuppgift
*Food Rescue, november 2021*<br>
*Jon Krantz, Pia Hagman, Poya Ghahremani, Gustav Alvérus*

## Planering
Vi skrev inget ER-diagram då vi redan hade den mesta koden klar, även om den skulle flyttas till Frontend. 
Vi valde efter diskussion kring lösningarna använda en av medlemmarnas Inlämningsuppgift 1 där den mesta kodningen redan var i Frontend. Det var svårt att förstå någon annans kod.
Därefter fördelade vi de olika Consolapparna och gjorde en Canban i Github. Jag tod Admin frontend och gjorde ett test till en av metoderna.
På tisdagen lades Gustav till i gruppen och han valde att utveckla en loginfunktion för admin.
Efter en vecka bestämde vi oss för att förbättra hur databasen anslöts, vilket Pia tog på sig.

### Möjliga förbättringar till nästa projekt
Det var absolut inga problem med att Gustav kom in senare i planeringen. Han fann sig snabbt och var tillmötesgående. Men eventuellt 
kanske han kände sig förbisedd eftersom vi redan hade tagit ett antal beslut utan honom.

## Implementering
Var och en arbetade på sin egen del, vi stämde av dagligen samt chattade på Discord.
När kopplingen till databasen förbättrades behövdes en mergning och ändringar i alla branches vilket blev omskrivning av kod. 


### Lösningar och beslut
Vi beslöt att skapa två databaser, en för testdata och en för livedata.

### Möjliga förbättringar
När kopplingen till databasen förbättrades behövdes ändringar i alla branches vilket blev omskrivning av kod. 
Det bästa måste naturligtvis vara att göra all programmering som påverkar olika brancher blir gjord redan innan mergning, 
annars kanske det blir som i vårt fall att man arbetar på en lösning som sedan måste korrigeras då bakomliggande program ändras.
Det finns saker i programvaran som kan förbättras, med en del krascher som går att tvinga fram (om man anstränger sig lite).


## Sammantagen känsla av projektet
Det var en lättarbetat grupp. 
Mycket av kodningen var redan gjord från början, och när man väl satt sig in i den gick det snabbt. 

## Tekniska noteringar
Vi använder testdata i live-databasen för att läraren ska ha något att arbeta med.