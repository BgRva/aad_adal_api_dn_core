AADx APIs (.net core 2.0.6)
==========================
This project is part of the Azure AD authentication model and provides two APIs, each
in a separate project.  Each API is constucted using asp.net core 2.0.6

This repo is part of the Azure Active Directory (AAD) authentication model and provides a solution with two separate API projects build with .net core.  The intention is to use these APIs in conjuncion with a separate UI
project in another repo [BgRva/aad_adal_ui_ng_js](https://github.com/BgRva/aad_adal_ui_ng_js)

This repo has multiple branches, each of which represent different chapters as authentication and authorization are implemented.  Each step builds upon the previous step.  The README file is different for each step and describes the changes with respect to the previous step.  To proceed through all the steps you will need an Azure subscription.  All samples use the default Azure AD features (i.e. free tier).

## Branches (i.e think Chapters)

 - Step_A:  Baseline API projects with no authentication, CORS is enabled using owin middleware

# Solution Structure

This solusion contains 2 Web API projects, each providing simple CRUD behavior.  Both projects use the Owin middleware
to enable CORS, (see the App_Start/Starup.cs class in each API project).  Additionally, certain json serialization settings
are applied in each api (see the App_Start/WebApiConfig.cs class in each API project).

## Common
A simple class project for models and common classes

## TodoApi  
Provides CRUD endpoints for TodoItem objects:

    public class TodoItem
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TeamType Team { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public FactionType Faction { get; set; }
    }
    
The Todo Api also provides a User controller to return user objects which is intended for later chapters where authorization
must be done in conjunction with local profile data.
    
## EventsApi  
Provides CRUD endpoints for EventItem objects:

    public class EventItem
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TeamType Team { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public FactionType Faction { get; set; }
    }
    
## Setup
After cloning or copying, make sure to update the solution to have 2 startup projects:

1) On the menu:  Debub -> Set Starup Projects
2) Choose 'Multiple Starup Projects'
3) Set EventsApi, TodoApi to 'Start'
4) Choose set startup projec
5) Click 'Apply'

Turn off web startup for each project
By default the API projects will spawn a web page, to turn this off do the following:
1) Right click on TodoApi or EventsApi
2) Choose properties
3) Click the 'Web' tab
4) Select "Don't open a page."