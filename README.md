##MyTryHard
by [mrtryhard](https://github.com/mrtryhard)
  
My simple personnal [website](https://www.mrtryhard.info/).  
It allows to publish, edit and delete articles, manage categories with a
simple user interface and simple admin panel with stats.
  
It is programmed with C# in Asp.Net Core.  
It works on both Ubuntu LTS 14.04 and Windows 8.  
  
All issues encountered will probably either feature as a blog entry or as a wiki entry on this repo
if it is considered useful.

Do not hesitate to file bug reports / pull requests.

**Directories**  
 
* `sql/` contains everything sql-related (duh!). It does contains initialization scripts (create\_scripts) and stored procedures (stored\_procs).   
* `MyTryHard/` contains the Asp.Net Core solution. 

**Dependencies**  
While NuGet should do its job, here's what it takes to run this.  

* Visual Studio 2015 is absolutely required  
* Npgsql 3.1.5
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
Host={IP_ADDRESS};Port=5432;Database={DATABASE};User Id={USER};Password={PASSWORD};Persist Security Info=true
```

### Licensing
See `LICENSE.md`.


### Other stuff
All icons (LinkedIn, Github, Twitter, USherbrooke), fonts (OpenSans from Google) are trademarks of their respective creators / owners. 
