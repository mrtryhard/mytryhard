##MyTryHard
by [mrtryhard](https://github.com/mrtryhard)
  
My simple personnal website (Not yet deployed). It allows to publish, edit and delete articles, manage categories, 
simple user interface and simple admin panel with stats.
  
It is programmed with C# in ASP.Net 5 MVC 6.

Do not hesitate to file bug reports / pull requests.

**Directories**  
 
* `sql/` contains everything sql-related (duh!). It does contains initialization scripts (create\_scripts) and stored procedures (stored\_procs).   
* `MyTryHard/` contains the ASP.NET5 MVC6 solution. 

**Dependencies**  
While NuGet should do its job, here's what it takes to run this.  

* Visual Studio 2015 is absolutely required  
* Npgsql 3.1.0-alpha6
* EntityFramework7 with Npgsql support
* PostgreSql 9.1 onward  

### Using
**Setting up PostgreSql**  
Before even considering to run it, you must run the scripts in the `sql/create\_scripts` folder, 
in specified order (ascending).  
  
Do not forget to run the stored procedures located in `sql/stored\_procedure` folder. 

*The default schema is `dbo`. You might want to change that. Just don't forget to run the stored
procedures in the right schema.*

**appsettings.json**  
In the `src/appsettings.json` file, you must give a valid Postgresql connection string.  The format
is as follows:  
```
Host={IP_ADDRESS};Port=5432;Database={DATABASE};User Id={USER};Password={PASSWORD};
```

### Licensing
See `LICENSE.md`.


### Other stuff
All icons (LinkedIn, Github, Twitter, USherbrooke), fonts (OpenSans from Google) are trademarks of their respective creators / owners. 
