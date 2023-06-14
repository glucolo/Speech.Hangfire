# Speech.Hangfire
Little demo for hangfire functionality and scalability

## What do you find in the solution
I've created a Layered Architecture solution with three main projects:
- Speech.Hangfire.WebAPI

    This is the starting point for the "Unique project demo" and/or "Web client component"

- Speech.Hangfire.ServerConsole
      
    This is the "Server component"

- Speech.Hangfire.Business
     
    This is the "Business component" that contains only the static methods, instance methods used as jobs
    <!-- -->
    
## How execute the examples
The start project is a **ASP.NET web.api** with swagger documentation to test and call Hangfire funzionality.

After starting the project, the browser will open on the swagger page, open another browser tab with same url and substitute "*swagger*" word with "*hangfire*" word

<details>
  
  ###   
  <summary>Unique process</summary>

  In **Speech.Hangfire.WebAPI** projects, open **program.cs** file:
  - row 39 have to be uncommented
  - rows 43,46,47 have to be commented
  <!-- -->
  Run WebApi project
  ***
</details>
<details>

  ###   
  <summary>Unique process with more workers</summary>

  In **Speech.Hangfire.WebAPI** projects, open **program.cs** file:
  - row 39 have to be commented
  - row 43 have to be uncommented
  - rows 46,47 have to be commented
  <!-- -->
  Run WebApi project
  ***
</details>
<details>

  ###   
  <summary>Unique process with always run job</summary>

  In **Speech.Hangfire.WebAPI** projects, open **program.cs** file:
  - row 39,43 have to be commented
  - rows 46,47 have to be uncommented
  <!-- -->
  Run WebApi project
  ***
</details>
<details>

  ###   
  <summary>Separated process</summary>

  In **Speech.Hangfire.WebAPI** projects, open **program.cs** file:
  - rows 39,43,46,47 have to be commented
  <!-- -->
  In **Speech.Hangfire.ServerConsole** projects, open **program.cs** file:
  - row 33 or 34 have to be commented
  <!-- -->
  Run both WebApi project and ServerConsole project
  ***
</details>
