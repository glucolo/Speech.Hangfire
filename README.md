
# Speech.Hangfire
[![GitHub Workflow Status (with branch)](https://img.shields.io/github/actions/workflow/status/glucolo/speech.hangfire/dotnet.yml?branch=master&label=build%20master&logo=github)](https://github.com/glucolo/speech.hangfire/actions/workflows/dotnet.yml)
[![GitHub Workflow Status (with branch)](https://img.shields.io/github/actions/workflow/status/glucolo/speech.hangfire/dotnet.yml?branch=DEV&label=build%20DEV&logo=github)](https://github.com/glucolo/speech.hangfire/actions/workflows/dotnet.yml)
[![GitHub Workflow Status](https://img.shields.io/github/actions/workflow/status/glucolo/speech.hangfire/codeql.yml?label=CodeQL&logo=github)](https://github.com/glucolo/speech.hangfire/actions/workflows/codeql.yml)
[![GitHub Workflow Status](https://img.shields.io/github/actions/workflow/status/glucolo/speech.hangfire/super-linter.yml?label=Lint%20Code%20Base&logo=github)](https://github.com/glucolo/speech.hangfire/actions/workflows/super-linter.yml)

![GitHub](https://img.shields.io/github/license/glucolo/speech.hangfire)

## Overview
Little demo for hangfire functionality and scalability. I propose two scenario: unique solution with ASP.NET. Multi-Layered application with ASP.NET client, handfire server and business logic. 

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
The start project is **ASP.NET Web.API** with swagger documentation to test and call Hangfire functionality.

After starting the project, the browser will open on the swagger page, open another browser tab with same URL and substitute "*swagger*" word with "*hangfire*" word

<details>
<br />
<summary>Unique process</summary>

In **Speech.Hangfire.WebAPI** projects, open **program.cs** file:
- row 39 have to be uncommented
- rows 43,46,47 have to be commented
<!-- -->
Run WebApi project
***
</details>

<details>
<br />
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
<br />
<summary>Unique process with always run job</summary>

In **Speech.Hangfire.WebAPI** projects, open **program.cs** file:
- row 39,43 have to be commented
- rows 46,47 have to be uncommented
<!-- -->
Run WebApi project
***
</details>

<details>
<br />
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
