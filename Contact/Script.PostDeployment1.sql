/*
Modèle de script de post-déploiement							
--------------------------------------------------------------------------------------
 Ce fichier contient des instructions SQL qui seront ajoutées au script de compilation.		
 Utilisez la syntaxe SQLCMD pour inclure un fichier dans le script de post-déploiement.			
 Exemple :      :r .\monfichier.sql								
 Utilisez la syntaxe SQLCMD pour référencer une variable dans le script de post-déploiement.		
 Exemple :      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

if(NOT exists(select * from Contact WHERE Email='arthur@kaamelott.com'))
BEGIN
    INSERT INTO Contact (LastName, FirstName, Email)
    VALUES ('Pendragon', 'Arthur', 'arthur@kaamelott.com')
END

if(NOT exists(select * from Contact WHERE Email='marty@future.com'))
BEGIN
    INSERT INTO Contact (LastName, FirstName, Email)
    VALUES ('McFly', 'Marty', 'marty@future.com')
END
if(NOT exists(select * from Roles WHERE Nom='Admin'))
BEGIN
insert into Roles (Nom) VALUES('Admin');
END
if(NOT exists(select * from Roles WHERE Nom='Membre'))
BEGIN
insert into Roles (Nom) VALUES('Membre');
END





