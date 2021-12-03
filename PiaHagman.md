# Grupparbete Datalagring
*Food Rescue, november 2021*<br>
*Pia Hagman, Poya Ghahremani, Jon Krantz, Gustav Alv�rus*


## Planering
Redan dagen efter vi tilldelades v�r grupp, fredag den 19:e hade vi ett inledande m�te d�r vi diskuterade varandras l�sningar till inl�mningsuppgift 2 i Datalagring,
f�r att best�mma oss f�r vilken av dessa vi skulle utg� fr�n. Vi kom fram till att kika n�rmare p� min och Poyas l�sning under den kommande helgen. P� m�ndagen landade 
vi i att forts�tta utveckla min l�sning i v�r gruppuppgift. Vi diskuterade om det var ett dumt val eftersom jag redan gjort stor del av frontend-biten, men
landade i att det kan vara nog s� komplicerat att f�rst� n�gon annans kod, samt att det fanns mycket kvar att f�rb�ttra.
<\br>Vi beslutade ocks� vem av oss som skulle g�ra vilken ConsolApp. Eftersom jag redan hade gjort en del
av inloggninsgbiten f�r kunder s� fick jag forts�tta d�r, dvs utveckla **CustomerClient och tester f�r UserBackend**. Poya fick ansvar f�r Restaurang-projektet och Jon f�r Admin. P� tisdagen lades Gustav till i gruppen och 
eftersom vi redan hade gjort en hel del planering fick Gustav m�jlighet att v�lja vilken del han ville hoppa in p�. Valet landade p� att utveckla en login-funktion
f�r admin. Vi besl�t oss f�r att inte l�gga s� mycket tid p� struktur i GitHub. Ett antal issues som ber�ttar vem som ansvarar f�r vilken del fick r�cka.

### M�jliga f�rb�ttringar till n�sta projekt
Det var absolut inga problem med att Gustav kom in senare i planeringen. Han fann sig snabbt och var tillm�tesg�ende. Men eventuellt 
kanske han k�nde sig f�rbisedd eftersom vi redan hade tagit ett antal beslut utan honom.

## Implementering
Till skillnad fr�n mitt senaste grupparbete har vi h�r jobbat en hel del p� egen hand. Olika scheman, kompletteringar som legat kvar ock tagit tid f�r n�gra och 
andra logistiska omst�ndligheter har gjort att vi ibland haft sv�rt att jobba samtidigt. Vi har dock st�mt av dagligen och haft en levande chat i Discord.
Personligen k�nde jag att jag redan hade gjort stora delar av uppgiftens frontend-del, varf�r uppgiften inte k�ndes s� betungande. 
Jag var snabbt f�rdig med b�de tester och f�rb�ttrad ConsoleApp. 

### L�sningar och beslut
Vi besl�t att skapa tv� databaser, en f�r testdata och en f�r livedata. Eftersom jag tidigt var klar med det andra, tog jag p� mig att implementera denna f�r�ndring. 
Vi valde dock att seeda databasen med testdatan f�r att g�ra g�ra det enklare att jobba med under utvecklingsfasen.
Vi diskuterade huruvida vi skulle implementera Dependency Injection i v�rt arbete eller inte, men besl�t att vi var lite f�r tidigt i l�rprocessen f�r att ge oss p� det. 
D�rav fanns lite tid f�r mig att testa att bygga en WinForms. Jag kom s�pass l�ngt att jag fick alla funktioner p� plats, d�remot n�stan ingen undantagshantering. Kika
g�rna p� denna om du vill! Den ligger i en separat branch som heter Winform (login: pia.hagman l�sen: HelloWorld1).

### M�jliga f�rb�ttringar
Jag skulle s�kert kunna utveckla mina tester en hel del. G�ra de mer kraftfulla och solida. Det handlar alltid om en prioritering av tid i slut�ndan. Och �n s� l�nge l�r vi ju oss. Jag skulle s�kert ocks� kunna g�ra Theory
n�gonstans. Jag skulle beh�va repetera detta, men jag fastnade i min WinForm som jag tyckte var grymt kul att pilla med ist�llet. </br> </br> Jag �r ocks� medventen om att jag inte har f�ngat alla undantag i min 
ConsoleApp. Jag har testat lite olika, exempelvis NullExceptions, ArgumentException och FormatException. Men jag skulle kunna g� igenom programmet grundligare f�r att se vart framf�rallt ov�ntad input f�r programmet att krascha.

## Sammantagen k�nsla av projektet
Projektet har flutit p� bra och det k�nns som att alla i gruppen har haft liknande tankar om vad som ska g�ras.  Vi har inte lagt oss i varandras projekt, med undantag f�r 
om n�gon velat ha hj�lp, eller d� vi beh�vt g�ra n�dv�ndiga f�r�ndringar f�r att kunna sl� ihop v�ra projekt till en solution med gemensam backend. Jag comittade mycket vid sj�lva mergen mellan v�ra olika delar. 
Men viktigt att po�ngtera �r att vi alla var uppkopplade och gjorde det tillsammans med delad sk�rm. Alla var lika delaktiga i detta med andra ord. </br> </br>Slutligen kommer vi alla superbra �verens socialt och jag skulle aldrig backa f�r att 
jobba med n�gon av dessa trevliga personer igen. 
  