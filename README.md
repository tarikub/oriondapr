# Fish Taco, Taqueria and Dapr 

The goal of the project is to learn about Dapr by converting an existing durable function project. You can check the summary article on the inspiration for the project and the unusual title for the repo on [medium.com](https://tarikub.medium.com/part-1-fish-taco-taqueria-and-dapr-e2e038bd2f9d). 

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. 

### Prerequisites

* [Docker Desktop](https://www.eclipse.org/)  
* [Dapr cli](hhttps://visualstudio.microsoft.com/vs/)  
* [.NET 6.0](https://ngrok.com)


### Running Locally 

#### Set Up

1. After installing Dapr cli. Run `dapr init` to install dapr on hosting platforms

#### Twilio Account

1. Sign up for a free [Twilio account](www.twilio.com/referral/gJ8V2m)
2. Clone this Git Repo
3. Create a new file `local.settings.json` with the following content and add it under the `Orion\Orion\` folder 
```
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "accountSid": "",
    "authToken": "",
    "appPhone": ""
  }
}
```

#### Running locally
1. Clone this Git Repo
1. From powershell terminal cd into the `OrionDapr` folder and run `docker compose up --build`

#### Future updates
To truly use the solution to support the Roku app the app need to be hosted in kubernetes cluster with cloud provider. Additional load balancing updates might be needed to make sure the Roku channel can reach out to the entry point of the `WebServer` application to call API end points.

## Built With

* [Dapr](https://azure.microsoft.com/en-us/services/functions/)


## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* Tip of the hat to @vriesmarcel & @Geertvdc for awesome presentation on Dapr at VS Live San Diego 2022.
