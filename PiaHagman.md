# Grupparbete Datalagring
*Food Rescue, november 2021*<br>
*Pia Hagman, Poya Ghahremani, Jon Krantz, Gustav Alvérus*


## Planering
Redan dagen efter vi tilldelades vår grupp, fredag den 19:e hade vi ett inledande möte där vi diskuterade varandras lösningar till inlämningsuppgift 2 i Datalagring,
för att bestämma oss för vilken av dessa vi skulle utgå från. Vi kom fram till att kika närmare på min och Poyas lösning under den kommande helgen. På måndagen landade 
vi i att fortsätta utveckla min lösning i vår gruppuppgift. Vi diskuterade om det var ett dumt val eftersom jag redan gjort stor del av frontend-biten, men
landade i att det kan vara nog så komplicerat att förstå någon annans kod, samt att det fanns mycket kvar att förbättra.
<\br>Vi beslutade också vem av oss som skulle göra vilken ConsolApp. Eftersom jag redan hade gjort en del
av inloggninsgbiten för kunder så fick jag fortsätta där, dvs utveckla **CustomerClient och tester för UserBackend**. Poya fick ansvar för Restaurang-projektet och Jon för Admin. På tisdagen lades Gustav till i gruppen och 
eftersom vi redan hade gjort en hel del planering fick Gustav möjlighet att välja vilken del han ville hoppa in på. Valet landade på att utveckla en login-funktion
för admin. Vi beslöt oss för att inte lägga så mycket tid på struktur i GitHub. Ett antal issues som berättar vem som ansvarar för vilken del fick räcka.

### Möjliga förbättringar till nästa projekt
Det var absolut inga problem med att Gustav kom in senare i planeringen. Han fann sig snabbt och var tillmötesgående. Men eventuellt 
kanske han kände sig förbisedd eftersom vi redan hade tagit ett antal beslut utan honom.

## Implementering
Till skillnad från mitt senaste grupparbete har vi här jobbat en hel del på egen hand. Olika scheman, kompletteringar som legat kvar ock tagit tid för några och 
andra logistiska omständligheter har gjort att vi ibland haft svårt att jobba samtidigt. Vi har dock stämt av dagligen och haft en levande chat i Discord.
Personligen kände jag att jag redan hade gjort stora delar av uppgiftens frontend-del, varför uppgiften inte kändes så betungande. 
Jag var snabbt färdig med både tester och förbättrad ConsoleApp. 

### Lösningar och beslut
Vi beslöt att skapa två databaser, en för testdata och en för livedata. Eftersom jag tidigt var klar med det andra, tog jag på mig att implementera denna förändring. 
Vi valde dock att seeda databasen med testdatan för att göra göra det enklare att jobba med under utvecklingsfasen.
Vi diskuterade huruvida vi skulle implementera Dependency Injection i vårt arbete eller inte, men beslöt att vi var lite för tidigt i lärprocessen för att ge oss på det. 
Därav fanns lite tid för mig att testa att bygga en WinForms. Jag kom såpass långt att jag fick alla funktioner på plats, däremot nästan ingen undantagshantering. Kika
gärna på denna om du vill! Den ligger i en separat branch som heter Winform (login: pia.hagman lösen: HelloWorld1).

### Möjliga förbättringar
Jag skulle säkert kunna utveckla mina tester en hel del. Göra de mer kraftfulla och solida. Det handlar alltid om en prioritering av tid i slutändan. Och än så länge lär vi ju oss. Jag skulle säkert också kunna göra Theory
någonstans. Jag skulle behöva repetera detta, men jag fastnade i min WinForm som jag tyckte var grymt kul att pilla med istället. </br> </br> Jag är också medventen om att jag inte har fångat alla undantag i min 
ConsoleApp. Jag har testat lite olika, exempelvis NullExceptions, ArgumentException och FormatException. Men jag skulle kunna gå igenom programmet grundligare för att se vart framförallt oväntad input får programmet att krascha.

## Sammantagen känsla av projektet
Projektet har flutit på bra och det känns som att alla i gruppen har haft liknande tankar om vad som ska göras.  Vi har inte lagt oss i varandras projekt, med undantag för 
om någon velat ha hjälp, eller då vi behövt göra nödvändiga förändringar för att kunna slå ihop våra projekt till en solution med gemensam backend. Jag comittade mycket vid själva mergen mellan våra olika delar. 
Men viktigt att poängtera är att vi alla var uppkopplade och gjorde det tillsammans med delad skärm. Alla var lika delaktiga i detta med andra ord. </br> </br>Slutligen kommer vi alla superbra överens socialt och jag skulle aldrig backa för att 
jobba med någon av dessa trevliga personer igen. 
  